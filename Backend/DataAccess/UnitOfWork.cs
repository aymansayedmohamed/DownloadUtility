using IDataAccess;
using System.Data.Entity;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContextTransaction _transaction;

        public DbContext dbContext { get; private set; }

        public UnitOfWork()
        {
            dbContext = new DownloadUtilityEntities();
        }

        public void BeginTransaction()
        {
            _transaction = dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }
    }
}
