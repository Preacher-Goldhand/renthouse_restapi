using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RentHouse.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public interface IUserService{
    public void CreateUser(UserModel user);
    public string GenerateJwt(UserModel dto);
}

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPasswordHasher<UserModel> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;
    
    public UserService(ApplicationDbContext dbContext, IPasswordHasher<UserModel> passwordHasher, AuthenticationSettings authenticationSettings)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
    }

    public void CreateUser(UserModel user){
        var newUser = new UserModel{
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PostalCode = user.PostalCode,
            Street = user.Street,
            StreetNumber = user.StreetNumber,
            City = user.City
        };

        var hashedPassword = _passwordHasher.HashPassword(newUser, user.UserPasswordHashed);
            newUser.UserPasswordHashed = hashedPassword;

        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
    }

    public string GenerateJwt(UserModel dto){
        var user = _dbContext.Users
            .FirstOrDefault(u => u.Email == dto.Email);
        if (user == null)
        {
            throw new ArgumentException("Invalid username or password");
        }
        var passwordHash = _passwordHasher.VerifyHashedPassword(user, user.UserPasswordHashed, dto.UserPasswordHashed);
        if (passwordHash == PasswordVerificationResult.Failed)
        {
            throw new ArgumentException("Invalid username or password");
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