using PropertyBuilding.Transversal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyBuilding.Domain.IRepositories
{
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<Property> ChangePrice(ChangePrice _change);
    }
}
