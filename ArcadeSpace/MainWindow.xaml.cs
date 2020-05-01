using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ArcadeSpace
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer;
        Ship ship;
        List<Laser> lasers = new List<Laser>();
        List<Explosion> exps = new List<Explosion>();
        List<Asteroid> asteroids = new List<Asteroid>();
        string key_down = "";
        int couldaun_shoot = 0, couldaun_asteroid = 0, score = 0, speed_game = 4, k_loop = 0;
        GifAnimation gf;
        Menu menu = new Menu();
        StackPanel ScoreStock;
        Label Score;
        bool resize_enabled = true;
        double FixWidth, FixHeight;
        public static MainWindow selfref { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            selfref = this;
            GameSpace.Width = ActualWidth;
            GameSpace.Height = ActualHeight;
            open_menu();
        }

        public void open_menu()
        {
            if (!GameSpace.Children.Contains(menu)) GameSpace.Children.Add(menu);
            menu.Width = ActualWidth;
            menu.Height = ActualHeight;
            Canvas.SetZIndex(menu, 999);
        }

        public void close_menu()
        {
            GameSpace.Children.Remove(menu);
        }

        public void start_game()
        {
            resize_enabled = false;
            FixWidth = Width;
            FixHeight = Height;
            lasers.Clear();
            exps.Clear();
            asteroids.Clear();
            GameSpace.Children.Clear();
            key_down = "";
            couldaun_shoot = 0;
            couldaun_asteroid = 0;

            ship = new Ship(ActualWidth, ActualHeight, ref GameSpace);
            ship.update_collaider();
            //ship.InitCollaiderView(ref GameSpace); // для отображения колизий

            timer = new Timer(timer_Tick, 0, 0, 30);
            gf = new GifAnimation();
            gf.create_gif_anim(ship, new[] { new BitmapImage(new Uri(@"pack://application:,,,/Resurces/ship1.png")), new BitmapImage(new Uri(@"pack://application:,,,/Resurces/ship2.png")) });
            
            ScoreStock = Menu.selfrefmenu.Score_Stock;
            Score = Menu.selfrefmenu.Score_Menu;
            Menu.selfrefmenu.MenuSpace.Children.Remove(ScoreStock);
            GameSpace.Children.Add(ScoreStock);
            Canvas.SetZIndex(ScoreStock, 998);
            Score.Content = "Score: 0";
            
            speed_game = 4;
            k_loop = 0;
        }

        public void end_game()
        {
            resize_enabled = true;
            timer.Dispose();
            open_menu();
            GameSpace.Children.Remove(ScoreStock);
            Menu.selfrefmenu.MenuSpace.Children.Add(ScoreStock);
            if (Menu.selfrefmenu.best_score < score)
            {
                Menu.selfrefmenu.best_score = score;
                Menu.selfrefmenu.Best_Score_Menu.Content = "Best score: " + score;
                File.WriteAllText("bc", (score + 228 + 1337).ToString());
            }
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (resize_enabled == true)
            {
                GameSpace.Width = ActualWidth;
                GameSpace.Height = ActualHeight;

                menu.Width = ActualWidth;
                menu.Height = ActualHeight;

                if (ship != null)
                {
                    ship.resize(ActualWidth);
                    ship.update_collaider();
                    //ship.MoveCollaiderView(); //для вывода коализии
                }

                if (asteroids.Count > 0)
                    foreach (Asteroid a in asteroids)
                    {
                        a.resize(ActualWidth);
                        a.update_collaider();
                        //a.MoveCollaiderView(); //для вывода коализии
                    }
            }
            else
            {
                Width = FixWidth;
                Height = FixHeight;
            } 
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (resize_enabled == true)
            {
                GameSpace.Width = ActualWidth;
                GameSpace.Height = ActualHeight;

                menu.Width = ActualWidth;
                menu.Height = ActualHeight;

                if (ship != null)
                {
                    ship.resize(ActualWidth);
                    ship.update_collaider();
                    //ship.MoveCollaiderView();  //для вывода коализии
                }

                if (asteroids.Count > 0)
                    foreach (Asteroid a in asteroids)
                    {
                        a.resize(ActualWidth);
                        a.update_collaider();
                        //a.MoveCollaiderView();  //для вывода коализии
                    }
            }
            else 
                WindowState = WindowState.Normal;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Menu.selfrefmenu.best_score < score && score > 0)
                File.WriteAllText("bc", (score + 228 + 1337).ToString());
        }

        private void timer_Tick(object sender)
        {
            //game_loop
            GameSpace.Dispatcher.BeginInvoke(new Action(delegate ()
            {
                try
                {
                    //game speed
                    if (k_loop >= 30 * 25)
                    { //  boost couldaun
                        k_loop = 0;
                        speed_game++;
                    }
                    else k_loop++;
                    //push to keyboard
                    ship.move(ActualWidth, ActualHeight, key_down);
                    ship.update_collaider();
                    //ship.MoveCollaiderView();  //для вывода коализии

                    if (key_down == "Space" && couldaun_shoot <= 0)
                    {
                        lasers.Add(new Laser(ref ship, ref GameSpace));
                        couldaun_shoot = 10; // change couldaun shoot
                    }

                    couldaun_shoot--;
                    key_down = "";

                    //lasers
                    if (lasers.Count > 0)
                    {
                        //move laser
                        foreach (Laser l in lasers)
                        {
                            l.move();
                            l.update_collaider();
                        }
                        //remove laser
                        for (int i = 0; i < lasers.Count; i++)
                            if (Canvas.GetLeft(lasers[i]) > this.ActualWidth + lasers[i].ActualWidth)
                            {
                                GameSpace.Children.Remove(lasers[i]);
                                lasers.Remove(lasers[i]);
                            }
                    }
                    //spawn asteroid
                    if (couldaun_asteroid <= 0)
                    {
                        asteroids.Add(new Asteroid(this.ActualWidth, this.ActualHeight, ref GameSpace));
                        couldaun_asteroid = 40;
                        asteroids[asteroids.Count - 1].Speed = speed_game;
                        //asteroids[asteroids.Count - 1].InitCollaiderView(ref GameSpace);  //для вывода коализии
                    }
                    couldaun_asteroid--;

                    if (asteroids.Count > 0)
                    {
                        //move asteroid
                        foreach (Asteroid a in asteroids)
                        {
                            a.move();
                            a.update_collaider();
                            // a.MoveCollaiderView();  //для вывода коализии
                        }
                        //remove asteroid
                        for (int i = 0; i < asteroids.Count; i++)
                            if (Canvas.GetLeft(asteroids[i]) <= 0 - asteroids[i].ActualWidth)
                            {
                                // asteroids[i].RemoveCollaiderView(ref GameSpace);  //для вывода коализии
                                GameSpace.Children.Remove(asteroids[i]);
                                asteroids.Remove(asteroids[i]);
                            }
                    }
                    //collision asteroid and laser
                    if (lasers.Count > 0 && asteroids.Count > 0)
                        for (int iast = 0; iast < asteroids.Count; iast++)
                            for (int ilas = 0; ilas < lasers.Count; ilas++)
                                if (asteroids.Count > 0 && lasers.Count > 0)
                                    if (check_collision(asteroids[iast].Collaider, lasers[ilas].Collaider))
                                    {
                                        exps.Add(new Explosion(this.ActualWidth, this.ActualHeight, new Point(Canvas.GetLeft(lasers[ilas]) - lasers[ilas].ActualWidth, Canvas.GetTop(lasers[ilas]) - asteroids[iast].ActualHeight / 2), ref GameSpace));

                                        if (asteroids[iast].CurrentHealth >= asteroids[iast].MaxHealth)
                                        {
                                            GameSpace.Children.Remove(asteroids[iast]);
                                            asteroids.Remove(asteroids[iast]);
                                            score += 10;
                                           // if (!GameSpace.Children.Contains(Score)) GameSpace.Children.Add(Score);
                                            Score.Content = "Score: " + score.ToString();
                                        }
                                        else
                                        {
                                            asteroids[iast].CurrentHealth += 1;
                                            asteroids[iast].resize(ActualWidth);
                                            Canvas.SetTop(asteroids[iast], Canvas.GetTop(lasers[ilas]) - asteroids[iast].ActualHeight / 2);
                                            score += 5;
                                           // if (!GameSpace.Children.Contains(Score)) GameSpace.Children.Add(Score);
                                            Score.Content = "Score: " + score.ToString();
                                        }

                                        GameSpace.Children.Remove(lasers[ilas]);
                                        lasers.Remove(lasers[ilas]);
                                    }
                    //explosion remove
                    if (exps.Count > 0)
                        for (int i = 0; i < exps.Count; i++)
                        {
                            if (exps[i].time <= 0)
                            {
                                GameSpace.Children.Remove(exps[i]);
                                exps.Remove(exps[i]);
                            }
                            else
                                exps[i].time--;
                        }
                    //collision asteroid and spaceship
                    if (asteroids.Count > 0)
                        for (int iast = 0; iast < asteroids.Count; iast++)
                            if (check_collision(ship.collaiders[0].getRect(), asteroids[iast].Collaider) || check_collision(ship.collaiders[1].getRect(), asteroids[iast].Collaider))
                            {
                                exps.Add(new Explosion(this.ActualWidth, this.ActualHeight, new Point(Canvas.GetLeft(ship) + ship.ActualWidth / 2, Canvas.GetTop(ship) + ship.ActualHeight / 2), ref GameSpace));
                                end_game();
                            }
                }
                catch (Exception e) { Console.WriteLine(e); }
            }));
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            key_down = e.Key.ToString();
        }
        bool check_collision(Rect one, Rect two)
        {
            return one.IntersectsWith(two);
        }
    }
}