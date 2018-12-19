namespace ConferenceManager.Model
{
    class Ficheiro
    {
        public string Nome { get; set; }
        public int IDArtigo { get; set; }
        public string NomeConferencia { get; set; }
        public int AnoConferencia { get; set; }
        public virtual Artigo ArtigoAssociado { get; set; }
    }
}
