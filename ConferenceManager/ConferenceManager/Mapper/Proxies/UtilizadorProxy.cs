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
    class UtilizadorProxy : Utilizador
    {
        private IContext context;
        public UtilizadorProxy(Utilizador u, IContext ctx) : base()
        {
            context = ctx;
            Email = u.Email;
            Nome = u.Nome;
            NomeInstituicao = u.NomeInstituicao;
            base.InstituicaoAssociada = null;
        }

        public override Instituicao InstituicaoAssociada
        {
            get
            {
                if (base.InstituicaoAssociada == null)
                {
                    UtilizadorMapper um = new UtilizadorMapper(context);
                    base.InstituicaoAssociada = um.LoadInstituicao(NomeInstituicao);
                }
                return base.InstituicaoAssociada;
            }

            set
            {
                base.InstituicaoAssociada = value;
            }
        }
    }
}
