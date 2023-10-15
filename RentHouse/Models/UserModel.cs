
using System.ComponentModel.DataAnnotations;

public class UserModel{
    [Required]
    public int Id {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string PostalCode {get; set;}
    public string AddrLine1 {get; set;}
    public string AddrLine2 {get; set;}
    public string City {get; set;}
    
    public IList<OrderModel> Orders {get; set;}
}