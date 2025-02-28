using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using NCalc;  // Подключаем coreCLR-ncalc


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для BasicCalculator.xaml
    /// </summary>
    public partial class BasicCalculator : UserControl
    {


        // Создаем событие для переключения на научный калькулятор
        public event EventHandler SwitchToScientific;

        public BasicCalculator()
        {
            InitializeComponent();

            Display.Focus();

            // Подписка на событие KeyDown для обработки нажатий клавиш на уровне UserControl
            // Это событие будет срабатывать, когда пользователь нажимает клавиши в пределах UserControl.
            this.KeyDown += UserControl_KeyDown;

            // Отключаем обработку событий клавиатуры в TextBox, чтобы избежать конфликтов
            // Display — это текстовое поле, в котором отображаются числа и операторы калькулятора.
            // Используется PreviewKeyDown, чтобы перехватить нажатие клавиш еще до того, как оно будет обработано самим TextBox.
            Display.PreviewKeyDown += Display_PreviewKeyDown;

            // Подписка на событие Click для кнопок
            foreach (UIElement el in buttonsGrid.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += Button_Click;
                }
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // Обрабатываем нажатие клавиши
                // В этой функции обрабатываются все нажатия клавиш на уровне UserControl
                HandleKeyPress(e.Key);  // Вызываем метод, который будет обрабатывать конкретные клавиши (например, цифры или операторы)

                // Если нажата клавиша Enter, останавливаем дальнейшее распространение события
                // Это необходимо, чтобы предотвратить выполнение нежелательных действий после нажатия Enter.
                if (e.Key == Key.Enter)
                {
                    e.Handled = true;  // Помечаем событие как обработанное, чтобы другие элементы не обрабатывали его.
                }
            }
            catch (Exception ex)
            {
                // Ловим все исключения, которые могут возникнуть при обработке нажатий клавиш
                // Например, если вдруг возникнет ошибка в обработке символа, будет выведено сообщение в отладчик.
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        private void Display_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Отменяем обработку события в текущем элементе, чтобы оно не распространялось дальше
            // Это важно, чтобы TextBox не обрабатывал клавиши по своему усмотрению (например, если нам нужно перехватить их для калькулятора)
            e.Handled = true;

            // Передаем событие в основной обработчик UserControl
            // Таким образом, все клавиши, которые нажаты в TextBox, обрабатываются как если бы они были нажаты на уровне UserControl.
            UserControl_KeyDown(sender, e);
        }

        private void HandleKeyPress(Key key)
        {
            string str = "";

            // Проверяем, нажата ли клавиша Shift
            bool isShiftPressed = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;

            // Преобразование клавиш в строки
            switch (key)
            {

                case Key.OemPlus: // Плюс "+" (Shift + =)
                    str = "+";
                    break;
                case Key.OemMinus: // Минус "-" (без Shift)
                    str = "-";
                    break;
                case Key.OemQuestion: // Слэш "/" (Shift + 7)
                    str = "/";
                    break;

                case Key.D0:
                case Key.NumPad0:
                    str = isShiftPressed ? ")" : "0"; // Shift + 0 -> ")", иначе "0"
                    break;
                case Key.D1:
                case Key.NumPad1:
                    str = "1";
                    break;
                case Key.D2:
                case Key.NumPad2:
                    str = "2";
                    break;
                case Key.D3:
                case Key.NumPad3:
                    str = "3";
                    break;
                case Key.D4:
                case Key.NumPad4:
                    str = "4";
                    break;
                case Key.D5:
                case Key.NumPad5:
                    str = "5";
                    break;
                case Key.D6:
                case Key.NumPad6:
                    str = "6";
                    break;
                case Key.D7:
                case Key.NumPad7:
                    str = isShiftPressed ? "/" : "7"; // Shift + 7 -> "/", иначе "7"
                    break;
                case Key.D8:
                case Key.NumPad8:
                    str = "8";
                    break;
                case Key.D9:
                case Key.NumPad9:
                    str = "9";
                    break;
                case Key.Add:
                    str = "+";
                    break;
                case Key.Subtract:
                    str = "-";
                    break;
                case Key.Multiply:
                    str = "×";
                    break;
                case Key.Divide:
                    str = "/";
                    break;
                case Key.Enter:
                    str = "=";
                    break;
                case Key.Back:
                    str = "⌫";
                    break;
                case Key.Escape:
                    str = "C";
                    break;
                case Key.OemPeriod:
                case Key.Decimal:
                    str = ",";
                    break;


                default:
                    return; // Игнорируем другие клавиши
            }

            // Обрабатываем ввод
            ProcessInput(str);
        }

        // Начальный размер шрифта
        protected const double InitialFontSize = 36;

        // Максимальная длина текста, при которой шрифт будет иметь начальный размер
        protected const int MaxLengthForInitialFontSize = 16;

        // Проверяем, является ли текущее содержимое дисплея результатом вычисления или ошибкой
        protected bool isResultOrError = true;

        // Метод для преобразования градусов в радианы
        protected static double DegreeToRadian(double degree)
        {
            return degree * (Math.PI / 180);
        }

        protected void Button_Click(object sender, RoutedEventArgs e)
        {
            // е - объект на который мы нажали
            // Content позволит получить надпись с нажатой кнопки а затем преобразуем ее в строку
            string str = (string)((Button)e.OriginalSource).Content;

            // Обрабатываем ввод
            ProcessInput(str);

            if (str != "sci")
                // Снимаем фокус с кнопки, чтобы фокус оставался на TextBox
                Display.Focus();
        }
        private void ProcessInput(string str) { 

            // Обработка различных кнопок
            switch (str)
            {
                case "C": // Очистка дисплея
                    Display.Text = "0";
                    isResultOrError = true;

                    break;

                case "⌫": // Удаление последнего символа
                    if (isResultOrError)
                    {
                        Display.Text = "0";
                        break;
                    }
                    if (Display.Text.Length > 1)
                    {
                        Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
                        isResultOrError = false;
                    }
                    else
                        // Если строка пустая или содержит только один символ
                        Display.Text = "0";
                        isResultOrError = true;
                    break;

                case ",": // В каждом числе может быть только одна запятая :)
                    if (isResultOrError)
                    {
                        // Если это результат или ошибка, начинаем новое число с запятой
                        Display.Text = "0,";
                        isResultOrError = false;
                    }
                    else
                    {
                        // Получаем текущее число (последний элемент после оператора)
                        string currentNumber = GetCurrentNumber(Display.Text);

                        // Проверяем, содержит ли текущее число уже запятую
                        if (!currentNumber.Contains(","))
                        {
                            // Проверяем, что перед запятой есть цифра
                            if (currentNumber.Length > 0)
                            {
                                Display.Text += str;
                            }
                            else
                            {
                                // Если текущее число пустое, добавляем "0,"
                                Display.Text += "0,";
                            }
                        }
                    }
                    break;

                case "=": // Вычисление выражения
                    try
                    {
                        // Проверяем, пусто ли выражение
                        if (string.IsNullOrEmpty(Display.Text))
                        {
                            Display.Text = "Error: empty";
                            break;
                        }

                        // Если последний сивол оператор - удаляем
                        char lastChar = Display.Text[Display.Text.Length - 1];
                        if (IsOperator(lastChar))
                        {
                            Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
                        }

                        // Используем NCalc для вычисления выражения
                        string expression = Display.Text.Replace(",", ".").Replace("×", "*"); // Используем строку с дисплея и заменяем запятые на точки в выражении
                        NCalc.Expression eCalc = new NCalc.Expression(expression);
                        var result = eCalc.Evaluate();

                        if (result == null)
                        {
                            Display.Text = "Error: Invalid expression";
                        }
                        if (result.ToString() == "∞")
                        {
                            Display.Text = "Error: division by 0";
                        }
                        else
                        {
                            Display.Text = result.ToString();
                        }

                        //// Заменяем запятые на точки в выражении
                        //string expression = Display.Text.Replace(",", ".");

                        //// Используем DataTable для вычисления выражения
                        //var result = new DataTable().Compute(expression, null);
                        //if (result.ToString() == "∞")
                        //{
                        //    Display.Text = "Error: division by 0";
                        //}
                        //else
                        //    Display.Text = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        Display.Text = "Error";
                    }
                    isResultOrError = true;
                    break;

                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    if (isResultOrError)
                    {
                        Display.Text = "";
                        Display.Text += str;
                        isResultOrError = false;
                    }
                    else
                        Display.Text += str;
                    break;

                case "+":
                case "-":
                case "×":
                case "/":
                    // Проверяем, что дисплей не пуст и последний символ не является оператором или запятой
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        Display.Text += str;
                        isResultOrError = false;
                    }
                    break;

                case "sci":
                    Display.Text = "0";
                    isResultOrError = true;
                    SwitchToScientific?.Invoke(this, EventArgs.Empty); // Обработчик события перехода к инженерному калькулятору
                    break;

                default: // Добавление символа на дисплей
                    Display.Text += str;
                    isResultOrError = false;
                    break;
            }

            // Вызываем метод для настройки размера шрифта
            AdjustFontSize();
        }

        // Метод для получения текущего числа (последнего числа в выражении)
        protected string GetCurrentNumber(string expression)
        {
            // Разделяем выражение на части по операторам
            char[] operators = { '+', '-', '*', '/', '×' };
            string[] parts = expression.Split(operators);

            // Берем последнюю часть (текущее число)
            string currentNumber = parts[parts.Length - 1];

            return currentNumber;
        }

        // Метод для проверки, является ли символ оператором
        protected bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/' || c == '×';
        }

        // Метод для настройки размера шрифта
        protected void AdjustFontSize()
        {
            // Определяем текущую длину текста
            int textLength = Display.Text.Length;

            // Если длина текста превышает MaxLengthForInitialFontSize, уменьшаем шрифт
            if (textLength > MaxLengthForInitialFontSize)
            {
                // Вычисляем новый размер шрифта
                double newFontSize = InitialFontSize * (MaxLengthForInitialFontSize / (double)textLength);

                // Устанавливаем минимальный размер шрифта (например, 12)
                Display.FontSize = Math.Max(newFontSize, 12);
            }
            else
            {
                // Если длина текста меньше или равна MaxLengthForInitialFontSize, возвращаем начальный размер
                Display.FontSize = InitialFontSize;
            }
        }
    }
}
