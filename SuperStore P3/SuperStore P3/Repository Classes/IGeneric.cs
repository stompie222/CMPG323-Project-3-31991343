using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoPower_Logistics.Repository_Classes
{
    public interface IGeneric<T> where T : class
    {
        public Task<T> FindById(Guid? id);
        public Task<IEnumerable<T>> GetAll();
        void Add(T entity);
        void Remove(T entity);
        void Edit(T entity);

    }
}