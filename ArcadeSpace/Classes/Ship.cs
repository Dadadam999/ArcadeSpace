using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ArcadeSpace
{
    public class Ship : Image
    {
        public List<CollaiderRect> collaiders { get; set; }
        double resizeK;

        public int Damage { get; set; }
        public double Speed { get; set; }

        public Ship(double WidthGameWindow, double HeightGameWindow, ref Canvas GameSpace)
        {
            Damage = 2;
            Speed = 10;

            Source = new BitmapImage(new Uri(@"pack://application:,,,/Resurces/ship1.png"));

            resize(WidthGameWindow);

            GameSpace.Children.Add(this);
            Canvas.SetTop(this, HeightGameWindow / 2 + ActualHeight / 2);
            Canvas.SetLeft(this, 0);
            Canvas.SetZIndex(this, 997);

            collaiders = new List<CollaiderRect>();
            collaiders.Add(new CollaiderRect());
            collaiders.Add(new CollaiderRect());
            update_collaider();
        }

        public void resize(double WidthGameWindow) {
            resizeK = ActualWidth / ActualHeight;
            Width = WidthGameWindow / 8;
            Height = ActualWidth / resizeK;
        }
        public void move(double WidthGameWindow, double HeightGameWindow,string key_down) {
            if (key_down == "W")
            {
                if (Canvas.GetTop(this) >= 0)
                    Canvas.SetTop(this, Canvas.GetTop(this) - Speed);
                else
                    Canvas.SetTop(this, 0);
            }
            if (key_down == "A")
            {
                if (Canvas.GetLeft(this) >= 0)
                    Canvas.SetLeft(this, Canvas.GetLeft(this) - Speed);
                else
                    Canvas.SetLeft(this, 0);
            }
            if (key_down == "S")
            {
                if (Canvas.GetTop(this) <= HeightGameWindow - ActualHeight)
                    Canvas.SetTop(this, Canvas.GetTop(this) + Speed);
                else
                    Canvas.SetTop(this, HeightGameWindow - ActualHeight);
            }
            if (key_down == "D")
            {
                if (Canvas.GetLeft(this) <= WidthGameWindow - ActualWidth)
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + Speed);
                else
                    Canvas.SetLeft(this, WidthGameWindow - ActualWidth);
            }
        }
        public void update_collaider()
        {
            collaiders[0].Width = ActualWidth * 0.5;
            collaiders[0].Height = ActualHeight * 0.4;
            collaiders[0].X = Canvas.GetLeft(this) + ActualWidth * 0.34;
            collaiders[0].Y = Canvas.GetTop(this) + ActualHeight * 0.2;

            collaiders[1].Width = ActualWidth * 0.2;
            collaiders[1].Height = ActualHeight * 0.9;
            collaiders[1].X = Canvas.GetLeft(this) + ActualWidth * 0.3;
            collaiders[1].Y = Canvas.GetTop(this) + ActualWidth * 0.01;
        }

        #region view_collision
        //public Rectangle CollaiderView1 { get; set; }
        //public Rectangle CollaiderView2 { get; set; }
        //public void InitCollaiderView(ref Canvas GameSpace)
        //{
        //    CollaiderView1 = new Rectangle();
        //    CollaiderView1.Fill = Brushes.Green;
        //    GameSpace.Children.Add(CollaiderView1);

        //    CollaiderView2 = new Rectangle();
        //    CollaiderView2.Fill = Brushes.Green;
        //    GameSpace.Children.Add(CollaiderView2);

        //    MoveCollaiderView();
        //}
        //public void MoveCollaiderView()
        //{
        //    CollaiderView1.Width = collaiders[0].Width;
        //    CollaiderView1.Height = collaiders[0].Height;
        //    Canvas.SetLeft(CollaiderView1, collaiders[0].X);
        //    Canvas.SetTop(CollaiderView1, collaiders[0].Y);

        //    CollaiderView2.Width = collaiders[1].Width;
        //    CollaiderView2.Height = collaiders[1].Height;
        //    Canvas.SetLeft(CollaiderView2, collaiders[1].X);
        //    Canvas.SetTop(CollaiderView2, collaiders[1].Y);
        //}
        //public void RemoveCollaiderView(ref Canvas GameSpace)
        //{
        //    GameSpace.Children.Remove(CollaiderView1);
        //    GameSpace.Children.Remove(CollaiderView2);
        //}
        #endregion
    }
}
