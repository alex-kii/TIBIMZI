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
            // подгрузка и попытка считать json
        }

        static byte count_click = 0;
        static byte[] Answers = new byte[22];       

        List<Access> Deser_Obj; // Глоб. переменная с данными

        private void DeserObj() // Метод для считывания данных
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

        private void CB1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Button1.Visibility = Visibility.Visible;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            AnswersBackground.Visibility = Visibility.Visible;
            StackAnswerPanel.Visibility = Visibility.Visible;
            ++count_click;

            // if(count_click == ??)
            // возможно создание нового окна и вывод в него результатов
            // progressbar...

            DeserObj();

            TT_Term.Text = Deser_Obj[0].Data.Question;
            TT_Term.ToolTip = Deser_Obj[0].Data.Termin;

            //CB_Answ1 = ppp[count_click]...
            //CB_Answ2 = ppp[count_click]...
            //CB_Answ3 = ppp[count_click]...
            //CB_Answ4 = ppp[count_click]...

            // считать радиобаттон


            // read file into a string and deserialize JSON to a type
            //Movie movie1 = JsonConvert.DeserializeObject<Movie>(File.ReadAllText(@"c:\movie.json"));

            // deserialize JSON directly from a file



            // MessageBox.Show($"{Deser_Obj[0].Data.Question} - кликов сделано");


        }

    }
}
