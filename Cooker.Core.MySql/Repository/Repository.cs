using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cooker.Core.WorkUnit;
using Cooker.Domain;
using Cooker.Domain.IocTag;
using Cooker.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Cooker.Core.MySql;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Cooker.Core.Repository
{
    public class Repository<TModel, TKey> : IRepository<TModel, TKey> where TModel : BaseEntity<TKey>
    {
        private readonly CookerDbContext _context;
        private readonly DbSet<TModel> _dbset;
        public Repository(CookerDbContext context)
        {
            this._context = context;
            this._dbset = context.Set<TModel>();
        }
        public async Task<TModel> AddEntityAsync(TModel model, CancellationToken ct)
        {
            var entity = (await _dbset.AddAsync(model)).Entity;
            return entity;
        }

        public async Task<TModel> DeleteEntityAsync(TModel model, CancellationToken ct)
        {
            var entity = _dbset.Remove(model).Entity;
            return entity;
        }

        public async Task<TModel> GetEntityAsync(Expression<Func<TModel, bool>> expression, CancellationToken ct)
        {
            return await _dbset.FirstOrDefaultAsync(expression, ct);
        }

        public IQueryable<TModel> GetIQueryable()
        {
            return _dbset.AsQueryable<TModel>();
        }

        public async Task<List<TModel>> GetListAsync(Expression<Func<TModel, bool>> expression, CancellationToken ct)
        {
            return await _dbset.Where(expression).ToListAsync(ct);
        }

        public async Task<TModel> UpdateEntityAsync(TModel model, CancellationToken ct)
        {
            var entity = _dbset.Update(model).Entity;
            return entity;
        }
    }
}