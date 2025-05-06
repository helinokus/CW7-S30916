using System.ComponentModel.DataAnnotations;

namespace CW7_S30916.Models;

public class Client
{
    public int IdClient { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public string Telephone { get; set; }
    [Length(11,11)]
    public string Pesel { get; set; }
}
