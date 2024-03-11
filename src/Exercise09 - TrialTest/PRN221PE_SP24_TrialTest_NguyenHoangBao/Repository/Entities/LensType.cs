using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Entities
{
    public partial class LensType
    {
        public LensType()
        {
            Eyeglasses = new HashSet<Eyeglass>();
        }

        public string LensTypeId { get; set; }
        public string LensTypeName { get; set; }
        public string LensTypeDescription { get; set; }
        public bool? IsPrescription { get; set; }

        public virtual ICollection<Eyeglass> Eyeglasses { get; set; }
    }
}
