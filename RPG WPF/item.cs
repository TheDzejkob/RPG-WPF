using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_WPF
{
    public class Item
    {
        public int Id;
        public string Name;
        public string Description;
        public int Price;
        public int Heal;
        public int Dmg;
        public bool Minetable;

        public Item(int id, string name, string description, int price, int heal, int dmg, bool minetable)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Heal = heal;
            this.Dmg = dmg;
            this.Minetable = minetable;
        }
    }
}
