using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;

namespace ConferenceManager.Concrete
{
    class SubmissaoRepository : ISubmissaoRepository
    {
        private IContext context;
        public SubmissaoRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Submissao> Find(Func<Submissao, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Submissao> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
