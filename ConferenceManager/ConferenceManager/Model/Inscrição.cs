using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManager.Model
{
    class Inscrição
    {
        public DateTime DataRegisto { get; set; }
        public string NomeConferencia { get; set; }
        public int AnoConferencia { get; set; }
        public string EmailUtilizador { get; set; }
    }
}
