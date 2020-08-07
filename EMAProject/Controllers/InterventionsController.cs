using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMAProject.Data;
using EMAProject.Models;
using EMAProject.Common;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

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
                .Include(m => m.ClientInterventions)
                .ThenInclude(m => m.Client)
                .FirstOrDefaultAsync(m => m.InterventionID == id);
            if (intervention == null)
            {
                return NotFound();
            }

            return View(intervention);
        }

        // GET: Interventions/Create
        public IActionResult Create(int id = -1)
        {
            if (id <= 0) { return NotFound(); }
            
            TempData["Client"] = _context.Clients.Find(id);
            HttpContext.Session.SetComplexData("InterventionSelectedClient", TempData["Client"]);

            List<Client> clients = _context.Clients.ToList();
            List<SelectListItem> clientsListItems = new List<SelectListItem>();

            clientsListItems.Add(new SelectListItem()
            {
                Value = "-1",
                Text = "-"
            });

            foreach (Client client in clients) {
                if (client.ClientID != id) 
                {
                    clientsListItems.Add(
                        new SelectListItem()
                        {
                            Value = client.ClientID.ToString(),
                            Text = $"{client.FirstName} {client.Lastname} ({client.NiNumber})"
                        }
                    );
                }
            }

            ViewData["InterventionAllClients"] = clientsListItems;

            return View();

        }

        // POST: Interventions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PreInterventionScore,Antecedence,Behaviours,Consequence,AdditionalClientID")] Intervention intervention)
        {

            if (ModelState.IsValid)
            {
                _context.Add(intervention);
                await _context.SaveChangesAsync();
                /* Upon creation, invoke a function that maps the intervention ID with the 
                 * ID of the client and any additional client.
                 */
                intervention.CreateClientInterventionLinks(_context, HttpContext.Session.GetComplexData<Client>("InterventionSelectedClient"));

                return RedirectToAction("Details", "Clients", new { id = HttpContext.Session.GetComplexData<Client>("InterventionSelectedClient").ClientID});
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
