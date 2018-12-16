using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public IEnumerable<Conferencia> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
