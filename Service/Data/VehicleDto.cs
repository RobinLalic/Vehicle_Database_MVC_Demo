using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Database_MVC.Models.Domain;

namespace Service.Data
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string VehicleName { get; set; }
        public string VehicleAbrv { get; set; }
        public int MakeId { get; set; }
        public VehicleMake VehicleMake { get; set; }
    }
}
