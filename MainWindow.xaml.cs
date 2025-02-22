using System;
using System.Data; // для =
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            /// Перебираем все элементы из сетки для кнопок и выбираем только кнопки 
            foreach (UIElement el in buttonsGrid.Children)
            {
                if (el is Button)
                {
                    ///объект el класса UIElement поэтому его нужно преобразовать в класс Button
                    // Подписываемся на событие Click для каждой кнопки
                    ((Button)el).Click += Button_Click;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // е - объект на который мы нажали
            // Content позволит получить надпись с нажатой кнопки а затем преобразуем ее в строку
            string str = (string)((Button)e.OriginalSource).Content;

            // Проверяем, является ли текущее содержимое дисплея результатом вычисления или ошибкой
            bool isResultOrError = false;

            // Обработка различных кнопок
            switch (str)
            {
                case "C": // Очистка дисплея
                    Display.Text = "";
                    break;

                case "x": // Удаление последнего символа
                    if (Display.Text.Length > 0)
                    {
                        Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
                    }
                    break;

                case "=": // Вычисление выражения
                    try
                    {
                        // Используем DataTable для вычисления выражения
                        var result = new DataTable().Compute(Display.Text, null);
                        Display.Text = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        Display.Text = "Error";
                    }
                    break;

                default: // Добавление символа на дисплей
                    Display.Text += str;
                    break;
            }

        }


    }
}
