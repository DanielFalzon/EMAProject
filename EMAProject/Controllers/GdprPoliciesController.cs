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
    public class GdprPoliciesController : Controller
    {
        private readonly ClinicContext _context;

        public GdprPoliciesController(ClinicContext context)
        {
            _context = context;
        }

        // GET: GdprPolicies
        public async Task<IActionResult> Index()
        {
            return View(await _context.GdprPolicies.ToListAsync());
        }

        // GET: GdprPolicies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gdprPolicy = await _context.GdprPolicies
                .FirstOrDefaultAsync(m => m.GdprPolicyID == id);
            if (gdprPolicy == null)
            {
                return NotFound();
            }

            return View(gdprPolicy);
        }

        // GET: GdprPolicies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GdprPolicies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GdprPolicyID,Title,Description,Link")] GdprPolicy gdprPolicy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gdprPolicy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gdprPolicy);
        }

        // GET: GdprPolicies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gdprPolicy = await _context.GdprPolicies.FindAsync(id);
            if (gdprPolicy == null)
            {
                return NotFound();
            }
            return View(gdprPolicy);
        }

        // POST: GdprPolicies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GdprPolicyID,Title,Description,Link")] GdprPolicy gdprPolicy)
        {
            if (id != gdprPolicy.GdprPolicyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gdprPolicy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GdprPolicyExists(gdprPolicy.GdprPolicyID))
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
            return View(gdprPolicy);
        }

        // GET: GdprPolicies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gdprPolicy = await _context.GdprPolicies
                .FirstOrDefaultAsync(m => m.GdprPolicyID == id);
            if (gdprPolicy == null)
            {
                return NotFound();
            }

            return View(gdprPolicy);
        }

        // POST: GdprPolicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gdprPolicy = await _context.GdprPolicies.FindAsync(id);
            _context.GdprPolicies.Remove(gdprPolicy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GdprPolicyExists(int id)
        {
            return _context.GdprPolicies.Any(e => e.GdprPolicyID == id);
        }
    }
}
