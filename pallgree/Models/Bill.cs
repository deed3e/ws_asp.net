using System;
using System.Collections.Generic;

#nullable disable

namespace pallgree.Models
{
    public partial class Bill
    {
        public int Id { get; set; }
        public int IdTable { get; set; }
        public long TimeCheckin { get; set; }
        public long? TimeCheckout { get; set; }
        public int Status { get; set; }
    }
}
