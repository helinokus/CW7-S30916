using CW7_S30916.Exceptions;
using CW7_S30916.Repositories;

namespace CW7_S30916.Services;

public interface IClientTripService
{
    Task CreateClientTripAsync(int clientId, int tripId);
    Task DeleteClientTripAsync(int clientId, int tripId);
}

public class ClientTripService : IClientTripService
{
    private readonly IClientTripRepository _clientTripRepository;
    private readonly IClientsRepository _clientsRepository;
    private readonly ITripsRepository _tripsRepository;

    public ClientTripService(IClientTripRepository clientTripRepository, IClientsRepository clientsRepository, ITripsRepository tripsRepository)
    {
        _clientTripRepository = clientTripRepository;
        _clientsRepository = clientsRepository;
        _tripsRepository = tripsRepository;
    }


    public async Task CreateClientTripAsync(int clientId, int tripId)
    {

        if (!await _clientsRepository.ClientExistsAsync(clientId))
        {
            throw new NotFoundException($"Client with id {clientId} does not exist");
        }

        if (!await _tripsRepository.IsExistingTripAsync(tripId))
        {
            throw new NotFoundException($"Trip with id {tripId} does not exist");
        }

        if (!(await _clientTripRepository.GetPeopleOnTripAsync(tripId) < await _tripsRepository.GetMaxPeopleAsync(tripId)))
        {
            throw new ConflictException("Trips already has max amount of people");
        }

        if (await _clientTripRepository.IsRegisteredAsync(clientId, tripId))
        {
            throw new ConflictException("Client trip already registered");
        }
        
        await _clientTripRepository.CreateClientTripAsync(clientId, tripId);
    }

    public async Task DeleteClientTripAsync(int clientId, int tripId)
    {
        if (!await _clientsRepository.ClientExistsAsync(clientId))
        {
            throw new NotFoundException($"Client with id {clientId} does not exist");
        }

        if (!await _tripsRepository.IsExistingTripAsync(tripId))
        {
            throw new NotFoundException($"Trip with id {tripId} does not exist");
        }

        if (!await _clientTripRepository.IsRegisteredAsync(clientId, tripId))
        {
            throw new ConflictException("This client doesn't register on this trip");
        }
        
        await _clientTripRepository.DeleteClientTripAsync(clientId, tripId);
    }
}