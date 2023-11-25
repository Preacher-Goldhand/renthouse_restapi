using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public IActionResult CreateOrder(OrderModel orderModel)
    {
        ClaimsPrincipal user = HttpContext.User;
        var createdOrder = _orderService.CreateOrder(orderModel, user);
        
        if (createdOrder == null)
        {
            return BadRequest("Błąd podczas tworzenia zamówienia.");
        }
        
        return Ok(createdOrder);
    }

    [HttpPost]
    public IActionResult CancelOrder(int orderId)
    {
        ClaimsPrincipal user = HttpContext.User;
        var canceledOrder = _orderService.CancelOrder(orderId, user);
        
        if (canceledOrder == null)
        {
            return NotFound();
        }
        
        return Ok(canceledOrder);
    }

    [HttpGet]
    public IActionResult MyOrders()
    {
        ClaimsPrincipal user = HttpContext.User;
        IList<OrderModel> orders = _orderService.GetOrdersFromUser(user);

        if (orders == null || orders.Count == 0)
        {
            return NotFound(); 
        }

        return Ok(orders.ToList()); 
    }
    [HttpGet]
    public IActionResult Order(int id){
        ClaimsPrincipal user = HttpContext.User;
        OrderModel order = _orderService.GetOrderModel(id, user);

        if (order == null)
        {
            return NotFound(); 
        }

        return Ok(order);
    }
}