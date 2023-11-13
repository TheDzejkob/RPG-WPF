using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RPG_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Player Hrac;
        public static Enemy NowEnemy;
        public static Krok NowKrok;
        public static Item NowItem;
        public static List<Item> inventory;
        public int killCount;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Default´placeholdery (prozatimni nez se initializuje v character okně
            Hrac = new Player("Default", 100, 10, 0, null,null);
            NowEnemy = new Enemy("Default", 1000, 1,null,0);
            NowKrok = new Krok("Default", 0, 0);
            NowItem = new Item(0,default,default,0,0,0,false);
        }
    }
}
