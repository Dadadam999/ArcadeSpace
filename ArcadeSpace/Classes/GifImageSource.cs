using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ArcadeSpace
{
    public class GifImageSource
    {
        public GifImageSource() {
            image = new Image();
            BitmapImages = new List<BitmapImage>();
            CurrentFrame = 0;
        }
        public GifImageSource(Image image, BitmapImage[] bitmapssoures) {
            this.image = image;
            BitmapImages = new List<BitmapImage>();
            BitmapImages.AddRange(bitmapssoures);
            CurrentFrame = 0;
        }
        public Image image { get; set; }
        public List<BitmapImage> BitmapImages { get; set; }
        public int CurrentFrame { get; set; }
    }
}
