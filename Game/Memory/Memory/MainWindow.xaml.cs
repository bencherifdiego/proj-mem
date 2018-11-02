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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            System.IO.Stream str = Memory.Properties.Resources.lobby;
            System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
            snd.Play();
        }

        /// <summary>
        /// Open a new gamewindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            new Spellenscherm().ShowDialog();
        }

        /// <summary>
        /// Resume a game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hervatten_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Show the highscores window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Highscore_Click(object sender, RoutedEventArgs e)
        {
            new Highscores().ShowDialog();
        }

        /// <summary>
        /// Set the themes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Themas_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Close the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
