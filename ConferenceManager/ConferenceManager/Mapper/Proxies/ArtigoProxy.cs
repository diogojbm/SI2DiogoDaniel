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
    class ArtigoProxy : Artigo
    {
        private IContext context;

        public ArtigoProxy(Artigo a, IContext ctx) : base()
        {
            context = ctx;
            Identificador = a.Identificador;
            Resumo = a.Resumo;
            DataSubmissao = a.DataSubmissao;
            NomeConferencia = a.NomeConferencia;
            AnoConferencia = a.AnoConferencia;
            Estado = a.Estado;
            base.FicheirosAssociados = null;
            base.RevisoresAssociados = null;
        }

        public override List<Ficheiro> FicheirosAssociados
        {
            get
            {
                if (base.FicheirosAssociados == null)
                {
                    ArtigoMapper am = new ArtigoMapper(context);
                    base.FicheirosAssociados = am.LoadFicheiros(this);
                }
                return base.FicheirosAssociados;
            }

            set
            {
                base.FicheirosAssociados = value;
            }
        }

        public override List<Utilizador> RevisoresAssociados
        {
            get
            {
                if (base.RevisoresAssociados == null)
                {
                    ArtigoMapper am = new ArtigoMapper(context);
                    base.RevisoresAssociados = am.LoadRevisores(this);
                }
                return base.RevisoresAssociados;
            }

            set
            {
                base.RevisoresAssociados = value;
            }
        }
    }
}
