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
        string filepathkroky = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Json\kroky.json");
        string filepathenemy = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Json\enemyes.json");
        string filepathitemy= System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Json\Itemy.json");
        string prectenikroky;
        string prectenienemy;
        string precteniitemy;
        bool heavyUsed = false;
        bool utekUsed = false;


        public Hra()
        {
            InitializeComponent();
            prectenikroky = File.ReadAllText(filepathkroky);
            prectenienemy = File.ReadAllText(filepathenemy);
            precteniitemy = File.ReadAllText(filepathitemy);
            KeyDown += Window_KeyDown;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the F1 key is down
            if (e.Key == Key.F1)
            {
                debugOverlay.Visibility = Visibility.Visible;
            }
            if (e.Key == Key.Escape)
            {
                debugOverlay.Visibility = Visibility.Collapsed;
            }
        }


        public void kroky()
        {
            List<Krok> KrokList = JsonSerializer.Deserialize<List<Krok>>(prectenikroky);
            int r = random.Next(KrokList.Count);
            App.NowKrok = KrokList[r];
            App.Hrac.Stepcounter = +1; // přičte krok

            if (App.NowKrok.Heal > 0) 
            {
                if (App.Hrac.Hp == App.Hrac.PlayerClassa.Basehp) // pokud má hráč plné hp
                {
                    TextBoxx.Text = App.NowKrok.Text;
                }
                else if (App.Hrac.Hp + App.NowKrok.Heal > App.Hrac.PlayerClassa.Basehp) // pokud by se při léčení překročilo max hp
                {
                    int NewHracHp = App.Hrac.PlayerClassa.Basehp;
                    App.Hrac.Hp = NewHracHp;
                    TextBoxx.Text = App.NowKrok.Text;
                }
                else // pokud je vše v pořádku (přičte heal)
                {
                    int NewHracHp = App.Hrac.Hp + App.NowKrok.Heal;
                    App.Hrac.Hp = NewHracHp;
                    TextBoxx.Text = App.NowKrok.Text;
                }
            }

            if (App.NowKrok.Dmg > 0) 
            {
                int NewHracHp = App.Hrac.Hp - App.NowKrok.Dmg;
                App.Hrac.Hp = NewHracHp;
                TextBoxx.Text = App.NowKrok.Text;
            }
            else
            {
                TextBoxx.Text = App.NowKrok.Text;
            }
        }
        void whilefight()
        {       
                krokButton.Visibility = Visibility.Collapsed;

                utokButton.Visibility = Visibility.Visible;
                heavyButton.Visibility = Visibility.Visible;
                utekButton.Visibility = Visibility.Visible;
                abilitaButton.Visibility = Visibility.Visible;
                if (App.NowEnemy.Hp == 0 || App.NowEnemy.Hp < 0) 
                {
                    krokButton.Visibility = Visibility.Visible;

                    utokButton.Visibility = Visibility.Collapsed;
                    heavyButton.Visibility = Visibility.Collapsed;
                    utekButton.Visibility = Visibility.Collapsed;
                    abilitaButton.Visibility = Visibility.Collapsed;
                    
                    List<Item> items = JsonSerializer.Deserialize<List<Item>>(precteniitemy);
                int index = App.NowEnemy.ItemID;
                if (index >= 0 && index < items.Count)
                {
                    App.NowItem = items[index];
                    App.Hrac.Inventory.Add(App.NowItem);
                    TextBoxx.Text = "Zabil jsi " + App.NowEnemy.Name + " a získal jsi " + App.NowItem.Name;
                }
                else
                {

                    TextBoxx.Text = "Zabil jsi " + App.NowEnemy.Name;

                }


            }
                if (App.Hrac.Hp == 0 || App.Hrac.Hp < 0)
                {
                    death();
                }
               
            
        }
        void death()
        {

            death death = new death();
            death.Show();
            this.Close();

        }

        void fight()
        {
            List<Enemy> EnemyList = JsonSerializer.Deserialize<List<Enemy>>(prectenienemy);
            int r = random.Next(EnemyList.Count);
            App.NowEnemy = EnemyList[r];
            TextBoxx.Text = "Začal jsi Bojovat s " + App.NowEnemy.Name;
            whilefight();
            
        }

        private async void utokButton_Click(object sender, RoutedEventArgs e)
        {
            int NewEnHp = App.NowEnemy.Hp - App.Hrac.Dmg;
            App.NowEnemy.Hp = NewEnHp;
            TextBoxx.Text = "Použil jsi normální útok a udělil jsi " + App.Hrac.Dmg + " Dmg." + Environment.NewLine + "Nepřítely "+  App.NowEnemy.Name+ " zbývá " + App.NowEnemy.Hp + "  HP.";
            await Task.Delay(1000);
            enemyUtok();
        }

        private async void heavyButton_Click(object sender, RoutedEventArgs e)
        {
            if (heavyUsed == false)
            {
            int NewEnHp = App.NowEnemy.Hp - App.Hrac.Dmg * 2;
            App.NowEnemy.Hp = NewEnHp;
            TextBoxx.Text = "Použil jsi těžký útok a udělil jsi " + App.Hrac.Dmg * 2 + " Dmg." + Environment.NewLine + "Nepřítely " + App.NowEnemy.Name + " zbývá " + App.NowEnemy.Hp + "  HP.";
            heavyUsed = true;
            await Task.Delay(1000);
            enemyUtok();
            }
            else if(heavyUsed == true)
            {
                TextBoxx.Text = "Těžký útok můžeš použít jen jednou za encaunter";
                whilefight();
            }
            else { }
        }
        
        //make new player object


        
        private async void utekButton_Click(object sender, RoutedEventArgs e)
        {
            if (utekUsed == false) 
            { 
                utekUsed = true;

                double randomnumber = random.NextDouble();
                if (randomnumber < 0.20)
                {
                    TextBoxx.Text = "Utekl jsi";
                    krokButton.Visibility = Visibility.Visible;
                    utokButton.Visibility = Visibility.Collapsed;
                    heavyButton.Visibility = Visibility.Collapsed;
                    utekButton.Visibility = Visibility.Collapsed;
                    abilitaButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    TextBoxx.Text = "Nepodařilo se ti utéct";
                    await Task.Delay(1000);
                    enemyUtok();
                }
            }
            else
            {
                TextBoxx.Text = "Utek můžeš použít jen jednou za encaunter";
                whilefight();
            }
        }

        void enemyUtok()
        {
            int NewHracHp = App.Hrac.Hp - App.NowEnemy.Dmg;
            App.Hrac.Hp = NewHracHp;
            TextBoxx.Text = "Nepřítel použil normální útok a udělil ti " + App.NowEnemy.Dmg + " Dmg." + Environment.NewLine + "A zbývá " + App.Hrac.Hp + "  HP.";
            whilefight();
        }

        void tezba()
        {
           List <Item> items = JsonSerializer.Deserialize<List<Item>>(precteniitemy);
           
            // Your tezba logic
        }

        void funcPicker()
        {
            double randomnumber = random.NextDouble();
            if (randomnumber < 0.75) // 75% šanca  na krok
            {
                kroky();
            }
            else if (randomnumber < 0.95) // 20% šanca pač 0.7 + 0.2 kkt  na fight
            {
                fight();
            }
            else // 5% na tezbu
            {
                
               
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           funcPicker();
        }

        
    }
}