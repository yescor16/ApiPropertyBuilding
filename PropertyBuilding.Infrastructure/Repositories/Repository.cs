using PropertyBuilding.Domain.IRepositories;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using PropertyBuilding.Transversal.Models;

namespace PropertyBuilding.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T :BaseEntity
    {
        protected readonly DatabaseContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public Repository(DatabaseContext context)
        
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public bool Delete(int Id)
        {
            if (Id.Equals(null)) throw new ArgumentNullException("entity");

            T entity = entities.SingleOrDefault(s => s.Id == Id);
            entities.Remove(entity);
            context.SaveChanges();
            return true;
        }

        public T FindByCondition(Expression<Func<T, bool>> criteria)
        {
            return context.Set<T>().FirstOrDefault(criteria);
        }

        public IEnumerable<T> GetAll()
        
        {
            return entities.AsEnumerable();
        }

        public T GetById(int Id)
        {
            return entities.SingleOrDefault(s => s.Id == Id);
        }

        public List<T> GetListByCondition(Expression<Func<T, bool>> criteria)
        {
            return context.Set<T>().Where(criteria).ToList();
        }

        public T Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            entities.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public bool Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            context.Update(entity);
            context.SaveChanges();
            return true;
        }

      
    }
}
