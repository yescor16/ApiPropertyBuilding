using System;
using System.Collections.Generic;
using System.Text;

namespace Property.Domain.Models
{
    public class Property : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public Double Price { get; set; }
        public int CodeInternal { get; set; }
        public int Year { get; set; }
        public Owner Owner { get; set; }
    }
}
