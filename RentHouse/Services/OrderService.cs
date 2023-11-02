using Microsoft.AspNetCore.Identity;
using RentHouse.Data;
using System.Collections.Generic;
using System.Linq;

public interface IOrderService 
{
    OrderModel CreateOrder(OrderModel orderModel);
    //IList<OrderModel> GetOrdersFromUser();
}

public class OrderService : IOrderService
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

    public OrderModel CreateOrder(OrderModel orderModel)
    {
        var newOrder = new OrderModel
        {
            User = orderModel.User,
            Machine = orderModel.Machine,
            StartDate = orderModel.StartDate,
            EndDate = orderModel.EndDate,
            TotalPrice = orderModel.TotalPrice,
            TotalTime = orderModel.TotalTime
        };

        // var userId = orderModel.User.Id; // Pobierz ID użytkownika
        // newOrder.User = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

        _dbContext.Orders.Add(newOrder);
        _dbContext.SaveChanges();

        return newOrder;
    }

//    public IList<OrderModel> GetOrdersFromUser()
//     {
//         // Pobierz aktualnie zalogowanego użytkownika
//         var user = _httpContextAccessor.HttpContext.User;

//         // Sprawdź, czy użytkownik jest zalogowany
//         if (user.Identity.IsAuthenticated)
//         {
//             // Pobierz ID zalogowanego użytkownika jako ciąg znaków
//             var userIdAsString = _userManager.GetUserId(user);

//             if (int.TryParse(userIdAsString, out int userId))
//             {
//                 // Zwróć zamówienia tylko dla danego użytkownika
//                 return _dbContext.Orders.Where(o => o.User.Id == userId).ToList();
//             }
//         }

//         // Jeśli użytkownik nie jest zalogowany lub konwersja nie powiodła się, zwróć pustą listę
//         return new List<OrderModel>();
//     }
}