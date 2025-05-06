using System.ComponentModel.DataAnnotations;

namespace CW7_S30916.Models;

public class Trip
{
    public int IdTrip { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public DateTime DateFrom { get; set; }
    [Required]
    public DateTime DateTo { get; set; }
    [Required]
    public int MaxPeople { get; set; }
}
