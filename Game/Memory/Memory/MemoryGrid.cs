using Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Xml.Serialization;

namespace SpellenScherm
{
    public class MemoryGrid
    {
        // defines the grid
        private Grid grid;

        // creates 2 players
        public static string Player1 { get; set; }
        public static string Player2 { get; set; }

        // define the number of rows and cols
        private int rows, cols;

        // variable for the number of clicks
        static int numberOfClicks = 0;

        // the total scores which will be displayed to the players
        static int scoreName1Tot;
        static int scoreName2Tot;

        // variables to count if there is made a new point, and to make it easier to keep track of the turns
        static int scoreName1;
        static int scoreName2;

        // a variable to count the number of pairs, and to know when the game is over
        static int numberOfPairs;

        // a bool to check if the game needs to wait and if currently waiting
        private bool hasDelay;

        // bools that control the turns
        private bool turnName1 = true;
        private bool turnName2 = false;

        // Images the are displayed
        private Image card;
        private Image Image1;
        private Image Image2;

        // Folder where images are stored
        public static string folder { get; set; }

        /// <summary>
        /// Initialize the grid and assign the images to the grid
        /// </summary>
        /// <param name="grid">Defines the grid</param>
        /// <param name="rows">How many rows there are</param>
        /// <param name="cols">How many cols there are</param>
        public MemoryGrid(Grid grid, int rows, int cols)
        {
            this.grid = grid;
            this.rows = rows;
            this.cols = cols;

            InitializeGrid();
            AddImages();
        }

        /// <summary>
        /// Initialises the GameGrid
        /// </summary>
        private void InitializeGrid()
        {
            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < cols; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        /// <summary>
        /// Adds images to the grid
        /// </summary>
        private void AddImages()
        {
            List<ImageSource> images = GetImagesList();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // assign the back of the image
                    Image back = new Image();
                    back.Source = new BitmapImage(new Uri(folder + "/back.png", UriKind.RelativeOrAbsolute));

                    // when one of the players click on a card
                    back.MouseDown += new System.Windows.Input.MouseButtonEventHandler(CardClick);

                    // set the cards
                    back.Tag = images.First();
                    images.RemoveAt(0);
                    Grid.SetColumn(back, col);
                    Grid.SetRow(back, row);
                    grid.Children.Add(back);
                }
            }
        }

