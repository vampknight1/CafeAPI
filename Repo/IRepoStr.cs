using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeAPI.Models;

namespace CafeAPI.Repo
{
    public interface IRepoStr<T> where T : BaseEntity
    {
        List<T> FindAll();
        T FindByID(string id);
        void Add(T itemObj);
        void Update(T itemObj);
        void ApiUpdate(T itemObj, string id);
        void Remove(string id);
    }
}
