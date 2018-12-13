using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManager.Model
{
    class Revisão
    {
        public int NotaMinima { get; set; }
        public int Nota { get; set; }
        public string Texto { get; set; }
        public int IDArtigo { get; set; }
        public string EmailRevisor { get; set; }
        public string NomeConferencia { get; set; }
        public int AnoConferencia { get; set; }
    }
}
