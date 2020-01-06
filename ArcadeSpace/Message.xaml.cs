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
using System.Windows.Shapes;

namespace ArcadeSpace
{
    /// <summary>
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message : Window
    {
        public Message(string nameForm, string Content)
        {
            InitializeComponent();
            Title = nameForm;
            ContentText.Document.Blocks.Add(new Paragraph(new Run(Content)));
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
           WindowState =  WindowState.Normal;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Width = 460;
            Height = 240;
        }
    }
}
