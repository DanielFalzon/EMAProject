using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMAProject.Data;
using EMAProject.Models;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.CodeAnalysis.FlowAnalysis;
using EMAProject.Classes;
using EMAProject.Common;

namespace EMAProject.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ClinicContext _context;

        public ClientsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var clients = from s in _context.Clients select s;

            switch (sortOrder)
            {
                case "name_desc":
                    clients = clients.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    clients = clients.OrderBy(s => s.FirstName);
                    break;
            }

            return View(await clients.AsNoTracking().ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            /* D.F. 20/07/2020
             * 1. Get all of the current healthcare providers. 
             * 2. Show all of the healthcare providers to a table.
             * 3. When user clicks on one of the items, the ID must be send to the session.
             * 4. Instead, store these in JS and set the checked value while table is being rendered. 
             */
            if (TempData["ClientCreateChosenHCP"] == null) {
                TempData["ClientCreateChosenHCP"] = new List<int>();
            }

            IHealthCareProviderService healthCareProviderService = new HealthCareProviderService(_context);

            HttpContext.Session.SetComplexData("HCPSelectedItems", new List<int>());

            ViewData["HCPModel"] = new HealthCareProvidersPaginationModel(healthCareProviderService);

            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientID,NiNumber,FirstName,Lastname,DateOfBirth,ContactNumber,Email,AddressLine1,AddressLine2,AddressLine3,ReferredBy,Subscriber,ClientNotes,Medications,SoFirstName,SoLastName,SoRelationship,SoContactNumber")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientID,NiNumber,FirstName,Lastname,DateOfBirth,ContactNumber,Email,AddressLine1,AddressLine2,AddressLine3,ReferredBy,Subscriber,ClientNotes,Medications,SoFirstName,SoLastName,SoRelationship,SoContactNumber")] Client client)
        {
            if (id != client.ClientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientID))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientID == id);
        }
    }
}
