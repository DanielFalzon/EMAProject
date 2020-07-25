using EMAProject.Classes;
using EMAProject.Data;
using EMAProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public ActionResult GetHcpPaginatedTable([FromQuery] int pageNum) 
        {
            /* Return deserialised JSON from model.data which holds
             * 1 - Whether the hcp is selected
             * 2 - Name of HCP
             * 3 - Contact number of HCP
             */
            IHealthCareProviderService healthCareProviderService = new HealthCareProviderService(_context);
            HealthCareProvidersPaginationModel model = new HealthCareProvidersPaginationModel(healthCareProviderService)
            {
                CurrentPage = pageNum
            };
            
            return PartialView("~/Views/Clients/Partials/_HealthCareProvidersPaginationPartial.cshtml", model);
        }
    }
}
