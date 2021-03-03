using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Assessment.Domains.Roles;
using Assessment.Models;
using Assessment.Domains.PaketSoals;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Assessment.Domains.Shared;
using Assessment.Domains.Rombel;
using Assessment.Domains.Intakes;

namespace Assessment.Domains.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, long>
    {
        public DbSet<PaketSoal> PaketSoal { get; set; }
        public DbSet<Soal> Soal { get; set; }
        public DbSet<SoalPg> SoalPg { get; set;  }

        public DbSet<RombonganBelajar> Rombels { get; set; }
        public DbSet<Siswa> Siswa { get; set; }

        public DbSet<Intake> Intakes { get; set; }

        public DbSet<JawabanIntake> JawabanIntakes { get;set; }

        public DbSet<IntakeCredential> Credential { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Soal>()
                .ToTable("Soal")
                .HasDiscriminator<int>("TipeSoal")
                .HasValue<Soal>(1)
                .HasValue<SoalPg>(2);

            base.OnModelCreating(builder);
        }


        const string Added = "Added";
        const string Modified = "Modified";
        const string Deleted = "Deleted";
        void BeforeSaving()
        {
            var httpContextAccessor = this.GetService<IHttpContextAccessor>();
            var userId = httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userLong = long.TryParse(userId, out var result) ? result : long.MinValue;

            foreach (var entry in ChangeTracker.Entries())
            {
                var state = string.Empty;
                if (entry.Entity is ITrackable trackable)
                {
                    state = entry.State.ToString();
                    if (state == Added)
                    {
                        trackable.Created = trackable.Updated = DateTime.Now;
                        if (trackable.CreatorId is long.MinValue or 0L)
                            trackable.CreatorId = trackable.UpdatorId = userLong;
                    }
                    else
                    {
                        entry.Property(nameof(ITrackable.CreatorId)).IsModified = false;
                        entry.Property(nameof(ITrackable.Created)).IsModified = false;
                        if (state == Modified)
                        {

                            trackable.Updated = DateTime.Now;
                            if (trackable.UpdatorId is long.MinValue or 0L)
                                trackable.UpdatorId = userLong;
                        }
                    }
                }
            }
        }
    }
}