        /// <summary>
        /// Give each card a random image
        /// </summary>
        /// <returns>Return the images</returns>
        public List<ImageSource> GetImagesList()
        {
            // a list that holds the images
            List<ImageSource> images = new List<ImageSource>();

            // two lists that keep track of the used images, so there are only 2 cards of the sort
            List<string> random1 = new List<string>();
            List<string> random2 = new List<string>();
            
            // variables used for saving the grind into a savefile
            string C1 = null;
            string C2 = null;
            string C3 = null;
            string C4 = null;
            string C5 = null;
            string C6 = null;
            string C7 = null;
            string C8 = null;
            string C9 = null;
            string C10 = null;
            string C11 = null;
            string C12 = null;
            string C13 = null;
            string C14 = null;
            string C15 = null;
            string C16 = null;

            // randomizer
            for (int i = 0; i < 16; i++)
            {
                if (i < 8)
                {
                    // a variable that represents the image that is going to be used
                    int imageNR = 0;

                    // generate a random int between 1 and 8
                    Random rnd = new Random();
                    imageNR = rnd.Next(1, 9);

                    // if the genrated number already exists (in the list 'random1'), generate a new number
                    if (random1.Contains(Convert.ToString(imageNR)))
                    {
                        i--;
                    }
                    // if the generated number does not exists (in the list 'random1'), grab the image with that number
                    else
                    {
                        random1.Add(Convert.ToString(imageNR));
                        ImageSource source = new BitmapImage(new Uri(folder + "/" + imageNR + ".png", UriKind.RelativeOrAbsolute));
                        images.Add(source);

                        //places the randomly picked cards into the variables
                        if (i == 0)
                        {
                            C1 = Convert.ToString(imageNR);
                        }
                        if (i == 1)
                        {
                            C2 = Convert.ToString(imageNR);
                        }
                        if (i == 2)
                        {
                            C3 = Convert.ToString(imageNR);
                        }
                        if (i == 3)
                        {
                            C4 = Convert.ToString(imageNR);
                        }
                        if (i == 4)
                        {
                            C5 = Convert.ToString(imageNR);
                        }
                        if (i == 5)
                        {
                            C6 = Convert.ToString(imageNR);
                        }
                        if (i == 6)
                        {
                            C7 = Convert.ToString(imageNR);
                        }
                        if (i == 7)
                        {
                            C8 = Convert.ToString(imageNR);
                        }
                    }
                }
                if (i >= 8)
                {
                    // a variable that represents the image that is going to be used
                    int imageNR = 0;

                    // generate a random int between 1 and 8
                    Random rnd = new Random();
                    imageNR = rnd.Next(1, 9);

                    // if the genrated number already exists (in the list 'random2'), generate a new number
                    if (random2.Contains(Convert.ToString(imageNR)))
                    {
                        i--;
                    }
                    // if the generated number does not exists (in the list 'random2'), grab the image with that number
                    else
                    {
                        random2.Add(Convert.ToString(imageNR));
                        ImageSource source = new BitmapImage(new Uri(folder + "/" + imageNR + ".png", UriKind.RelativeOrAbsolute));
                        images.Add(source);

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

                        string delimiter = ";";

                        //places the randomly picked cards into variables
                        if (i == 8)
                        {
                            C9 = Convert.ToString(imageNR);
                        }
                        if (i == 9)
                        {
                            C10 = Convert.ToString(imageNR);
                        }
                        if (i == 10)
                        {
                            C11 = Convert.ToString(imageNR);
                        }
                        if (i == 11)
                        {
                            C12 = Convert.ToString(imageNR);
                        }
                        if (i == 12)
                        {
                            C13 = Convert.ToString(imageNR);
                        }
                        if (i == 13)
                        {
                            C14 = Convert.ToString(imageNR);
                        }
                        if (i == 14)
                        {
                            C15 = Convert.ToString(imageNR);
                        }
                        if (i == 15)
                        {
                            C16 = Convert.ToString(imageNR);
                        }

                        //writes the variables into the savefile
                        File.WriteAllText(path, data[0][0] + delimiter + data[0][1] + delimiter + data[0][2] + delimiter + data[0][3] + Environment.NewLine + "0" + delimiter + "0" + delimiter + data[1][2] + delimiter + data[1][3] + Environment.NewLine + C1 + delimiter + C2 + delimiter + C3 + delimiter + C4 + Environment.NewLine + C5 + delimiter + C6 + delimiter + C7 + delimiter + C8 + Environment.NewLine + C9 + delimiter + C10 + delimiter + C11 + delimiter + C12 + Environment.NewLine + C13 + delimiter + C14 + delimiter + C15 + delimiter + C16 + Environment.NewLine + data[6][0]);
                    }
                }
            }
            return images;
        }

        /// <summary>
        /// Turns the card when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardClick(object sender, MouseButtonEventArgs e)
        {
            // wait with showing the card if this is true, the previous turn needs to finish first
            if (hasDelay) return;

            // if the game does not have to wait, show the card
            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            card.Source = front;
            numberOfClicks++;

            CheckCards(card);
        }

        /// <summary>
        /// Shows who's turn it is under the players name
        /// </summary>
        private void ShowTurn()
        {
            // if its player1's turn, show 'Aan de beurt' under their name
            if (turnName1 == true)
            {
                Spellenscherm.main.setTurn1 = "Aan de beurt";
                Spellenscherm.main.setTurn2 = "";

            }
            // if its player2's turn, show 'Aan de beurt' under their name
            else if (turnName2 == true)
            {
                Spellenscherm.main.setTurn1 = "";
                Spellenscherm.main.setTurn2 = "Aan de beurt";
            }
        }

