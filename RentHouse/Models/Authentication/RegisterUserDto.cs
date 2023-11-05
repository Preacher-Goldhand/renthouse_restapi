using System.ComponentModel.DataAnnotations;

public class RegisterUserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    [MaxLength(30)]
    public string Email { get; set; }
    [Required]
    public string UserPasswordHashed { get; set; }
    [Required]
    public string ConfirmUserPasswordHash { get; set; }
}