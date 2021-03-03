using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assessment.Domains.Data;
using Microsoft.AspNetCore.Authorization;

namespace Assessment.Domains.PaketSoals
{
    [AllowAnonymous]
    public class PaketSoalsController : Controller
    {
        private readonly AppDbContext _context;

        public PaketSoalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PaketSoals
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaketSoal.ToListAsync());
        }

        // GET: PaketSoals/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paketSoal = await _context.PaketSoal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paketSoal == null)
            {
                return NotFound();
            }

            return View(paketSoal);
        }

        // GET: PaketSoals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaketSoals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipe,MataPelajaran,Domain,Kelas,TingkatKesulitan,Keterangan,Created,CreatorId,Updated,UpdatorId")] PaketSoal paketSoal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paketSoal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paketSoal);
        }

        // GET: PaketSoals/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paketSoal = await _context.PaketSoal.FindAsync(id);
            if (paketSoal == null)
            {
                return NotFound();
            }
            return View(paketSoal);
        }

        // POST: PaketSoals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Tipe,MataPelajaran,Domain,Kelas,TingkatKesulitan,Keterangan,Created,CreatorId,Updated,UpdatorId")] PaketSoal paketSoal)
        {
            if (id != paketSoal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paketSoal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaketSoalExists(paketSoal.Id))
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
            return View(paketSoal);
        }

        // GET: PaketSoals/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paketSoal = await _context.PaketSoal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paketSoal == null)
            {
                return NotFound();
            }

            return View(paketSoal);
        }

        // POST: PaketSoals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var paketSoal = await _context.PaketSoal.FindAsync(id);
            _context.PaketSoal.Remove(paketSoal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaketSoalExists(long id)
        {
            return _context.PaketSoal.Any(e => e.Id == id);
        }
    }
}
