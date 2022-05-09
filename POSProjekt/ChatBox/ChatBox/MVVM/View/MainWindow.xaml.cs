﻿using ChatBox.MVVM.Core;
using ChatBox.MVVM.ViewModel;
using ChatBox.Net;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

      //  MainViewModel mvm;
        public MainWindow(MainViewModel mvm)
        {
            InitializeComponent();
            mvm = new MainViewModel();
            DataContext = mvm;
        }
    }
}
