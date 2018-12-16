using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;

namespace ConferenceManager.Concrete
{
    class ArtigoRepository : IArtigoRepository
    {
        private IContext context;
        public ArtigoRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Artigo> Find(Func<Artigo, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Artigo> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
