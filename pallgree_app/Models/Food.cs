using System;
using System.Collections.Generic;

#nullable disable

namespace pallgree_app.Models
{
    public partial class Food
    {
        public int Id { get; set; }
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? Status { get; set; }
    }
}
