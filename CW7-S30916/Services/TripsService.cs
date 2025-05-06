using CW7_S30916.Dtos;
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
        return await _tripsRepository.GetTripsAsync();
    }
    
}