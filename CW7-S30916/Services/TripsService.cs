using CW7_S30916.Dtos;
using CW7_S30916.Exceptions;
using CW7_S30916.Models;
using CW7_S30916.Repositories;

namespace CW7_S30916.Services;

public interface ITripsService
{
    Task<List<GetTripsInfoDto>> GetTripsAsync();
}

public class TripsService : ITripsService
{
    private readonly ITripsRepository _tripsRepository;

    public TripsService(ITripsRepository tripsRepository)
    {
        _tripsRepository = tripsRepository;
    }
    
    public async Task<List<GetTripsInfoDto>> GetTripsAsync()
    {
        var res = await _tripsRepository.GetTripsAsync();
        if (res.Count == 0)
        {
            throw new NotFoundException("Trips not found");
        }
        
        return res;
    }
    
}