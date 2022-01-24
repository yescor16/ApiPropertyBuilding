using PropertyBuilding.Transversal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyBuilding.Security.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(User credentials);
    }
}
