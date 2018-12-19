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
    class ConferenciaProxy : Conferencia
    {
        private IContext context;

        public ConferenciaProxy(Conferencia c, IContext ctx) : base()
        {
            context = ctx;
            Nome = c.Nome;
            AnoRealizacao = c.AnoRealizacao;
            Acronimo = c.Acronimo;
            DataLimiteRevisao = c.DataLimiteRevisao;
            DataLimiteSubmissao = c.DataLimiteSubmissao;
            EmailPresidente = c.EmailPresidente;
            base.ArtigosAssociados = null;
            base.UtilizadoresAssociados = null;
        }

        public override List<Artigo> ArtigosAssociados
        {
            get
            {
                if (base.ArtigosAssociados == null)
                {
                    ConferenciaMapper cm = new ConferenciaMapper(context);
                    base.ArtigosAssociados = cm.LoadArtigos(this);
                }
                return base.ArtigosAssociados;
            }

            set
            {
                base.ArtigosAssociados = value;
            }
        }

        public override List<Utilizador> UtilizadoresAssociados
        {
            get
            {
                if (base.UtilizadoresAssociados == null)
                {
                    ConferenciaMapper cm = new ConferenciaMapper(context);
                    base.UtilizadoresAssociados = cm.LoadUtilizadores(this);
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
