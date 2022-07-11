using System;
using System.Collections.Generic;

#nullable disable

namespace pallgree_app.Models
{
    public partial class BillDetail
    {
        public int Id { get; set; }
        public int IdBill { get; set; }
        public int IdFood { get; set; }
        public int? Count { get; set; }
        public int? Total { get; set; }
    }
}
