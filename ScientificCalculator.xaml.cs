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
using NCalc;
using MathNet.Numerics;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ScientificCalculator.xaml
    /// </summary>
    public partial class ScientificCalculator : UserControl
    {
        // Создаем событие для переключения на обычный калькулятор
        public event EventHandler SwitchToBasic;

        public ScientificCalculator()
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
                    str = isShiftPressed ? "×" : "8"; // Shift + 8 -> "×", иначе "8"
                    break;
                case Key.D9:
                case Key.NumPad9:
                    str = isShiftPressed ? "(" : "9"; // Shift + 9 -> "(", иначе "9"
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

            // Снимаем фокус с кнопки, чтобы фокус оставался на TextBox
            if (str != "bas")
                // Снимаем фокус с кнопки, чтобы фокус оставался на TextBox
                Display.Focus();
        }

        private void ProcessInput(string str)
        {

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
                    {
                        // Если строка пустая или содержит только один символ
                        Display.Text = "0";
                        isResultOrError = true;
                    }
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

                        // Проверяем, есть ли в выражении символ "^"
                        if (Display.Text.Contains("^"))
                        {
                            // Разделяем выражение на основание и показатель степени
                            string[] parts = Display.Text.Split('^');
                            if (parts.Length == 2)
                            {
                                // Преобразуем основание в число
                                if (double.TryParse(parts[0], out double baseNumber))
                                {
                                    // Проверяем, является ли показатель степени корнем (начинается с "(1/")
                                    if (parts[1].StartsWith("(1/"))
                                    {
                                        // Это корень произвольной степени
                                        string exponentStr = parts[1].Substring(3); // Убираем "(1/"

                                        // Проверяем, есть ли закрывающая скобка
                                        if (exponentStr.EndsWith(")"))
                                        {
                                            exponentStr = exponentStr.Substring(0, exponentStr.Length - 1); // Убираем ")"
                                        }

                                        if (double.TryParse(exponentStr, out double rootExponent))
                                        {
                                            if (rootExponent == 0)
                                            {
                                                Display.Text = "Error: Division by zero";
                                            }
                                            else if (baseNumber < 0 && Math.Abs(rootExponent % 2) < 1e-10)
                                            {
                                                // Если основание отрицательное, а степень корня четная
                                                Display.Text = "Error: Negative base with even root";
                                            }
                                            else
                                            {
                                                // Вычисляем корень произвольной степени: x^(1/y)
                                                double result = Math.Pow(baseNumber, 1 / rootExponent);
                                                Display.Text = result.ToString();
                                            }
                                        }
                                        else
                                        {
                                            Display.Text = "Error: Invalid exponent";
                                        }
                                    }
                                    else
                                    {
                                        // Это возведение в степень
                                        if (double.TryParse(parts[1], out double exponent))
                                        {
                                            if (baseNumber < 0 && !IsInteger(exponent))
                                            {
                                                // Если основание отрицательное, а степень не целая
                                                Display.Text = "Error: Negative base with non-integer exponent";
                                            }
                                            else
                                            {
                                                // Вычисляем степень: x^y
                                                double result = Math.Pow(baseNumber, exponent);
                                                Display.Text = result.ToString();
                                            }
                                        }
                                        else
                                        {
                                            Display.Text = "Error: Invalid exponent";
                                        }
                                    }
                                }
                                else
                                {
                                    Display.Text = "Error: Invalid base number";
                                }
                            }
                            else
                            {
                                Display.Text = "Error: Invalid expression";
                            }
                        }

                        else
                        {
                            // Используем NCalc для вычисления выражения
                            string expression = Display.Text
                            .Replace(",", ".")
                            .Replace("×", "*"); // Используем строку с дисплея и заменяем запятые на точки в выражении
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
                        }             

                    }
                    catch (Exception ex)
                    {
                        Display.Text = "Error: Invalid input";
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

                case "(":
                case ")":
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
                    // Проверяем, что дисплей не пуст и последний символ не является оператором или запятой
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        Display.Text += str;
                        isResultOrError = false;
                    }

                    // Разрешаем ввод "+" или "-" только после "e" или в начале выражения
                    if (!isResultOrError && Display.Text.Length > 0 && Display.Text[Display.Text.Length - 1] == 'e')
                    {
                        Display.Text += str;
                        isResultOrError = false;
                    }
                    break;

                case "*":
                case "/":
                case "×":
                    // Проверяем, что дисплей не пуст и последний символ не является оператором или запятой
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        Display.Text += str;
                        isResultOrError = false;
                    }
                    break;

                case "sin": // Синус
                case "cos": // Косинус
                case "tan": // Тангенс
                case "cot": // Котангенс
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        try
                        {
                            // Вычисляем выражение на экране
                            string expression = Display.Text
                                .Replace(",", ".")
                                .Replace("×", "*");

                            NCalc.Expression eCalc = new NCalc.Expression(expression);
                            var result = eCalc.Evaluate();

                            if (result == null)
                            {
                                Display.Text = "Error: Invalid expression";
                                break;
                            }

                            // Преобразуем результат в double
                            if (double.TryParse(result.ToString(), out double number))
                            {
                                // Преобразуем градусы в радианы
                                double radians = DegreeToRadian(number);

                                // Вычисляем значение тригонометрической функции
                                double trigResult = 0;
                                bool isError = false;

                                switch (str)
                                {
                                    case "sin":
                                        trigResult = Math.Sin(radians);
                                        break;

                                    case "cos":
                                        trigResult = Math.Cos(radians);
                                        break;

                                    case "tan":
                                        // Проверка на деление на ноль (когда cos(radians) = 0)
                                        if (Math.Abs(Math.Cos(radians)) < 1e-10)
                                        {
                                            Display.Text = "Error: Division by zero (tan is undefined)";
                                            isError = true;
                                        }
                                        else
                                        {
                                            trigResult = Math.Tan(radians);
                                        }
                                        break;

                                    case "cot":
                                        // Проверка на деление на ноль (когда sin(radians) = 0)
                                        if (Math.Abs(Math.Sin(radians)) < 1e-10)
                                        {
                                            Display.Text = "Error: Division by zero (cot is undefined)";
                                            isError = true;
                                        }
                                        else
                                        {
                                            trigResult = 1 / Math.Tan(radians);
                                        }
                                        break;
                                }

                                // Если ошибки не было, выводим результат
                                if (!isError)
                                {
                                    // Проверка на корректность результата
                                    if (double.IsInfinity(trigResult) || double.IsNaN(trigResult))
                                    {
                                        Display.Text = "Error: Invalid result";
                                    }
                                    else
                                    {
                                        Display.Text = trigResult.ToString();
                                    }
                                }
                            }
                            else
                            {
                                Display.Text = "Error: Invalid input";
                            }
                        }
                        catch (Exception ex)
                        {
                            Display.Text = "Error: " + ex.Message;
                        }
                        isResultOrError = true;
                    }
                    break;

                case "log": // Логарифм по основанию 10
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        try
                        {
                            // Вычисляем выражение на экране
                            string expression = Display.Text
                                .Replace(",", ".")
                                .Replace("×", "*");

                            NCalc.Expression eCalc = new NCalc.Expression(expression);
                            var result = eCalc.Evaluate();

                            if (result == null)
                            {
                                Display.Text = "Error: Invalid expression";
                                break;
                            }

                            // Преобразуем результат в double
                            if (double.TryParse(result.ToString(), out double number))
                            {
                                // Проверяем, является ли число положительным
                                if (number <= 0)
                                {
                                    Display.Text = "Error: Negative input";
                                }
                                else
                                {
                                    // Вычисляем логарифм по основанию 10
                                    double logResult = Math.Log10(number);
                                    Display.Text = logResult.ToString();
                                }
                            }
                            else
                            {
                                Display.Text = "Error: Invalid input";
                            }
                        }
                        catch (Exception ex)
                        {
                            Display.Text = "Error: " + ex.Message;
                        }
                        isResultOrError = true;
                    }
                    break;

                case "ln": // Натуральный логарифм
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        try
                        {
                            // Вычисляем выражение на экране
                            string expression = Display.Text
                                .Replace(",", ".")
                                .Replace("×", "*");

                            NCalc.Expression eCalc = new NCalc.Expression(expression);
                            var result = eCalc.Evaluate();

                            if (result == null)
                            {
                                Display.Text = "Error: Invalid expression";
                                break;
                            }

                            // Преобразуем результат в double
                            if (double.TryParse(result.ToString(), out double number))
                            {
                                // Проверяем, является ли число положительным
                                if (number <= 0)
                                {
                                    Display.Text = "Error: Negative input";
                                }
                                else
                                {
                                    // Вычисляем натуральный логарифм
                                    double lnResult = Math.Log(number);
                                    Display.Text = lnResult.ToString();
                                }
                            }
                            else
                            {
                                Display.Text = "Error: Invalid input";
                            }
                        }
                        catch (Exception ex)
                        {
                            Display.Text = "Error: " + ex.Message;
                        }
                        isResultOrError = true;
                    }
                    break;

                case "x!": // Факториал
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        try
                        {
                            // Вычисляем выражение на экране
                            string expression = Display.Text
                                .Replace(",", ".")
                                .Replace("×", "*");

                            NCalc.Expression eCalc = new NCalc.Expression(expression);
                            var result = eCalc.Evaluate();

                            if (result == null)
                            {
                                Display.Text = "Error: Invalid expression";
                                break;
                            }

                            // Преобразуем результат в double
                            if (double.TryParse(result.ToString(), out double number))
                            {
                                // Проверяем, является ли число отрицательным
                                if (number < 0)
                                {
                                    Display.Text = "Error: Negative input";
                                }
                                else
                                {
                                    // Вычисляем факториал
                                    double factorialResult = Factorial(number);
                                    Display.Text = factorialResult.ToString();
                                }
                            }
                            else
                            {
                                Display.Text = "Error: Invalid input";
                            }
                        }
                        catch (Exception ex)
                        {
                            Display.Text = "Error: " + ex.Message;
                        }
                        isResultOrError = true;
                    }
                    break;

                case "x²": // Возведение в квадрат
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        string currentNumber = GetCurrentNumber(Display.Text);
                        if (double.TryParse(currentNumber, out double number))
                        {
                            double result = Math.Pow(number, 2);
                            Display.Text = result.ToString();
                            isResultOrError = true;
                        }
                        else
                        {
                            Display.Text = "Error: Invalid input";
                            isResultOrError = true;
                        }
                    }
                    break;

                case "xʸ": // Возведение в степень
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        // Проверяем, что на экране есть только одно число (без операторов)
                        if (!Display.Text.Contains("+") && !Display.Text.Contains("-") &&
                            !Display.Text.Contains("*") && !Display.Text.Contains("/") &&
                            !Display.Text.Contains("×") && !Display.Text.Contains("^"))
                        {
                            // Добавляем символ "^" для обозначения степени
                            Display.Text += "^";
                            isResultOrError = false;
                        }
                        else
                        {
                            // Если на экране уже есть операторы, выводим ошибку
                            Display.Text = "Error: Operators when calculating the degree";
                            isResultOrError = true;
                        }
                    }
                    break;

                case "√x": // Квадратный корень
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        try
                        {
                            // Вычисляем выражение на экране
                            string expression = Display.Text
                                .Replace(",", ".")
                                .Replace("×", "*");

                            NCalc.Expression eCalc = new NCalc.Expression(expression);
                            var result = eCalc.Evaluate();

                            if (result == null)
                            {
                                Display.Text = "Error: Invalid expression";
                                break;
                            }

                            // Преобразуем результат в double
                            if (double.TryParse(result.ToString(), out double number))
                            {
                                // Проверяем, является ли число неотрицательным
                                if (number < 0)
                                {
                                    Display.Text = "Error: Input must be non-negative";
                                }
                                else
                                {
                                    // Вычисляем квадратный корень
                                    double sqrtResult = Math.Sqrt(number);
                                    Display.Text = sqrtResult.ToString();
                                }
                            }
                            else
                            {
                                Display.Text = "Error: Invalid input";
                            }
                        }
                        catch (Exception ex)
                        {
                            Display.Text = "Error: " + ex.Message;
                        }
                        isResultOrError = true;
                    }
                    break;

                case "ʸ√x": // Корень произвольной степени
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        // Проверяем, что на экране есть только одно число (без операторов)
                        if (!Display.Text.Contains("+") && !Display.Text.Contains("-") &&
                            !Display.Text.Contains("*") && !Display.Text.Contains("/") &&
                            !Display.Text.Contains("×") && !Display.Text.Contains("^"))
                        {
                            // Добавляем символ "^(1/" для обозначения корня произвольной степени
                            Display.Text += "^(1/";      
                            isResultOrError = false;
                        }
                        else
                        {
                            // Если на экране уже есть операторы, выводим ошибку
                            Display.Text = "Error: Operators when calculating the root";
                            isResultOrError = true;
                        }
                    }
                    break;

                case "eˣ": // Экспонента
                           // Разрешаем ввод "e" только если это не первый символ и предыдущий символ не оператор
                    if (!isResultOrError && Display.Text.Length > 0 && !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        Display.Text += "e";
                        isResultOrError = false;
                    }
                    break;

                case "+/-": // Кнопка "negative" (изменение знака числа)
                    
                    break;

                case "bas":
                    Display.Text = "0";
                    isResultOrError = true;
                    SwitchToBasic?.Invoke(this, EventArgs.Empty); // Обработчик события перехода к инженерному калькулятору
                    break;

                default: // Добавление символа на дисплей
                    Display.Text += str;
                    isResultOrError = false;
                    break;
            }

            // Вызываем метод для настройки размера шрифта
            AdjustFontSize();
        }

        // Метод для вычисления факториала (включая дробные числа)
        protected double Factorial(double n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Факториал отрицательного числа не определен.");
            }
            return SpecialFunctions.Gamma(n + 1); // Гамма-функция для вычисления факториала
        }

        // Метод для проверки, является ли число целым
        protected bool IsInteger(double number)
        {
            return Math.Abs(number % 1) < 1e-10;
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
