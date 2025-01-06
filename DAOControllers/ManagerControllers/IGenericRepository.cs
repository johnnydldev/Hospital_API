using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAOControllers.ManagerControllers
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<int> Create(T model);
        Task<bool> Edit(T model);
        Task<bool> Delete(int id);
        Task<int> GetMaxId();
        Task<List<T>> GetAllMatchedBy(int idModel);
        Task<List<T>> GetAllMatchesWith(string name);
        Task<List<T>> GetAllMatches(int idModel, string name);

    }//End IGeneric Repository interface
}//End namespace
