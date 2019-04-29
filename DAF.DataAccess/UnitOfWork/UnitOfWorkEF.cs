using System;
using System.Collections;
using DAF.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace DAF.DataAccess.UnitOfWork
{
    public class UnitOfWorkEF : IUnitOfWork
    {
        private bool _disposed;
        private readonly DbContext _db;
        private Hashtable _repositories;

        /// <summary>
        /// Создает экземпляр класса <see cref="UnitOfWorkEF"/>.
        /// </summary>
        /// <param name="db">Экземпляр контекста данных. <seealso cref="DbContext"/></param>
        public UnitOfWorkEF(DbContext db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public virtual IRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            string typeName = typeof(T).Name;

            if (!_repositories.ContainsKey(typeName))
            {
                Type repType = GetTargetRepositoryType();

                object repInstance = Activator
                    .CreateInstance(repType
                            .MakeGenericType(typeof(T)),
                        _db);

                _repositories.Add(typeName, repInstance);
            }

            return (IRepository<T>)_repositories[typeName];
        }

        private Type GetTargetRepositoryType()
        {
            return typeof(GenericRepositoryEF<>);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }

            _disposed = true;
        }
    }
}