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

    [HttpPut("{id}/trips/{tripId}")]
    public async Task<IActionResult> AddClientToTrip(int idClient, int tripId)
    {
        try
        {
            await _clientTripService.CreateClientTripAsync(idClient, tripId);
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

    [HttpDelete("{id}/trips/{tripId}")]
    public async Task<IActionResult> RemoveClientFromTrip(int idClient, int tripId)
    {
        try
        {
            await _clientTripService.DeleteClientTripAsync(idClient, tripId);
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