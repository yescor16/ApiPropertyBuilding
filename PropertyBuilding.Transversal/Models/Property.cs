using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PropertyBuilding.Transversal.Models
{
    public class Property : BaseEntity
    {
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required] 
        public decimal Price { get; set; }
        [Required] 
        public int CodeInternal { get; set; }
        [Required] 
        public int Year { get; set; }
        [Required] 
        public int IdOwner { get; set; }
    }
}
