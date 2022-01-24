using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PropertyBuilding.Transversal.Models
{
    public class PropertyTrace : BaseEntity
    {
        [Required]
        public DateTime DateSale { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required] 
        public Double Value { get; set; }
        [Required] 
        public Double Tax { get; set; }
        [Required] 
        public Property Property{ get; set; }
    }
}
