using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TIBIMZI
{
    /// <summary>
    /// Логика взаимодействия для ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        public ResultsWindow()
        {
            InitializeComponent();
        }

        public void PrintRes(bool easy_type, ArrayList LAA = null)
        {
            if (LAA.Count != 0)
            {
                try
                {
                    TB_balls.Text = LAA[0].ToString();

                    string[] sov1 = (string[])LAA[1];

                    //string[] sov2 = new string[16];

                    for (int i = 0; i < sov1.Length; i++)
                    {
                        TB_Bad_solutions.Text += sov1[i] + Environment.NewLine;
                    }
                }
                catch (Exception)
                {


                }
            }
            
            
        }

        // добавить кнопку Выйти, убрать возможно менять размер 

    }
}
