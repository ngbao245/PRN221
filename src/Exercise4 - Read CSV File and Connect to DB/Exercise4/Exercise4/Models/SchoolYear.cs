using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SchoolYear
    {
        private static long lastAssignedId = 0;
        public long Id { get; set; } = ++lastAssignedId;
        public string Name { get; set; }
        public string ExamYear { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
