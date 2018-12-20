using ConferenceManager.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;


namespace ConferenceManager.Concrete
{
    class Context : IContext
    {
        private string connectionString;
        private SqlConnection con = null;

        private ArtigoRepository ctxArtigoRepository;
        private ConferenciaRepository ctxConferenciaRepository;
        private FicheiroRepository ctxFicheiroRepository;
        private InscricaoRepository ctxInscricaoRepository;
        private InstituicaoRepository ctxInstituicaoRepository;
        private RevisaoRepository ctxRevisaoRepository;
        private SubmissaoRepository ctxSubmissaoRepository;
        private UtilizadorRepository ctxUtilizadorRepository;

        public Context(string cs)
        {
            connectionString = cs;
            ctxArtigoRepository = new ArtigoRepository(this);
            ctxConferenciaRepository = new ConferenciaRepository(this);
            ctxFicheiroRepository = new FicheiroRepository(this);
            ctxInscricaoRepository = new InscricaoRepository(this);
            ctxInstituicaoRepository = new InstituicaoRepository(this);
            ctxRevisaoRepository = new RevisaoRepository(this);
            ctxSubmissaoRepository = new SubmissaoRepository(this);
            ctxUtilizadorRepository = new UtilizadorRepository(this);
        }

        public void Open()
        {
            if (con == null) con = new SqlConnection(connectionString);
            if (con.State != ConnectionState.Open) con.Open();
        }

        public SqlCommand createCommand()
        {
            Open();
            SqlCommand cmd = con.CreateCommand();
            return cmd;
        }

        public void Dispose()
        {
            if (con != null)
            {
                con.Dispose();
                con = null;
            }
        }

        public void EnlistTransaction()
        {
            if (con != null) con.EnlistTransaction(Transaction.Current);
        }      

        public ArtigoRepository Artigos
        {
            get
            {
                return ctxArtigoRepository;
            }
        }

        public ConferenciaRepository Conferencias
        {
            get
            {
                return ctxConferenciaRepository;
            }
        }

        public FicheiroRepository Ficheiros
        {
            get
            {
                return ctxFicheiroRepository;
            }
        }

        public InscricaoRepository Inscricoes
        {
            get
            {
                return ctxInscricaoRepository;
            }
        }

        public InstituicaoRepository Instituicoes
        {
            get
            {
                return ctxInstituicaoRepository;
            }
        }

        public RevisaoRepository Revisoes
        {
            get
            {
                return ctxRevisaoRepository;
            }
        }

        public SubmissaoRepository Submissoes
        {
            get
            {
                return ctxSubmissaoRepository;
            }
        }

        public UtilizadorRepository Utilizadores
        {
            get
            {
                return ctxUtilizadorRepository;
            }
        }
    }
}
