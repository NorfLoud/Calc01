using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc01
{
    class Program
    {
        static void Main(string[] args)
        {
            UserConsole _UserConsole = new UserConsole();

            Console.WriteLine("Добро пожаловать в программу \"Калькулятор\"");
            while (_UserConsole.Command != 0)
            {
                _UserConsole.CommandList();

                try
                {
                    Console.Write("Введите код выбранной команды: ");
                    _UserConsole.DoOperation(Convert.ToInt32(Console.ReadLine()));      // Ожидаем ввода кода команды
                }
                catch (Exception ex) 
                {
                    _UserConsole.MessageOutput("Сообщение: " + ex.Message + "\nВведена не допустимая команда! Попробуйте снова.");
                }
            }
        }
    }

    public class UserConsole : Functions
    {
        private Func[] _Funcs;
        private int _Command;

        /// <summary>
        /// Свойство, содержащее код текущей команды
        /// </summary>
        public int Command
        {
            get => _Command;
            private set
            {
                if (_Command != value)
                {
                    _Command = value;
                }
            }
        }

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public UserConsole()
        {
            Command = -1;

            _Funcs = new Func[5];
            _Funcs[0] = null;
            _Funcs[1] = Sum;
            _Funcs[2] = Sub;
            _Funcs[3] = Mul;
            _Funcs[4] = Div;
        }

        /// <summary>
        /// Метод выводит в консоль список доступных команд
        /// </summary>
        public void CommandList()
        {
            Console.WriteLine("Список команд:");
            Console.WriteLine("0 - выход из программы;");
            Console.WriteLine("1 - вычислить сумму;");
            Console.WriteLine("2 - вычислить разность;");
            Console.WriteLine("3 - вычислить произведение;");
            Console.WriteLine("4 - вычислить частоное от деления;");
        }

        /// <summary>
        /// Метод выполняет переданную ему команду
        /// </summary>
        /// <param name="aCode">Код команды</param>
        public void DoOperation(int aCode)
        {
            Command = aCode;

            if (Command > 0)
            {
                if (Command < _Funcs.Length)
                {
                    double x1, x2, result;

                    do
                    {
                        x1 = GetArg("первый");
                        x2 = GetArg("второй");
                    } while (!_Funcs[Command](x1, x2, out result));

                    MessageOutput("Результат выполнения операции равен: " + result.ToString());
                    // Console.WriteLine("Нажмите Enter для продолжения."); Console.ReadLine();
                }
                else
                {
                    MessageOutput("Введена не верная команда!");
                }
            }
        }

        /// <summary>
        /// Метод для получения значения аргументов функции
        /// </summary>
        /// <param name="aStr">Строка, указывающая на порядковый номер аргумента</param>
        /// <returns></returns>
        private double GetArg(string aStr)
        {
            double? result;

            do
            {
                Console.Write("Введите " + aStr + " аргумент: ");
                try
                {
                    result = Convert.ToDouble(Console.ReadLine().Replace('.', ','));
                }
                catch (Exception ex)
                {
                    result = null;
                    MessageOutput("Сообщение: " + ex.Message);
                }
            } while (result == null);

            return (double)result;
        }

        /// <summary>
        /// Метод для вывода сообщений программы в обрамлении полей
        /// </summary>
        /// <param name="aStr">Строка с сообщением для вывода</param>
        public void MessageOutput(string aStr)
        {
            Console.WriteLine("/-----/\n" + aStr + "\n/-----/");
        }
    }

    public abstract class Functions
    {
        protected delegate bool Func(double aArg1, double aArg2, out double result);

        /// <summary>
        /// Метод вычисляет сумму двух аргументов
        /// </summary>
        /// <param name="aArg1">Первый агрумент</param>
        /// <param name="aArg2">Второй аргумент</param>
        /// <param name="result">Результат выполнения операции над аргументами</param>
        /// <returns></returns>
        protected bool Sum(double aArg1, double aArg2, out double result)
        {
            result = 0.0;

            try
            {
                result = aArg1 + aArg2;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Метод вычисляет разность двух аргументов
        /// </summary>
        /// <param name="aArg1">Первый агрумент</param>
        /// <param name="aArg2">Второй аргумент</param>
        /// <param name="result">Результат выполнения операции над аргументами</param>
        /// <returns></returns>
        protected bool Sub(double aArg1, double aArg2, out double result)
        {
            result = 0.0;

            try
            {
                result = aArg1 - aArg2;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Метод вычисляет произведение двух аргументов
        /// </summary>
        /// <param name="aArg1">Первый агрумент</param>
        /// <param name="aArg2">Второй аргумент</param>
        /// <param name="result">Результат выполнения операции над аргументами</param>
        /// <returns></returns>
        protected bool Mul(double aArg1, double aArg2, out double result)
        {
            result = 0.0;

            try
            {
                result = aArg1 * aArg2;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Метод вычисляет результат деления первого аргумента на второй
        /// </summary>
        /// <param name="aArg1">Делимое</param>
        /// <param name="aArg2">Делитель</param>
        /// <param name="result">Реузльтат выполнения операции над аргументами</param>
        /// <returns></returns>
        protected bool Div(double aArg1, double aArg2, out double result)
        {
            result = 0.0;

            try
            {
                result = aArg1 / aArg2;
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
