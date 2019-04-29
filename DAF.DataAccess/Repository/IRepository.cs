using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAF.DataAccess.Repository
{
    /// <summary>
    /// Представляет обобщенный репозиторий.
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Возвращает набор сущностей.
        /// </summary>
        IQueryable<TEntity> GetItems();

        /// <summary>
        /// Возвращает набор сущностей.
        /// Можно задать фильтр.
        /// </summary>
        /// <param name="filter">Условие</param>
        IQueryable<TEntity> GetItems(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Возвращает набор сущностей.
        /// Можно задать фильтр или сортировку.
        /// </summary>
        /// <param name="filter">Условие</param>
        /// <param name="orderBy">Сортировка</param>
        IQueryable<TEntity> GetItems(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

        /// <summary>
        /// Возвращает элемент по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        TEntity GetItem(Guid id);

        /// <summary>
        /// Возращает элемент, отвечающий условию.
        /// Если элемент не найден, возвращает null.
        /// </summary>
        /// <param name="filter">Условие</param>
        TEntity GetItem(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Добавляет в контекст сущность.
        /// </summary>
        /// <param name="item">Экземпляр сущности</param>
        void Insert(TEntity item);

        /// <summary>
        /// Добавляет в контекст набор сущностей.
        /// </summary>
        /// <param name="items">Перечисление сущностей</param>
        void Insert(IEnumerable<TEntity> items);

        /// <summary>
        /// Помечает сущность в контексте как модифицированную.
        /// При вызове метода Save() изменения будут записаны в базу данных.
        /// </summary>
        /// <param name="item">Экземпляр сущности</param>
        void Update(TEntity item);

        /// <summary>
        /// Помечает сущности в контексте как модифицированные.
        /// При вызове метода Save() изменения будут записаны в базу данных.
        /// </summary>
        /// <param name="items">Перечисление сущностей</param>
        void Update(IEnumerable<TEntity> items);

        /// <summary>
        /// Помечает поля в сущности как модифицированные.
        /// При вызове метода Save() изменения будут записаны в базу данных.
        /// </summary>
        /// <param name="item">Экземпляр сущности</param>
        /// <param name="fieldNameArray">Поля для обновления</param>
        void UpdateByFields(TEntity item, IEnumerable<string> fieldNameArray);

        /// <summary>
        /// Помечает сущность в контексте как удаленную.
        /// При вызове метода Save() изменения будут записаны в базу данных.
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        void Delete(Guid id);

        /// <summary>
        /// Помечает сущности в контексте как удаленные.
        /// При вызове метода Save() изменения будут записаны в базу данных.
        /// </summary>
        /// <param name="ids">Массив идентификаторов</param>
        void Delete(Guid[] ids);

        /// <summary>
        /// Помечает сущность в контексте как удаленную.
        /// При вызове метода Save() изменения будут записаны в базу данных.
        /// </summary>
        /// <param name="item">Экземпляр сущности</param>
        void Delete(TEntity item);

        /// <summary>
        /// Помечает сущности в контексте как удаленные.
        /// При вызове метода Save() изменения будут записаны в базу данных.
        /// </summary>
        /// <param name="items">Перечисление сущностей</param>
        void Delete(IEnumerable<TEntity> items);
    }
}