        /// <summary>
        /// Grab the clicked card
        /// </summary>
        /// <param name="card">The card that has been clicked</param>
        private void CheckCards(Image card)
        {
            this.card = card;

            // card the cards that have been clicked
            if (numberOfClicks < 2 || numberOfClicks == 2)
            {

                if (this.Image1 == null)
                {
                    Image1 = card;
                }
                else if (this.Image2 == null)
                {
                    Image2 = card;
                }
            }

            // when to cards have been clicked, check if they are a pair
            if (numberOfClicks == 2)
            {
                CheckPair(Image1, Image2);

                // reset the variables for the next turn
                numberOfClicks = 0;
                Image1 = null;
                Image2 = null;
            }
        }

        /// <summary>
        /// Check if the two clicked cards have the same image (source)
        /// </summary>
        /// <param name="card1">The first card that has been clicked</param>
        /// <param name="card2">The second card that has been clicked</param>
        public void CheckPair(Image card1, Image card2)
        {
            this.Image1 = card1;
            this.Image2 = card2;

            // if 2 images are clicked, there are the same and the same card is not clicked twice
            if (Convert.ToString(card1.Source) == Convert.ToString(card2.Source) && (card1 != card2))
            {
                PlaySoundPositive();
                GetPoint(card1, card2);


                //reads savefile
                string path = @"Save1.csv";
                string cardnr = null;

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

                //turns the card's name into a number
                for (int b = 1; b < 9; b++)
                {
                    if (Convert.ToString(card1.Source).Contains(b + ".png"))
                    {
                        cardnr = Convert.ToString(b);
                    }
                }

                //removes the 2 clicked cards from the savefile
                string delimiter = ";";
                int i;
                int x;
                for (i = 2; i < 6; i++)
                {
                    for (x = 0; x < 4; x++)
                    {
                        var test = data[i][x];
                        if (data[i][x] == cardnr)
                        {
                            data[i][x] = null;
                        }
                    }
                }

                File.WriteAllText(path, data[0][0] + delimiter + data[0][1] + delimiter + data[0][2] + delimiter + data[0][3] + Environment.NewLine + data[1][0] + delimiter + data[1][1] + delimiter + data[1][2] + delimiter + data[1][3] + Environment.NewLine + data[2][0] + delimiter + data[2][1] + delimiter + data[2][2] + delimiter + data[2][3] + Environment.NewLine + data[3][0] + delimiter + data[3][1] + delimiter + data[3][2] + delimiter + data[3][3] + Environment.NewLine + data[4][0] + delimiter + data[4][1] + delimiter + data[4][2] + delimiter + data[4][3] + Environment.NewLine + data[5][0] + delimiter + data[5][1] + delimiter + data[5][2] + delimiter + data[5][3] + Environment.NewLine + data[6][0]);
            }
            // if 2 images are clicked, they are not the same and the same card is not clicked twice
            else
            {
                PlaySoundNegative();
                ResetCards(Image1, Image2);
            }   

            CheckTurn();

            // if the same card is clicked twice, the player keeps their turn
            if (Convert.ToString(card1.Source) == Convert.ToString(card2.Source) && (card1 == card2))
            {
                PlaySoundStupid();
                StayTurn();
            }

            UpdateScore();

            // if all the pair have been found
            if (numberOfPairs == 8)
            {
                CheckWinner();
            }

            // reset the variables for the next turn
            scoreName1 = 0;
            scoreName2 = 0;
            ShowTurn();
        }

