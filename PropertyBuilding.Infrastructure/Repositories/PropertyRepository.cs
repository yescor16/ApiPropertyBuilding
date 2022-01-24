using Microsoft.EntityFrameworkCore;
using PropertyBuilding.Domain.IRepositories;
using PropertyBuilding.Transversal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyBuilding.Infrastructure.Repositories
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(DatabaseContext context) : base(context) { }
       
        public Task<Property> ChangePrice(ChangePrice _change)
        {
            var entity = context.Set<Property>().FirstOrDefaultAsync(p => p.Id == _change.idProperty);
            if (entity != null)
            {
                entity.Result.Price = _change.newPrice;
                
                context.SaveChangesAsync();
                return context.Set<Property>().FirstOrDefaultAsync(p => p.Id == _change.idProperty); 
            }
            else
            {
                return null;
            }
        }
    }
}
