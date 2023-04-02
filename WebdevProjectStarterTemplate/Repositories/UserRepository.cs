﻿using System.Data;
using System.Collections;
using Dapper;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories
{
	public class UserRepository
	{
		private IDbConnection GetConnection()
		{
			return new DbUtils().GetDbConnection();
		}

        public User Add(string username, string email, string password)
		{
			using var connectin = GetConnection();
			string sql =	@"INSERT INTO ober (Name, Email, Password) VALUES (@Name, @Email, @Password);
							SELECT * FROM ober WHERE OberId = LAST_INSERT_ID();";

			var parameters = new { Name = username, Email = email, Password = password };
			var user = connectin.QuerySingle<User>(sql, parameters);
			return user;
		}

		public User Get(string email)
		{
			using var connection = GetConnection();

			// create good sql
			string sql = @"SELECT * FROM ober WHERE Email = @email";

			var parameters = new { email };

			var user = connection.QuerySingleOrDefault<User>(sql, parameters);
			return user;
		}
    }
}
