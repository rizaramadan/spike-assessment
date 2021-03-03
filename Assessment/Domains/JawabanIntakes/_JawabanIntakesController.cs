using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assessment.Domains.Data;
using Assessment.Domains.Intakes;

namespace Assessment.Domains.JawabanIntakes
{
    public class JawabanIntakesController : Controller
    {
        private readonly AppDbContext _context;

        public JawabanIntakesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: JawabanIntakes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.JawabanIntakes.Include(j => j.Intake).Include(j => j.Siswa);
            return View(await appDbContext.ToListAsync());
        }

        // GET: JawabanIntakes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jawabanIntake = await _context.JawabanIntakes
                .Include(j => j.Intake)
                .Include(j => j.Siswa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jawabanIntake == null)
            {
                return NotFound();
            }

            return View(jawabanIntake);
        }

        // GET: JawabanIntakes/Create
        public IActionResult Create()
        {
            ViewData["IntakeId"] = new SelectList(_context.Intakes, "Id", "Name");
            ViewData["SiswaId"] = new SelectList(_context.Siswa, "Id", "Name");
            return View();
        }

        // POST: JawabanIntakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IntakeId,SoalId,SiswaId,Username,Password,Created,Jawaban,Skor")] JawabanIntake jawabanIntake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jawabanIntake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IntakeId"] = new SelectList(_context.Intakes, "Id", "Name", jawabanIntake.IntakeId);
            ViewData["SiswaId"] = new SelectList(_context.Siswa, "Id", "Name", jawabanIntake.SiswaId);
            return View(jawabanIntake);
        }

        // GET: JawabanIntakes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jawabanIntake = await _context.JawabanIntakes.FindAsync(id);
            if (jawabanIntake == null)
            {
                return NotFound();
            }
            ViewData["IntakeId"] = new SelectList(_context.Intakes, "Id", "Name", jawabanIntake.IntakeId);
            ViewData["SiswaId"] = new SelectList(_context.Siswa, "Id", "Name", jawabanIntake.SiswaId);
            return View(jawabanIntake);
        }

        // POST: JawabanIntakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,IntakeId,SoalId,SiswaId,Username,Password,Created,Jawaban,Skor")] JawabanIntake jawabanIntake)
        {
            if (id != jawabanIntake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jawabanIntake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JawabanIntakeExists(jawabanIntake.Id))
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
            ViewData["IntakeId"] = new SelectList(_context.Intakes, "Id", "Name", jawabanIntake.IntakeId);
            ViewData["SiswaId"] = new SelectList(_context.Siswa, "Id", "Name", jawabanIntake.SiswaId);
            return View(jawabanIntake);
        }

        // GET: JawabanIntakes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jawabanIntake = await _context.JawabanIntakes
                .Include(j => j.Intake)
                .Include(j => j.Siswa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jawabanIntake == null)
            {
                return NotFound();
            }

            return View(jawabanIntake);
        }

        // POST: JawabanIntakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var jawabanIntake = await _context.JawabanIntakes.FindAsync(id);
            _context.JawabanIntakes.Remove(jawabanIntake);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JawabanIntakeExists(long id)
        {
            return _context.JawabanIntakes.Any(e => e.Id == id);
        }
    }
}
