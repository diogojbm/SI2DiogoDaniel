using ConferenceManager.Model;
using System;
using System.Collections.Generic;

namespace ConferenceManager.DAL.mapper.interfaces
{
    interface IConferenciaMapper : IMapper<Conferencia, Tuple<string, int>, List<Conferencia>>
    {
    }
}