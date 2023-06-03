using Dapper;
using System.Data;
namespace WebdevProjectStarterTemplate.Repositories;

public static class MainRepository<T>
{
	private static IDbConnection GetConnection()
	{
		return new DbUtils().GetDbConnection();
	}

	/// <summary>
	/// Get product by id from database
	/// </summary>
	/// <param name="id"></param>
	/// <returns>The product based on id</returns>
	public static T GetByProductId(int id)
	{
		var sql = $"SELECT * FROM {typeof(T).Name} WHERE {typeof(T).Name}Id = @id";
		using var connection = GetConnection();
		var product = connection.QuerySingleOrDefault<T>(sql, new { id });
		return product;
	}

	public static IEnumerable<T> Get()
	{
		string sql = $"SELECT * FROM {typeof(T).Name}";
		using IDbConnection connection = GetConnection();
		IEnumerable<T> item = connection.Query<T>(sql);
		return item;
	}
}