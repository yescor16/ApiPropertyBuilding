using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyBuilding.Transversal.Models
{
    public class UploadFile
    {
        public int idProperty { get; set; }
        public IFormFile File { get; set; }
    }
}
