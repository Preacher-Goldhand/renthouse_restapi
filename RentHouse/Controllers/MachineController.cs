using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetMachineDetails(int machineId)
    {
        MachineModel machine = _machineService.GetMachineById(machineId);
        if (machine == null)
        {
            return NotFound();
        }
        return Ok(machine);
    }
}
