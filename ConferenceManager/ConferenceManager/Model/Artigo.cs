using System;
using System.Collections.Generic;

namespace ConferenceManager.Model
{
    class Artigo
    {
        public int Identificador { get; set; }
        public string Resumo { get; set; }
        public DateTime DataSubmissao { get; set; }
        public string NomeConferencia { get; set; }
        public int AnoConferencia { get; set; }
        public string Estado { get; set; }
        public virtual List<Ficheiro> FicheirosAssociados { get; set; }
        public virtual List<Utilizador> RevisoresAssociados { get; set; }

    }
}
