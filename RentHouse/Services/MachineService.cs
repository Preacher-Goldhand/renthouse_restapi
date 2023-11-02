using RentHouse.Data;
using System.Collections.Generic;
using System.Linq;

public interface IMachineService 
{
    IList<MachineModel> GetAllMachines();
    MachineModel GetMachineById(int machineId);
}

public class MachineService : IMachineService
{
    private readonly ApplicationDbContext _dbContext;

    public MachineService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IList<MachineModel> GetAllMachines()
    {
        return _dbContext.Machines.ToList();
    }

    public MachineModel GetMachineById(int id)
    {
        return _dbContext.Machines.FirstOrDefault(m => m.Id == id);
    }
}
