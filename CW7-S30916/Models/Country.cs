using System.ComponentModel.DataAnnotations;

namespace CW7_S30916.Models;

public class Country
{
    public int IdCountry { get; set; }
    [Required]
    public string Name { get; set; }
}
