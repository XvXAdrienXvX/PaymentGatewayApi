using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity GetByID(object id);

        Task<TEntity> GetByIDAsync(object id);

        Task<List<TEntity>> GetAll();

        Task Insert(TEntity entity);

        void Delete(object Id);

        void Delete(TEntity entity);

        void Update(TEntity entity);
    }
}
