using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperAPI.DataServices
{
    public interface IDb2Service<TEntity> where TEntity : class
    {
        Task<TEntity> Insert(TEntity obj);
        Task Update(TEntity obj);
        Task Delete(int id);
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        void Dispose();
    }
}
