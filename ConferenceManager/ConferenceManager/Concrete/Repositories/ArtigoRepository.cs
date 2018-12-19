using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return FindAll().Where(criteria);
        }

        public IEnumerable<Artigo> FindAll()
        {
            return new ArtigoMapper(context).ReadAll();
        }
    }
}
