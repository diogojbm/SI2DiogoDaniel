using ConferenceManager.DAL;
using ConferenceManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return FindAll().Where(criteria);
        }

        public IEnumerable<Instituicao> FindAll()
        {
            return new InstituicaoMapper(context).ReadAll();
        }
    }
}
