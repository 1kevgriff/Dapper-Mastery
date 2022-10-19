using Microsoft.Data.SqlClient;
using Dapper;

var connectionString = "Server=localhost\\DEMOS;Database=DapperMastery;Trusted_Connection=true;TrustServerCertificate=True";

await using var connection = new SqlConnection(connectionString);

var sql = @"SELECT [Id]
      ,[FirstName]
      ,[LastName]
      ,[PasswordHash]
      ,[DateOfBirth]
      ,[CreatedDate]
      ,[LastUpdated]
  FROM [dbo].[ApplicationUsers]
  WHERE [DateOfBirth] > @dateA AND [DateOfBirth] < @dateB";

var options = new QueryOptions()
{
    DateA = new DateTime(2002, 01, 01), DateB = new DateTime(2012, 01, 01)
};

var users = await connection.QueryAsync<ApplicationUser>(sql, options);

// spit out result
foreach (var user in users)
    Console.WriteLine($"{user.FirstName} was born on {user.DateOfBirth:d}");


public class QueryOptions
{
    public DateTime DateA {get;set;}
    public DateTime DateB {get;set;}
}