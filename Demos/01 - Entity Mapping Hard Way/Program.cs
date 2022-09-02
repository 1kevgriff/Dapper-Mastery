using Microsoft.Data.SqlClient;

var connectionString = "Server=localhost\\DEMOS;Database=DapperMastery;Trusted_Connection=true;TrustServerCertificate=True";

await using var connection = new SqlConnection(connectionString);
await connection.OpenAsync();

var sql = @"SELECT [Id]
      ,[FirstName]
      ,[LastName]
      ,[PasswordHash]
      ,[DateOfBirth]
      ,[CreatedDate]
      ,[LastUpdated]
  FROM [dbo].[ApplicationUsers]";

var command = connection.CreateCommand();
command.CommandText = sql;

var reader = await command.ExecuteReaderAsync();


var users = new List<ApplicationUser>();

while (await reader.ReadAsync())
{
    var applicationUser = new ApplicationUser();
    applicationUser.Id = reader.GetInt64(0);
    applicationUser.FirstName = reader.GetString(1);
    applicationUser.LastName = reader.GetString(2);
    applicationUser.PasswordHash = reader.GetString(3);
    applicationUser.DateOfBirth = reader.GetDateTime(4);
    applicationUser.CreatedDate = reader.GetDateTime(5);
    applicationUser.LastUpdated = reader.GetDateTime(6);

    users.Add(applicationUser);
}

// spit out result
foreach (var user in users)
    Console.WriteLine($"{user.FirstName} was born on {user.DateOfBirth:d}");