using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assessment.Domains.Data;
using Microsoft.Extensions.Caching.Memory;

namespace Assessment.Domains.Intakes
{
    public class IntakesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly IIntakeService _service;


        public IntakesController(AppDbContext context, IMemoryCache cache, IIntakeService s)
        {
            _context = context;
            _cache = cache;
            _service = s;
        }
        public async Task<IActionResult> GenerateJawabanIntake(long id)
        {
            await _service.GenerateJawabanIntake(id);
            return RedirectToAction(nameof(Details), new { id });
        }
        
        //TODO: validasi tanggal mulai dan tanggal selesai juga
        public async Task<IActionResult> SiswaIntake(long id, long siswaId, long? jawabanIntakeId) 
        {
            var cred = (IntakeCredential) _cache.Get($"intake{id}-siswa{siswaId}");
            if (cred != null)
            {
                var jawabanIntakes = await _context.JawabanIntakes.Include(x => x.Soal)
                    .Where(x => x.IntakeId == id && x.SiswaId == siswaId)
                    .OrderBy(x => x.Soal.No)
                    .ToListAsync();

                var result = new SubmitJawaban { Username = cred.Username, Password = cred.Password };
                var index = 0;
                if (jawabanIntakeId.HasValue)
                {
                    index = jawabanIntakes.FindIndex(0, x => x.Id == jawabanIntakeId.Value);
                }
                var isLast = jawabanIntakes.Count - 1 == index;
                var isFirst = index == 0;
                if (!isLast || !isFirst)
                {
                    ViewData["Prev"] = isFirst ? null : (long?)jawabanIntakes[index - 1].Id;
                    ViewData["Next"] = isLast ? null : (long?)jawabanIntakes[index + 1].Id;
                }
                return View(result.FromJawabanIntake(jawabanIntakes[index])) ;
            }
            return View(nameof(LoginIntake), new LoginIntake { IntakeId = id, SiswaId = siswaId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(long id, SubmitJawaban sub)
        {
            var jawaban = await _context.JawabanIntakes.FirstOrDefaultAsync(x => x.Id == sub.Id && x.SiswaId == sub.SiswaId && x.IntakeId == sub.IntakeId);
            jawaban.Jawaban = sub.Jawaban;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SiswaIntake), new { id = sub.IntakeId, siswaId = sub.SiswaId, jawabanIntakeId = sub.Id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginIntake(LoginIntake login) 
        {
            var cred = await _context.Credential.FirstOrDefaultAsync(x => 
                x.IntakeId == login.IntakeId 
                && x.SiswaId == login.SiswaId 
                && x.Username == login.Username 
                && x.Password == login.Password );
            if (cred != null) 
            {
                _cache.Set($"intake{login.IntakeId}-siswa{login.SiswaId}", cred);
            }
            //TODO: give info to client when failed to login
            return RedirectToAction(nameof(SiswaIntake), new { id = login.IntakeId, siswaId = login.SiswaId });
        }

        #region CRUD
        // GET: Intakes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Intakes.Include(i => i.PaketSoal).Include(i => i.Rombongan);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Intakes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intake = await _context.Intakes
                .Include(i => i.PaketSoal)
                .Include(i => i.Rombongan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intake == null)
            {
                return NotFound();
            }

            return View(intake);
        }

        
        // GET: Intakes/Create
        public IActionResult Create()
        {
            ViewData["PaketSoalId"] = new SelectList(_context.PaketSoal, "Id", "Id");
            ViewData["RombonganId"] = new SelectList(_context.Rombels, "Id", "Name");
            return View();
        }

        // POST: Intakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Tipe,Start,End,RombonganId,PaketSoalId")] Intake intake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaketSoalId"] = new SelectList(_context.PaketSoal, "Id", "Id", intake.PaketSoalId);
            ViewData["RombonganId"] = new SelectList(_context.Rombels, "Id", "Name", intake.RombonganId);
            return View(intake);
        }

        // GET: Intakes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intake = await _context.Intakes.FindAsync(id);
            if (intake == null)
            {
                return NotFound();
            }
            ViewData["PaketSoalId"] = new SelectList(_context.PaketSoal, "Id", "Id", intake.PaketSoalId);
            ViewData["RombonganId"] = new SelectList(_context.Rombels, "Id", "Name", intake.RombonganId);
            return View(intake);
        }

        // POST: Intakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Tipe,Start,End,RombonganId,PaketSoalId")] Intake intake)
        {
            if (id != intake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntakeExists(intake.Id))
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
            ViewData["PaketSoalId"] = new SelectList(_context.PaketSoal, "Id", "Id", intake.PaketSoalId);
            ViewData["RombonganId"] = new SelectList(_context.Rombels, "Id", "Name", intake.RombonganId);
            return View(intake);
        }

        // GET: Intakes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intake = await _context.Intakes
                .Include(i => i.PaketSoal)
                .Include(i => i.Rombongan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intake == null)
            {
                return NotFound();
            }

            return View(intake);
        }

        // POST: Intakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var intake = await _context.Intakes.FindAsync(id);
            _context.Intakes.Remove(intake);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntakeExists(long id)
        {
            return _context.Intakes.Any(e => e.Id == id);
        }
        #endregion
    }
}
