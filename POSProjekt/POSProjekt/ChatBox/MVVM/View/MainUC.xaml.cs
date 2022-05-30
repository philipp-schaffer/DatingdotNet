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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ChatBox.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainUC.xaml
    /// </summary>
    public partial class MainUC : UserControl
    {
        DispatcherTimer timer;

        double panelWidth;
        bool hidden;
        MainViewModel _mvm;
        public MainUC(MainViewModel mvm)
        {
            InitializeComponent();

            DataContext = mvm;

            _mvm = mvm;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += Timer_Tick;
            panelWidth = 150;
            hidden = true;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (hidden)
            {
                sideDockPanel.Width = 0;
                sidePanel.Width += 5;
                if (sidePanel.Width >= panelWidth)
                {
                    timer.Stop();
                    hidden = false;
                }
            }
            else
            {
                sideDockPanel.Width = 160;
                sidePanel.Width -= 5;
                if (sidePanel.Width <= 40)
                {
                    timer.Stop();
                    hidden = true;
                }
            }
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sidePanel.Width = 40;
            maingrid.Children.Clear();
            maingrid.Children.Add(new SwipeUC(_mvm));
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            maingrid.Children.Clear();
            maingrid.Children.Add(new MainUC(_mvm));
        }
    }
}
