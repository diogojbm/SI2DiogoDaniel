using ConferenceManager.Model;
using System;
using System.Collections.Generic;

namespace ConferenceManager.DAL.mapper.interfaces
{
    interface ISubmissaoMapper : IMapper<Submissao, Tuple<int, string, string, int>, List<Submissao>>
    {
    }
}