using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_5_Final.Models
{
    public class Superbowl
    {
        public int SuperbowlId { get; set; }
        public int Year { get; set; }
        public int SuperBowlNum { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
