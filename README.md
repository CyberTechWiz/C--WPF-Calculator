# C# WPF .NET 7.0.

## Калькулятор из ада
Программа использует следующие библиотеки:
1. CoreCLR-NCalc;
2. MathNet.Numerics (для подсчёта факториала дробных чисел).

Инструкция:
0. Переключение на инженерный/обычный режим осуществляется с помощью кнопки "sci"/"bas" в левом нижнем углу;

1. log, ln, cos, sin, tan, cot, факториал рассчитываются мгновенно на основе числа, введенного на экране.

2. Возможно переполнение (В NCalc по умолчанию используется Int32 для целых чисел). Если нужно посчитать большие числа, умножайте одно из них на "1,0" или дописывайте в конец числа ",0":
Пример: "1,0 * 1000000 * 1000000" или "1000000,0 * 1000000"

3. Можно набирать с клавиатуры. Esc- Очистить экран (С).

4. При вводе корня произвольной степени закрывающую скобку можно не ставить.

5. Чтобы сменить тему надо нажать на шестеренку в левом углу на верхней панели управления.

6. Файл "Calc 1.0.dll.config" сохраняет название последней установленной темы для последующего запуска программы с этой темой.

## WpfApp1
 The program uses the following libraries:
1. CoreCLR-NCalc;
2. MathNet.Numerics (for calculating the factorial of fractional numbers).

Instructions: 
0. Переключение на инженерный/обычный режим осуществляется с помощью кнопки "sci"/"bas" в левом нижнем углу;

1. log, ln, cos, sin, tan, cot, factorial are calculated instantly from the number that is entered on the screen.

2. Overflow is possible (In NCalc, Int32 is used by default for integers). If you need to count large numbers, multiply one of them by "1,0" or add ",0" to the end of the number: Example: "1,0 * 1000000 * 1000000" или "1000000,0 * 1000000"

3. You can type from the keyboard. Esc- Clear the screen (C).

4. When entering a root of any degree, you don't need to put a closing parenthesis.

5. To change the theme, click on the gear in the left corner of the upper control panel.

6. The file "Calc 1.0.dll.config" saves the name of the last installed theme for the subsequent launch of the program with this theme.
