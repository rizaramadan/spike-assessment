using Assessment.Domains.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Domains.Intakes
{
    public interface IIntakeService 
    {
        Task GenerateJawabanIntake(long id);
    }

    public class IntakeService : IIntakeService
    {
        private readonly AppDbContext _context;

        public IntakeService(AppDbContext c) => _context = c;

        public async Task GenerateJawabanIntake(long id)
        {
            var jawabans = await _context.JawabanIntakes.Where(x => x.IntakeId == id).ToListAsync();
            if (jawabans != null && jawabans.Count > 0)
            {
                _context.JawabanIntakes.RemoveRange(jawabans);
                await _context.SaveChangesAsync();
            }
            var credentials = await _context.Credential.Where(x => x.IntakeId == id).ToListAsync();
            if (credentials != null && credentials.Count > 0)
            {
                _context.Credential.RemoveRange(credentials);
                await _context.SaveChangesAsync();
            }

            var intake = await _context.Intakes.FindAsync(id);
            var soals = await _context.Soal.Where(x => x.PaketSoalId == intake.PaketSoalId).ToListAsync();
            var siswas = await _context.Siswa.Where(x => x.RombonganBelajarId == intake.RombonganId).ToListAsync();
            credentials = siswas.Select(x => new IntakeCredential
            {
                IntakeId = intake.Id,
                SiswaId = x.Id,
                Username = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            }).ToList();
            await _context.Credential.AddRangeAsync(credentials);

            var now = DateTime.Now;
            var list = new List<JawabanIntake>();
            foreach (var soal in soals)
            {
                var jawabanIntakes = siswas.Select(x => new JawabanIntake
                {
                    Name = $"Jawaban {x.Name}",
                    IntakeId = intake.Id,
                    SoalId = soal.Id,
                    SiswaId = x.Id,
                    Created = now,
                });
                list.AddRange(jawabanIntakes);
            }
            await _context.JawabanIntakes.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }
    }
}
