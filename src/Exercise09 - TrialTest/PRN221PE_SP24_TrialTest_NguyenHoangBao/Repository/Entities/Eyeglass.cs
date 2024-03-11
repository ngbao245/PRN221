using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Entities
{
    public partial class Eyeglass
    {
        public int EyeglassesId { get; set; }
        public string EyeglassesName { get; set; }
        public string EyeglassesDescription { get; set; }
        public string FrameColor { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LensTypeId { get; set; }

        public virtual LensType LensType { get; set; }
    }
}
