using ApiGruposummaOperaciones.Data;
using ApiGruposummaOperaciones.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiGruposummaOperaciones
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //  Obtain key of cyfrado 
            string cypherKey = builder.Configuration["CypherKey"];

            //ready encrypted the string conection
            string encryptedConnectionString = builder.Configuration.GetSection("ConnectionStrings:CadenaSQL").Value;
            string decryptedConnectionString = EncryptionService.Decrypt(encryptedConnectionString, cypherKey);

            // Agrega los servicios aquí, antes de app = builder.Build()

            decryptedConnectionString = decryptedConnectionString.Replace(@"\\", @"\"); //****** function to return two slash instead of four
            // Configurar Entity Framework con SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(decryptedConnectionString));


            //Configuration JwtService
            builder.Services.AddScoped<JwtServices>();

            //Configuration of Autentication JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters

                      {
                          ValidateIssuer = true,
                          ValidateAudience = true,
                          ValidateLifetime = true,
                          ValidateIssuerSigningKey = true,
                          ValidIssuer = builder.Configuration["Jwt:Issuer"],
                          ValidAudience = builder.Configuration["Jwt:Audience"],
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                      };
                  });

            //ADD configurations of cors but use in Apis
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", policy =>
                {
                    policy.WithOrigins("http://localhost:3000") // Permitir solo este origen change on the url by the conection of azure cloud 
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .SetIsOriginAllowed(_ => true)
                          .AllowCredentials();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            // Habilitar CORS para pruebas locales
            app.UseCors("AllowOrigin");

            app.UseAuthorization();

            app.UseDeveloperExceptionPage();

            app.MapControllers();

            app.Run();
        }
    }
}