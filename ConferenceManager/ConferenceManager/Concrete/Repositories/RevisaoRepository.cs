using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return FindAll().Where(criteria);
        }

        public IEnumerable<Revisao> FindAll()
        {
            return new RevisaoMapper(context).ReadAll();
        }
    }
}
