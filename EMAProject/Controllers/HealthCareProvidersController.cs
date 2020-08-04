using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMAProject.Data;
using EMAProject.Models;

namespace EMAProject.Controllers
{
    public class HealthCareProvidersController : Controller
    {
        private readonly ClinicContext _context;

        public HealthCareProvidersController(ClinicContext context)
        {
            _context = context;
        }

        // GET: HealthCareProviders
        public async Task<IActionResult> Index()
        {
            return View(await _context.HealthCareProviders.ToListAsync());
        }

        // GET: HealthCareProviders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthCareProvider = await _context.HealthCareProviders
                .FirstOrDefaultAsync(m => m.HealthCareProviderID == id);
            if (healthCareProvider == null)
            {
                return NotFound();
            }

            return View(healthCareProvider);
        }

        // GET: HealthCareProviders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HealthCareProviders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HealthCareProviderID,Name,ContactNumber")] HealthCareProvider healthCareProvider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(healthCareProvider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(healthCareProvider);

           
        }

        // GET: HealthCareProviders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthCareProvider = await _context.HealthCareProviders.FindAsync(id);
            if (healthCareProvider == null)
            {
                return NotFound();
            }
            return View(healthCareProvider);
        }

        // POST: HealthCareProviders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HealthCareProviderID,Name,ContactNumber")] HealthCareProvider healthCareProvider)
        {
            if (id != healthCareProvider.HealthCareProviderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(healthCareProvider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthCareProviderExists(healthCareProvider.HealthCareProviderID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(healthCareProvider);
        }

        // GET: HealthCareProviders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var healthCareProvider = await _context.HealthCareProviders
                .FirstOrDefaultAsync(m => m.HealthCareProviderID == id);
            if (healthCareProvider == null)
            {
                return NotFound();
            }

            return View(healthCareProvider);
        }

        // POST: HealthCareProviders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var healthCareProvider = await _context.HealthCareProviders.FindAsync(id);
            _context.HealthCareProviders.Remove(healthCareProvider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HealthCareProviderExists(int id)
        {
            return _context.HealthCareProviders.Any(e => e.HealthCareProviderID == id);
        }
    }
}
