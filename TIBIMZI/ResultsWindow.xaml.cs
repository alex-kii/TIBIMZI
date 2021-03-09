using System;
using System.Collections;
using System.Windows;
using System.Windows.Media;

namespace TIBIMZI
{
    public partial class ResultsWindow : Window
    {
        public ResultsWindow()
        {
            InitializeComponent();
        }

        public void PrintRes(ArrayList arrayList, bool easy_type)
        {
            if (arrayList.Count != 0)
            {
                // Для большинства
                // >= 135 - хорошо
                // 100-134 - средне
                // <100 - плохо

                // Для последней организации
                // >= 100 - хорошо
                // 80-99 - средне
                // <80 - плохо

                try
                {
                    if (easy_type)
                    {
                        if ((float)arrayList[0] >= 100)
                            TB_balls.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF5BD64E");

                        else if ((float)arrayList[0] >= 80 && (float)arrayList[0] < 100)
                            TB_balls.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFE4E42B");

                        else
                            TB_balls.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFC33527");
                    }
                    else
                    {
                        if ((float)arrayList[0] >= 135)
                            TB_balls.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF5BD64E");

                        else if ((float)arrayList[0] >= 100 && (float)arrayList[0] < 135)
                            TB_balls.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFE4E42B");

                        else
                            TB_balls.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFC33527");
                    }

                    TB_balls.Text = arrayList[0].ToString();
                    
                    foreach (var item in (string[])arrayList[1])
                    {
                        if (item != null)
                            TB_Bad_solutions.Text += " - " + item + Environment.NewLine;
                    }

                    foreach (var item in (string[])arrayList[2])
                    {
                        if (item != null)
                            TB_Normal_solutions.Text += " - " + item + Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
            }
             
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
