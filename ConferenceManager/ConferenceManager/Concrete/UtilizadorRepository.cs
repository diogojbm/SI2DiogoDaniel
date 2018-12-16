using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;

namespace ConferenceManager.Concrete
{
    class UtilizadorRepository : IUtilizadorRepository
    {
        private IContext context;
        public UtilizadorRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Utilizador> Find(Func<Utilizador, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Utilizador> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
