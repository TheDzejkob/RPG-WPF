using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_WPF
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Heal { get; set; }
        public int Dmg { get; set; }
        public bool Minetable { get; set; }

        public Item(int id, string name, string description, int price, int heal, int dmg, bool minetable)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Heal = heal;
            Dmg = dmg;
            Minetable = minetable;
        }
    }
}
