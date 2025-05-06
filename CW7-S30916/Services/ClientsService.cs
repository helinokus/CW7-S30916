using System.Text.RegularExpressions;
using CW7_S30916.Dtos;
using CW7_S30916.Exceptions;
using CW7_S30916.Models;
using CW7_S30916.Repositories;
using CreateClientDto = CW7_S30916.Dtos.CreateClientDto;

namespace CW7_S30916.Services;

public interface IClientService
{
    Task<List<GetClientTripDto>> GetClientTripsAsync(int idClient);
    Task<int> CreateClientAsync(CreateClientDto client);
}

public class ClientService : IClientService
{
    private readonly IClientsRepository _clientRepository;

    public ClientService(IClientsRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<List<GetClientTripDto>> GetClientTripsAsync(int idClient)
    {
        Console.WriteLine(await _clientRepository.ClientExistsAsync(idClient));

        if (idClient <= 0)
        {
            throw new ConflictException("Client Id is invalid");
        }

        if (!await _clientRepository.ClientExistsAsync(idClient))
        {
            throw new NotFoundException("Client does not exist");
        }

        return await _clientRepository.GetClientTripsAsync(idClient);
    }

    
    public async Task<int> CreateClientAsync(CreateClientDto client)
    {
        Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        );
        
        if (client.FirstName.Any(char.IsDigit) || client.LastName.Any(char.IsDigit))
        {
            throw new ConflictException("First name or Last name cannot contain digits");
        }

        if (!client.Telephone.All(char.IsDigit) || client.Telephone.Length != 12 || !client.Telephone.StartsWith("+"))
        {
            throw new ConflictException("Wrong telephone number");
        }

        if (!EmailRegex.IsMatch(client.Email))
        {
            throw new ConflictException("Invalid email address");
        }

        if (client.Pesel.Length != 11)
        {
            throw new ConflictException("Pesel must be 11 characters long");
        }

        if (await _clientRepository.EmailExistsAsync(client.Email))
        {
            throw new NotFoundException("Email already exists");
        }

        if (await _clientRepository.PeselExistsAsync(client.Pesel))
        {
            throw new NotFoundException("Pesel already exists");
        }
        
        return await _clientRepository.CreateClientAsync(client);
        
    }
}