using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    ///    


    // 135 - хорошо
    // 100-134 - средне
    // <100 - плохо


    // Последняя организация
    // 100 - хорошо
    // 80-99 - средне
    // <80 - плохо


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0, 0, 2);
        }

        private void timer_tick(object sender, EventArgs e)
        {
            TB_PB.Text = System_mes[(int)ProgBar.Value / 25];
            ProgBar.Value += 25;
            
            if (ProgBar.Value == ProgBar.Maximum)
            {
                timer.Stop();

                MessageBox.Show("Анализ ответов и формирование рекомендаций завершены.", "Блок анализа", MessageBoxButton.OK, MessageBoxImage.Information);

                ResultsWindow WinRes = new ResultsWindow();

                WinRes.Owner = this;

                WinRes.PrintRes(easy_type, Analyzing());

                WinRes.ShowDialog();

                Button1.IsEnabled = true;

                ProgBar.Value = ProgBar.Minimum;

            }

        }

        static sbyte count_click = -2;
        bool easy_type = false;
        static byte[] Answers = new byte[16];
        string[] System_mes = new string[4]
        {
                "Начало анализа ответов.",
                "Завершение анализа ответов.",
                "Формирование блока рекомендаций.",
                "Финальная проверка результата анализа."
        };

        static List<Access> Deser_Obj; // Глоб. переменная с данными

        private void DeserObj() // Метод для считывания данных
        {
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
                this.Close();
            }

        }

        private ArrayList Analyzing()
        {
            float sum = 0.0f;

            ArrayList arrayList = new ArrayList();

            string[] Advices1 = new string[16]; // массив рекомедация для устаревших решений
            string[] Advices2 = new string[16]; // массив рекомедация теряющих актуальность решений

            for (int i = 0; i < Answers.Length; i++)
            {             
                switch (Answers[i])
                {
                    case 1:
                        sum += Deser_Obj[i].Data.Priority1;

                        if (Deser_Obj[i].Data.Priority1 == 0.25)
                            Advices1[i] = Deser_Obj[i].Data.Advice1;
                        else if (Deser_Obj[i].Data.Priority1 < 1)
                            Advices2[i] = Deser_Obj[i].Data.Advice2;

                        break;

                    case 2:
                        sum += Deser_Obj[i].Data.Priority2;

                        if (Deser_Obj[i].Data.Priority2 == 0.25)
                            Advices1[i] = Deser_Obj[i].Data.Advice1;
                        else if (Deser_Obj[i].Data.Priority2 < 1)
                            Advices2[i] = Deser_Obj[i].Data.Advice2;

                        break;

                    case 3:
                        sum += Deser_Obj[i].Data.Priority3;

                        if (Deser_Obj[i].Data.Priority2 == 0.25)
                            Advices1[i] = Deser_Obj[i].Data.Advice1;
                        else if (Deser_Obj[i].Data.Priority2 < 1)
                            Advices2[i] = Deser_Obj[i].Data.Advice2;

                        break;

                    case 4:
                        sum += Deser_Obj[i].Data.Priority4;

                        if (Deser_Obj[i].Data.Priority2 == 0.25)
                            Advices1[i] = Deser_Obj[i].Data.Advice1;
                        else if (Deser_Obj[i].Data.Priority2 < 1)
                            Advices2[i] = Deser_Obj[i].Data.Advice2;

                        break;

                    default:
                        sum += 0;
                        break;
                }
              
            }

            arrayList.Add(sum * 10);
            arrayList.Add(Advices1);
            arrayList.Add(Advices2);

            return arrayList;
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


                if (count_click + 1 <= 15)
                {
                    // Заполнение вопроса и ответов
                    TT_Term.Text = Deser_Obj[count_click + 1].Data.Question;
                    TT_Term.ToolTip = Deser_Obj[count_click + 1].Data.Termin;
                    RB1_TB.Text = Deser_Obj[count_click + 1].Data.Answer1;
                    RB2_TB.Text = Deser_Obj[count_click + 1].Data.Answer2;
                    RB3_TB.Text = Deser_Obj[count_click + 1].Data.Answer3;
                    RB4_TB.Text = Deser_Obj[count_click + 1].Data.Answer4;
                }

                if (count_click == 14)
                    Button1.Content = "Завершить";
                
            }
            
            if (count_click >= 15) //>= 15
            {
                switch (CB1.Text)
                {
                    case "Банки при подключении к ЕБС":
                        easy_type = true;
                        break;
                    default:
                        easy_type = false;
                        break;
                }

                ProgBar.Visibility = Visibility.Visible;

                timer.Start();

                Button1.IsEnabled = false;

                --count_click;
          
            }   
        
        }
    }
}