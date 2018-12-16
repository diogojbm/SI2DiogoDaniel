using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;

namespace ConferenceManager.Concrete
{
    class InstituicaoRepository : IInstituicaoRepository
    {
        private IContext context;
        public InstituicaoRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Instituicao> Find(Func<Instituicao, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instituicao> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
