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
    class InstituicaoProxy : Instituicao
    {
        private IContext context;
        public InstituicaoProxy(Instituicao i, IContext ctx) : base()
        {
            context = ctx;
            Nome = i.Nome;
            Morada = i.Morada;
            Pais = i.Pais;
            base.UtilizadoresAssociados = null;
        }
        public override List<Utilizador> UtilizadoresAssociados
        {
            get
            {
                if (base.UtilizadoresAssociados == null)
                {
                    InstituicaoMapper im = new InstituicaoMapper(context);
                    base.UtilizadoresAssociados = im.LoadUtilizadores(this);
                }
                return base.UtilizadoresAssociados;
            }

            set
            {
                base.UtilizadoresAssociados = value;
            }
        }
    }
}
