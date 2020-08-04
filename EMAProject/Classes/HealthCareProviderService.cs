using EMAProject.Data;
using EMAProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Classes
{
    public interface IHealthCareProviderService 
    {
        Task<List<HealthCareProvider>> GetPaginatedResult(int currentPage, int pageSize = 5);
        Task<int> GetCount();
    }

    public class HealthCareProviderService : IHealthCareProviderService
    {
        private readonly ClinicContext _context;

        public HealthCareProviderService(ClinicContext context)
        {
            _context = context;
        }

        private async Task<List<HealthCareProvider>> GetData() 
        {
            List<HealthCareProvider> result = await _context.HealthCareProviders.ToListAsync();
            return result;
        }

        public async Task<int> GetCount() 
        {
            var data = await GetData();
            return data.Count;
        }

        public async Task<List<HealthCareProvider>> GetPaginatedResult(int currentPage, int pageSize = 5)
        {
            var data = await GetData();
            return data.OrderBy(x => x.Name).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
