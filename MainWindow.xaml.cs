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

            ThemeManager.ApplyTheme("DarkLavenderTheme");

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










        // Обработчики для кастомной панели заголовка
        private void TitleBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();  // Перетаскивание окна
        }



        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;  // Сворачивание окна
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;  // Разворачивание/восстановление окна
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();  // Закрытие окна
        }






        // Контекстное меню
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuPopup.IsOpen = true;
        }
        // Сменить тему
        private void Change_Theme_Click(object sender, RoutedEventArgs e)
        {
            ThemePopup.IsOpen = true;
        }
        // О программе
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Сохранить как...");
        }



        //Метод для смены темы
        public static class ThemeManager
        {
            /// <summary>
            /// Применяет указанную тему к приложению.
            /// </summary>
            /// <param name="themeName">Имя темы (например, "LightTheme").</param>
            public static void ApplyTheme(string themeName)
            {
                // Формируем путь к файлу темы
                var themeUri = new Uri($"Themes/{themeName}.xaml", UriKind.Relative);

                // Очищаем текущие ресурсы
                Application.Current.Resources.MergedDictionaries.Clear();

                // Загружаем новую тему
                var themeDictionary = new ResourceDictionary { Source = themeUri };
                Application.Current.Resources.MergedDictionaries.Add(themeDictionary);

                // Загружаем стили кнопок
                var buttonStylesUri = new Uri("Styles/ButtonStyles.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = buttonStylesUri });
            }
        }

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            // Применяем светлую тему
            ThemeManager.ApplyTheme("LightTheme");
            ThemePopup.IsOpen = false; // Закрыть Popup после выбора
            MenuPopup.IsOpen = false; // Закрыть Popup после выбора
        }

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {
            // Применяем темную тему
            ThemeManager.ApplyTheme("DarkTheme");
            ThemePopup.IsOpen = false; // Закрыть Popup после выбора
            MenuPopup.IsOpen = false; // Закрыть Popup после выбора
        }
        private void DarkLavenderTheme_Click(object sender, RoutedEventArgs e)
        {
            // Применяем темную тему
            ThemeManager.ApplyTheme("DarkLavenderTheme");
            ThemePopup.IsOpen = false; // Закрыть Popup после выбора
            MenuPopup.IsOpen = false; // Закрыть Popup после выбора
        }

    }
}
