using ConferenceManager.Model;
using System;
using System.Collections.Generic;

namespace ConferenceManager.DAL.mapper.interfaces
{
    interface IFicheiroMapper : IMapper<Ficheiro, Tuple<string, int, string, int>, List<Ficheiro>>
    {
    }
}