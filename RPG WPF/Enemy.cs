using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_WPF
{
    public class Enemy
    {
        public string Name { get; set; }
        public int Hp {  get; set; }
        public int Dmg { get; set; }

        public string Description { get; set; }
        public Item DropItem { get; set; }

        public Enemy (string name, int hp, int dmg, string description, Item dropItem)
        {
            Name = name;
            Hp = hp;
            Dmg = dmg;
            Description = description;
            DropItem = dropItem;
        }
    }
}
