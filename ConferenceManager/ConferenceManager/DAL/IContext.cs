using System;
using System.Data.SqlClient;
using ConferenceManager.Concrete;

namespace ConferenceManager.DAL
{
    interface IContext : IDisposable
    {
        void Open();
        SqlCommand createCommand();
        void EnlistTransaction();

        ArtigoRepository Artigos { get; }
        ConferenciaRepository Conferencias { get; }
        FicheiroRepository Ficheiros { get; }
        InscricaoRepository Inscricoes { get; }
        InstituicaoRepository Instituicoes { get; }
        RevisaoRepository Revisoes { get; }
        SubmissaoRepository Submissoes { get; }
        UtilizadorRepository Utilizadores { get; }
    }
}
