using Microsoft.AspNetCore.Mvc;
using Vehicle_Database_MVC.Data;
using Vehicle_Database_MVC.Models.Domain;
using Vehicle_Database_MVC.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Service.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Vehicle_Database_MVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly VehicleDbContext vehicleDbContext;
        private readonly IMapper _mapper;

        public VehicleModelsController(VehicleDbContext vehicleDbContext, IMapper mapper)
        {
            this.vehicleDbContext = vehicleDbContext;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> IndexVehicleModel()
        {
            var models = await vehicleDbContext.Models.ToListAsync();
            var modelsDto = new List<VehicleModelDto>();
            if (models.Any())
            {
                foreach (var model in models)
                {
                    var vehicleModel =
                      _mapper.Map<VehicleModel, VehicleModelDto>(model);
                    modelsDto.Add(vehicleModel);
                }
            }
            return View(modelsDto);
        }
        public IActionResult AddVehicleModel()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddVehicleModel(VehicleModelDto vehicleModel)
        {
            var vehicle = _mapper.Map<VehicleModel>(vehicleModel);
            
            await vehicleDbContext.Models.AddAsync(vehicle);
            await vehicleDbContext.SaveChangesAsync();
            return RedirectToAction("AddVehicleModel");
        }


        [HttpGet]
        public async Task<IActionResult> ViewVehicleModel(int id)
        {
            var model = await vehicleDbContext.Models.FirstOrDefaultAsync(x => x.Id == id);
            if (model != null)
            {

                var viewModel = new VehicleModel()
                {
                    Id = model.Id,
                    MakeId = model.MakeId,
                    VehicleName = model.VehicleName,
                    VehicleAbrv = model.VehicleAbrv
                };
                var viewModelDto = _mapper.Map<VehicleModelDto>(viewModel);
                return await Task.Run(() => View("ViewVehicleModel", viewModelDto));
            }
            return RedirectToAction("IndexVehicleModel");
        }

 
        [HttpPost]
        public async Task<IActionResult> ViewVehicleModel(VehicleModelDto model)
        {
            var vehicleModel = await vehicleDbContext.Models.FindAsync(model.Id);
            if (vehicleModel != null)
            {
                vehicleModel.VehicleName = model.VehicleName;
                vehicleModel.VehicleAbrv = model.VehicleAbrv;
                await vehicleDbContext.SaveChangesAsync();
                return RedirectToAction("IndexVehicleModel");
            }
            return RedirectToAction("IndexVehicleModel");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(VehicleModelDto model)
        {
            var vehicleModel = await vehicleDbContext.Models.FindAsync(model.Id);
            if (vehicleModel != null)
            {
                vehicleDbContext.Models.Remove(vehicleModel);
                await vehicleDbContext.SaveChangesAsync();
                return RedirectToAction("IndexVehicleModel");
            }
            return RedirectToAction("IndexVehicleModel");
        }

    }
}