        /// <summary>
        /// Gives turns
        /// </summary>
        private void CheckTurn()
        {
            //reads savefile
            string path = @"Save1.csv";
            string delimiter = ";";

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

            // check if its player1's turn
            if (turnName1 == true)
            {
                // if player 1 has a point, they keep their turn and their score increases with one
                if (scoreName1 == 1)
                {
                    scoreName1Tot = scoreName1 + scoreName1Tot;
                }
                // if player 1 does not have a point, they lose their turn and their score stays the same
                else if (scoreName1 == 0)
                {
                    turnName1 = false;
                    turnName2 = true;

                    File.WriteAllText(path, data[0][0] + delimiter + data[0][1] + delimiter + data[0][2] + delimiter + data[0][3] + Environment.NewLine + data[1][0] + delimiter + data[1][1] + delimiter + data[1][2] + delimiter + data[1][3] + Environment.NewLine + data[2][0] + delimiter + data[2][1] + delimiter + data[2][2] + delimiter + data[2][3] + Environment.NewLine + data[3][0] + delimiter + data[3][1] + delimiter + data[3][2] + delimiter + data[3][3] + Environment.NewLine + data[4][0] + delimiter + data[4][1] + delimiter + data[4][2] + delimiter + data[4][3] + Environment.NewLine + data[5][0] + delimiter + data[5][1] + delimiter + data[5][2] + delimiter + data[5][3] + Environment.NewLine + "P2");
                }
            }
            // check if its player2's turn
            else if (turnName2 == true)
            {
                // if player 2 has a point, they keep their turn and their score increases with one
                if (scoreName2 == 1)
                {
                    scoreName2Tot = scoreName2 + scoreName2Tot;
                }
                // if player 2 does not have a point, they lose their turn and their score stays the same
                else if (scoreName2 == 0)
                {
                    turnName2 = false;
                    turnName1 = true;

                    File.WriteAllText(path, data[0][0] + delimiter + data[0][1] + delimiter + data[0][2] + delimiter + data[0][3] + Environment.NewLine + data[1][0] + delimiter + data[1][1] + delimiter + data[1][2] + delimiter + data[1][3] + Environment.NewLine + data[2][0] + delimiter + data[2][1] + delimiter + data[2][2] + delimiter + data[2][3] + Environment.NewLine + data[3][0] + delimiter + data[3][1] + delimiter + data[3][2] + delimiter + data[3][3] + Environment.NewLine + data[4][0] + delimiter + data[4][1] + delimiter + data[4][2] + delimiter + data[4][3] + Environment.NewLine + data[5][0] + delimiter + data[5][1] + delimiter + data[5][2] + delimiter + data[5][3] + Environment.NewLine + "P1");
                }
            }
        }

        /// <summary>
        /// When the same card is doubleclicked, the turn is kept
        /// </summary>
        private void StayTurn()
        {
            // check if its player1's turn, give the turn to player 2
            if (turnName1 == true)
            {
                turnName1 = false;
                turnName2 = true;
            }
            // check if its player2's turn and give the turn to player 1
            else if (turnName1 == false)
            {
                turnName1 = true;
                turnName2 = false;
            }
        }

        /// <summary>
        /// Updates Score Labels
        /// </summary>
        private void UpdateScore()
        {
            Spellenscherm.main.Score1 = "Score: " + scoreName1Tot;
            Spellenscherm.main.Score2 = "Score: " + scoreName2Tot;
        }

