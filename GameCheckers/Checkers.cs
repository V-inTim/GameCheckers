using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;


namespace GameCheckers
{
    
    class Checkers
    {
        List<Ellipse> chips1;
        List<Ellipse> chips2;
        List<Ellipse> points;
        
        int[,] array1;
        int[,] arrayVr;
        int counter, typer, x,y;
       // int[,] p;
        System.Windows.Controls.Grid grid1;
        public Checkers(System.Windows.Controls.Grid grid)
        {
            
            grid1 = grid;
            counter = 1;
            typer = 1; x = -1;y = -1;
           // p = new int[13, 2];
            array1 = new int[8, 8];
            arrayVr = new int[12, 2];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    array1[i, j] = -1;
            for (int j=0; j<4; j++)
                array1[3, j*2] = 0;
            for (int j = 0; j < 4; j++)
                array1[4, j * 2+1] = 0;

            chips1 = new List<Ellipse>();
            chips2 = new List<Ellipse>();
            points = new List<Ellipse>();
            for (int i = 0; i < 12; i++)
            {
                Ellipse chip= new Ellipse();
                chip.Width = 50;
                chip.Height = 50;

                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromRgb(240, 240, 210);
                chip.Fill = mySolidColorBrush;

                chip.HorizontalAlignment = HorizontalAlignment.Left;
                chip.VerticalAlignment = VerticalAlignment.Top;
                chip.MouseDown += Ellipse_MouseDown;


                int x0=0, y0=0;

                if (i < 4)
                {
                    x0 = (1 + 2 * i) * 50 + 30;
                    y0 = 10;
                    array1[ 0, 1 + 2 * i] = 1;
                }
                if (4 <= i && i < 8)
                {
                    x0 = (i - 4) * 2 * 50 + 30;
                    y0 = 60;
                    array1[ 1, (i - 4) * 2] = 1;
                }
                if (i >= 8)
                {
                    x0 = ((i - 8) * 2 + 1) * 50 + 30;
                    y0 = 110;
                    array1[2, (i - 8) * 2 + 1] = 1;
                }
                chip.Margin = new Thickness(x0, y0, 0, 0);
                chips1.Add(chip);

            }
            for (int i = 0; i < 12; i++)
            {
                Ellipse chip = new Ellipse();
                chip.Width = 50;
                chip.Height = 50;

                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                 mySolidColorBrush.Color = Color.FromRgb(240, 76, 69);
                chip.Fill = mySolidColorBrush;

                chip.HorizontalAlignment = HorizontalAlignment.Left;
                chip.VerticalAlignment = VerticalAlignment.Top;
                chip.MouseDown += Ellipse_MouseDown;

                //  mySolidColorBrush.Color = Color.FromRgb(240, 76, 69);
                int x0 = 0, y0 = 0;
                
                if (i < 4)
                {
                    x0 = i*2 * 50 + 30;
                    y0 = 10 + 5 * 50;
                    array1[ 5, i * 2] = 2;
                }
                if (4 <= i && i < 8)
                {
                    x0 = ((i - 4) * 2 + 1) * 50 + 30;
                    y0 = 10+6*50;
                    array1[ 6, (i - 4) * 2 + 1] = 2;
                }
                if (i >= 8)
                {
                    x0 = (i - 8) * 2 * 50 + 30;
                    y0 = 10+7*50;
                    array1[7, (i - 8) * 2] = 2;
                }
                chip.Margin = new Thickness(x0, y0, 0, 0);
                chips2.Add(chip);
            }

            for (int i=0; i < 12; i++)
            {
                grid.Children.Add(chips1[i]);
            }
            for (int i = 0; i < 12; i++)
            {
                grid.Children.Add(chips2[i]);
            }
            
            
        }
        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < points.Count; i++)
            {
                grid1.Children.Remove(points[i]);
            }
            points.Clear();
            for(int i = 0; i < chips1.Count; i++)
            {
                DropShadowEffect drop = new DropShadowEffect();
                drop.Color = Colors.Black;
                drop.BlurRadius = 0;
                drop.ShadowDepth = 0;
                chips1[i].Effect = drop;
            }
            for (int i = 0; i < chips2.Count; i++)
            {
                DropShadowEffect drop = new DropShadowEffect();
                drop.Color = Colors.Black;
                drop.BlurRadius = 0;
                drop.ShadowDepth = 0;
                chips2[i].Effect = drop;
            }
            if (typer == 1)
                if (counter % 2 == 1)
                    Verific_(1);
                else
                    Verific_(2);


