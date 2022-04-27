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
using System.Windows.Threading;


namespace GameCheckers
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Checkers game;
        DispatcherTimer timer = new DispatcherTimer();
        int tp;
        public MainWindow()
        {
            
            InitializeComponent();
            CreateGameBoard();
            game = new Checkers(grid);
            tp = 3000;

           
            timer.Interval = TimeSpan.FromMilliseconds(tp);
           
            timer.Tick += Timer_Tick;
            timer.IsEnabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int p = game.exam();
            if (p == 1)
            {
                MessageBox.Show("WIN Player2");
                this.Close();
            }
            if (p == 2)
            {
                MessageBox.Show("WIN Player1");
                this.Close();
            }

        }

        private void CreateGameBoard()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = 50;
                    rectangle.Height = 50;
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    if (i % 2 == 1)
                    {
                        if (j % 2 == 0)
                        {
                            mySolidColorBrush.Color = Color.FromRgb(1, 1, 1);
                            rectangle.Fill = mySolidColorBrush;
                        }
                        else
                        {
                            mySolidColorBrush.Color = Color.FromArgb(239, 228, 206, 80);
                            rectangle.Fill = mySolidColorBrush;
                        }
                    }
                    else
                    {
                        if (j % 2 == 1)
                        {
                            mySolidColorBrush.Color = Color.FromRgb(1, 1, 1);
                            rectangle.Fill = mySolidColorBrush;
                        }
                        else
                        {
                            mySolidColorBrush.Color = Color.FromArgb(239, 228, 206, 80);
                            rectangle.Fill = mySolidColorBrush;
                        }
                    }
                    rectangle.HorizontalAlignment = HorizontalAlignment.Left;
                    rectangle.VerticalAlignment = VerticalAlignment.Top;

                    int x = (int)(30 + rectangle.Width * j);
                    int y = (int)(10 + rectangle.Height * i);

                    rectangle.Margin = new Thickness(x, y, 0, 0);

                    grid.Children.Add(rectangle);

                }
        }
        private void grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        

       

        private void Button_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            int p = game.counnt();
            if (p == 1)
            {
                MessageBox.Show("WIN Player2");
                this.Close();
            }
            if (p == 2)
            {
                MessageBox.Show("WIN Player1");
                this.Close();
            }
        }
    }
}
