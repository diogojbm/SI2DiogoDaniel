using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceManager.Concrete
{
    class InscricaoRepository : IInscricaoRepository
    {
        private IContext context;
        public InscricaoRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Inscricao> Find(Func<Inscricao, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<Inscricao> FindAll()
        {
            return new InscricaoMapper(context).ReadAll();
        }
    }
}
