﻿using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private PaymentDBContext _paymentDBContext;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(PaymentDBContext Context)
        {
            this._paymentDBContext = Context;
            this._dbSet = _paymentDBContext.Set<TEntity>();
        }

        public void Delete(object Id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public TEntity GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> GetByIDAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _paymentDBContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
