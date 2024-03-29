using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentHouse.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

public interface IOrderService 
{
    OrderModel CreateOrder(OrderModel orderModel);
    IList<OrderModel> GetOrdersFromUser(ClaimsPrincipal user);
    IList<OrderModel> GetOrders();
    public void RemoveOrder(int orderId);
    public OrderModel GetOrderModel(int orderId);
}

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _dbContext;

    public OrderService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public OrderModel CreateOrder(OrderModel orderModel)
    {
        var newOrder = new OrderModel
        {
            UserId = orderModel.UserId,
            MachineId = orderModel.MachineId,
            StartDate = orderModel.StartDate,
            EndDate = orderModel.EndDate,
            TotalPrice = orderModel.TotalPrice,
            TotalTime = orderModel.TotalTime
        };

        _dbContext.Orders.Add(newOrder);
        _dbContext.SaveChanges();

        return newOrder;
    }

    public void RemoveOrder(int orderId)
    {
        var orderRemoved = _dbContext.Orders.FirstOrDefault(m => m.Id == orderId);

        if (orderRemoved != null)
        {
            _dbContext.Orders.Remove(orderRemoved);
            _dbContext.SaveChanges();
        }
    }

    public IList<OrderModel> GetOrdersFromUser(ClaimsPrincipal user)
    {
        if (!int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier).Value, out int userId))
            return new List<OrderModel>();

        return _dbContext.Orders
            .Include(o => o.User)
            .Include(o => o.Machine)
            .Where(o => o.User.Id == userId)
            .ToList();
    }

    public IList<OrderModel> GetOrders()
    {
        return _dbContext.Orders
            .Include(o => o.User)
            .Include(o => o.Machine)
            .Where(o => o.Id > 0)
            .ToList();
    }

    public OrderModel GetOrderModel(int id)
    {
        var order = _dbContext.Orders
            .Include(o => o.User)
            .Include(o => o.Machine)
            .FirstOrDefault(m => m.Id == id);

        return order;
    }
}
