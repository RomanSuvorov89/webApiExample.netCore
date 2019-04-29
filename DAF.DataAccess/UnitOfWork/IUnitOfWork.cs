using System;
using DAF.DataAccess.Repository;

namespace DAF.DataAccess.UnitOfWork
{
    /// <summary>
    /// Представляет контекст UnitOfWork.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Возвращает экземпляр репозитория для сущностей определенного типа.
        /// </summary>
        /// <typeparam name="T">Тип сущностей</typeparam>
        /// <returns>IRepository</returns>
        IRepository<T> Repository<T>() where T : class;

        /// <summary>
        /// Фиксирует изменения в базе данных.
        /// </summary>
        void Save();
    }
}