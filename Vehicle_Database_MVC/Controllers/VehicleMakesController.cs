using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Data;
using Vehicle_Database_MVC.Data;
using Vehicle_Database_MVC.Models;
using Vehicle_Database_MVC.Models.Domain;
using Service;

namespace Vehicle_Database_MVC.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly VehicleDbContext vehicleDbContext;
        private readonly IMapper _mapper;

        public VehicleMakesController(VehicleDbContext vehicleDbContext, IMapper mapper)
        {
            this.vehicleDbContext = vehicleDbContext;
            this._mapper = mapper;
        }

      

        [HttpGet]
        public async Task<IActionResult> Index()
       
        { 
            var makes = await vehicleDbContext.Makes.ToListAsync();
            var makesDto = new List<VehicleMakeDto>();

            if (makes.Any())
            {
                foreach (var make in makes)
                {
                    var vehicleMake =
                      _mapper.Map<VehicleMake, VehicleMakeDto>(make);
                    makesDto.Add(vehicleMake);
                }
            }

            return View(makesDto);

        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(VehicleMakeDto vehicleMake)
        {
            var vehicle = _mapper.Map<VehicleMake>(vehicleMake);
    
            await vehicleDbContext.Makes.AddAsync(vehicle);
            await vehicleDbContext.SaveChangesAsync();
            return RedirectToAction("Add");

        }
        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var make = await vehicleDbContext.Makes.FirstOrDefaultAsync(x => x.Id == id);
            if (make != null) {

                var viewModel = new VehicleMake()
                {
                    Id = make.Id,
                    VehicleName = make.VehicleName,
                    VehicleAbrv = make.VehicleAbrv
                };
                var viewmodelDto = _mapper.Map<VehicleMakeDto>(viewModel);
                return await Task.Run(() => View("View", viewmodelDto));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(VehicleMakeDto model)
        {
            var vehicleMake = await vehicleDbContext.Makes.FindAsync(model.Id);
            if (vehicleMake != null)
            {
                vehicleMake.VehicleName= model.VehicleName;
                vehicleMake.VehicleAbrv= model.VehicleAbrv;
                await vehicleDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(VehicleMakeDto model)
        {
            var vehicleMake = await vehicleDbContext.Makes.FindAsync(model.Id);
            if (vehicleMake != null)
            {
                vehicleDbContext.Makes.Remove(vehicleMake);
                await vehicleDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
