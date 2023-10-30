﻿using System;
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
        string prectenikroky;
        string prectenienemy;

        public Hra()
        {
            InitializeComponent();
            prectenikroky = File.ReadAllText(filepathkroky);
            prectenienemy = File.ReadAllText(filepathenemy);
           
        }
        

        public void kroky()
        {
            List<Krok> KrokList = JsonSerializer.Deserialize<List<Krok>>(prectenikroky);
            int r = random.Next(KrokList.Count);
            TextBoxx.Text = KrokList[r].Text;
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
                    TextBoxx.Text = "Zabil jsi " + App.NowEnemy.Name;
                    
                }
               
            
        }

        void fight()
        {
            List<Enemy> EnemyList = JsonSerializer.Deserialize<List<Enemy>>(prectenienemy);
            int r = random.Next(EnemyList.Count);
            App.NowEnemy = EnemyList[r];
            TextBoxx.Text = "Začal jsi Bojovat s " + App.NowEnemy.Name;
            whilefight();
            
        }

        private void utokButton_Click(object sender, RoutedEventArgs e)
        {
            //App.NowEnemy.Hp = 0;
            //TextBoxx.Text = "";



            int NewEnHp = App.NowEnemy.Hp - App.Hrac.Dmg;
            App.NowEnemy.Hp = NewEnHp;
            TextBoxx.Text = "Použil jsi normální útok a udělil jsi " + App.Hrac.Dmg + " Dmg." + Environment.NewLine + "Nepřítely "+  App.NowEnemy.Name+ " zbývá" + App.NowEnemy.Hp + " HP.";
            whilefight();

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