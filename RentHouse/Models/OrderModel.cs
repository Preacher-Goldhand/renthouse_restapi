public class OrderModel
{
    public int Id {get; set;}

    public int UserId {get; set;}
    public virtual UserModel User {get; set;}
    public int MachineId {get; set;}
    public virtual MachineModel Machine {get; set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
    public decimal TotalPrice {get; set;}
    public int TotalTime {get; set;}
}