using PropertyBuilding.Domain.IRepositories;
using PropertyBuilding.Transversal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyBuilding.Domain.IUnitOfWorks
{
    public interface IUnitOfWork<T> : IDisposable 
    {
        //private DatabaseContext context = new DatabaseContext();
        IRepository<T> GenericRepository<T>() where T : BaseEntity;
        void Save();
        Task<Property> ChangePriceProperty(ChangePrice _change);


    }
}
