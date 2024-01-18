using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetLoginPage()
    {
        var htmlContent = System.IO.File.ReadAllText("./Views/Login/Login.html");
        return Content(htmlContent, "text/html");
    }
    [HttpGet]
    public IActionResult GetRegisterPage()
    {
        var htmlContent = System.IO.File.ReadAllText("./Views/Register/Register.html");
        return Content(htmlContent, "text/html");
    }

    [HttpPost]
    public ActionResult Register([FromBody] UserModel userModel)
    {
        _userService.CreateUser(userModel);
        return Ok();
    }

    [HttpPost]
    public ActionResult Login([FromBody] UserModel userModel)
    {
        string token = _userService.GenerateJwt(userModel);
        return Ok(token);
    }
}
