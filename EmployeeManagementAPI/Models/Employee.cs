using System.Text.Json.Serialization;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int DepartmentId { get; set; }
    public int CountryId { get; set; }
    public int StateId { get; set; }
    public int CityId { get; set; }

    // Make navigation properties nullable
    [JsonIgnore]
    public Department? Department { get; set; }

    [JsonIgnore]
    public Country? Country { get; set; }

    [JsonIgnore]
    public State? State { get; set; }

    [JsonIgnore]
    public City? City { get; set; }
}
