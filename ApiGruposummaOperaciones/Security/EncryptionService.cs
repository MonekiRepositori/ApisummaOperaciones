using System.Security.Cryptography;
using System.Text;

namespace ApiGruposummaOperaciones.Security
{
    public class EncryptionService
    {
        public static string Encrypt(string source, string cypherKey)
        {
            using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
                {
                    byte[] byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(cypherKey));
                    tripleDESCryptoService.Key = byteHash;
                    tripleDESCryptoService.Mode = CipherMode.ECB;
                    byte[] data = Encoding.UTF8.GetBytes(source);
                    return Convert.ToBase64String(tripleDESCryptoService.CreateEncryptor().TransformFinalBlock(data, 0, data.Length));
                }
            }
        }

        public static string Decrypt(string encrypted, string cypherKey)
        {
            using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
                {
                    byte[] keyHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(cypherKey));
                    tripleDESCryptoService.Key = keyHash;
                    tripleDESCryptoService.Mode = CipherMode.ECB;
                    tripleDESCryptoService.Padding = PaddingMode.PKCS7;

                    byte[] encryptedData = Convert.FromBase64String(encrypted);
                    byte[] decryptedData = tripleDESCryptoService.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);

                    // Convertir a texto y normalizar barras
                    string result = Encoding.UTF8.GetString(decryptedData);

                    // Forzar a que las barras sean exactamente dobles (una sola limpieza)
                    result = result.Replace(@"\\", @"\"); // Elimina duplicados innecesarios
                    return result.Replace(@"\", @"\\");  // Asegura exactamente barras dobles
                }

               
            }
        }
    }
}

