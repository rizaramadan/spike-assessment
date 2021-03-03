using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assessment.Domains.Data;
using Assessment.Domains.PaketSoals;

namespace Assessment.Domains.Soals
{
    public class SoalsController : Controller
    {
        private readonly AppDbContext _context;

        public SoalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Soals
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Soal.Include(s => s.PaketSoal);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Soals/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soal = await _context.Soal
                .Include(s => s.PaketSoal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soal == null)
            {
                return NotFound();
            }

            return View(soal);
        }

        // GET: Soals/Create
        public IActionResult Create()
        {
            ViewData["PaketSoalId"] = new SelectList(_context.PaketSoal, "Id", "Id");
            return View();
        }

        // POST: Soals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoalId,PaketSoalId,Pertanyaan,Tipe,No")] Soal soal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaketSoalId"] = new SelectList(_context.PaketSoal, "Id", "Id", soal.PaketSoalId);
            return View(soal);
        }

        // GET: Soals/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soal = await _context.Soal.FindAsync(id);
            if (soal == null)
            {
                return NotFound();
            }
            ViewData["PaketSoalId"] = new SelectList(_context.PaketSoal, "Id", "Id", soal.PaketSoalId);
            return View(soal);
        }

        // POST: Soals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,PaketSoalId,Pertanyaan,Tipe,No")] Soal soal)
        {
            if (id != soal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoalExists(soal.Id))
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
            ViewData["PaketSoalId"] = new SelectList(_context.PaketSoal, "Id", "Id", soal.PaketSoalId);
            return View(soal);
        }

        // GET: Soals/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soal = await _context.Soal
                .Include(s => s.PaketSoal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soal == null)
            {
                return NotFound();
            }

            return View(soal);
        }

        // POST: Soals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var soal = await _context.Soal.FindAsync(id);
            _context.Soal.Remove(soal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoalExists(long id)
        {
            return _context.Soal.Any(e => e.Id == id);
        }
    }
}
