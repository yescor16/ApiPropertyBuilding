using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyBuilding.Transversal.Models
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
