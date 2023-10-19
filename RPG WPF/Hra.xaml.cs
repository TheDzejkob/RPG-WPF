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
        public Hra()
        {
            InitializeComponent();
            void krok()
            {
                string filepath = @"C:\Users\PCnetz\Desktop\RPG WPF\RPG WPF\Json\kroky.json";

            }

            void fight()
            {

            }

            void tezba() 
            {
            
            }

            void funcPicker() 
            {
                double randomnumber = random.NextDouble();
                if (randomnumber < 0.7)//70% šanca 
                {
                    krok();
                } 
                else if (randomnumber < 0.9)//20% šanca pač 0.7 +0.2 kkt 
                {
                    fight();
                }
                else 
                {
                    tezba();
                }
            }

        }
    }
}
