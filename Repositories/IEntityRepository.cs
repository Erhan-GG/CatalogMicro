using CatalogService.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogService.Repositories
{
    public interface IEntityRepository<T> where T : IEntity
    {
        Task CreateAsync(T item);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(T item);
    }
}