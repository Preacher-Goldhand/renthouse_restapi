using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// [ApiController]
// [Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult Register()
    {
        
        return Content(htmlContent, "text/html");
    }


}
