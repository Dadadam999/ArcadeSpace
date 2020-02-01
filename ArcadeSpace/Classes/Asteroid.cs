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
    public class Asteroid : Image
    {
        public Rect Collaider;
        double resizeK;
        public static Random rnd = new Random();
        double WidthGameWindow, HeightGameWindow;
        public BitmapImage SourceInit { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public int MinHealth { get; set; }
        public double Speed { get; set; }
        public bool isVertical { get; set; }

        public Asteroid(double WidthGameWindow, double HeightGameWindow, ref Canvas GameSpace)
        {
            this.WidthGameWindow = WidthGameWindow;
            this.HeightGameWindow = HeightGameWindow;
            SourceInit = new BitmapImage();
            SourceInit.BeginInit();
            SourceInit.UriSource = new Uri(@"pack://application:,,,/Resurces/Asteroid_fix.png");
            switch (rnd.Next(1, 4))
            {
                default:
                    SourceInit.Rotation = Rotation.Rotate0;
                    isVertical = false;
                    break;
                case 1:
                    SourceInit.Rotation = Rotation.Rotate0;
                    isVertical = false;
                    break;
                case 2:
                    SourceInit.Rotation = Rotation.Rotate90;
                    isVertical = true;
                    break;
                case 3:
                    SourceInit.Rotation = Rotation.Rotate180;
                    isVertical = false;
                    break;
                case 4:
                    SourceInit.Rotation = Rotation.Rotate270;
                    isVertical = true;
                    break;
            }
            SourceInit.EndInit();
            MinHealth = 2;
            MaxHealth = 10;
            CurrentHealth = rnd.Next(MinHealth, MaxHealth);
            Source = SourceInit;
            GameSpace.Children.Add(this);
            resize(WidthGameWindow);
            Canvas.SetTop(this, rnd.Next(0, Convert.ToInt32(HeightGameWindow - ActualHeight)));
            Canvas.SetLeft(this, WidthGameWindow + ActualWidth);

           // Canvas.SetZIndex(this, 998);
            Collaider = new Rect();
            update_collaider();
        }
        public void resize(double WidthGW)
        {
            resizeK = ActualWidth / ActualHeight;
            Width = WidthGW / CurrentHealth / 2;
            Height = ActualWidth / resizeK;
        }
        public void move()
        {
            Canvas.SetLeft(this, Canvas.GetLeft(this) - Speed);
        }
        public void update_collaider()
        {
            Collaider.Width = ActualWidth * 0.5;
            Collaider.Height = ActualHeight * 0.9;
            Collaider.X = Canvas.GetLeft(this) + ActualWidth * 0.25;
            Collaider.Y = Canvas.GetTop(this) + ActualWidth * 0.05;
        }

        #region view_collision
        public Rectangle CollaiderView { get; set; }
        public void InitCollaiderView(ref Canvas GameSpace)
        {
            CollaiderView = new Rectangle();
            CollaiderView.Fill = Brushes.Red;
            MoveCollaiderView();
            GameSpace.Children.Add(CollaiderView);
        }
        public void MoveCollaiderView()
        {
            CollaiderView.Width = Collaider.Width;
            CollaiderView.Height = Collaider.Height;
            Canvas.SetLeft(CollaiderView, Collaider.X);
            Canvas.SetTop(CollaiderView, Collaider.Y);
        }
        public void RemoveCollaiderView(ref Canvas GameSpace)
        {
            GameSpace.Children.Remove(CollaiderView);
        }
        #endregion
    }
}