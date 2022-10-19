using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using RandomUserSharp;
using RandomUserSharp.Models;

var connectionString =
    "Server=localhost\\DEMOS;Database=DapperMastery;Trusted_Connection=true;TrustServerCertificate=True";

await using var connection = new SqlConnection(connectionString);

var insertSql = @"INSERT INTO [dbo].[ApplicationUsers]
                           ([FirstName]
                           ,[LastName]
                           ,[PasswordHash]
                           ,[DateOfBirth]
                           ,[CreatedDate]
                           ,[LastUpdated])
                     VALUES
                           (@firstName, @lastName, @passwordHash, @dateOfBirth, @createdDate, @lastUpdated)";

var users = new List<ApplicationUser>();

for(int x = 0; x < 50; x++)
{
    var u = await GenerateRandomUser();
    users.Add(u);
}

var result = await connection.ExecuteAsync(insertSql, users);
Console.WriteLine($"Added {result} rows");















/// Generate a Random User
async Task<ApplicationUser> GenerateRandomUser()
{
    var randomUserClient = new RandomUserClient();
    var randomUser = await randomUserClient.GetRandomUsersAsync(natioanlitites: new List<Nationality>()
    {
        Nationality.US
    });

    var user = randomUser.First();
    return new ApplicationUser()
    {
        FirstName = user.Name.First,
        LastName = user.Name.Last,
        CreatedDate = DateTime.UtcNow,
        DateOfBirth = user.DateOfBirth.Date,
        LastUpdated = DateTime.UtcNow,
        PasswordHash = Base64UrlEncoder.Encode($"{user.Name.First}{user.Name.Last}") // never hash this way, lol
    };
}