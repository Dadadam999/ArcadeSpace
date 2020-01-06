using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ArcadeSpace
{
    public class Explosion : Image
    {
        public Explosion(double WidthGameWindow, double HeightGameWindow, Point location, ref Canvas GameSpace)
        {
            time = 3; 

            Source = new BitmapImage(new Uri(@"pack://application:,,,/Resurces/explosion.png"));
            Width = WidthGameWindow / 15;
            Height = HeightGameWindow / 15;
            GameSpace.Children.Add(this);
            Canvas.SetTop(this, location.Y);
            Canvas.SetLeft(this, location.X);
        }
        public int time { get; set; }
    }
}