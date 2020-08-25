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
using Microsoft.AspNetCore.DataProtection;

namespace EMAProject.Controllers
{
    public class InterventionsController : Controller
    {
        private readonly ClinicContext _context;
        private readonly IDataProtector _protector;

        public InterventionsController(ClinicContext context, IDataProtectionProvider provider)
        {
            _context = context;
            _protector = provider.CreateProtector("EMAProject.ContentEncryptor.v1");
        }

        // GET: Interventions
        public async Task<IActionResult> Index()
        {
            ViewData["WebViewGdprPolicies"] = _context.WebViews.Where(wv => String.Equals(wv.ViewName, "Index"))
                .Include(wv => wv.GdprPolicyWebViews).ThenInclude(gpwv => gpwv.GdprPolicy).FirstOrDefault();

            List<Intervention> interventions = await _context.Interventions.Include(i => i.ClientInterventions).ThenInclude(ci => ci.Client).ToListAsync();

            foreach (ClientIntervention ci in interventions.SelectMany(i => i.ClientInterventions))
            {
                ci.Client.UnProtect(_protector);
            }

            return View(interventions);
        }

        // GET: Interventions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["WebViewGdprPolicies"] = _context.WebViews.Where(wv => String.Equals(wv.ViewName, "Details"))
                .Include(wv => wv.GdprPolicyWebViews).ThenInclude(gpwv => gpwv.GdprPolicy).FirstOrDefault();

            var intervention = await _context.Interventions
                .Include(m => m.ClientInterventions)
                .ThenInclude(m => m.Client)
                .Include(m => m.Sessions)
                .FirstOrDefaultAsync(m => m.InterventionID == id);
            if (intervention == null)
            {
                return NotFound();
            }

            foreach (ClientIntervention ci in intervention.ClientInterventions) 
            {
                ci.Client.UnProtect(_protector);
            }

            return View(intervention);
        }

        // GET: Interventions/Create
        public IActionResult Create(int id = -1)
        {
            if (id <= 0) { return NotFound(); }

            ViewData["WebViewGdprPolicies"] = _context.WebViews.Where(wv => String.Equals(wv.ViewName, "Create/Edit"))
                .Include(wv => wv.GdprPolicyWebViews).ThenInclude(gpwv => gpwv.GdprPolicy).FirstOrDefault();

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

            ViewData["WebViewGdprPolicies"] = _context.WebViews.Where(wv => String.Equals(wv.ViewName, "Create/Edit"))
                .Include(wv => wv.GdprPolicyWebViews).ThenInclude(gpwv => gpwv.GdprPolicy).FirstOrDefault();

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

        [HttpPost, ActionName("Close")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseIntervention([Bind("InterventionID,PostInterventionScore,Treatment")] Intervention intervention) 
        {
            Intervention oldIntervention = await _context.Interventions.FindAsync(intervention.InterventionID);
            if (oldIntervention != null) 
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        oldIntervention.PostInterventionScore = intervention.PostInterventionScore;
                        oldIntervention.Treatment = intervention.Treatment;
                        _context.Update(oldIntervention);
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
                }
            }
            
            return RedirectToAction("Details", new { id = intervention.InterventionID });
        }

        private bool InterventionExists(int id)
        {
            return _context.Interventions.Any(e => e.InterventionID == id);
        }
    }
}
