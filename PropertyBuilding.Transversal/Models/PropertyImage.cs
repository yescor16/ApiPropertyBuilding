using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PropertyBuilding.Transversal.Models
{
    public class PropertyImage : BaseEntity
    {
        [Required]
        public int IdProperty { get; set; }
        public string File { get; set; }
        public int Enabled { get; set; }
    }
}
