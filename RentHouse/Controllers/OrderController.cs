using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// [ApiController]
// [Authorize]
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
        var createdOrder = _orderService.CreateOrder(orderModel);
        
        if (createdOrder == null)
        {
            return BadRequest("Błąd podczas tworzenia zamówienia.");
        }
        
        return Ok(createdOrder);
    }

    // [HttpGet]
    // public IActionResult MyOrders()
    // {
    //     IList<OrderModel> orders = _orderService.GetOrdersFromUser();

    //     if (orders == null || orders.Count == 0)
    //     {
    //         return NotFound(); 
    //     }

    //     return Ok(orders.ToList()); 
    // }
}