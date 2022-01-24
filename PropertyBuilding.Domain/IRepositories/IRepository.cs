
using PropertyBuilding.Transversal.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace PropertyBuilding.Domain.IRepositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        T Insert(T entity);
        bool Update(T entity);
        bool Delete(int Id);
        T FindByCondition(Expression<Func<T,bool>> criteria);
        List<T> GetListByCondition(Expression<Func<T, bool>> criteria);
    }
}
