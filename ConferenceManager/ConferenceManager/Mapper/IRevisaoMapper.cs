using ConferenceManager.Model;
using System;
using System.Collections.Generic;

namespace ConferenceManager.DAL.mapper.interfaces
{
    interface IRevisaoMapper : IMapper<Revisao, Tuple<int, string, string, int>, List<Revisao>>
    {
    }
}