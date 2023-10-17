using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO;

namespace RPG_WPF
{
    /// <summary>
    /// Interakční logika pro Character.xaml
    /// </summary>
    public partial class Character : Window
    {
        string jsonFilePath = "C:\\Users\\PCnetz\\Desktop\\RPG WPF\\RPG WPF\\Classy.json";
        

        public Character()
        {
            InitializeComponent();
            


        // čte z Jsonu classy
        string jsonContent = File.ReadAllText(jsonFilePath);

                // prečita veci z jasonu a predelava je zpet na objekty
                List<Classa> classaList = JsonSerializer.Deserialize<List<Classa>>(jsonContent);
                listboxx.ItemsSource = classaList;
                
                
                //foreach (var item in classaList)
                //{
                //    Console.WriteLine($"Name: {item.Name}");
                //    Console.WriteLine($"Description: {item.Description}");
                //    Console.WriteLine($"Basehp: {item.Basehp}");
                //    Console.WriteLine($"Basedmg: {item.Basedmg}");
                //    Console.WriteLine(); // Add a line break for separation
                //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(menoTextBox.Text)|| listboxx.SelectedIndex == -1) 
            {
                alert alertWin = new alert();
                alertWin.ShowDialog();
            }
            else
            {
                Classa selectedClassa = listboxx.SelectedItem as Classa;


                App.Hrac = new Player(menoTextBox.Text, selectedClassa.Basehp, selectedClassa.Basedmg, 0, selectedClassa);
                Hra hraWindow = new Hra();
                hraWindow.Show();
                Close();
            }
        }
    }
}
