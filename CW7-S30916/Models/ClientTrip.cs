using System.ComponentModel.DataAnnotations;

namespace CW7_S30916.Models;

public class ClientTrip
{
    [Required]
    public int IdClient { get; set; }
    [Required]
    public int IdTrip { get; set; }
    [Required]
    public DateTime RegisteredAt { get; set; }
    public DateTime? PaymentDate { get; set; }
    
    public Trip Trip { get; set; }
}