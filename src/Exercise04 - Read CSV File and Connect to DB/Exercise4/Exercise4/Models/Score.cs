using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Score
    {
        private static long lastAssignedId = 0;
        public long Id { get; set; } = ++lastAssignedId;
        public double Grade { get; set; }
        public Subject Subject { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
