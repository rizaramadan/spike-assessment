using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assessment.Domains.Data;
using Assessment.Domains.Rombel;

namespace Assessment.Domains.Siswas
{
    public class SiswasController : Controller
    {
        private readonly AppDbContext _context;

        public SiswasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Siswas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Siswa.Include(s => s.RombonganBelajar);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Siswas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siswa = await _context.Siswa
                .Include(s => s.RombonganBelajar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siswa == null)
            {
                return NotFound();
            }

            return View(siswa);
        }

        // GET: Siswas/Create
        public IActionResult Create()
        {
            ViewData["RombonganBelajarId"] = new SelectList(_context.Rombels, "Id", "Name");
            return View();
        }

        // POST: Siswas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Handphone,RombonganBelajarId")] Siswa siswa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siswa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RombonganBelajarId"] = new SelectList(_context.Rombels, "Id", "Name", siswa.RombonganBelajarId);
            return View(siswa);
        }

        // GET: Siswas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siswa = await _context.Siswa.FindAsync(id);
            if (siswa == null)
            {
                return NotFound();
            }
            ViewData["RombonganBelajarId"] = new SelectList(_context.Rombels, "Id", "Name", siswa.RombonganBelajarId);
            return View(siswa);
        }

        // POST: Siswas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Handphone,RombonganBelajarId")] Siswa siswa)
        {
            if (id != siswa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siswa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiswaExists(siswa.Id))
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
            ViewData["RombonganBelajarId"] = new SelectList(_context.Rombels, "Id", "Name", siswa.RombonganBelajarId);
            return View(siswa);
        }

        // GET: Siswas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siswa = await _context.Siswa
                .Include(s => s.RombonganBelajar)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siswa == null)
            {
                return NotFound();
            }

            return View(siswa);
        }

        // POST: Siswas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var siswa = await _context.Siswa.FindAsync(id);
            _context.Siswa.Remove(siswa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiswaExists(long id)
        {
            return _context.Siswa.Any(e => e.Id == id);
        }
    }
}
