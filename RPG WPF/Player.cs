using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_WPF
{
    public class Player
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Dmg { get; set; }
        public int Stepcounter { get; set; }
        public Classa PlayerClassa { get; set; }
        public List<Item> Inventory { get; set; } = new List<Item>();

        public Player(string name, int hp, int dmg, int stepcounter, Classa classa,)
        {
            Name = name;
            Hp = hp;
            Dmg = dmg;
            Stepcounter = stepcounter;
            PlayerClassa = classa;
        }
    }
}
