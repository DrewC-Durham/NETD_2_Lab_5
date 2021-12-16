using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab_5_Final.Models;

namespace Lab_5_Final.Controllers
{
    public class SuperbowlsController : Controller
    {
        private readonly FootballContext _context;

        public SuperbowlsController(FootballContext context)
        {
            _context = context;
        }

        // GET: Superbowls
        public async Task<IActionResult> Index()
        {
            // Order by descending year for cleaner view
            var footballContext = _context.Superbowls.Include(s => s.Team).OrderByDescending(s => s.Year);
            return View(await footballContext.ToListAsync());
        }

        // GET: Superbowls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superbowl = await _context.Superbowls
                .Include(s => s.Team)
                .FirstOrDefaultAsync(m => m.SuperbowlId == id);
            if (superbowl == null)
            {
                return NotFound();
            }

            return View(superbowl);
        }

        // GET: Superbowls/Create
        public IActionResult Create()
        {
            var teams = from s in _context.Teams select s;

            teams = teams.OrderBy(s => s.Name);

            ViewData["TeamId"] = new SelectList(teams, "TeamId", "Name");
            return View();
        }

        // POST: Superbowls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SuperbowlId,Year,SuperBowlNum,TeamId")] Superbowl superbowl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(superbowl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", superbowl.TeamId);
            return View(superbowl);
        }

        // GET: Superbowls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superbowl = await _context.Superbowls.FindAsync(id);
            if (superbowl == null)
            {
                return NotFound();
            }
            var teams = from s in _context.Teams select s;

            teams = teams.OrderBy(s => s.Name);
            ViewData["TeamId"] = new SelectList(teams, "TeamId", "Name", superbowl.TeamId);
            return View(superbowl);
        }

        // POST: Superbowls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SuperbowlId,Year,SuperBowlNum,TeamId")] Superbowl superbowl)
        {
            if (id != superbowl.SuperbowlId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(superbowl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuperbowlExists(superbowl.SuperbowlId))
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
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", superbowl.TeamId);
            return View(superbowl);
        }

        // GET: Superbowls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superbowl = await _context.Superbowls
                .Include(s => s.Team)
                .FirstOrDefaultAsync(m => m.SuperbowlId == id);
            if (superbowl == null)
            {
                return NotFound();
            }

            return View(superbowl);
        }

        // POST: Superbowls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var superbowl = await _context.Superbowls.FindAsync(id);
            _context.Superbowls.Remove(superbowl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuperbowlExists(int id)
        {
            return _context.Superbowls.Any(e => e.SuperbowlId == id);
        }
    }
}
