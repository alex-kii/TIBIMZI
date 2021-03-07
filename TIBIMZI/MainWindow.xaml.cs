using Newtonsoft.Json;
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
using TIBIMZI.Models;

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

        static sbyte count_click = -2;
        static byte[] Answers = new byte[16];

        List<Access> Deser_Obj; // Глоб. переменная с данными

        private void DeserObj() // Метод для считывания данных
        {
            // read file into a string and deserialize JSON to a type
            //Movie movie1 = JsonConvert.DeserializeObject<Movie>(File.ReadAllText(@"c:\movie.json"));

            try
            {
                // десериализация JSON из файла
                using (StreamReader file = File.OpenText(@"C:\Users\zifof\source\repos\TIBIMZI\TIBIMZI\SourceFile.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    Deser_Obj = (List<Access>)serializer.Deserialize(file, typeof(List<Access>));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        // СТЕРЕТЬ
        string[] sov1 = new string[16]; // советы для плохого случая 
        string[] sov2 = new string[16]; // советы для хорошего случая. Иначе потом заебёмся анализировать что куда вставить

        private float Analyzing()
        {
            float sum = 0.0f;
            

            for (int i = 0; i < Answers.Length; i++)
            {

                switch (Answers[i])
                {
                    case 1:
                        sum += Deser_Obj[i].Data.Priority1;

                        if (Deser_Obj[i].Data.Priority1 == 0.25)
                            sov1[i] = Deser_Obj[i].Data.Advice1;
                        else if (Deser_Obj[i].Data.Priority1 < 1)
                            sov2[i] = Deser_Obj[i].Data.Advice2;

                        break;

                    case 2:
                        sum += Deser_Obj[i].Data.Priority2;

                        if (Deser_Obj[i].Data.Priority2 == 0.25)
                            sov1[i] = Deser_Obj[i].Data.Advice1;
                        else if (Deser_Obj[i].Data.Priority2 < 1)
                            sov2[i] = Deser_Obj[i].Data.Advice2;

                        break;

                    case 3:
                        sum += Deser_Obj[i].Data.Priority3;

                        if (Deser_Obj[i].Data.Priority2 == 0.25)
                            sov1[i] = Deser_Obj[i].Data.Advice1;
                        else if (Deser_Obj[i].Data.Priority2 < 1)
                            sov2[i] = Deser_Obj[i].Data.Advice2;

                        break;

                    case 4:
                        sum += Deser_Obj[i].Data.Priority4;

                        if (Deser_Obj[i].Data.Priority2 == 0.25)
                            sov1[i] = Deser_Obj[i].Data.Advice1;
                        else if (Deser_Obj[i].Data.Priority2 < 1)
                            sov2[i] = Deser_Obj[i].Data.Advice2;

                        break;

                    default:
                        sum += 0;

                        if (Deser_Obj[i].Data.Priority2 == 0.25)
                            sov1[i] = Deser_Obj[i].Data.Advice1;
                        else if (Deser_Obj[i].Data.Priority2 < 1)
                            sov2[i] = Deser_Obj[i].Data.Advice2;

                        break;
                }

            }

            return sum*10;
        }

        private void CB1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Button1.Visibility = Visibility.Visible;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ++count_click;
            RB1.IsChecked = true;

            if (count_click == -1)
            {
                DeserObj();

                GOST.Text = "";
                AnswersBackground.Visibility = Visibility.Visible;
                StackAnswerPanel.Visibility = Visibility.Visible;

                TT_Term.Text = Deser_Obj[count_click + 1].Data.Question;
                TT_Term.ToolTip = Deser_Obj[count_click + 1].Data.Termin;
                RB1_TB.Text = Deser_Obj[count_click + 1].Data.Answer1;
                RB2_TB.Text = Deser_Obj[count_click + 1].Data.Answer2;
                RB3_TB.Text = Deser_Obj[count_click + 1].Data.Answer3;
                RB4_TB.Text = Deser_Obj[count_click + 1].Data.Answer4;
            }
            else if(count_click >= 0 && count_click < 16)
            {
                // Считывание ответа (RadioButton)
                if ((bool)RB1.IsChecked)
                    Answers[count_click] = 1;
                else if ((bool)RB2.IsChecked)
                    Answers[count_click] = 2;
                else if ((bool)RB3.IsChecked)
                    Answers[count_click] = 3;
                else if ((bool)RB4.IsChecked)
                    Answers[count_click] = 4;

                if (count_click +1 <= 15)
                {
                    // Заполнение вопроса и ответов
                    TT_Term.Text = Deser_Obj[count_click + 1].Data.Question;
                    TT_Term.ToolTip = Deser_Obj[count_click + 1].Data.Termin;
                    RB1_TB.Text = Deser_Obj[count_click + 1].Data.Answer1;
                    RB2_TB.Text = Deser_Obj[count_click + 1].Data.Answer2;
                    RB3_TB.Text = Deser_Obj[count_click + 1].Data.Answer3;
                    RB4_TB.Text = Deser_Obj[count_click + 1].Data.Answer4;
                }
                
            }
            
            if (count_click >= 15) //>= 15
            {
                ResultsWindow WinRes = new ResultsWindow();

                WinRes.Owner = this;

                WinRes.ShowDialog();

                Analyzing();

                return;

                // возможно создание нового окна и вывод в него результатов
                // progressbar... (добавить прогресс бар и скрыть его нахуй)
            }
        
        }
    }
}