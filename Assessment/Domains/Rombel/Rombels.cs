using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Domains.Rombel
{
    public class Siswa
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Handphone { get; set; }
        public RombonganBelajar RombonganBelajar { get; set; }
        public long RombonganBelajarId { get; set; }
    }

    public class RombonganBelajar
    { 
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Siswa> Siswa { get; set; }
    }
}
