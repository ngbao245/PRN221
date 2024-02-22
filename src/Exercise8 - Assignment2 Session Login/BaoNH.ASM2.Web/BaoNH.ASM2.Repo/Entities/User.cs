using System;
using System.Collections.Generic;

namespace BaoNH.ASM2.Repo.Entities
{
    public partial class User
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
