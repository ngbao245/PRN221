using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Entities
{
    public partial class StoreAccount
    {
        public int AccountId { get; set; }
        public string AccountPassword { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public int? Role { get; set; }
    }
}
