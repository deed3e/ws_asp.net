﻿using System;
using System.Collections.Generic;

#nullable disable

namespace pallgree.Models
{
    public partial class Account
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string NumberPhone { get; set; }
        public DateTime? Dob { get; set; }
        public int TotalSpending { get; set; }
    }
}
