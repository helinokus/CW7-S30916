using CW7_S30916.Models;
using Microsoft.Data.SqlClient;

namespace CW7_S30916.Repositories;

public interface IClientTripRepository
{
    Task CreateClientTripAsync(int clientId, int tripId);
    Task<bool> IsRegisteredAsync(int clientId, int tripId);
    Task<int> GetPeopleOnTripAsync(int clientId, int tripId);
    Task DeleteClientTripAsync(int clientId, int tripId);
}

public class ClientTripRepository(IConfiguration config) : IClientTripRepository
{
    public async Task CreateClientTripAsync(int clientId, int tripId)
    {
        var connectionString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(connectionString);
        var sql = @"insert into ClientTrip (ClientId, TripId, Registered_At) values (@ClientId, @TripId, GETDATE())";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@ClientId", clientId);
        command.Parameters.AddWithValue("@TripId", tripId);
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task<bool> IsRegisteredAsync(int clientId, int tripId)
    {
        var connectionString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(connectionString);
        var sql = @"select 1 from ClientTrip where ClientId = @ClientId and TripId = @TripId;";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@ClientId", clientId);
        command.Parameters.AddWithValue("@TripId", tripId);
        await connection.OpenAsync();
        return await command.ExecuteScalarAsync() !=null;  
    }

    public async Task<int> GetPeopleOnTripAsync(int clientId, int tripId)
    {
        var connectionString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(connectionString);
        var sql = @"select count(*) from ClientTrip where ClientId = @ClientId and TripId = @TripId;";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@ClientId", clientId);
        command.Parameters.AddWithValue("@TripId", tripId);
        await connection.OpenAsync();
        return Convert.ToInt32(await command.ExecuteScalarAsync());
    }

    public async Task DeleteClientTripAsync(int clientId, int tripId)
    {
        var connectionString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(connectionString);
        var sql = @"delete from ClientTrip where ClientId = @ClientId and TripId = @TripId;";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@ClientId", clientId);
        command.Parameters.AddWithValue("@TripId", tripId);
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }
}