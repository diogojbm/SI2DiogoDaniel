using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return FindAll().Where(criteria);
        }

        public IEnumerable<Submissao> FindAll()
        {
            return new SubmissaoMapper(context).ReadAll();
        }
    }
}
