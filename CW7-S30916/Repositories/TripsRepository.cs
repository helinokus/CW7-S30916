using CW7_S30916.Dtos;
using CW7_S30916.Models;
using Microsoft.Data.SqlClient;

namespace CW7_S30916.Repositories;

public interface ITripsRepository
{
    Task<List<GetTripsInfoDto>> GetTripsAsync();
    Task<bool> IsExistingTripAsync(int idTrip);
    Task<int> GetMaxPeopleAsync(int idTrip);

}

public class TripsRepository(IConfiguration config) : ITripsRepository
{
    public async Task<List<GetTripsInfoDto>> GetTripsAsync()
    {
        var trips = new List<GetTripsInfoDto>();
        
        var conString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(conString);
        var sql = @"
        SELECT 
            t.IdTrip, t.Name, t.Description, 
            t.DateFrom, t.DateTo, t.MaxPeople,
            c.IdCountry, c.Name as CountryName
        FROM Trip t
        LEFT JOIN Country_Trip ct ON t.IdTrip = ct.IdTrip
        LEFT JOIN Country c ON ct.IdCountry = c.IdCountry";
        
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync();
        await using var readerAsync =  await command.ExecuteReaderAsync();

        while (await readerAsync.ReadAsync())
        {
            trips.Add(new GetTripsInfoDto()
            {
                IdTrip = readerAsync.GetInt32(0),
                Name = readerAsync.GetString(1),
                Description = readerAsync.GetString(2),
                DateFrom = readerAsync.GetDateTime(3),
                DateTo = readerAsync.GetDateTime(4),
                MaxPeople = readerAsync.GetInt32(5),
                IdCountry = readerAsync.GetInt32(6),
                CountryName = readerAsync.GetString(7)
                
            });
        }
        
        return trips;

    }

    public async Task<bool> IsExistingTripAsync(int idTrip)
    {
        var connectionString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(connectionString);
        var sql = @"select 1 from Trip where IdTrip = @idTrip";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@idTrip", idTrip);
        await connection.OpenAsync();
        return await command.ExecuteScalarAsync() != null;
    }

    public async Task<int> GetMaxPeopleAsync(int idTrip)
    {
        var connectionString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(connectionString);
        var sql = @"select MaxPeople from Trip where IdTrip = @idTrip";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@idTrip", idTrip);
        await connection.OpenAsync();
        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }
}