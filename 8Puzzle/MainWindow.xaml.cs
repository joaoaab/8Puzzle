using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Threading;
using _8Puzzle;

namespace _8Puzzle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Puzzle game;
        public Solver inteligence;
        public MainWindow()
        {
            InitializeComponent();
            game = new Puzzle();
            if (CompletedLabel.Text == "Not Completed :/")
            {
                if (game.IsComplete())
                {
                    CompletedLabel.Text = "Completed :)";
                }
            }
        }

        /// <summary>
        /// It shuffles the vector so it creates a puzzle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShuffleNumbers(object sender, RoutedEventArgs e)
        {
            Shuffles();
        }

        private void Shuffles()
        {
            game.Shuffle();
            Console.WriteLine(game.GetBoardString());
            /// Trying another Shuffle if the board is not solvable
            while (!game.IsSolvable())
            {
                game.Shuffle();
                Console.WriteLine("Not Solvable, Getting Another Board");
                Console.WriteLine(game.GetBoardString());
            }

            int[] gameBoard = game.GetBoard();
            UpdateBoard(gameBoard);
            WinText();
            game.SetMoveCount(0);
            UpdateCount();
        }

        /// <summary>
        /// Re-evaluates the win condition
        /// </summary>
        private void WinText()
        {
            if (CompletedLabel.Text == "Completed :)")
            {
                if (!game.IsComplete())
                {
                    CompletedLabel.Text = "Not Completed :/";
                    CompletedLabel.Foreground = Brushes.Red;
                }
            }
            else
            {
                if (game.IsComplete())
                {
                    CompletedLabel.Text = "Completed :)";
                    CompletedLabel.Foreground = Brushes.Green;
                }
            }
        }

        /// Updates the Buttons with the numbers coming from the board
        public void UpdateBoard(int[] gameBoard)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.game.SetBoard(gameBoard);
                foreach (UIElement element in boardGrid.Children)
                {
                    Button btn = (Button)element;
                    ///Gets the gameBoard index to update from the button's name
                    int temp = gameBoard[(int)Char.GetNumericValue(btn.Name[btn.Name.Length - 1]) - 1]; ;
                    if (temp == 0)
                    {
                        btn.Content = "";
                    }
                    else
                    {
                        btn.Content = temp;
                    }
                }
                WinText();
                UpdateCount();
            });
            
        }

        public void UpdateCount()
        {
            MoveCountLabel.Text = "Number of Movements Made: " + game.GetMoveCount();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if(Button1.Content.ToString() != "")
            {
                game.Move(0);
                UpdateBoard(game.GetBoard());
                UpdateCount();
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (Button2.Content.ToString() != "")
            {
                game.Move(1);
                UpdateBoard(game.GetBoard());
                UpdateCount();
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (Button3.Content.ToString() != "")
            {
                game.Move(2);
                UpdateBoard(game.GetBoard());
                UpdateCount();
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            if (Button4.Content.ToString() != "")
            {
                game.Move(3);
                UpdateBoard(game.GetBoard());
                UpdateCount();
            }
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            if (Button5.Content.ToString() != "")
            {
                game.Move(4);
                UpdateBoard(game.GetBoard());
                UpdateCount();
            }
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            if (Button6.Content.ToString() != "")
            {
                game.Move(5);
                UpdateBoard(game.GetBoard());
                UpdateCount();
            }
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            if (Button7.Content.ToString() != "")
            {
                game.Move(6);
                UpdateBoard(game.GetBoard());
                UpdateCount();
            }
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            if (Button8.Content.ToString() != "")
            {
                game.Move(7);
                UpdateBoard(game.GetBoard());
                UpdateCount();
            }
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            if (Button9.Content.ToString() != "")
            {
                game.Move(8);
                UpdateBoard(game.GetBoard());
                UpdateCount();
            }
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            if (game.IsSolvable())
            {
                MainWindow myWindow = Window.GetWindow(this) as MainWindow;
                inteligence = new Solver(game.GetBoard(), myWindow, game);
                game.SetMoveCount(0);
                Thread th;
                switch (AIPicker.SelectedIndex)
                {
                    case 0:
                        th = new Thread(inteligence.BFS);
                        th.Start();
                        break;

                    case 1:
                        th = new Thread(inteligence.AStar);
                        th.Start();
                        break;

                    case 2:
                        th = new Thread(inteligence.BestFirstSearch);
                        th.Start();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Board not solvable");
            }
        }


        /// <summary>
        /// Updates the Board with the values in the text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetBoardButton_Click(object sender, RoutedEventArgs e)
        {
            int[] newBoard = new int[9];
            foreach(TextBox element in GridSetter.Children.OfType<TextBox>())
            {
                TextBox temp = (TextBox)element;
                if((int)Char.GetNumericValue(temp.Name[temp.Name.Length - 1]) == 0)
                {
                    if(temp.Text != "")
                    {
                        newBoard[8] = (int)Char.GetNumericValue(temp.Text[0]);
                    }
                }
                else if(temp.Text != "")
                {
                    newBoard[(int)Char.GetNumericValue(temp.Name[temp.Name.Length - 1]) - 1] = (int)Char.GetNumericValue(temp.Text[0]);
                }
                else { break; }
            }
            game.SetBoard(newBoard);
            UpdateBoard(game.GetBoard());
        }

        private void Setter1_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            foreach (TextBox element in GridSetter.Children.OfType<TextBox>())
            {
                TextBox temp = (TextBox)element;
                temp.Text = "";
            }
        }
    }
}
