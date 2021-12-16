using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_5_Final.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Conference { get; set; }
        public string Division { get; set; }
        public string Location { get; set; }
        public int Championships { get; set; }

        public ICollection<Superbowl> Superbowls { get; set; }
    }
}
