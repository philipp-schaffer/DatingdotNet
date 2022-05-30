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

namespace ChatBox.MVVM.View
{
    /// <summary>
    /// Interaction logic for SwipeUC.xaml
    /// </summary>
    public partial class SwipeUC : UserControl
    {
        public SwipeUC(MainViewModel mvm)
        {
            InitializeComponent();

            DataContext = mvm;

        }
    }
}
