using Microsoft.Data.SqlClient;

var connectionString = "Server=localhost/DEMOS;Database=DapperMastery;Trusted_Connection=True;";

await using var connection = new SqlConnection(connectionString);
