namespace CW7_S30916.Dtos;

public class GetTripsInfoDto
{
    public int IdTrip { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public int IdCountry { get; set; }
    public string CountryName { get; set; }
}