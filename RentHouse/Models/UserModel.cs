using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

public class UserModel
{
    [JsonIgnore]
    public int Id {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string Email {get; set;}
    public string UserPasswordHashed {get; set;}
    public string PostalCode {get; set;}
    public string Street {get; set;}
    public string StreetNumber {get; set;}
    public string City {get; set;} 
    [JsonIgnore]
    public virtual IList<OrderModel> Orders {get; set;}
}