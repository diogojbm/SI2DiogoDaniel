using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;

namespace ConferenceManager.Concrete
{
    class RevisaoRepository : IRevisaoRepository
    {
        private IContext context;
        public RevisaoRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Revisao> Find(Func<Revisao, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Revisao> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
