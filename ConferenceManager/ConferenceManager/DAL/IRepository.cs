using System.Collections.Generic;

namespace ConferenceManager.DAL
{
    interface IRepository<T>
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> Find(System.Func<T, bool> criteria);
    }
}
