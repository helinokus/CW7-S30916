using CW7_S30916.Dtos;
using CW7_S30916.Exceptions;
using CW7_S30916.Models;
using CW7_S30916.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW7_S30916.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IClientTripService _clientTripService;

    public ClientsController(IClientService clientService, IClientTripService clientTripService)
    {
        _clientService = clientService;
        _clientTripService = clientTripService;
    }

    /*Wyszukaj wszystkie wycieczki powiązane z konkretnym klientem 
    var sql = @"SELECT 
                t.IdTrip, t.Name, t.Description, 
                t.DateFrom, t.DateTo, t.MaxPeople,
                ct.RegisteredAt, ct.PaymentDate
            FROM Client_Trip ct
            JOIN Trip t ON ct.IdTrip = t.IdTrip
            WHERE ct.IdClient = @IdClient"*/
    
    [HttpGet("{idClient}/trips")]
    public async Task<IActionResult> GetClientTrips(int idClient)
    {

        try
        {
            var results = await _clientService.GetClientTripsAsync(idClient);
            return Ok(results);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
    }
    /*Utwórz nowego klienta 
        var sql = "insert into Client (FirstName, LastName, Email, Telephone, Pesel)
        values (@FirstName, @LastName, @Email, @Telephone, @Pesel); 
        select scope_identity();";*/

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientDto clientDto)
    {
        try
        {
            var clientId = await _clientService.CreateClientAsync(clientDto);
            return Ok(clientId);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }    
    }

    /*Zarejestruj klienta na konkretną wycieczkę
        var sql = @"insert into Client_Trip (IdClient, IdTrip, RegisteredAt)
         values (@ClientId, @TripId, @RegisteredAt)";*/
    
    [HttpPut("{id}/trips/{tripId}")]
    public async Task<IActionResult> AddClientToTrip(int id, int tripId)
    {
        try
        {
            await _clientTripService.CreateClientTripAsync(id, tripId);
            return Ok("Added client to trip");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
    }

    /*Usuń rejestrację klienta z wycieczki 
        var sql = @"delete 
        from Client_Trip 
        where IdClient = @ClientId and IdTrip = @TripId;";*/

    
    [HttpDelete("{id}/trips/{tripId}")]
    public async Task<IActionResult> RemoveClientFromTrip(int id, int tripId)
    {
        try
        {
            await _clientTripService.DeleteClientTripAsync(id, tripId);
            return Ok("Removed client from trip");
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
    }
    
}