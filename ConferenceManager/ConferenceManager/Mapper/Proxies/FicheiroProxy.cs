using ConferenceManager.Concrete;
using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManager.Mapper
{
    class FicheiroProxy : Ficheiro
    {
        private IContext context;

        public FicheiroProxy(Ficheiro f, IContext ctx) : base()
        {
            context = ctx;
            Nome = f.Nome;
            IDArtigo = f.IDArtigo;
            NomeConferencia = f.NomeConferencia;
            AnoConferencia = f.AnoConferencia;
            base.ArtigoAssociado = null;
        }

        public override Artigo ArtigoAssociado
        {
            get
            {
                if (base.ArtigoAssociado == null)
                {
                    FicheiroMapper fm = new FicheiroMapper(context);
                    base.ArtigoAssociado = fm.LoadArtigo(IDArtigo, NomeConferencia, AnoConferencia);
                }
                return base.ArtigoAssociado;
            }

            set
            {
                base.ArtigoAssociado = value;
            }
        }
    }
}
