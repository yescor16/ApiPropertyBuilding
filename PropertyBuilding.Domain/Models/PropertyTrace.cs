using System;
using System.Collections.Generic;
using System.Text;

namespace Property.Domain.Models
{
    public class PropertyTrace : BaseEntity
    {
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public Double Value { get; set; }
        public Double Tax { get; set; }
        public Property Property{ get; set; }
    }
}
