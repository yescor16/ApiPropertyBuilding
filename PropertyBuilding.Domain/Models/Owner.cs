using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text;

namespace Property.Domain.Models
{
   
    public class Owner : BaseEntity
    {
        [Key]
        public int IdOwner { get; set; }
        [NotMapped]
        public new int Id { get => IdOwner; set => IdOwner = value; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string Photo { get; set; }
    }
}
