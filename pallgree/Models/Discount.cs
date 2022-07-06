using System;
using System.Collections.Generic;

#nullable disable

namespace pallgree.Models
{
    public partial class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Discount1 { get; set; }
        public int MinSpending { get; set; }
    }
}
