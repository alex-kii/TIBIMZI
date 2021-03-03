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

        static SByte count_click = -1;
        static byte[] Answers = new byte[22];       

        List<Access> Deser_Obj; // Глоб. переменная с данными

        /// <summary>
        /// ПЕРЕПРОЕКТИРОВАТЬ СЕРИАЛИЗАЦИЮ ПОД НОВЫЙ ДЖСОН!!!!!!!!!!!!!!
        /// </summary>

        private void DeserObj() // Метод для считывания данных !!!!!!!!!ДОБАВИТЬ ТРУ КАТЧ!!!!!!!!!!!!
        {
            // read file into a string and deserialize JSON to a type
            //Movie movie1 = JsonConvert.DeserializeObject<Movie>(File.ReadAllText(@"c:\movie.json"));

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(@"C:\Users\zifof\source\repos\TIBIMZI\TIBIMZI\SourceFile.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                Deser_Obj = (List<Access>)serializer.Deserialize(file, typeof(List<Access>));
            }
        }

        private void Analyzing()
        { 
            MessageBox.Show("Some Code...");
        }

        private void CB1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Button1.Visibility = Visibility.Visible;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            GOST.Text = "";
            AnswersBackground.Visibility = Visibility.Visible;
            StackAnswerPanel.Visibility = Visibility.Visible;
            RB1.IsChecked = true;
            ++count_click;

            // if(count_click == ??)
            // возможно создание нового окна и вывод в него результатов
            // progressbar... (добавить прогресс бар и скрыть его нахуй)

            DeserObj();

            // Заполнение вопроса и ответов
            TT_Term.Text = Deser_Obj[count_click].Data.Question;
            TT_Term.ToolTip = Deser_Obj[count_click].Data.Termin;
            RB1_TB.Text = Deser_Obj[count_click].Data.Answer1;
            RB2_TB.Text = Deser_Obj[count_click].Data.Answer2;
            RB3_TB.Text = Deser_Obj[count_click].Data.Answer3;
            RB4_TB.Text = Deser_Obj[count_click].Data.Answer4;

            // Считывание ответа (RadioButton)
            if ((bool)RB1.IsChecked)
                Answers[count_click] = 1;
            else if ((bool)RB2.IsChecked)
                Answers[count_click] = 2;
            else if ((bool)RB3.IsChecked)
                Answers[count_click] = 3;
            else if ((bool)RB4.IsChecked)
                Answers[count_click] = 4;
            else if (count_click > 0)
            {
                Answers[count_click] = 0;
                MessageBox.Show($"Вы пропустили ответ на вопрос №{count_click}.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
                

            // MessageBox.Show($"{Deser_Obj[0].Data.Question} - кликов сделано");


        }


    }
}
