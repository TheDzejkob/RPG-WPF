﻿using System;
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

namespace RPG_WPF
{
    /// <summary>
    /// Interakční logika pro death.xaml
    /// </summary>
    public partial class death : Window
    {
        public death()
        {
            InitializeComponent();
            DeathLabel.Content = "Zemřel jsi " + App.Hrac.Name + " !" + Environment.NewLine + "S počtem kroků " + App.Hrac.Stepcounter;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}