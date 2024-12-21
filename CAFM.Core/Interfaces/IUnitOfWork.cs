namespace CAFM.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //-----------------------------------------------------------------------------------
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }

}
