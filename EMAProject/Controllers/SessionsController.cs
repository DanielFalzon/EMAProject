using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMAProject.Data;
using EMAProject.Models;
using System.IO;
using Microsoft.AspNetCore.DataProtection;

namespace EMAProject.Controllers
{
    public class SessionsController : Controller
    {
        private readonly ClinicContext _context;
        private readonly IDataProtector _protector;

        public SessionsController(ClinicContext context, IDataProtectionProvider provider)
        {
            _context = context;
            _protector = provider.CreateProtector("EMAProject.ContentEncryptor.v1");
        }

        // GET: Sessions
        public async Task<IActionResult> Index()
        {
            ViewData["WebViewGdprPolicies"] = _context.WebViews.Where(wv => String.Equals(wv.ViewName, "Index"))
                .Include(wv => wv.GdprPolicyWebViews).ThenInclude(gpwv => gpwv.GdprPolicy).FirstOrDefault();

            var clinicContext = _context.Sessions.Include(s => s.Intervention).ThenInclude(ci => ci.ClientInterventions).ThenInclude(ci => ci.Client);

            foreach (ClientIntervention ci in clinicContext.Select(cc => cc.Intervention)
                .SelectMany(i => i.ClientInterventions)) 
            {
                ci.Client.UnProtect(_protector);
            }

            return View(await clinicContext.ToListAsync());
        }

        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["WebViewGdprPolicies"] = _context.WebViews.Where(wv => String.Equals(wv.ViewName, "Details"))
                .Include(wv => wv.GdprPolicyWebViews).ThenInclude(gpwv => gpwv.GdprPolicy).FirstOrDefault();

            var session = await _context.Sessions
                .Include(s => s.Intervention).ThenInclude(i => i.ClientInterventions).ThenInclude(i => i.Client)
                .FirstOrDefaultAsync(m => m.SessionID == id);

            if (session == null)
            {
                return NotFound();
            }

            foreach (ClientIntervention ci in session.Intervention.ClientInterventions)
            {
                ci.Client.UnProtect(_protector);
            }

            return View(session);
        }

        // GET: Sessions/Create
        public IActionResult Create(int id)
        {
            if (id < 0) { return NotFound(); }

            ViewData["WebViewGdprPolicies"] = _context.WebViews.Where(wv => String.Equals(wv.ViewName, "Create/Edit"))
                .Include(wv => wv.GdprPolicyWebViews).ThenInclude(gpwv => gpwv.GdprPolicy).FirstOrDefault();

            ViewData["InterventionID"] = id;
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("SessionID,SessionTime,IsAccompanied,IsDelivered,CancelledBy,InterventionID,PreSessionNotes")] Session session)
        {
            if (ModelState.IsValid)
            {
                session.InterventionID = id;
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction("details", "interventions", new { id = session.InterventionID });
            }
            ViewData["InterventionID"] = new SelectList(_context.Interventions, "InterventionID", "InterventionID", session.InterventionID);
            return View(session);
        }

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["WebViewGdprPolicies"] = _context.WebViews.Where(wv => String.Equals(wv.ViewName, "Create/Edit"))
                .Include(wv => wv.GdprPolicyWebViews).ThenInclude(gpwv => gpwv.GdprPolicy).FirstOrDefault();

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            ViewData["InterventionID"] = new SelectList(_context.Interventions, "InterventionID", "InterventionID", session.InterventionID);
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SessionID,SessionTime,IsAccompanied,IsDelivered,CancelledBy,InterventionID,PreSessionNotes")] Session session)
        {
            if (id != session.SessionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.SessionID))
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
            ViewData["InterventionID"] = new SelectList(_context.Interventions, "InterventionID", "InterventionID", session.InterventionID);
            return View(session);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Intervention)
                .FirstOrDefaultAsync(m => m.SessionID == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Close")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseSession(Session session)
        {
            using (var memoryStream = new MemoryStream())
            {
                await session.FormFile.CopyToAsync(memoryStream);

                if (memoryStream.Length > 0)
                {
                    Session dbSession = await _context.Sessions.FindAsync(session.SessionID);

                    var file = new SessionNote()
                    {
                        NoteFile = memoryStream.ToArray()
                    };

                    _context.SessionNotes.Add(file);

                    await _context.SaveChangesAsync();

                    dbSession.SessionNoteID = file.SessionNoteID;
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("details", new { id = session.SessionID });
        }

        public async Task<ActionResult> ShowNotes(int id) {
            SessionNote note = await _context.SessionNotes.FindAsync(id);
            return new FileContentResult(note.NoteFile, "application/pdf");
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.SessionID == id);
        }
    }
}
