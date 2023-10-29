using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RentHouse.Models;
using RentHouse.Data;
using System.Linq;

namespace RentHouse.Controllers;

public class MachineController : Controller
{
    private readonly ILogger<MachineController> _logger;
    private readonly ApplicationDbContext _db;

    public MachineController(ILogger<MachineController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Details(int? Id)
    {
        if(!Id.HasValue){
            return Json(_db.Machines.ToList());
        }else{
            //string query = "SELECT * FROM Machines WHERE ID = @p0"
            MachineModel machine = -_db.Machines.FirstOrDeafult(machine => machine.Id == Id);
            return Json(machine);
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
