namespace ConferenceManager.Model
{
    class Submissao
    {
        public int IDArtigo { get; set; }
        public string EmailAutor { get; set; }
        public bool Responsavel { get; set; }
        public string NomeConferencia { get; set; }
        public int AnoConferencia { get; set; }
    }
}
