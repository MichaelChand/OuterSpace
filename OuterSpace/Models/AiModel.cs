using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.Models
{
    public class AiModel
    {

        public int ID { get; set; }
        public string Texture { get; set; }
        public int Strength { get; set; }
        public int HeadingRange { get; set; }
        public string RandomStart { get; set; }
        public int Weapon { get; set; }
        public double FireGranularity { get; set; }
        public int Speed { get; set; }
        public double ScanRange { get; set; }
        public bool HitBarShow { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
