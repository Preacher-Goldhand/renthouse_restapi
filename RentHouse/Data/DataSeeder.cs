using RentHouse.Data;

public class DataSeeder
{
    private readonly ApplicationDbContext _dbContext;

    public DataSeeder(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (!_dbContext.Machines.Any())
            {
                var machines = GetMachines();
                _dbContext.Machines.AddRange(machines);
                _dbContext.SaveChanges();
            }
        }
    }

    private IEnumerable<MachineModel> GetMachines()
    {
        List<MachineModel> machines = new List<MachineModel>
        {
            new MachineModel
            {
                Id = 1,
                Name = "Koparka CAT 320D",
                PricePerDay = 350.0M,
                Descrition = "Koparka gąsienicowa o mocy 160 KM",
                IsAvailable = true,
                OperatingHours = 1200
            },
            new MachineModel
            {
                Id = 2,
                Name = "Spychacz John Deere 750K",
                PricePerDay = 750.0M,
                Descrition = "Spychacz gąsienicowy do prac ziemnych",
                IsAvailable = false,
                OperatingHours = 800
            },
            new MachineModel
            {
                Id = 3,
                Name = "Walec drogowy Bomag BW120AD-5",
                PricePerDay = 850.0M,
                Descrition = "Walec tandemowy do utwardzania nawierzchni",
                IsAvailable = true,
                OperatingHours = 500
            },
            new MachineModel
            {
                Id = 4,
                Name = "Dźwig Liebherr 200 HC-L",
                PricePerDay = 3200.0M,
                Descrition = "Urządzenie służące do podnoszenia i przemieszczania materiałów na budowie",
                IsAvailable = true,
                OperatingHours = 340
            },
            new MachineModel
            {
                Id = 5,
                Name = "Betoniarka SANY SY306C-8",
                PricePerDay = 200.0M,
                Descrition = "Maszyna do mieszania betonu, cementu i innych materiałów budowlanych",
                IsAvailable = true,
                OperatingHours = 340
            },
            new MachineModel
            {
                Id = 6,
                Name = "Walec drogowy Hamm HD12VV",
                PricePerDay = 900.0M,
                Descrition = "Maszyna używana do ubijania i zagęszczania nawierzchni dróg",
                IsAvailable = true,
                OperatingHours = 340
            },
            new MachineModel
            {
                Id = 7,
                Name = "Ładowarka kołowa Volvo L120H",
                PricePerDay = 1000.0M,
                Descrition = "Maszyna do załadunku i rozładunku materiałów, często używana na placach budowy",
                IsAvailable = true,
                OperatingHours = 129
            },
            new MachineModel
            {
                Id = 8,
                Name = "Żuraw budowlany Liebherr LTM 1200-5.1",
                PricePerDay = 2200.0M,
                Descrition = "Żuraw samojezdny o udźwigu 200 ton",
                IsAvailable = true,
                OperatingHours = 320
            },
            new MachineModel
            {
                Id = 9,
                Name = "Frezarka drogowa Wirtgen W 200",
                PricePerDay = 1120.0M,
                Descrition = "Maszyna do frezowania i usuwania starej nawierzchni drogowej",
                IsAvailable = true,
                OperatingHours = 720
            },
            new MachineModel
            {
                Id = 10,
                Name = "Piła Husqvarna K970",
                PricePerDay = 80.0M,
                Descrition = "Wykorzystywana do precyzyjnego cięcia betonu i innych materiałów budowlanych",
                IsAvailable = true,
                OperatingHours = 320
            },
            new MachineModel
            {
                Id = 11,
                Name = "Spychacz Komatsu D65EX-16",
                PricePerDay = 920.0M,
                Descrition = "Maszyna służąca do przesuwania dużych ilości ziemi, gruzu lub innych materiałów",
                IsAvailable = true,
                OperatingHours = 1320
            },
            new MachineModel
            {
                Id = 12,
                Name = "Ładowarka CASE 621G",
                PricePerDay = 900.0M,
                Descrition = "Maszyna służąca do przesuwania dużych ilości ziemi, gruzu lub innych materiałów",
                IsAvailable = true,
                OperatingHours = 320
            }
        };

        return machines;
    }
}