            Ellipse rect = (Ellipse)sender;
            if (array1[(int)((rect.Margin.Top - 10) / 50), (int)((rect.Margin.Left - 30) / 50)] % 2 == counter % 2)
            {
                DropShadowEffect drop = new DropShadowEffect();
                drop.Color = Colors.Orange;
                drop.BlurRadius = 10;
                drop.ShadowDepth = 3;
                rect.Effect = drop;
                if (typer == 1)
                {

                    CommandIn((int)((rect.Margin.Left - 30) / 50), (int)((rect.Margin.Top - 10) / 50));
                }
                else
                {
                    int i = 0;
                    while (arrayVr[i, 0] != -1)
                    {

                        if (((int)(rect.Margin.Left) - 30) / 50 == arrayVr[i, 0] && ((int)(rect.Margin.Top) - 10) / 50 == arrayVr[i, 1])
                        {

                            Eating(arrayVr[i, 0], arrayVr[i, 1]);
                        }

                        i++;
                    }

                }
            }
        }

        private void Eating(int _x, int _y)
        {
            x = _x; y = _y;
           
            if (array1[_y,_x]==1|| array1[_y, _x] == 2)
            {
               
                if ((_y + 2 < 8 && _x + 2 < 8) &&
                           (array1[_y + 1, _x + 1] != array1[_y, _x] && array1[_y + 1, _x + 1] != array1[_y, _x] + 2 &&
                            array1[_y + 1, _x + 1] != 0) && array1[_y + 2, _x + 2] == 0)
                    createPoint(_x + 2, _y + 2);
                if ((_y + 2 < 8 && _x - 2 >= 0) &&
                  (array1[_y + 1, _x - 1] != array1[_y, _x] && array1[_y + 1, _x - 1] != array1[_y, _x] + 2 &&
                   array1[_y + 1, _x - 1] != 0) && array1[_y + 2, _x - 2] == 0)
                { createPoint(_x - 2, _y + 2);  }
                if ((_x + 2 < 8 && _y - 2 >= 0) &&
                  (array1[_y - 1, _x + 1] != array1[_y, _x] && array1[_y - 1, _x + 1] != array1[_y, _x] + 2 &&
                   array1[_y - 1, _x + 1] != 0) && array1[_y - 2, _x + 2] == 0)
                    createPoint(_x + 2, _y - 2);
                if ((_x - 2 >= 0 && _y - 2 >= 0) &&
                  (array1[_y - 1, _x - 1] != array1[_y, _x] && array1[_y - 1, _x - 1] != array1[_y, _x] + 2 &&
                   array1[_y - 1, _x - 1] != 0) && array1[_y - 2, _x - 2] == 0)
                    createPoint(_x - 2, _y - 2);
            }
            if (array1[_y, _x] == 3 || array1[_y, _x] == 4)
            {
                int prj = _x + 1, pri = _y + 1;
                while (prj < 7 && pri < 7)
                {
                    if (array1[pri, prj] == array1[_y, _x] || array1[pri, prj] == array1[_y, _x] + 2)
                        break;
                    if (array1[pri, prj] != 0 && array1[pri + 1, prj + 1] == 0)
                    {
                        createPoint(prj + 1, pri + 1);
                        break;
                    }
                    else if (array1[pri, prj] != 0 && array1[pri + 1, prj + 1] != 0) break;
                        prj++; pri++; 
                }
                prj = _x + 1; pri = _y - 1;
                    while (prj < 7 && pri > 0)
                    {
                        if (array1[pri, prj] == array1[_y, _x] || array1[pri, prj] == array1[_y, _x] + 2)
                            break;
                    if (array1[pri, prj] != 0 && array1[pri - 1, prj + 1] == 0)
                    {
                        createPoint(prj + 1, pri - 1);
                        break;
                    }
                    else if (array1[pri, prj] != 0 && array1[pri - 1, prj + 1] != 0) break;
                        prj++; pri--;
                    }
                prj = _x - 1; pri = _y - 1;
               
                    while (prj > 0 && pri > 0)
                    {
                        if (array1[pri, prj] == array1[_y, _x] || array1[pri, prj] == array1[_y, _x] + 2)
                            break;
                    if (array1[pri, prj] != 0 && array1[pri - 1, prj - 1] == 0)
                    {
                        createPoint(prj - 1, pri - 1);
                        break;
                    }
                    else if (array1[pri, prj] != 0 && array1[pri - 1, prj - 1] != 0) break;
                        prj--; pri--; 
                    }
                prj = _x - 1; pri = _y + 1;
                
                    while (prj > 0 && pri < 7)
                    {
                        if (array1[pri, prj] == array1[_y, _x] || array1[pri, prj] == array1[_y, _x] + 2)
                            break;
                    if (array1[pri, prj] != 0 && array1[pri + 1, prj - 1] == 0)
                    {
                        createPoint(prj - 1, pri + 1);
                        break;
                    }
                    else if (array1[pri, prj] != 0 && array1[pri + 1, prj - 1] != 0) break;
                        prj--; pri++; 
                    }
            }
        }
      
         void Verific_(int player)
        {
            int k = 0;
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 2; j++)
                    arrayVr[i, j] = -1;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    int d = 0;
                    if (array1[i, j] == player)
                    {
                        if ((i + 2 < 8 && j + 2 < 8) &&
                           (array1[i + 1, j + 1] != player && array1[i + 1, j + 1] != player + 2 &&
                            array1[i + 1, j + 1] != 0) && array1[i + 2, j + 2] == 0)
                        {
                            arrayVr[k, 0] = j;
                            arrayVr[k++, 1] = i;
                            d = 1;
                        }
                        if (d == 0 && (i + 2 < 8 && j - 2 >= 0) &&
                          (array1[i + 1, j - 1] != player && array1[i + 1, j - 1] != player + 2 &&
                           array1[i + 1, j - 1] != 0) && array1[i + 2, j - 2] == 0)
                        {
                            arrayVr[k, 0] = j;
                            arrayVr[k++, 1] = i;
                            d = 1;
                        }
                        if (d == 0 && (j + 2 < 8 && i - 2 >= 0) &&
                          (array1[i - 1, j + 1] != player && array1[i - 1, j + 1] != player + 2 &&
                           array1[i - 1, j + 1] != 0) && array1[i - 2, j + 2] == 0)
                        {
                            arrayVr[k, 0] = j;
                            arrayVr[k++, 1] = i;
                            d = 1;
                        }
                        if (d == 0 && (j - 2 >=0 && i - 2 >= 0) &&
                          (array1[i - 1, j - 1] != player && array1[i - 1, j - 1] != player + 2 &&
                           array1[i - 1, j - 1] != 0) && array1[i - 2, j - 2] == 0)
                        {
                            arrayVr[k, 0] = j;
                            arrayVr[k++, 1] = i;

                        }
                    }
                    if (array1[i, j] == player + 2)
                    {
                        int prj = j + 1, pri = i + 1;
                        while (prj < 7 && pri < 7)
                        {
                            if (array1[pri, prj] == player || array1[pri, prj] == player + 2)
                                break;
                            if (array1[pri, prj] != 0 && array1[pri + 1, prj + 1] == 0)
                            {
                                arrayVr[k, 0] = j;
                                arrayVr[k, 1] = i;
                                d = 1; k++;
                                break;
                            }
                            else if (array1[pri, prj] != 0 && array1[pri + 1, prj + 1] != 0) break;
                                prj++; pri++;
                        }
                        prj = j + 1; pri = i - 1;
                        if (d == 0)
                            while (prj < 7 && pri > 0)
                            {
                                if (array1[pri, prj] == player || array1[pri, prj] == player + 2)
                                    break;
                                if (array1[pri, prj] != 0 && array1[pri - 1, prj + 1] == 0)
                                {
                                    arrayVr[k, 0] = j;
                                    arrayVr[k, 1] = i;
                                    d = 1; k++;
                                    break;
                                }
                                else if (array1[pri, prj] != 0 && array1[pri - 1, prj + 1] != 0) break;
                                    prj++; pri--; 
                            }
                        prj = j - 1; pri = i - 1;
                        if (d == 0)
                            while (prj > 0 && pri > 0)
                            {
                                if (array1[pri, prj] == player || array1[pri, prj] == player + 2)
                                    break;
                                if (array1[pri, prj] != 0 && array1[pri - 1, prj - 1] == 0)
                                {
                                    arrayVr[k, 0] = j;
                                    arrayVr[k, 1] = i;
                                    d = 1; k++;
                                    break;
                                }
                                else if (array1[pri, prj] != 0 && array1[pri - 1, prj - 1] != 0) break;
                                    prj--; pri--; 
                            }
                        prj = j - 1; pri = i + 1;
                        if (d == 0)
                            while (prj > 0 && pri < 7)
                            {
                                if (array1[pri, prj] == player || array1[pri, prj] == player + 2)
                                    break;
                                if (array1[pri, prj] != 0 && array1[pri + 1, prj - 1] == 0)
                                {
                                    arrayVr[k, 0] = j;
                                    arrayVr[k, 1] = i;
                                    d = 1; k++;
                                    break;
                                }
                                else 
                                  if (array1[pri, prj] != 0 && array1[pri + 1, prj - 1] != 0)
                                    break;
                                prj--; pri++; 
                            }
                    }
                }
            if (arrayVr[0, 0] != -1)
                typer = 2;
            else
                typer = 1;
        }
        void CommandIn(int _x, int _y)
        {
            x = _x; y = _y;
      
            if (counter%2== 1)
            {
                
                if (array1[_y,_x] == 1)
                {
                    
                    if (_y + 1 < 8 && _x - 1 >= 0 && array1[_y + 1, _x - 1] == 0)
                        createPoint(_x - 1, _y + 1);
                    if (_y + 1 < 8 && _x + 1 < 8 && array1[_y + 1, _x + 1] == 0)
                        createPoint(_x + 1, _y + 1);
                }
                if (array1[_y, _x] == 3)
                {
                    int prj = _x + 1, pri = _y + 1;
                    while (prj < 8 || pri < 8)
                    {
                        if (array1[pri, prj] == 0)
                            createPoint(prj, pri);
                        else break;
                        prj++; pri++;
                    }

                    prj = _x + 1; pri = _y - 1;
                    while (prj < 8 || pri >= 0)
                    {
                        if (array1[pri, prj] == 0)
                            createPoint(prj, pri);
                        else break;
                        prj++; pri--;
                    }

                    prj = _x - 1; pri = _y - 1;
                    while (prj >= 0 || pri >= 0)
                    {
                        if (array1[pri, prj] == 0)
                            createPoint(prj, pri);
                        else break;
                        prj--; pri--;
                    }

                    prj = _x - 1; pri = _y + 1;
                    while (prj >= 0 || pri < 8)
                    {
                        if (array1[pri, prj] == 0)
                            createPoint(prj, pri);
                        else break;
                        prj--; pri++;
                    }
                }
              

            }

            if (counter%2 == 0)
            {
                
                if (array1[_y, _x] == 2)
                {
                   
                    if (_y - 1 >= 0 && _x - 1 >= 0 && array1[_y - 1, _x - 1] == 0)
                        createPoint(_x - 1, _y - 1);
                    if (_y - 1 >= 0 && _x + 1 < 8 && array1[_y - 1, _x + 1] == 0)
                        createPoint(_x + 1, _y - 1);
                }
                if (array1[_y, _x] == 4)
                {
                    int prj = _x + 1, pri = _y + 1;
                    while (prj < 8 && pri < 8)
                    {
                        if (array1[pri, prj] == 0)
                            createPoint(prj, pri);
                        else break;
                        prj++; pri++;
                    }

                    prj = _x + 1; pri = _y - 1;
                    while (prj < 8 && pri >= 0)
                    {
                        if (array1[pri, prj] == 0)
                            createPoint(prj, pri);
                        else break;
                        prj++; pri--;
                    }

                    prj = _x - 1; pri = _y - 1;
                    while (prj >= 0 && pri >= 0)
                    {
                        if (array1[pri, prj] == 0)
                            createPoint(prj, pri);
                        else break;
                        prj--; pri--;
                    }

                    prj = _x - 1; pri = _y + 1;
                    while (prj >= 0 && pri < 8)
                    {
                        if (array1[pri, prj] == 0)
                            createPoint(prj, pri);
                        else break;
                        prj--; pri++;
                    }
                }
                
                    
            }
            
        }
        private void createPoint(int _x, int _y)
        {
            Ellipse chip = new Ellipse();
            chip.Width = 50;
            chip.Height = 50;

            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromRgb(3, 20, 30);
            chip.Fill = mySolidColorBrush;
           
            chip.HorizontalAlignment = HorizontalAlignment.Left;
            chip.VerticalAlignment = VerticalAlignment.Top;
            
            chip.MouseDown += Ellipse1_MouseDown;
           
            chip.Margin = new Thickness( _x*50+30,_y*50+10, 0, 0);
            points.Add(chip);
            grid1.Children.Add(points[points.Count-1]);

        }
        private void Ellipse1_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            for (int i = 0; i < points.Count; i++)
            {
                grid1.Children.Remove(points[i]);
            }
            points.Clear();
           
            Ellipse rect = (Ellipse)sender;
            if(typer==1)
              Move((int)((rect.Margin.Left - 30) / 50), (int)((rect.Margin.Top - 10) / 50));
            else
            {
                if((rect.Margin.Left - 30) / 50 - x>0 && (rect.Margin.Top - 10) / 50 -y > 0)
                Eat((int)(rect.Margin.Left - 30) / 50-1 , (int)(rect.Margin.Top - 10) / 50-1  );
                if ((rect.Margin.Left - 30) / 50 - x < 0 && (rect.Margin.Top - 10) / 50 - y > 0)
                    Eat((int)(rect.Margin.Left - 30) / 50 + 1, (int)(rect.Margin.Top - 10) / 50 - 1 );
                if ((rect.Margin.Left - 30) / 50 - x > 0 && (rect.Margin.Top - 10) / 50 - y < 0)
                    Eat((int)(rect.Margin.Left - 30) / 50 -1, (int)(rect.Margin.Top - 10) / 50 + 1);
                if ((rect.Margin.Left - 30) / 50 - x < 0 && (rect.Margin.Top - 10) / 50 - y < 0)
                    Eat((int)(rect.Margin.Left - 30) / 50 + 1, (int)(rect.Margin.Top - 10) / 50 + 1 );
                Move((int)((rect.Margin.Left - 30) / 50), (int)((rect.Margin.Top - 10) / 50));
               
            }
        }

        private void Eat(int _x, int _y)
        {
            
            if (counter % 2 == 0) {
                for (int i = 0; i < chips1.Count; i++)
                    if (chips1[i].Margin.Left == _x * 50 + 30 && chips1[i].Margin.Top == _y * 50 + 10)
                    {
                        grid1.Children.Remove(chips1[i]);
                        chips1.RemoveAt(i);
                        array1[_y, _x] = 0;
                    } }
            else
                for (int i = 0; i < chips2.Count; i++)
                    if (chips2[i].Margin.Left == _x * 50 + 30 && chips2[i].Margin.Top == _y * 50 + 10)
                    {
                        grid1.Children.Remove(chips2[i]);
                        chips2.RemoveAt(i);
                        array1[_y, _x] = 0;
                    }

        }

        private void Move(int _x, int _y)
        {
            DropShadowEffect drop = new DropShadowEffect();
            drop.Color = Colors.Black;
            drop.BlurRadius = 0;
            drop.ShadowDepth = 0;
            if (counter % 2 == 0)
                for (int i = 0; i < chips2.Count; i++)
                {
                    if (chips2[i].Margin.Left == x * 50 + 30 && chips2[i].Margin.Top == y * 50 + 10)
                    {
                        chips2[i].Effect = drop;
                        break;
                    }
                }
            else
                for (int i = 0; i < chips1.Count; i++)
                {
                    if (chips1[i].Margin.Left == x * 50 + 30 && chips1[i].Margin.Top == y * 50 + 10)
                    {
                        chips1[i].Effect = drop;
                        break;
                    }
                }
            if (counter % 2 == 1)
            {
                for (int i = 0; i < chips1.Count; i++)
                    if ((int)chips1[i].Margin.Left == x * 50 + 30 && (int)chips1[i].Margin.Top == y * 50 + 10)
                    {
                        int t;

                        chips1[i].Margin = new Thickness(_x * 50 + 30, _y * 50 + 10, 0, 0);
                        t = array1[y, x];
                        array1[y, x] = array1[_y, _x];
                        array1[_y, _x] = t;
                    }
            }
            else
                for (int i = 0; i < chips2.Count; i++)
                    if ((int)chips2[i].Margin.Left == x * 50 + 30 && (int)chips2[i].Margin.Top == y * 50 + 10)
                    {
                        int t;
                        chips2[i].Margin = new Thickness(_x * 50 + 30, _y * 50 + 10, 0, 0);
                        t = array1[y, x];
                        array1[y, x] = array1[_y, _x];
                        array1[_y, _x] = t;
                    }
            if (typer==1)
            counter++;
            else
            {
                if (counter % 2 == 1)
                    Verific_(1);
                else
                    Verific_(2);
                int i = 0, d=0;
                while (arrayVr[i, 0] != -1)
                {
                    if (_x == arrayVr[i, 0] && _y == arrayVr[i, 1]) d = 1;
                    i++;
                }
                if (d == 0) { counter++; typer=1; }
            }
            Lady();
        }

        private void Lady()
        {
            for(int i=0; i<8; i++)
                if (array1[0, i] == 2)
                {
                    array1[0, i] = 4;
                    for (int j = 0; j < chips2.Count; j++)
                     if(chips2[j].Margin.Left==30+50*i && chips2[j].Margin.Top == 10)
                        {
                            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                            mySolidColorBrush.Color = Color.FromRgb(153, 36, 0);
                            chips2[j].Fill = mySolidColorBrush;
                        }
                }
            for (int i = 0; i < 8; i++)
                if (array1[7, i] == 1)
                {
                    array1[0, i] = 3;
                    for (int j = 0; j < chips1.Count; j++)
                        if (chips1[j].Margin.Left == 30 + 50 * i && chips1[j].Margin.Top == 10+7*50)
                        {
                            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                            mySolidColorBrush.Color = Color.FromRgb(138, 104, 47);
                            chips1[j].Fill = mySolidColorBrush;
                        }
                }

        }
        public int exam()
        {
            if (chips1.Count==0)
            {
                return 1;
            }
            if (chips2.Count == 0)
            {
                return 2;
            }
            return 0;
        }
        public int counnt()
        {
            if (counter % 2 == 1)
                return 1;
            else
                return 2;
        }
    }
}
