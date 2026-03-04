using ApiGruposummaOperaciones.Models;
using ApiGruposummaOperaciones.ModelsDto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;


namespace ApiGruposummaOperaciones.Data
{

    #region
    /// <summary>
    /// class is a part of the Entity Framework Core architecture. It serves as the primary context for interacting with the database and defines the structure of the database model. Here’s a brief overview of its components:
    //DbSet Properties: The class defines DbSet properties for each entity(User, Role, RegisterUser, and OperationSumma). These properties allow for querying and saving instances of these entities in the database.
    //OnModelCreating Method: This method is overridden to configure the entity model. Inside this method:
    //Each entity's primary key is configured using the HasKey method, ensuring that each table in the database has a unique identifier (primary key).
    //Overall, this code sets up the foundation for database interactions in the application by establishing the entities and their relationships within the Entity Framework Core context.
    /// </summary>
    /// 
    #endregion
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Class registrations for use in the controllers.
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<RegisterUser> UserRegistration { get; set; }
        public DbSet<OperationSumma> Operations { get; set; }
        public DbSet<Platform> Platforms { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<StatusOperation> StatusOperations { get; set; }

        public DbSet<Foreigncurrencys> Currencys { get; set; }

        public DbSet<Tickets> Tickets { get; set; }

        public DbSet<StatusTicket> StatusTickets { get; set; }

        public DbSet<Comment> Comments { get; set; }

        #region

        //public DbSet<MenuOptions> MenuOptions { get; set; }

        //public DbSet<AccessMenuRoles> AccessMenuRoles { get; set; }
        #endregion
        public DbSet<CallOptionsDeals> CallOptionsDeals { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configuration for OperationSumma
            modelBuilder.Entity<OperationSumma>(entity =>
            {
                entity.ToTable("Operaciones");

                entity.HasKey(e => e.Id_Operaciones);
                entity.Property(e => e.Id_Operaciones)
                    .HasColumnName("Id_Operaciones")
                    .ValueGeneratedOnAdd(); // Automatically generated

                entity.Property(e => e.Deal)
                    .HasColumnName("Deal")
                    .IsRequired();
                // Relation with Foreigncurrencys
                entity.HasOne(o => o.Currencys)
                    .WithMany()
                    .HasForeignKey(o => o.Id_Divisas)
                    .OnDelete(DeleteBehavior.Cascade);


                // Relation with StatusOperation
                entity.HasOne(o => o.Statusoperations)
                   .WithMany()
                   .HasForeignKey(o => o.Id_EstatusOperacion)
                   .OnDelete(DeleteBehavior.Cascade);


                // Map other properties as needed...
            });

            // Primary key configuration for Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Rol"); // Name of the table of database
                entity.HasKey(r => r.Id_Rol); // Define the primary key
                entity.Property(r => r.TipoDeRol)
                      .HasColumnName("TipoDeRol") // Map of the column name in the database
                      .IsRequired()
                      .HasMaxLength(50); // Define restrictions on the column
            }); // Ensuring Id_Rol is the primary key

            // Primary key configuration for User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id_usuario);

            // Primary key configuration for RegisterUser
            modelBuilder.Entity<RegisterUser>()
                .HasKey(ru => ru.UserRecordId);

            // Primary key configuration for Operation
            modelBuilder.Entity<OperationSumma>()
                .HasKey(o => o.Id_Operaciones);


            modelBuilder.Entity<RefreshToken>()
               .HasKey(rt => rt.Id_RefreshTocken);

            modelBuilder.Entity<Platform>()
            .HasKey(p => p.Id_BankingPlatform);

            // Relationship between RegisterUser and Role
            modelBuilder.Entity<RegisterUser>()
                .HasOne(r => r.Role) // RegisterUser has one Role
                .WithMany()          // A Role can be assigned to many RegisterUsers
                .HasForeignKey(r => r.TipodeUsuario); // Foreign key is TipodeUsuario in RegisterUser


            modelBuilder.Entity<RefreshToken>()
                   .HasOne(rt => rt.UserRecordId)
                   .WithMany(u => u.RefreshTokens)
                   .HasForeignKey(rt => rt.RecordId)
                   .OnDelete(DeleteBehavior.Cascade);

            //Configuration of he table StatusOperation
            modelBuilder.Entity<StatusOperation>(entity =>
            {
                entity.ToTable("EstatusOperacion"); // Name correct in the database 

                entity.HasKey(e => e.Id_EstatusOperacion); // Defing key primary

                entity.Property(e => e.Id_EstatusOperacion)
                      .HasColumnName("Id_EstatusOperacion");

                entity.Property(e => e.Description)
                      .HasColumnName("Descripcion")
                      .IsRequired()
                      .HasMaxLength(30);
            });

