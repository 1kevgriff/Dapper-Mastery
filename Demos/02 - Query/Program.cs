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
  FROM [dbo].[ApplicationUsers]";

var users = await connection.QueryAsync<ApplicationUser>(sql);

// spit out result
foreach (var user in users)
    Console.WriteLine($"{user.FirstName} was born on {user.DateOfBirth:d}");