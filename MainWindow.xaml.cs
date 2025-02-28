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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NCalc;  // Подключаем coreCLR-ncalc


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

            // Подписываемся на событие SwitchToScientific
            BasicCalculator.SwitchToScientific += BasicCalculator_SwitchToScientific;
            ScientificCalculator.SwitchToBasic += ScientificCalculator_SwitchToBasic;

            // Изначально виден обычный калькулятор
            BasicCalculator.Visibility = Visibility.Visible;
            ScientificCalculator.Visibility = Visibility.Collapsed;

            // Подписываемся на событие загрузки окна
            this.Loaded += MainWindow_Loaded;
            
        }

        //Устанавливает фокус на дисплей базвого клькулятора (всё ради ввода с клавиатуры)
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Устанавливаем фокус на TextBox внутри UserControl
            BasicCalculator.Display.Focus();
        }



        // Обработчик события SwitchToScientific
        private void BasicCalculator_SwitchToScientific(object sender, EventArgs e)
        {
            // Переключаем видимость
            ScientificCalculator.Visibility = Visibility.Visible;
            BasicCalculator.Visibility = Visibility.Collapsed;

            // Отложенный вызов Focus() через Dispatcher
            Dispatcher.BeginInvoke(new Action(() =>
            {

                // Устанавливаем фокус на TextBox базового калькулятора
                ScientificCalculator.Display.Focus();
            }), System.Windows.Threading.DispatcherPriority.Render);
        }

        // Обработчик события SwitchToBasic
        private void ScientificCalculator_SwitchToBasic(object sender, EventArgs e)
        {
            // Переключаем видимость
            BasicCalculator.Visibility = Visibility.Visible;
            ScientificCalculator.Visibility = Visibility.Collapsed;

            // Устанавливаем фокус на TextBox научного калькулятора
            BasicCalculator.Display.Focus();
        }


    }
}
