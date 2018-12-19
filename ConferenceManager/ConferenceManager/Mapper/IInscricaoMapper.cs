using ConferenceManager.Model;
using System;
using System.Collections.Generic;

namespace ConferenceManager.DAL.mapper.interfaces
{
    interface IInscricaoMapper : IMapper<Inscricao, Tuple<string, int, string>, List<Inscricao>>
    {
    }
}