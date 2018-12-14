namespace ConferenceManager.Model
{
    class Revisao
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
