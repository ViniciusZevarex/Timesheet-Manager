using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace TimesheetManager.Api.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> List();
        Task<T> GetById(int id);
        Task<int> Insert(T entidade);
        Task Delete(int id);
        Task Update(int id, T entidade);
    }
}