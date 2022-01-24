using PropertyBuilding.Domain.IRepositories;
using PropertyBuilding.Domain.IUnitOfWorks;
using PropertyBuilding.Infrastructure.Repositories;
using PropertyBuilding.Transversal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyBuilding.Infrastructure.UnitOfWorks
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : BaseEntity
    {
        public DatabaseContext context; 
        public IRepository<T> _repository;
        private bool disposed = false;

        private IPropertyRepository especificPropertyRepository;

        public UnitOfWork(DatabaseContext context)
        {
            this.context = context;
            _repository = new Repository<T>(context);
            especificPropertyRepository = new PropertyRepository(context);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        public IRepository<T> GenericRepository<T>() where T : BaseEntity
        {
            return (Repository<T>) _repository;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        //Repository Especific
        public Task<Property> ChangePriceProperty(ChangePrice _change)
        {
            return especificPropertyRepository.ChangePrice(_change);
        }
    }
}
