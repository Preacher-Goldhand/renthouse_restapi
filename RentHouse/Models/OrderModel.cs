

using System.ComponentModel.DataAnnotations;

public class OrderModel{
    [Required]
    public int Id {get; set;}
    public UserModel User {get; set;}
    public MachineModel Machine {get; set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
    public double TotalPrice {get; set;}
    public int TotalTime {get; set;}
}