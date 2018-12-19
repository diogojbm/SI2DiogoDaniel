using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceManager.Concrete
{
    class FicheiroRepository : IFicheiroRepository
    {
        private IContext context;
        public FicheiroRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Ficheiro> Find(Func<Ficheiro, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<Ficheiro> FindAll()
        {
            return new FicheiroMapper(context).ReadAll();
        }
    }
}
