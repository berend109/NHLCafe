﻿using System.Security.Cryptography;
using System.Text;

namespace WebdevProjectStarterTemplate.Helpers
{
	public class Hash
	{
		public static string HashPassword(string? password)
		{
			using var sha256 = SHA256.Create();
			if (password == null) return "";
			var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
			return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
		}
	}
}
