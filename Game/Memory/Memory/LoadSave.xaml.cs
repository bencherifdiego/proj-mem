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
using System.IO;

namespace Memory
{
    /// <summary>
    /// Interaction logic for LoadSave.xaml
    /// </summary>
    public partial class LoadSave : Window
    {
        private string delimiter = ";";

        public LoadSave()
        {
            InitializeComponent();
        }

        private void Save1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            string path = @"Save1.csv";
            if (File.Exists(path))
            {

            }
            else
            {
                File.WriteAllText(path, delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter);
            }
            new Spellenscherm1().ShowDialog();
        }

        private void Save2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            string path = @"Save2.csv";
            if (File.Exists(path))
            {

            }
            else
            {
                File.WriteAllText(path, delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter);
            }
            new Spellenscherm2().ShowDialog();
        }

        private void Save3_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            string path = @"Save2.csv";
            if (File.Exists(path))
            {

            }
            else
            {
                File.WriteAllText(path, delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter + Environment.NewLine + delimiter + delimiter + delimiter);
            }
            new Spellenscherm3().ShowDialog();
        }
    }
}
