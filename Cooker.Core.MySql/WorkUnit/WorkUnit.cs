using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Cooker.Core.MySql;
using Microsoft.EntityFrameworkCore;

namespace Cooker.Core.WorkUnit
{
    public class WorkUnit : IWorkUnit, IDisposable
    {
        private readonly CookerDbContext _context;
        public WorkUnit(CookerDbContext context)
        {
            this._context = context;
        }
        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }
        public async Task<int> CommitAsync()
        {
            int result = 0;
            try
            {
                result = await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw new TransactionException($"commit 异常 {ex}");
            }
            return result;
        }
        public async Task RollBackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}