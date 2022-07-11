using System;
using System.Collections.Generic;

#nullable disable

namespace pallgree_app.Models
{
    public partial class Bill
    {
        public int Id { get; set; }
        public int IdTable { get; set; }
        public DateTime TimeCheckin { get; set; }
        public DateTime? TimeCheckout { get; set; }
        public int Status { get; set; }
        public int? EmployeeCheckout { get; set; }
    }
}
