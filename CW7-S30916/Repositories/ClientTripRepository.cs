using CW7_S30916.Models;
using Microsoft.Data.SqlClient;

namespace CW7_S30916.Repositories;

public interface IClientTripRepository
{
    Task CreateClientTripAsync(int clientId, int tripId);
    Task<bool> IsRegisteredAsync(int clientId, int tripId);
    Task<int> GetPeopleOnTripAsync(int tripId);
    Task DeleteClientTripAsync(int clientId, int tripId);
}

public class ClientTripRepository(IConfiguration config) : IClientTripRepository
{
    public async Task CreateClientTripAsync(int clientId, int tripId)
    {
        var connectionString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(connectionString);
        var sql = @"insert into Client_Trip (IdClient, IdTrip, RegisteredAt) values (@ClientId, @TripId, @RegisteredAt)";
        int dateAsInt = int.Parse(DateTime.Now.ToString("yyyyMMdd"));

        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@ClientId", clientId);
        command.Parameters.AddWithValue("@TripId", tripId);
        command.Parameters.AddWithValue("@RegisteredAt", dateAsInt);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task<bool> IsRegisteredAsync(int clientId, int tripId)
    {
        var connectionString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(connectionString);
        var sql = @"select 1 from Client_Trip where IdClient = @ClientId and IdTrip = @TripId;";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@ClientId", clientId);
        command.Parameters.AddWithValue("@TripId", tripId);
        await connection.OpenAsync();
        return await command.ExecuteScalarAsync() !=null;  
    }

    public async Task<int> GetPeopleOnTripAsync(int tripId)
    {
        var connectionString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(connectionString);
        var sql = @"select count(*) from Client_Trip where IdTrip = @TripId;";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@TripId", tripId);
        await connection.OpenAsync();
        return Convert.ToInt32(await command.ExecuteScalarAsync());
    }

    public async Task DeleteClientTripAsync(int clientId, int tripId)
    {
        var connectionString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(connectionString);
        var sql = @"delete from Client_Trip where IdClient = @ClientId and IdTrip = @TripId;";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@ClientId", clientId);
        command.Parameters.AddWithValue("@TripId", tripId);
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }
}