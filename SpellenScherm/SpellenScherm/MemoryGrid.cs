using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpellenScherm
{
    public class MemoryGrid
    {
        private Grid grid;
        private int rows, cols;
        static int numberOfClicks = 0;
        static int score;
        private bool hasDelay;
        private Image card;
        private Image Image1;
        private Image Image2;

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
                    Image back = new Image();
                    back.Source = new BitmapImage(new Uri("/images/back.png", UriKind.Relative));

                    back.MouseDown += new System.Windows.Input.MouseButtonEventHandler(CardClick);

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
            List<ImageSource> images = new List<ImageSource>();
            List<string> random1 = new List<string>();
            List<string> random2 = new List<string>();

            for (int i = 0; i < 16; i++)
            {
                if (i < 8)
                {
                    int imageNR = 0;

                    Random rnd = new Random();
                    imageNR = rnd.Next(1, 9);
                    if (random1.Contains(Convert.ToString(imageNR)))
                    {
                        i--;
                    }
                    else
                    {
                        random1.Add(Convert.ToString(imageNR));
                        ImageSource source = new BitmapImage(new Uri("images/" + imageNR + ".png", UriKind.Relative));
                        images.Add(source);
                    }
                }
                if (i >= 8)
                {
                    int imageNR = 0;

                    Random rnd = new Random();
                    imageNR = rnd.Next(1, 9);
                    if (random2.Contains(Convert.ToString(imageNR)))
                    {
                        i--;
                    }
                    else
                    {
                        random2.Add(Convert.ToString(imageNR));
                        ImageSource source = new BitmapImage(new Uri("images/" + imageNR + ".png", UriKind.Relative));
                        images.Add(source);
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
            if (hasDelay) return;

            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            card.Source = front;
            numberOfClicks++;

            checkCards(card);
        }

        /// <summary>
        /// Grab the clicked card
        /// </summary>
        /// <param name="card">The card that has been clicked</param>
        private void checkCards(Image card)
        {

            this.card = card;
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

            if (numberOfClicks == 2)
            {
                checkPair(Image1, Image2);

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
        public void checkPair(Image card1, Image card2)
        {
            this.Image1 = card1;
            this.Image2 = card2;

            if (Convert.ToString(card1.Source) == Convert.ToString(card2.Source) && (card1 != card2))
            {
                getPoint(card1, card2);
            }
            else
            {
                resetCards(Image1, Image2);
            }

        }

        /// <summary>
        /// Give points and remove pairs from GameGrid
        /// </summary>
        /// <param name="card1">The first card that has been clicked</param>
        /// <param name="card2">The second card that has been clicked</param>
        private async void getPoint(Image card1, Image card2)
        {
            score++;
            hasDelay = true;
            await Task.Delay(300);

            card1.Source = new BitmapImage(new Uri("", UriKind.Relative));
            card2.Source = new BitmapImage(new Uri("", UriKind.Relative));

            hasDelay = false;
        }

        /// <summary>
        /// Reset the cards
        /// </summary>
        /// <param name="card1">The first card that has been clicked</param>
        /// <param name="card2">The second card that has been clicked</param>
        private async void resetCards(Image card1, Image card2)
        {
            this.Image1 = card1;
            this.Image2 = card2;

            hasDelay = true;
            await Task.Delay(1000);


            card1.Source = new BitmapImage(new Uri("/images/back.png", UriKind.Relative));
            card2.Source = new BitmapImage(new Uri("/images/back.png", UriKind.Relative));
            hasDelay = false;
        }
    }
}
