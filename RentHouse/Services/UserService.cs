using Microsoft.AspNetCore.Identity;
using RentHouse.Data;
using Renthouse Models;
using System.Collections.Generic;
using System.Linq;

public interface IUserService
{
   
}

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;
    // private readonly IHttpContextAccessor _httpContextAccessor;
    // private readonly UserManager<UserModel> _userManager;

    public OrderService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        // _httpContextAccessor = httpContextAccessor;
        // _userManager = userManager;
    }

    public UserModel CreateUser(){
        var newUser = new UserModel{
            PostalCode
        }

        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();

        return newOrder;
    }
   
}