        /// <summary>
        /// Give points and remove pairs from GameGrid
        /// </summary>
        /// <param name="card1">The first card that has been clicked</param>
        /// <param name="card2">The second card that has been clicked</param>
        private async void GetPoint(Image card1, Image card2)
        {
            //reads savefile
            string path = @"Save1.csv";
            string delimiter = ";";

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

            // if its player1's turn, increase their score
            if (turnName1 == true)
            {
                numberOfPairs++;
                scoreName1++;

                data[1][0] = Convert.ToString(Convert.ToInt32(data[1][0]) + 1);

                File.WriteAllText(path, data[0][0] + delimiter + data[0][1] + delimiter + data[0][2] + delimiter + data[0][3] + Environment.NewLine + data[1][0] + delimiter + data[1][1] + delimiter + data[1][2] + delimiter + data[1][3] + Environment.NewLine + data[2][0] + delimiter + data[2][1] + delimiter + data[2][2] + delimiter + data[2][3] + Environment.NewLine + data[3][0] + delimiter + data[3][1] + delimiter + data[3][2] + delimiter + data[3][3] + Environment.NewLine + data[4][0] + delimiter + data[4][1] + delimiter + data[4][2] + delimiter + data[4][3] + Environment.NewLine + data[5][0] + delimiter + data[5][1] + delimiter + data[5][2] + delimiter + data[5][3] + Environment.NewLine + data[6][0]);
            }
            // if its player2's turn, increase their score
            else if (turnName2 == true)
            {
                numberOfPairs++;
                scoreName2++;

                data[1][1] = Convert.ToString(Convert.ToInt32(data[1][1]) + 1);

                File.WriteAllText(path, data[0][0] + delimiter + data[0][1] + delimiter + data[0][2] + delimiter + data[0][3] + Environment.NewLine + data[1][0] + delimiter + data[1][1] + delimiter + data[1][2] + delimiter + data[1][3] + Environment.NewLine + data[2][0] + delimiter + data[2][1] + delimiter + data[2][2] + delimiter + data[2][3] + Environment.NewLine + data[3][0] + delimiter + data[3][1] + delimiter + data[3][2] + delimiter + data[3][3] + Environment.NewLine + data[4][0] + delimiter + data[4][1] + delimiter + data[4][2] + delimiter + data[4][3] + Environment.NewLine + data[5][0] + delimiter + data[5][1] + delimiter + data[5][2] + delimiter + data[5][3] + Environment.NewLine + data[6][0]);
            }

            // wait a third of a second, show the second card first
            hasDelay = true;
            await Task.Delay(300);

            // remove the cards from the board
            card1.Source = new BitmapImage(new Uri("", UriKind.RelativeOrAbsolute));
            card2.Source = new BitmapImage(new Uri("", UriKind.RelativeOrAbsolute));

            hasDelay = false;
        }

        /// <summary>
        /// Reset the cards
        /// </summary>
        /// <param name="card1">The first card that has been clicked</param>
        /// <param name="card2">The second card that has been clicked</param>
        private async void ResetCards(Image card1, Image card2)
        {
            this.Image1 = card1;
            this.Image2 = card2;

            // wait a second, show the card first before showing its back again
            hasDelay = true;
            await Task.Delay(1000);

            // show the back of the card again.
            card1.Source = new BitmapImage(new Uri(folder + "/back.png", UriKind.RelativeOrAbsolute));
            card2.Source = new BitmapImage(new Uri(folder + "/back.png", UriKind.RelativeOrAbsolute));
            hasDelay = false;
        }

        /// <summary>
        /// Announce the winner of the game
        /// </summary>
        private void CheckWinner()
        {
            SetHighscore();

            // when the scores of player1 and player2 are the same
            if (scoreName1Tot == scoreName2Tot)
            {
                System.IO.Stream str = Memory.Properties.Resources.even;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
                MessageBox.Show("Gelijkspel!");
            }
            // if the scores of player1 and player2 are not the same, announce the winner, who is the player with the most points
            else
            {
                System.IO.Stream str = Memory.Properties.Resources.win;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
                string winner = (scoreName1Tot > scoreName2Tot) ? Player1 : Player2;
                MessageBox.Show(winner + " heeft gewonnen!");
            }
        }

        /// <summary>
        /// Play a positive soundeffect
        /// </summary>
        private void PlaySoundPositive()
        {
            System.IO.Stream str = Memory.Properties.Resources.pair;
            System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
            snd.Play();
        }

        /// <summary>
        /// Play a negaive soundeffect
        /// </summary>
        private void PlaySoundNegative()
        {
            System.IO.Stream str = Memory.Properties.Resources.fail;
            System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
            snd.Play();
        }

