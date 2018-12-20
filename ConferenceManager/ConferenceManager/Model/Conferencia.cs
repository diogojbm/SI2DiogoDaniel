using System;
using System.Collections.Generic;

namespace ConferenceManager.Model
{
    class Conferencia
    {
        public string Nome { get; set; }
        public int AnoRealizacao { get; set; }
        public string Acronimo { get; set; }
        public string DataLimiteRevisao { get; set; }
        public string DataLimiteSubmissao { get; set; }
        public string EmailPresidente { get; set; }
        public virtual List<Artigo> ArtigosAssociados { get; set; }
        public virtual List<Utilizador> UtilizadoresAssociados { get; set; }
    }
}
