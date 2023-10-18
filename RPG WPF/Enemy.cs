using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_WPF
{
    public class Enemy
    {
        string Name { get; set; }
        int Hp {  get; set; }
        int Dmg { get; set; }

        string Description { get; set; }

        public Enemy (string name, int hp, int dmg, string description)
        {
            Name = name;
            Hp = hp;
            Dmg = dmg;
            Description = description;
        }
    }
}
