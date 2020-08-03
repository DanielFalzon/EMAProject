using EMAProject.Classes;
using EMAProject.Data;
using EMAProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Controllers
{
    public class JsonProviderController: Controller
    {
        private readonly ClinicContext _context;

        public JsonProviderController(ClinicContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetHcpPaginatedTable([FromQuery] int pageNum = 0) 
        {
            IHealthCareProviderService healthCareProviderService = new HealthCareProviderService(_context);
            HealthCareProvidersPaginationModel model = new HealthCareProvidersPaginationModel(healthCareProviderService, pageNum);

            return Json(JsonSerializer.Serialize(model));
        }
    }
}
