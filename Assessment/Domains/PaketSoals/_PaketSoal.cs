using Assessment.Domains.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Domains.PaketSoals
{
    public class PaketSoal: ITrackable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Tipe { get; set; } //diagnostik / formatik
        public string MataPelajaran { get; set; } //matematika
        public string Domain { get; set; } //trigono, aljabar
        public string Kelas { get; set; } //1SD 2SD
        public string TingkatKesulitan { get; set; }
        public string Keterangan { get; set; }
        public DateTime Created { get; set; }
        public long CreatorId { get; set; }
        public DateTime Updated { get; set; }
        public long UpdatorId { get; set; }
    }

    public enum TipeSoal
    {
        Essay = 1,
        PG = 2
    }

    public class Soal 
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long PaketSoalId { get; set; }
        public PaketSoal PaketSoal { get; set; }
        public string Pertanyaan { get; set; }
        public string Kunci { get; set; }
        public TipeSoal Tipe { get; set; }
        public int No { get; set; }
        public Soal() { Tipe = TipeSoal.Essay; }

    }

    //TODO: gimana kalo mau specify beda jumlah jawaban
    public class SoalPg : Soal
    {
        public List<string> Jawaban { get; set; }
        public int JawabanBenar { get; set; }
        public SoalPg() { Tipe = TipeSoal.PG; }
    }
}
