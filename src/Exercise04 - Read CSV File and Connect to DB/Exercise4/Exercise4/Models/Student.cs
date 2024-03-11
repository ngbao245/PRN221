using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Student
    {
        private static long lastAssignedId = 0;
        public long Id { get; set; } = ++lastAssignedId;
        public string StudentCode { get; set; }
        public SchoolYear SchoolYear { get; set; }
    }
}
