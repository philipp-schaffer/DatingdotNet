using ChatBox.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        MainViewModel mv;

        public LogWindow()
        {
            InitializeComponent();
            mv = new MainViewModel();
            DataContext = mv;
            AllocConsole();
           

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // LoginButton
            var bat = sender as Button;
            bat.Command.Execute(mv.ConnectToServerCommand);
            WindowContent.Children.Clear();
            var view = new MainUC(mv);
            WindowContent.Children.Add(view);
        }

        private void testBut_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(mv.LoginVM.Username);
        }
    }
}
