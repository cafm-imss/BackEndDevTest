using CAFM.Core.Interfaces;
using CAFM.Database.Models;

namespace CAFM.Core.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CmmsBeTestContext _context;

        public UnitOfWork(CmmsBeTestContext context)
        {
            _context = context;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
