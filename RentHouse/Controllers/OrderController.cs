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
    public IActionResult RemoveOrder(int orderId)
    {
        _orderService.RemoveOrder(orderId);
     
        return Ok();
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
    public IActionResult GetOrderById(int id){
        var order = _orderService.GetOrderModel(id);

        if (order == null)
        {
            return NotFound(); 
        }

        return Ok(order);
    }
}