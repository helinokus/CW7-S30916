using CW7_S30916.Models;
using CW7_S30916.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CW7_S30916.Controllers;


[ApiController]
[Route("[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripsRepository _tripsRepository;

    public TripsController(ITripsRepository tripsRepository)
    {
        _tripsRepository = tripsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTrips()
    {
        try
        {
            var trips = await _tripsRepository.GetTripsAsync();
            return Ok(trips);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}