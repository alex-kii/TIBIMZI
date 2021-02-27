﻿using System;
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

namespace TIBIMZI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static byte count_click = 0;

        private void CB1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Button1.Visibility = Visibility.Visible;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            AnswersBackground.Visibility = Visibility.Visible;
            StackAnswerPanel.Visibility = Visibility.Visible;
            ++count_click;
            MessageBox.Show($"{count_click} - кликов сделано");


        }

    }
}