using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManager.Model
{
    class Conferência
    {
        public string Nome { get; set; }
        public int AnoRealizacao { get; set; }
        public string Acronimo { get; set; }
        public DateTime DataLimiteRevisao { get; set; }
        public DateTime DataLimiteSubmissao { get; set; }
        public string EmailPresidente { get; set; }
    }
}
