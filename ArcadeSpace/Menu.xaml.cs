using System;
using System.Collections.Generic;
using System.IO;
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

namespace ArcadeSpace {
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : UserControl {
        public int best_score = 0;
        public static Menu selfrefmenu { get; set; }
        public Menu()
        {
            InitializeComponent();
            selfrefmenu = this;
            Canvas.SetLeft(StackMenu, ActualWidth / 2 - StackMenu.Width / 2);
            Canvas.SetTop(StackMenu, ActualHeight / 2 - StackMenu.Height / 2);
            Canvas.SetLeft(Score_Menu, 0);
            Canvas.SetTop(Score_Menu, 0);

            if (File.Exists("bc"))
                try {
                   best_score = Convert.ToInt32(File.ReadAllText("bc")) - 228 - 1337;
                   Best_Score_Menu.Content = "Best score: " + best_score;
                } catch (Exception) { };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.selfref.close_menu();
            MainWindow.selfref.start_game();
        }

        private void MenuControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetLeft(StackMenu, ActualWidth / 2 - StackMenu.Width / 2);
            Canvas.SetTop(StackMenu, ActualHeight / 2 - StackMenu.Height / 2);

            Canvas.SetLeft(Score_Menu, 0);
            Canvas.SetTop(Score_Menu, 0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.selfref.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Message m = new Message("About", "Author by Dadadam.\nLink: https://vk.com/dadadam999\nVersion: Learning WPF trial.\nAll rights belong to their respective owners.");
            m.Show();
        }
    }
}



