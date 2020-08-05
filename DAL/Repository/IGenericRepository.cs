using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity GetByID(object id);

        IEnumerable<TEntity> GetAll();

        void Insert(TEntity entity);

        void Delete(object Id);

        void Delete(TEntity entity);

        void Update(TEntity entity);
    }
}
