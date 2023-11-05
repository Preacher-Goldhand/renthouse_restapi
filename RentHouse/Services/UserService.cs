using Microsoft.AspNetCore.Identity;
using RentHouse.Data;
using Renthouse Models;
using System.Collections.Generic;
using System.Linq;

public interface IUserService{
    public UserModel CreateUser(UserModel user);
    public string GenerateJwt(UserModel dto);
}

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly AuthenticationSettings _authenticationsettings;
    
    public UserService(ApplicationDbContext dbContext, IPasswordHasher passwordHasher, AuthenticationSettings authenticationSettings)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _authenticationsettings = authenticationSettings;
    }

    public UserModel CreateUser(UserModel user){
        var newUser = new UserModel{
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PostalCode = user.PostalCode,
            Street = user.Street,
            StreetNumber = user.StreetNumber,
            City = user,City,
        }

        var hashedPassword = _passwordHasher.HashedPassword;
        newUserPasswordHashed = user.UserPasswordHashedewUser.UserPasswordHashed = hashedPassword;

        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
    }

    public string GenerateJwt(UserModel dto){
        var user = _dbContext.Users
            .FirstOrDefault(u => u.Email == dto.Email);
        if (user == null)
        {
            throw new BadRequestException("Invalid username or password");
        }
        var passwordHash = _passwordHasher.VerifyHashedPassword(user, user.UserPasswordHashed, dto.UserPasswordHashed);
        if (passwordHash == PasswordVerificationResult.Failed)
        {
            throw new BadRequestException("Invalid username or password");
        }
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName } {user.LastName}"),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                    _authenticationSettings.JwtIssuer, claims,
                    expires: expires, signingCredentials: creds);
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.WriteToken(token);
        return jwtToken;
    }   

}