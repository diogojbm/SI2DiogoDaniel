using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return FindAll().Where(criteria);
        }

        public IEnumerable<Utilizador> FindAll()
        {
            return new UtilizadorMapper(context).ReadAll();
        }
    }
}
