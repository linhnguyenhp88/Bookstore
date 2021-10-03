using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Interfaces.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> Query();
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        IDbContextTransaction BeginTransaction();          
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        TEntity Get(int id);     
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
