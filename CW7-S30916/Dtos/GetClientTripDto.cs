using System.ComponentModel.DataAnnotations;

namespace CW7_S30916.Dtos;

public class GetClientTripDto
{
    [Required]
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
    
    public int RegisteredAt { get; set; }
    public int? PaymentDate { get; set; }
}