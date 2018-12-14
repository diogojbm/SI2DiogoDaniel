using System;

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
    }
}
