using System;

namespace ConferenceManager.Model
{
    class Conferencia
    {
        public string Nome { get; set; }
        public int AnoRealizacao { get; set; }
        public string Acronimo { get; set; }
        public DateTime DataLimiteRevisao { get; set; }
        public DateTime DataLimiteSubmissao { get; set; }
        public string EmailPresidente { get; set; }
    }
}
