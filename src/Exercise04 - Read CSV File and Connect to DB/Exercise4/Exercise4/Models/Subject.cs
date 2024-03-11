using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Subject
    {
        private static long lastAssignedCode = 0;
        public string Id { get; set; }
        public long Code { get; set; } = ++lastAssignedCode;
        public string Name { get; set; }
    }
}
