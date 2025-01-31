using System.Security.Cryptography;
using System.Text;

namespace PruebaAspirantes.Tools
{
    public class Encrypt
    {
        public static string GetSHA256(string str)
        {
            /*
            using (SHA256 sha256 = SHA256.Create())  // ✅ Reemplazo correcto
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower(); // ✅ Más eficiente
            }
            */
            
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
            
        }
    }
}
