using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_WPF
{
    public class krok
    {
        string Text {  get; set; }
        int Dmg {  get; set; }
        int Heal { get; set; }

        public krok(string text, int dmg, int heal)
        {
        
            Text = text;
            Dmg = dmg;
            Heal = heal;
        }
    }
}
