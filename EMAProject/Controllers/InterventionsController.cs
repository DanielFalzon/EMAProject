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
    public class InterventionsController : Controller
    {
        private readonly ClinicContext _context;

        public InterventionsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Interventions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Interventions.ToListAsync());
        }

        // GET: Interventions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions
                .FirstOrDefaultAsync(m => m.InterventionID == id);
            if (intervention == null)
            {
                return NotFound();
            }

            return View(intervention);
        }

        // GET: Interventions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Interventions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InterventionID,PreInterventionScore,PostInterventionScore,Antecedence,Behaviours,Consequence,Treatment")] Intervention intervention)
        {

            if (ModelState.IsValid)
            {
                _context.Add(intervention);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(intervention);
        }

        // GET: Interventions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }
            return View(intervention);
        }

        // POST: Interventions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InterventionID,PreInterventionScore,PostInterventionScore,Antecedence,Behaviours,Consequence,Treatment")] Intervention intervention)
        {
            if (id != intervention.InterventionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intervention);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterventionExists(intervention.InterventionID))
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
            return View(intervention);
        }

        // GET: Interventions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions
                .FirstOrDefaultAsync(m => m.InterventionID == id);
            if (intervention == null)
            {
                return NotFound();
            }

            return View(intervention);
        }

        // POST: Interventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intervention = await _context.Interventions.FindAsync(id);
            _context.Interventions.Remove(intervention);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterventionExists(int id)
        {
            return _context.Interventions.Any(e => e.InterventionID == id);
        }
    }
}
