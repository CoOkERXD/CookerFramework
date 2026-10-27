using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cooker.Domain.IocTag;

namespace Cooker.Domain.Repository
{
    public interface IRepository<TModel, TKey> where TModel : BaseEntity<TKey>
    {
        IQueryable<TModel> GetIQueryable();
        Task<List<TModel>> GetListAsync(Expression<Func<TModel, bool>> expression, CancellationToken ct);
        Task<TModel> GetEntityAsync(Expression<Func<TModel, bool>> expression, CancellationToken ct);
        Task<TModel> DeleteEntityAsync(TModel model, CancellationToken ct);
        Task<TModel> UpdateEntityAsync(TModel model, CancellationToken ct);
        Task<TModel> AddEntityAsync(TModel model, CancellationToken ct);
    }
}