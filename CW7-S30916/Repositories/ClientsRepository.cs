using CW7_S30916.Models;
using Microsoft.Data.SqlClient;

namespace CW7_S30916.Repositories;

public interface IClientsRepository
{
    Task<List<Client>> GetClientsAsync();
    Task<bool> ClientExistsAsync(int idClient);
    Task<List<ClientTrip>> GetClientTripsAsync(int idClient);
    Task<int> CreateClientAsync(CreateClientDto client);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> PeselExistsAsync(string pesel);
}

public class ClientsRepository(IConfiguration config) : IClientsRepository 
{
    public async Task<List<Client>> GetClientsAsync()
    {
        var clients = new List<Client>();
        
        var conString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(conString);
        var sql = "select * from Client";
        
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync();
        await using var readerAsync =  await command.ExecuteReaderAsync();

        while (readerAsync.Read())
        {
            clients.Add(new Client()
            {
                IdClient = Convert.ToInt32(readerAsync["IdClient"]),
                FirstName = Convert.ToString(readerAsync["FirstName"]),
                LastName = Convert.ToString(readerAsync["LastName"]),
                Email = Convert.ToString(readerAsync["Email"]),
                Telephone = Convert.ToString(readerAsync["Telephone"]),
            });
        }
        return clients;
    }

    public async Task<bool> ClientExistsAsync(int idClient)
    {
        
        var conString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(conString);
        var sql = @"select 1 from Client where IdClient = @IdClient";
        
        
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@IdClient", idClient);
        
        await connection.OpenAsync();
        return await command.ExecuteReaderAsync() != null;
    }

    public async Task<List<ClientTrip>> GetClientTripsAsync(int idClient)
    {
        var clientTrips = new List<ClientTrip>();
        
        var conString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(conString);
        var sql = @"
            SELECT 
                t.IdTrip, t.Name, t.Description, 
                t.DateFrom, t.DateTo, t.MaxPeople,
                ct.RegisteredAt, ct.PaymentDate
            FROM Client_Trip ct
            JOIN Trip t ON ct.IdTrip = t.IdTrip
            WHERE ct.IdClient = @IdClient";
        
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@IdClient", idClient);
        
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            clientTrips.Add(new ClientTrip()
            {
                Trip = new Trip()
                {
                    IdTrip = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    DateFrom = reader.GetDateTime(3),
                    DateTo = reader.GetDateTime(4),
                    MaxPeople = reader.GetInt32(5)
                },
                RegisteredAt = reader.GetDateTime(6),
                PaymentDate = reader.IsDBNull(7) ? null : reader.GetDateTime(7)
            });
        }
        
        return clientTrips;
    }

    public async Task<int> CreateClientAsync(CreateClientDto client)
    {
        var conString = config.GetConnectionString("Default");
        await using var connection = new SqlConnection(conString);
        var sql = "insert into Client (FirstName, LastName, Email, Telephone, Pesel) values (@FirstName, @LastName, @Email, @Telephone, @Pesel); select scope_identity();";
        
        
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@FirstName", client.FirstName);
        command.Parameters.AddWithValue("@LastName", client.LastName);
        command.Parameters.AddWithValue("@Email", client.Email);
        command.Parameters.AddWithValue("@Telephone", client.Telephone);
        command.Parameters.AddWithValue("@Pesel", client.Pesel);
        
        await connection.OpenAsync();
        
        return Convert.ToInt32(await command.ExecuteScalarAsync());
    
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        using var connection = new SqlConnection(config.GetConnectionString("Default"));
        await connection.OpenAsync();

        const string sql = "SELECT 1 FROM Client WHERE Email = @Email";
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Email", email);

        return await command.ExecuteScalarAsync() != null;
    }

    public async Task<bool> PeselExistsAsync(string pesel)
    {
        using var connection = new SqlConnection(config.GetConnectionString("Default"));
        await connection.OpenAsync();

        const string sql = "SELECT 1 FROM Client WHERE Pesel = @Pesel";
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Pesel", pesel);

        return await command.ExecuteScalarAsync() != null;
    }
}