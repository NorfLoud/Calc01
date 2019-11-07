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
                catch (Exception) { }
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

            _Funcs = new Func[4];
            _Funcs[0] = null;
            _Funcs[1] = Sum;
            _Funcs[2] = Sub;
            _Funcs[3] = Mul;
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
                    double x1 = GetArg("первый"), x2 = GetArg("второй");

                    bool funcResult = _Funcs[Command](x1, x2, out double result);
                    if (funcResult)
                    {
                        
                    }

                    Console.WriteLine("/-----/\n" + "Результат выполнения операции равен: " + result.ToString() + "\n/-----/");
                    // Console.WriteLine("Нажмите Enter для продолжения."); Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("/-----/\n" + "Введена не верная команда!" + "\n/-----/");
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
                catch
                {
                    result = null;
                }
            } while (result == null);

            return (double)result;
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
    }
}
