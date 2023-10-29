using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RentHouse.Models;

namespace RentHouse.Controllers;

public class MachineController : Controller
{
    private readonly ILogger<MachineController> _logger;

    public MachineController(ILogger<MachineController> logger)
    {
        _logger = logger;
    }

    public IActionResult Details(int? Id)
    {
        if(!Id.hasValue){
        
        }else{
            
            return Json();
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
