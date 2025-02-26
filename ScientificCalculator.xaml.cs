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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NCalc;

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
                        string expression = Display.Text
                            .Replace(",", ".")
                            .Replace("×", "*"); // Используем строку с дисплея и заменяем запятые на точки в выражении
                            //.Replace("^", "Pow"); // Заменяем "^" на "Pow"
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
                    // Проверяем, что дисплей не пуст и последний символ не является оператором или запятой
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        Display.Text += str;
                        isResultOrError = false;
                    }
                    break;

                case "cos":
                case "sin":
                case "tan":
                case "cot":
                    // Проверяем, что дисплей не пуст и последний символ не является оператором или запятой
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        // Получаем текущее число
                        string currentNumber = GetCurrentNumber(Display.Text);

                        // Пытаемся преобразовать текущее число в double
                        if (double.TryParse(currentNumber, out double number))
                        {
                            // Преобразуем градусы в радианы
                            double radians = DegreeToRadian(number);

                            // Вычисляем значение тригонометрической функции
                            double result = 0;
                            switch (str)
                            {
                                case "cos":
                                    result = Math.Cos(radians);
                                    break;
                                case "sin":
                                    result = Math.Sin(radians);
                                    break;
                                case "tan":
                                    result = Math.Tan(radians);
                                    break;
                                case "cot":
                                    result = 1 / Math.Tan(radians); // Котангенс = 1 / тангенс
                                    break;
                            }

                            // Выводим результат на дисплей
                            Display.Text = result.ToString();
                            isResultOrError = true;
                        }
                        else
                        {
                            // Если не удалось преобразовать в число, выводим ошибку: неверный ввод
                            Display.Text = "Error: Invalid input";
                            isResultOrError = true;
                        }
                    }
                    break;

                case "log": // Логарифм по основанию 10
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        string currentNumber = GetCurrentNumber(Display.Text);
                        if (double.TryParse(currentNumber, out double number) && number > 0)
                        {
                            double result = Math.Log10(number);
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

                case "ln": // Натуральный логарифм
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        string currentNumber = GetCurrentNumber(Display.Text);
                        if (double.TryParse(currentNumber, out double number) && number > 0)
                        {
                            double result = Math.Log(number);
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

                case "x!": // Факториал
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        string currentNumber = GetCurrentNumber(Display.Text);
                        if (int.TryParse(currentNumber, out int number) && number >= 0)
                        {
                            double result = Factorial(number);
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
                        Display.Text += "^"; // Используем символ "^" для обозначения степени
                        isResultOrError = false;
                    }
                    break;

                case "√x": // Квадратный корень
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        string currentNumber = GetCurrentNumber(Display.Text);
                        if (double.TryParse(currentNumber, out double number) && number >= 0)
                        {
                            double result = Math.Sqrt(number);
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

                case "ʸ√x": // Корень произвольной степени
                    if (!string.IsNullOrEmpty(Display.Text) &&
                        !IsOperator(Display.Text[Display.Text.Length - 1]))
                    {
                        Display.Text += "^(1/"; // Используем символ "^" для обозначения корня
                        isResultOrError = false;
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


                case "bas":
                    Display.Text = "0";
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

        // Метод для вычисления факториала
        protected double Factorial(int n)
        {
            if (n == 0 || n == 1)
                return 1;
            return n * Factorial(n - 1);
        }

        // Метод для получения текущего числа (последнего числа в выражении)
        protected string GetCurrentNumber(string expression)
        {
            // Разделяем выражение на части по операторам
            char[] operators = { '+', '-', '*', '/' };
            string[] parts = expression.Split(operators);

            // Берем последнюю часть (текущее число)
            string currentNumber = parts[parts.Length - 1];

            return currentNumber;
        }

        // Метод для проверки, является ли символ оператором
        protected bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
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
