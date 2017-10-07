using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterSpace.Models
{
    public class LevelModel
    {
        public int ID { get; set; }
        public string Chapter { get; set; }
        public List<int> AiTypes { get; set; }
        public int Speed { get; set; }
        public int LevelStartAnimationType { get;  set;}
        public int LevelEndAnimationType { get; set; }
        public int LevelBackgroundType { get; set; }
    }
}
