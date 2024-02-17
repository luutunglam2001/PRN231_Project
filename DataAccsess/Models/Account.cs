using System;
using System.Collections.Generic;

namespace DataAccsess.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public int? CustomerId { get; set; }
        public string Usename { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
