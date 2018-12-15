using ConferenceManager.Model;
using System.Collections.Generic;

namespace ConferenceManager.DAL.mapper.interfaces
{
    interface IConferenciaMapper : IMapper<Conferencia, int?, List<Conferencia>>
    {
    }
}