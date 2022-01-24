using System;
using System.Collections.Generic;
using System.Text;

namespace Property.Domain.Models
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
