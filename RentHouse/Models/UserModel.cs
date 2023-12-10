using Microsoft.AspNetCore.Identity;

public class UserModel
{
    public int Id {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string Email {get; set;}
    public string UserPasswordHashed {get; set;}
    public string PostalCode {get; set;}
    public string Street {get; set;}
    public string StreetNumber {get; set;}
    public string City {get; set;} 
    public IList<OrderModel> Orders {get; set;}
}