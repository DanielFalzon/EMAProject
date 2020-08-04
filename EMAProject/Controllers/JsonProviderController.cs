using EMAProject.Classes;
using EMAProject.Data;
using EMAProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMAProject.Common;
using Newtonsoft;
using System;

namespace EMAProject.Controllers
{
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    public class JsonProviderController : Controller
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

            foreach (HealthCareProvider hcp in model.Data)
            {
                hcp.isSelected = HttpContext.Session.GetComplexData<List<int>>("HCPSelectedItems").Contains(hcp.HealthCareProviderID);
            }

            return Json(JsonSerializer.Serialize(model));
        }

        [HttpPost]
        public JsonResult ToggleSelectedHcp([FromBody] JsonElement hcpId)
        {
            int selectedItem;
            Int32.TryParse(hcpId.GetProperty("hcpId").GetString(), out selectedItem);

            List<int> selectedItems = HttpContext.Session.GetComplexData<List<int>>("HCPSelectedItems");
            bool result = selectedItems.Contains(selectedItem);

            if (result == false)
            {
                selectedItems.Add(selectedItem);
            }
            else
            {
                selectedItems.Remove(selectedItem);
            }

            HttpContext.Session.SetComplexData("HCPSelectedItems", selectedItems);

            return Json(!result);
        }
    }
}
