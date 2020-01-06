using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ArcadeSpace {
    public class GifAnimation {
        private GifBitmapEncoder GBD;
        private Timer timer;

        public List<GifImageSource> gifs { get; set; }

        public GifAnimation()
        {
            GBD = new GifBitmapEncoder();
            gifs = new List<GifImageSource>();
            timer = new Timer(timer_Tick, 0, 0, 100);
        }

        public void create_gif_anim(Image image, BitmapImage[] imgs)  {
            gifs.Add(new GifImageSource(image, imgs));
        }

        private void timer_Tick(object sender)
        {
            //gif_update
            GBD.Dispatcher.BeginInvoke(new Action(delegate () {
                if (gifs.Count > 0)
                    for (int i = 0; i < gifs.Count; i++)
                    {
                        if (gifs[i].CurrentFrame >= gifs[i].BitmapImages.Count)
                        {
                            gifs[i].CurrentFrame = 0;
                            gifs[i].image.Source = gifs[i].BitmapImages[gifs[i].CurrentFrame];
                        }
                        else
                        {
                            gifs[i].image.Source = gifs[i].BitmapImages[gifs[i].CurrentFrame];
                            gifs[i].CurrentFrame++;
                        }
                    }
            }));
        }
    }
}

