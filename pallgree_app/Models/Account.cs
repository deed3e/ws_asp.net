using System;
using System.Collections.Generic;

#nullable disable

namespace pallgree_app.Models
{
    public partial class Account
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
    }
}
