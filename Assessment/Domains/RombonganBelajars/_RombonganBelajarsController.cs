using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assessment.Domains.Data;
using Assessment.Domains.Rombel;

namespace Assessment.Domains.RombonganBelajars
{
    public class RombonganBelajarsController : Controller
    {
        private readonly AppDbContext _context;

        public RombonganBelajarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RombonganBelajars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rombels.ToListAsync());
        }

        // GET: RombonganBelajars/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rombonganBelajar = await _context.Rombels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rombonganBelajar == null)
            {
                return NotFound();
            }

            return View(rombonganBelajar);
        }

        // GET: RombonganBelajars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RombonganBelajars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] RombonganBelajar rombonganBelajar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rombonganBelajar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rombonganBelajar);
        }

        // GET: RombonganBelajars/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rombonganBelajar = await _context.Rombels.FindAsync(id);
            if (rombonganBelajar == null)
            {
                return NotFound();
            }
            return View(rombonganBelajar);
        }

        // POST: RombonganBelajars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] RombonganBelajar rombonganBelajar)
        {
            if (id != rombonganBelajar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rombonganBelajar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RombonganBelajarExists(rombonganBelajar.Id))
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
            return View(rombonganBelajar);
        }

        // GET: RombonganBelajars/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rombonganBelajar = await _context.Rombels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rombonganBelajar == null)
            {
                return NotFound();
            }

            return View(rombonganBelajar);
        }

        // POST: RombonganBelajars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var rombonganBelajar = await _context.Rombels.FindAsync(id);
            _context.Rombels.Remove(rombonganBelajar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RombonganBelajarExists(long id)
        {
            return _context.Rombels.Any(e => e.Id == id);
        }
    }
}
