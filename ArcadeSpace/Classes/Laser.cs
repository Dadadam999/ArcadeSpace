using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ArcadeSpace {
    public class Laser : Image {

        public Rect Collaider;
        double resizeK;

        public int Damage { get; set; }
        public double Speed { get; set; }

        public Laser(ref Ship ship, ref Canvas GameSpace) {
            
            Damage = 2;
            Speed = 10;

            Source = new BitmapImage(new Uri(@"pack://application:,,,/Resurces/laser.png"));
            resize(ship.ActualWidth);
            GameSpace.Children.Add(this);
            Canvas.SetTop(this, Canvas.GetTop(ship) + ship.ActualHeight / 3);
            Canvas.SetLeft(this, Canvas.GetLeft(ship) + ship.ActualWidth);

            update_collaider();
        }
        public void move()
        {
            Canvas.SetLeft(this, Canvas.GetLeft(this) + Speed);
        }
        public void resize(double WidthShip)
        {
            resizeK = Width / Height;
            Width = WidthShip / 8;
            Height = Width / resizeK;
        }

        public void update_collaider() {
            Collaider = new Rect();
            Collaider.Width = ActualWidth;
            Collaider.Height = ActualHeight;
            Collaider.X = Canvas.GetLeft(this);
            Collaider.Y =  Canvas.GetTop(this);
        }
    }
}
