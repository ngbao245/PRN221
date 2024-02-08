using System;
using System.Collections.Generic;

namespace CodeInBlue.Entities
{
    public partial class IdentityRole
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}
