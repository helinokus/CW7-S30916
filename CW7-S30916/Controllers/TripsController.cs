using CW7_S30916.Models;
using CW7_S30916.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CW7_S30916.Controllers;

[ApiController]
[Route("[controller]")]
public class TripsController : ControllerBase
{
    private readonly TripsRepository _tripsRepository;

    [HttpGet]
    
    public async Task<IActionResult> GetAllTrips()
    {
        var tripsAsync = _tripsRepository.GetTripsAsync();
        
        return Ok(tripsAsync);
    }
    
}