            // Configuration of the table Foreigncurrencys
            modelBuilder.Entity<Foreigncurrencys>(entity =>
            {
                entity.ToTable("Divisas"); // ✅ Name correct of the table
                entity.HasKey(e => e.Id_Divisas); // ✅ Name correct of the PK

                entity.Property(e => e.Id_Divisas)
                      .HasColumnName("Id_Divisas"); // ✅ Name correct in the BD

                entity.Property(e => e.Description)
                      .HasColumnName("Descripcion")
                      .IsRequired()
                      .HasMaxLength(3);
            });

            //configuracion de  la tabla.
            modelBuilder.Entity<Tickets>(entity =>
            {
                entity.ToTable("Tickets");

                // Configuración de la clave primaria
                entity.HasKey(t => t.Id_Tickets);

                // Configuración de propiedades
                entity.Property(t => t.Id_Tickets)
                    .HasColumnName("Id_ticket")
                    .ValueGeneratedOnAdd(); // Auto Increment

                entity.Property(t => t.Descripcion)
                    .HasColumnName("Descripcion")
                    .HasMaxLength(500); // Asumiendo un tamaño de columna de 500 caracteres

                entity.Property(t => t.CreatedDate)
                    .HasColumnName("FechaCreacion")
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(t => t.ClosedDate)
                    .HasColumnName("FechaCierre");

                entity.Property(t => t.TicketStatusId)
                    .HasColumnName("Id_StatusTicket")
                    .IsRequired(false); // Asumiendo que puede ser nulo

                // Configuración de relaciones
                entity.HasOne(t => t.registerUser)
                    .WithMany()
                    .HasForeignKey(t => t.UserRecordId)
                    .OnDelete(DeleteBehavior.Cascade); // Dependiendo del comportamiento deseado

                // Relación con los comentarios
                entity.HasMany(t => t.Comments)
                    .WithOne(c => c.Ticket)
                    .HasForeignKey(c => c.TicketId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.TicketStatus)
                    .WithMany(s => s.Tickets)
                    .HasForeignKey(t => t.TicketStatusId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(t => t.Statusoperations)
                   .WithMany()
                   .HasForeignKey(o => o.Id_EstatusOperacion)
                   .OnDelete(DeleteBehavior.Cascade);

                // Ajusta el comportamiento de eliminación si es necesario
            });

            // Configuración de la tabla Comment
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comentarios");

                // Configuración de la clave primaria
                entity.HasKey(c => c.CommentId);

                // Configuración de propiedades
                entity.Property(c => c.Comentario)
                    .HasColumnName("Comentario")
                    .IsRequired()
                    .HasMaxLength(400); // Ajusta según lo necesario

                entity.Property(c => c.CommentDate)
                    .HasColumnName("FechaComentario")
                    .HasDefaultValueSql("GETDATE()");

                // Relación con Ticket
                entity.HasOne(c => c.Ticket)
                    .WithMany(t => t.Comments)
                    .HasForeignKey(c => c.TicketId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relación con RegisterUser
                entity.HasOne(c => c.RegisterUser)
                    .WithMany()
                    .HasForeignKey(c => c.UserRecordId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Otras configuraciones de entidades, como StatusOperation, OperationSumma, etc.
            modelBuilder.Entity<StatusOperation>(entity =>
            {
                entity.ToTable("EstatusOperacion");
                entity.HasKey(e => e.Id_EstatusOperacion);
                entity.Property(e => e.Description)
                    .HasColumnName("Descripcion")
                    .IsRequired()
                    .HasMaxLength(30);
            });

            // Relación uno a uno entre Operaciones y Tickets
            modelBuilder.Entity<OperationSumma>()
                .HasOne(o => o.Ticket)
                .WithOne()
                .HasForeignKey<OperationSumma>(o => o.TicketId)
                .OnDelete(DeleteBehavior.SetNull); // o .Cascade si prefieres eliminar en cascada


            // Configuración de la tabla StatusTicket
            modelBuilder.Entity<StatusTicket>(entity =>
            {
                entity.ToTable("StatusTicket");
                entity.HasKey(s => s.Id_StatusTicket);
                entity.Property(s => s.Id_StatusTicket)
                .HasColumnName("Id_StatusTicket");
                entity.Property(s => s.Descripcion)
                .HasColumnName("Descripcion")
                .IsRequired()
                .HasMaxLength(50);
            });
           
        }
    }
}


