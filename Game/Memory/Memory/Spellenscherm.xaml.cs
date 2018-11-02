using SpellenScherm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Spellenscherm.xaml
    /// </summary>
    public partial class Spellenscherm : Window
    {
        private MemoryGrid grid;
        private string path = @"Save1.csv";
        public static string delimiter = ";";

        public Spellenscherm()
        {
            InitializeComponent();
            main = this;
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "ready" + delimiter + "ready" + delimiter + "ready" + delimiter + "ready" + Environment.NewLine + "ready" + delimiter + "ready" + delimiter + "ready" + delimiter + "ready" + Environment.NewLine + "ready" + delimiter + "ready" + delimiter + "ready" + delimiter + "ready" + Environment.NewLine + "ready" + delimiter + "ready" + delimiter + "ready" + delimiter + "ready" + Environment.NewLine + "ready" + delimiter + "ready" + delimiter + "ready" + delimiter + "ready" + Environment.NewLine + "ready" + delimiter + "ready" + delimiter + "ready" + delimiter + "ready" + Environment.NewLine + "ready");
            }
        }

        /// <summary>
        /// Show the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_Click(object sender, RoutedEventArgs e)
        {
            menuBar.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void start_Click(object sender, RoutedEventArgs e)
        {
            // check if a custom folder has been set
            if (Convert.ToString(folderDisplay.Content) == "Folder: /images")
            {
                MemoryGrid.folder = "/images";
            }
            setFolderBox.Visibility = Visibility.Collapsed;
            setFolder.Visibility = Visibility.Collapsed;
            folderDisplay.Width = 1058;
            
            // initialize grid
            grid = new MemoryGrid(GameGrid, 4, 4);
            start.Visibility = Visibility.Collapsed;
            turn1.Content = "Aan de beurt";
        }

        /// <summary>
        /// Set the players name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setNames_Click(object sender, RoutedEventArgs e)
        {
            string userName1 = nameEnter1.Text;
            string userName2 = nameEnter2.Text;

             MemoryGrid.Player1 = userName1;
             MemoryGrid.Player2 = userName2;

            name1.Content = userName1;
            name2.Content = userName2;
            set1.Visibility = Visibility.Collapsed;
            set2.Visibility = Visibility.Collapsed;

            //reads savefile
            string path = @"Save1.csv";

            var reader = new StreamReader(File.OpenRead(path));
            var data = new List<List<string>>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                data.Add(new List<String> { values[0], values[1], values[2], values[3]
                        });
            }
            reader.Close();

            File.WriteAllText(path, userName1 + delimiter + userName2 + delimiter + data[0][2] + delimiter + data[0][3] + Environment.NewLine + data[1][0] + delimiter + data[1][1] + delimiter + data[1][2] + delimiter + data[1][3] + Environment.NewLine + data[2][0] + delimiter + data[2][1] + delimiter + data[2][2] + delimiter + data[2][3] + Environment.NewLine + data[3][0] + delimiter + data[3][1] + delimiter + data[3][2] + delimiter + data[3][3] + Environment.NewLine + data[4][0] + delimiter + data[4][1] + delimiter + data[4][2] + delimiter + data[4][3] + Environment.NewLine + data[5][0] + delimiter + data[5][1] + delimiter + data[5][2] + delimiter + data[5][3] + Environment.NewLine + data[6][0]);
        }

        /// <summary>
        /// Save the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You can't save your game yet.");
        }

        /// <summary>
        /// Load a game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You can't load a game yet.");
        }

        /// <summary>
        /// Reset the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetGame_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Back to mainscreen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Close the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_Click(object sender, RoutedEventArgs e)
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }

        /// <summary>
        /// Go to the wikipedia page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://nl.wikipedia.org/wiki/Memory");
        }

        /// <summary>
        /// Close the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseMenuBar_Click(object sender, RoutedEventArgs e)
        {
            menuBar.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Call the scores from the grid and set them on the screen
        /// </summary>
        internal static Spellenscherm main;
        internal string Score1
        {
            get { return scoreName1.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { scoreName1.Content = value; })); }
        }

        internal string Score2
        {
            get { return scoreName2.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { scoreName2.Content = value; })); }
        }

        internal string setTurn1
        {
            get { return turn1.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { turn1.Content = value; })); }
        }

        internal string setTurn2
        {
            get { return turn2.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { turn2.Content = value; })); }
        }

        /// <summary>
        /// Sets the image folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setFolder_Click(object sender, RoutedEventArgs e)
        {
            string folderSet = setFolderBox.Text;
            MemoryGrid.folder = folderSet;

            folderDisplay.Content = "Folder: " + folderSet;
        }
    }
}
