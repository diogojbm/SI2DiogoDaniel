namespace ConferenceManager.Model
{
    class Utilizador
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string NomeInstituicao { get; set; }
        public virtual Instituicao InstituicaoAssociada { get; set; }
    }
}
