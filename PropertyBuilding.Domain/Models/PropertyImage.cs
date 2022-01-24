using System;
using System.Collections.Generic;
using System.Text;

namespace Property.Domain.Models
{
    public class PropertyImage : BaseEntity
    {
        public Property Property { get; set; }
        public string File { get; set; }
        public int Enabled { get; set; }
    }
}
