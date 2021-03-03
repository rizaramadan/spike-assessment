using Assessment.Domains.Data;
using Assessment.Domains.Intakes;
using Assessment.Domains.PaketSoals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Domains.AdminPaket
{
    [Authorize]
    public class AdminPaketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IIntakeService _service;
        public AdminPaketController(AppDbContext context, IIntakeService s)
        {
            _context = context;
            _service = s;
        }
        
        public async Task<IActionResult> Index()
        {
            var paket = await _context.PaketSoal.OrderBy(x => x.Created).ToListAsync();
            return View(paket);
        }

        public async Task<IActionResult> Ongoing()
        {
            var intakes = await _context.Intakes
                .Include(x => x.PaketSoal)
                .Include(x => x.Rombongan)
                .ToListAsync();
            return View(intakes);
        }

        public async Task<IActionResult> ShareAssessment(long id) 
        {
            var data = await _context.Intakes
                .Include(x => x.PaketSoal)
                .Include(x => x.Rombongan).ThenInclude(y => y.Siswa)
                .FirstOrDefaultAsync(x => x.Id == id);
            return View(data);
        }

        public IActionResult Create() => View();

        // POST: PaketSoals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaketSoal paketSoal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paketSoal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit), new { id = paketSoal.Id });
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

            var soal = await _context.Soal.Where(x => x.PaketSoalId == paketSoal.Id && x.Tipe == TipeSoal.Essay).ToListAsync();
            soal.AddRange(await _context.SoalPg.Where(x => x.PaketSoalId == paketSoal.Id).ToListAsync());
            ViewData["Soal"] = soal.OrderBy(x => x.No).ToList();
            return View(paketSoal);
        }

        public IActionResult AddSoalEssay(long id)
        {
            ViewData[nameof(Soal.PaketSoalId)] = id;
            ViewData[nameof(Soal.Tipe)] = (int) TipeSoal.Essay;
            return View();
        }

        // POST: Soals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSoal(Soal soal)
        {
            if (ModelState.IsValid)
            {
                _context.Soal.Add(soal);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Edit), new { Id = soal.PaketSoalId });
        }

        public IActionResult AddSoalPg(long id)
        {
            ViewData[nameof(Soal.PaketSoalId)] = id;
            ViewData[nameof(Soal.Tipe)] = (int) TipeSoal.PG;
            return View();
        }

        // POST: Soals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSoalPg(SoalPg soal)
        {
            if (ModelState.IsValid)
            {
                _context.Soal.Add(soal);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Edit), new { Id = soal.PaketSoalId });
        }

        public IActionResult Use(long id)
        {
            ViewData["PaketSoalId"] = id;
            ViewData["RombonganId"] = new SelectList(_context.Rombels, "Id", "Name");
            return View();
        }

        // POST: Intakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Use(Intake intake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intake);
                await _context.SaveChangesAsync();
                await _service.GenerateJawabanIntake(intake.Id);
                return RedirectToAction(nameof(Index));
            }
            return View(intake);
        }
    }
}
