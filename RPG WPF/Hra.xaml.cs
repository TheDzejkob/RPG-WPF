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
using System.Text.RegularExpressions;

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
            List<Item> Craftableitems = new List<Item>();
            Craftableitems.Add(new Item(0,"Kamená sekyra", "kamená sekyra kterou jsi vyrobyl", 0, 0, 5, false));

            CraftingListBox.ItemsSource = Craftableitems;
            CraftingListBox.DisplayMemberPath = "Name";

            invListBox.ItemsSource = App.Hrac.Inventory;
            invListBox.DisplayMemberPath = "Name";

            inspectorLabel.Text = "Jméno: " + Environment.NewLine +
                                     "Popisek: " + Environment.NewLine + 
                                     "Heal: " + Environment.NewLine + 
                                     "Dmg: ";
            prectenikroky = File.ReadAllText(filepathkroky);
            prectenienemy = File.ReadAllText(filepathenemy);
            precteniitemy = File.ReadAllText(filepathitemy);
            KeyDown += Window_KeyDown;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

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
            int a = 1;
            App.Hrac.Stepcounter = App.Hrac.Stepcounter + a; // přičte krok

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
                    int a = 1;
                    App.Hrac.Stepcounter = App.Hrac.Stepcounter + a;
                    invListBox.Items.Refresh();
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
            }
            else // 5% na tezbu
            {
                
               
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           funcPicker();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (invListBox.SelectedItem is Item selectedItem)
            {

                inspectorLabel.Text = "Jméno: " + selectedItem.Name + Environment.NewLine +
                                        "Popisek: " + selectedItem.Description + Environment.NewLine +
                                        "Heal: " + selectedItem.Heal + Environment.NewLine +
                                        "Dmg: " + selectedItem.Dmg;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            App.Hrac.Inventory.Remove(invListBox.SelectedItem as Item);
            invListBox.Items.Refresh();
            inspectorLabel.Text = "Jméno: " + Environment.NewLine +
                                     "Popisek: " + Environment.NewLine +
                                     "Heal: " + Environment.NewLine +
                                     "Dmg: ";

        }
        private void ProcessDebugCommand(string command)
        {
            
            if (command.StartsWith("give "))
            {
               
                string[] parts = command.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[1], out int itemId))
                {
                    
                    List<Item> items = JsonSerializer.Deserialize<List<Item>>(precteniitemy);
                    Item itemToAdd = items.FirstOrDefault(item => item.Id == itemId);

                    
                    if (itemToAdd != null)
                    {
                        App.Hrac.Inventory.Add(itemToAdd);
                        invListBox.Items.Refresh();
                    }
                    else
                    {
                        Console.WriteLine($"Item s ID {itemId} nebyl nalezen.");
                    }
                }
                else
                {
                   
                }
            }
            else if (command.StartsWith("clearinv"))
            {
                App.Hrac.Inventory.Clear();
                invListBox.Items.Refresh();
            }
            else
            {
                Console.WriteLine("neznámý command");
            }
        }

        private void ConfirmCommandButton_Click(object sender, RoutedEventArgs e)
        {
            string command = debugCommandLine.Text.Trim();
            ProcessDebugCommand(command);
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(CraftingListBox.SelectedItem is Item selectedItem)
            {
                if (selectedItem.Name == "Kamená sekyra")
                {
                    Item hledanyItem = new Item(1, "šutr", "prostě šutr idk", 10, 0, 0, true);
                    Item hledanyItem2 = new Item(0, "klacek", "prostě klacek idk", 10, 0, 0, true);
                    if (App.Hrac.Inventory.Contains(hledanyItem) && App.Hrac.Inventory.Contains(hledanyItem2))
                    {
                        App.Hrac.Inventory.Remove(hledanyItem);
                        App.Hrac.Inventory.Remove(hledanyItem2);
                        App.Hrac.Inventory.Add(new Item(0, "kamená sekyra", "kamená sekyra kterou jsi vyrobyl", 0, 0, 5, false));
                        invListBox.Items.Refresh();
                        await Task.Delay(800);
                        TextBoxx.Text = "Vyrobyl jsi kamenou sekyru a přidala se ti do inventáře tvůj dmg se zvětšil o 5";
                        int a = App.Hrac.Dmg;
                        App.Hrac.Dmg = a + 5;
                    }
                   
                    else
                    {
                        TextBoxx.Text = "Nemáš dostatek surovin";
                        await Task.Delay(800);
                        TextBoxx.Text = "";
                        return;
                    }
                   
                }
            }
           

            
        }
    }
}