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
        }

        // Обработчик события SwitchToScientific
        private void BasicCalculator_SwitchToScientific(object sender, EventArgs e)
        {
            // Переключаем видимость
            ScientificCalculator.Visibility = Visibility.Visible;
            BasicCalculator.Visibility = Visibility.Collapsed;
        }


    }
}