        /// <summary>
        /// Play a soundeffect when the same card is clicked twice
        /// </summary>
        private void PlaySoundStupid()
        {
            System.IO.Stream str = Memory.Properties.Resources.huh;
            System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
            snd.Play();
        }

        /// <summary>
        /// Return the scores and playernames and put them on the scoreboard
        /// </summary>
        private void SetHighscore()
        {
            // grad the current highscores
            int OldHighscore1 = Memory.Properties.Settings.Default.highscore1;
            int OldHighscore2 = Memory.Properties.Settings.Default.highscore2;
            int OldHighscore3 = Memory.Properties.Settings.Default.highscore3;
            int OldHighscore4 = Memory.Properties.Settings.Default.highscore4;
            int OldHighscore5 = Memory.Properties.Settings.Default.highscore5;

            // for player1
            if (scoreName1Tot > OldHighscore1 && scoreName1Tot != OldHighscore1)
            {
                Memory.Properties.Settings.Default.highscore2 = Memory.Properties.Settings.Default.highscore1;
                Memory.Properties.Settings.Default.highscore3 = Memory.Properties.Settings.Default.highscore2;
                Memory.Properties.Settings.Default.highscore4 = Memory.Properties.Settings.Default.highscore3;
                Memory.Properties.Settings.Default.highscore5 = Memory.Properties.Settings.Default.highscore4;

                Memory.Properties.Settings.Default.name2 = Memory.Properties.Settings.Default.name1;
                Memory.Properties.Settings.Default.name3 = Memory.Properties.Settings.Default.name2;
                Memory.Properties.Settings.Default.name4 = Memory.Properties.Settings.Default.name3;
                Memory.Properties.Settings.Default.name5 = Memory.Properties.Settings.Default.name4;

                Memory.Properties.Settings.Default.highscore1 = scoreName1Tot;
                Memory.Properties.Settings.Default.name1 = Player1;
            }
            else if (scoreName1Tot > OldHighscore2 && scoreName1Tot != OldHighscore2)
            {
                Memory.Properties.Settings.Default.highscore3 = Memory.Properties.Settings.Default.highscore2;
                Memory.Properties.Settings.Default.highscore4 = Memory.Properties.Settings.Default.highscore3;
                Memory.Properties.Settings.Default.highscore5 = Memory.Properties.Settings.Default.highscore4;

                Memory.Properties.Settings.Default.name3 = Memory.Properties.Settings.Default.name2;
                Memory.Properties.Settings.Default.name4 = Memory.Properties.Settings.Default.name3;
                Memory.Properties.Settings.Default.name5 = Memory.Properties.Settings.Default.name4;

                Memory.Properties.Settings.Default.highscore2 = scoreName1Tot;
                Memory.Properties.Settings.Default.name2 = Player1;
            }
            else if (scoreName1Tot > OldHighscore3 && scoreName1Tot != OldHighscore3)
            {
                Memory.Properties.Settings.Default.highscore4 = Memory.Properties.Settings.Default.highscore3;
                Memory.Properties.Settings.Default.highscore5 = Memory.Properties.Settings.Default.highscore4;

                Memory.Properties.Settings.Default.name4 = Memory.Properties.Settings.Default.name3;
                Memory.Properties.Settings.Default.name5 = Memory.Properties.Settings.Default.name4;

                Memory.Properties.Settings.Default.highscore3 = scoreName1Tot;
                Memory.Properties.Settings.Default.name3 = Player1;
            }
            else if (scoreName1Tot > OldHighscore4 && scoreName1Tot != OldHighscore4)
            {
                Memory.Properties.Settings.Default.highscore5 = Memory.Properties.Settings.Default.highscore4;

                Memory.Properties.Settings.Default.name5 = Memory.Properties.Settings.Default.name4;

                Memory.Properties.Settings.Default.highscore4 = scoreName1Tot;
                Memory.Properties.Settings.Default.name4 = Player1;
            }
            else if (scoreName1Tot > OldHighscore5 && scoreName1Tot != OldHighscore5)
            {
                Memory.Properties.Settings.Default.highscore5 = scoreName1Tot;
                Memory.Properties.Settings.Default.name5 = Player1;
            }
            // for player2
            if (scoreName2Tot > OldHighscore1 && scoreName2Tot != OldHighscore1)
            {
                Memory.Properties.Settings.Default.highscore2 = Memory.Properties.Settings.Default.highscore1;
                Memory.Properties.Settings.Default.highscore3 = Memory.Properties.Settings.Default.highscore2;
                Memory.Properties.Settings.Default.highscore4 = Memory.Properties.Settings.Default.highscore3;
                Memory.Properties.Settings.Default.highscore5 = Memory.Properties.Settings.Default.highscore4;

                Memory.Properties.Settings.Default.name2 = Memory.Properties.Settings.Default.name1;
                Memory.Properties.Settings.Default.name3 = Memory.Properties.Settings.Default.name2;
                Memory.Properties.Settings.Default.name4 = Memory.Properties.Settings.Default.name3;
                Memory.Properties.Settings.Default.name5 = Memory.Properties.Settings.Default.name4;

                Memory.Properties.Settings.Default.highscore1 = scoreName2Tot;
                Memory.Properties.Settings.Default.name1 = Player2;
            }
            else if (scoreName2Tot > OldHighscore2 && scoreName2Tot != OldHighscore2)
            {
                Memory.Properties.Settings.Default.highscore3 = Memory.Properties.Settings.Default.highscore2;
                Memory.Properties.Settings.Default.highscore4 = Memory.Properties.Settings.Default.highscore3;
                Memory.Properties.Settings.Default.highscore5 = Memory.Properties.Settings.Default.highscore4;

                Memory.Properties.Settings.Default.name3 = Memory.Properties.Settings.Default.name2;
                Memory.Properties.Settings.Default.name4 = Memory.Properties.Settings.Default.name3;
                Memory.Properties.Settings.Default.name5 = Memory.Properties.Settings.Default.name4;

                Memory.Properties.Settings.Default.highscore2 = scoreName2Tot;
                Memory.Properties.Settings.Default.name2 = Player2;
            }
            else if (scoreName2Tot > OldHighscore3 && scoreName2Tot != OldHighscore3)
            {
                Memory.Properties.Settings.Default.highscore4 = Memory.Properties.Settings.Default.highscore3;
                Memory.Properties.Settings.Default.highscore5 = Memory.Properties.Settings.Default.highscore4;

                Memory.Properties.Settings.Default.name4 = Memory.Properties.Settings.Default.name3;
                Memory.Properties.Settings.Default.name5 = Memory.Properties.Settings.Default.name4;

                Memory.Properties.Settings.Default.highscore3 = scoreName2Tot;
                Memory.Properties.Settings.Default.name3 = Player2;
            }
            else if (scoreName2Tot > OldHighscore4 && scoreName2Tot != OldHighscore4)
            {
                Memory.Properties.Settings.Default.highscore5 = Memory.Properties.Settings.Default.highscore4;

                Memory.Properties.Settings.Default.name5 = Memory.Properties.Settings.Default.name4;

                Memory.Properties.Settings.Default.highscore4 = scoreName2Tot;
                Memory.Properties.Settings.Default.name4 = Player2;
            }
            else if (scoreName2Tot > OldHighscore5 && scoreName2Tot != OldHighscore5)
            {
                Memory.Properties.Settings.Default.highscore5 = scoreName2Tot;
                Memory.Properties.Settings.Default.name5 = Player2;
            }
            Memory.Properties.Settings.Default.Save();
            Memory.Properties.Settings.Default.Reload();
        }
    }
}
