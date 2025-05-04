using CW7_S30916.Models;
using Microsoft.Data.SqlClient;

namespace CW7_S30916.Repositories;

public interface ITripsRepository
{
    Task<List<Trip>> GetTripsAsync();
}

public class TripsRepository(IConfiguration config) : ITripsRepository
{
    public async Task<List<Trip>> GetTripsAsync()
    {
        var trips = new List<Trip>();
        
        var conString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(conString);
        var sql = "select * from Trip";
        
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync();
        await using var readerAsync =  await command.ExecuteReaderAsync();

        while (await readerAsync.ReadAsync())
        {
            trips.Add(new Trip()
            {
                IdTrip = readerAsync.GetInt32(0),
                Name = readerAsync.GetString(1),
                Description = readerAsync.GetString(2),
                DateFrom = readerAsync.GetDateTime(3),
                DateTo = readerAsync.GetDateTime(4),
                MaxPeople = readerAsync.GetInt32(5)
                
            });
        }
        
        return trips;

    }
}