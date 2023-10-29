using Microsoft.AspNetCore.Identity;

public class UserModel : IdentityUser
{
    public string PostalCode {get; set;}
    public string Street {get; set;}
    public string StreetNumber {get; set;}
    public string City {get; set;} 
    public IList<OrderModel> Orders {get; set;}
}