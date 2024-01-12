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
    public IActionResult CreateOrder([FromBody] OrderModel orderModel)
    {
        var createdOrder = _orderService.CreateOrder(orderModel);
        
        if (createdOrder == null)
        {
            return BadRequest("Błąd podczas tworzenia zamówienia.");
        }
        
        return Ok(createdOrder);
    }

    [HttpDelete]
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

    [HttpGet][AllowAnonymous]
    public IActionResult AllOrders()
    {
        
        IList<OrderModel> orders = _orderService.GetOrders();

        if (orders == null || orders.Count == 0)
        {
            return NotFound(); 
        }

        return Ok(orders.ToList()); 
    }

    [HttpGet][AllowAnonymous]
    public IActionResult GetOrderById(int id){
        var order = _orderService.GetOrderModel(id);

        if (order == null)
        {
            return NotFound(); 
        }

        return Ok(order);
    }

    [HttpGet][AllowAnonymous]
    public IActionResult GetOrderPage()
    {
        var htmlContent = System.IO.File.ReadAllText("./Views/Orders/Order.html");
        return Content(htmlContent, "text/html");
    }
}