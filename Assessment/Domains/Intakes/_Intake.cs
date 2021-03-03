using Assessment.Domains.PaketSoals;
using Assessment.Domains.Rombel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Domains.Intakes
{
    public enum TipeIntake 
    {
        Online = 1,
        Tatap
    }

    public class Intake
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public TipeIntake Tipe { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public RombonganBelajar Rombongan { get; set; }
        public long RombonganId { get; set; }
        public PaketSoal PaketSoal { get; set; }
        public long PaketSoalId { get; set; }
        //TODO: intake perlu ada state-nya ga
        public bool Done { get; set; }
    }

    public class IntakeCredential 
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long IntakeId { get; set; }
        public Intake Intake { get; set; }
        public long SiswaId { get; set; }
        public Siswa Siswa { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class JawabanIntake 
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long IntakeId { get; set; }
        public Intake Intake { get; set; }
        public long SoalId { get; set; }
        public Soal Soal { get; set; }

        public Siswa Siswa { get; set; }
        public long SiswaId { get; set; }

        //TODO: gimana kalo mau ganti jawaban
        public DateTime Created { get; set; }
        //TODO: perlu nyatet kapan jawab?
        public string Jawaban { get; set; }

        //TODO: skor bisa koma ga
        public int Skor { get; set; }
    }

    public class SubmitJawaban : JawabanIntake 
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public SubmitJawaban FromJawabanIntake(JawabanIntake j) 
        {
            Id = j.Id;
            Name = j.Name;
            IntakeId = j.IntakeId;
            Intake = j.Intake;
            SoalId = j.SoalId;
            Soal = j.Soal;
            SiswaId = j.SiswaId;
            Siswa = j.Siswa;
            Created = j.Created;
            Jawaban = j.Jawaban;
            Skor = j.Skor;
            return this;
        }
    }

    public class LoginIntake 
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public long IntakeId { get; set; }
        public long SiswaId { get; set; }
    }
}
