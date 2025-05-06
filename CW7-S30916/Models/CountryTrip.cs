using System.ComponentModel.DataAnnotations;

namespace CW7_S30916.Models;

public class CountryTrip
{
    [Required]
    public int IdCountry { get; set; }
    [Required]
    public int IdTrip { get; set; }
    
}