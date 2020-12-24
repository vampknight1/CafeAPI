using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeAPI.Models;

namespace CafeAPI.Repo
{
    public interface IRepo<T> where T : BaseEntity
    {
        //IEnumerable<T> GetAll();
        List<T> FindAll();
        T FindByID(int id);
        void Add(T itemObj);
        void Update(T itemObj);
        void ApiUpdate(T itemObj, int id);
        void Remove(int id);
    }
}