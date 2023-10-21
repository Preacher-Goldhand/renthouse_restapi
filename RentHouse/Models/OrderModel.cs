public class OrderModel
{
    public int Id {get; set;}
    public UserModel User {get; set;}
    public MachineModel Machine {get; set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
    public decimal TotalPrice {get; set;}
    public int TotalTime {get; set;}
}