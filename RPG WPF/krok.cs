using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_WPF
{
    public class Krok
    {
        public string Text {  get; set; }
        public int Dmg {  get; set; }
        public int Heal { get; set; }

        public Krok(string text, int dmg, int heal)
        {
        
            Text = text;
            Dmg = dmg;
            Heal = heal;
        }
    }
}
