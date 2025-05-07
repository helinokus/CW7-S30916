using CW7_S30916.Exceptions;
using CW7_S30916.Models;
using CW7_S30916.Repositories;
using CW7_S30916.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW7_S30916.Controllers;


[ApiController]
[Route("/api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripsService _tripsService;

    public TripsController(ITripsService tripsService)
    {
        _tripsService = tripsService;
    }
    /*Wyszukuj wszystkie dostępne wycieczki z wymagającą informacją
     var sql = @"SELECT 
        t.IdTrip, t.Name, t.Description, 
        t.DateFrom, t.DateTo, t.MaxPeople,
        c.IdCountry, c.Name as CountryName
        FROM Trip t
        LEFT JOIN Country_Trip ct ON t.IdTrip = ct.IdTrip
        LEFT JOIN Country c ON ct.IdCountry = c.IdCountry"*/
    
    [HttpGet]
    public async Task<IActionResult> GetAllTrips()
    {
        try
        {
            var trips = await _tripsService.GetTripsAsync();
            return Ok(trips);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}