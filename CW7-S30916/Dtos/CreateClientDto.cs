using System.ComponentModel.DataAnnotations;

namespace CW7_S30916.Dtos;

public class CreateClientDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Pesel { get; set; }
}