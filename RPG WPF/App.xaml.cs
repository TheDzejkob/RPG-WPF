﻿using System;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Default´placeholdery (prozatimni nez se initializuje v character okně
            Hrac = new Player("Default", 100, 10, 0, null);


        }
    }
}
