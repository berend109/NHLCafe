using System.Security.Cryptography;
using System.Text;

namespace WebdevProjectStarterTemplate.Helpers
{
	public class Hasj
	{
		public static string HasjPassword(string password)
		{
			using var sha256 = SHA256.Create();
			var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
			return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
		}
	}
}
