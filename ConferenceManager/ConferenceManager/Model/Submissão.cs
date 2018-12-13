using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManager.Model
{
    class Submissão
    {
        public int IDArtigo { get; set; }
        public string EmailAutor { get; set; }
        public bool Responsavel { get; set; }
        public string NomeConferencia { get; set; }
        public int AnoConferencia { get; set; }
    }
}
