using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public IEnumerable<Ficheiro> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
