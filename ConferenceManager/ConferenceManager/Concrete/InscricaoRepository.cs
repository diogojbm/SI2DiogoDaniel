using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public IEnumerable<Inscricao> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
