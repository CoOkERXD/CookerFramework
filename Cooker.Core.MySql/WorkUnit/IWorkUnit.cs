using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooker.Core.WorkUnit
{
    public interface IWorkUnit
    {
        Task BeginTransactionAsync();
        Task<int> CommitAsync();
        Task RollBackAsync();
        Task SaveChangesAsync();
    }
}