
using System.ComponentModel.DataAnnotations;

public class MachineModel{
    [Required]
    public int Id {get; set;}
    public string Name{get; set;}
    public float PricePerDay {get; set;}
    public int MaxRentTime {get; set;}
    public IList<Dictionary<string, string>> TechnicalDetails {get; set;}
    public bool IsAvailable {get; set;}
    public int OperatingHours {get; set;}
}