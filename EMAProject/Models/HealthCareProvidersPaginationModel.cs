using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMAProject.Classes;
using Microsoft.AspNetCore.Mvc;

namespace EMAProject.Models
{
    public class HealthCareProvidersPaginationModel
    {
        private readonly IHealthCareProviderService _healthCareProviderService;

        public HealthCareProvidersPaginationModel(IHealthCareProviderService healthCareProviderService)
        {
            _healthCareProviderService = healthCareProviderService;
            PopulateModel();
        }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 5;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public List<HealthCareProvider> Data { get; set; }

        public void PopulateModel ()
        {
            Data = _healthCareProviderService.GetPaginatedResult(CurrentPage, PageSize).Result;
            Count = _healthCareProviderService.GetCount().Result;
        }
    }
}
