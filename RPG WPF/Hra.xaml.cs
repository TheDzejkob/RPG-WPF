using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.Json;
using System.Text.Json.Serialization;
using RPG_WPF;
using System.IO;
using System.Net.Http.Json;
using System.Windows.Markup;
using System.Xml;

namespace RPG_WPF
{
    /// <summary>
    /// Interakční logika pro Hra.xaml
    ///
    /// </summary>
    public partial class Hra : Window
    {
        GameManager gameManager = new GameManager(new List<Classa>());

        private Random random = new Random();
        string filepathkroky = @"C:\Users\PCnetz\Desktop\RPG WPF\RPG WPF\Json\kroky.json";
        string filepathenemy = @"C:\Users\PCnetz\Desktop\RPG WPF\RPG WPF\Json\enemyes.json";
        string prectenikroky;
        string prectenienemy;

        public Hra()
        {
            InitializeComponent();
            prectenikroky = File.ReadAllText(filepathkroky);
            prectenienemy = File.ReadAllText(filepathenemy);
           
        }
        void takeDmg(int hp,int dmgTaken) 
        {
            hp = hp - dmgTaken;
        }
        

        public void kroky()
        {
            List<Krok> KrokList = JsonSerializer.Deserialize<List<Krok>>(prectenikroky);
            int r = random.Next(KrokList.Count);
            TextBoxx.Text = KrokList[r].Text;
        }

        void fight()
        {
            List<Enemy> EnemyList = JsonSerializer.Deserialize<List<Enemy>>(prectenienemy);
            int r = random.Next(EnemyList.Count);
            TextBoxx.Text = "Začal jsi Bojovat s " + EnemyList[r].Name;
            bool boj = true;
            while (boj == true) 
            {
                
                krokButton.Visibility = Visibility.Collapsed;

                utokButton.Visibility = Visibility.Visible;
                heavyButton.Visibility = Visibility.Visible;
                utekButton.Visibility = Visibility.Visible;
                abilitaButton.Visibility = Visibility.Visible;
                break;
            }
            // Your fight logic
        }


        void tezba()
        {
            // Your tezba logic
        }

        void funcPicker()
        {
            double randomnumber = random.NextDouble();
            if (randomnumber < 0.80) // 80% šanca  na krok
            {
                kroky();
            }
            else if (randomnumber < 0.95) // 15% šanca pač 0.7 + 0.2 kkt  na fight
            {
                fight();
                // Do something else
            }
            else // 5% na tezbu
            {
                
                // Do something else
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           funcPicker();
        }

    }
}