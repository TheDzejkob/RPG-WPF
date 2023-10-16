using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_WPF
{
    public class Classa
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Basehp { get; set; }
        public int Basedmg { get; set; }
        public Classa(string name, string description, int basehp, int basedmg)
        {
            Name = name;
            Description = description;
            Basehp = basehp;
            Basedmg = basedmg;
        }
    }
}
