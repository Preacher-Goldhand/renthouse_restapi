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

    [HttpPost]
    public IActionResult CancelOrder(int orderId)
    {
        var canceledOrder = _orderService.CancelOrder(orderId);
        
        if (canceledOrder == null)
        {
            return NotFound();
        }
        
        return Ok(canceledOrder);
    }

    //[HttpGet]
    // public IActionResult MyOrders()
    // {
    //     IList<OrderModel> orders = _orderService.GetOrdersFromUser();

    //     if (orders == null || orders.Count == 0)
    //     {
    //         return NotFound(); 
    //     }

    //     return Ok(orders.ToList()); 
    // }
    [HttpGet]
    public IActionResult Order(int id){
        OrderModel order = _orderService.GetOrderModel(id);

        if (order == null)
        {
            return NotFound(); 
        }

        return Ok(order);
    }
}