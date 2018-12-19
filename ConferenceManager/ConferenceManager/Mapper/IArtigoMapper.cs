using ConferenceManager.Model;
using System;
using System.Collections.Generic;

namespace ConferenceManager.DAL.mapper.interfaces
{
    interface IArtigoMapper : IMapper<Artigo, Tuple<int, string, int>, List<Artigo>>
    {
    }
}