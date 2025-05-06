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

    [HttpGet]
    public async Task<IActionResult> GetAllTrips()
    {
        try
        {
            var trips = await _tripsService.GetTripsAsync();
            return Ok(trips);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}