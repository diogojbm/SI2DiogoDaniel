using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceManager.Concrete
{
    class ConferenciaRepository : IConferenciaRepository
    {
        private IContext context;
        public ConferenciaRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Conferencia> Find(Func<Conferencia, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<Conferencia> FindAll()
        {
            return new ConferenciaMapper(context).ReadAll();
        }
    }
}
