using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// [ApiController]
// [Authorize]
public class MachineController : Controller
{
    private readonly IMachineService _machineService;

    public MachineController(IMachineService machineService)
    {
        _machineService = machineService;
    }

    [HttpGet]
    public IActionResult GetMachines()
    {
        var htmlContent = System.IO.File.ReadAllText("./Views/Machine/Machine.html");
        return Content(htmlContent, "text/html");
    }

    [HttpGet]
    public IActionResult GetAllMachines()
    {
        IList<MachineModel> machines = _machineService.GetAllMachines();

        if (machines == null || machines.Count == 0)
        {
            return NotFound(); 
        }

        return Ok(machines); 
    }

    [HttpGet]
    public IActionResult GetMachineDetails(int id)
    {
        MachineModel machine = _machineService.GetMachineById(id);

        if (machine == null)
        {
            return NotFound();
        }

        return Ok(machine);
    }
}
