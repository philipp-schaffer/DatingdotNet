using ChatBox.MVVM.ViewModel;
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

namespace ChatBox.MVVM.View
{
    /// <summary>
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        MainViewModel mv;

        public LogWindow()
        {
            InitializeComponent();
            mv = new MainViewModel();
            DataContext = mv;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            WindowContent.Children.Clear();
            var view = new MainUC(mv);
            WindowContent.Children.Add(view);
        }
    }
}
