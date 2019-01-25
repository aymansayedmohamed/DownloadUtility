using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IDataAccess
{
    public interface IUnitOfWork: IDisposable
    {
        DbContext DbContext { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
