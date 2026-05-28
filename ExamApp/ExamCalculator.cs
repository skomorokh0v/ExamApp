using ExamApp;
using System;
namespace ExamApp
{
    public static class ExamCalculator
    {
        public const int MaxModule1 = 10;
        public const int MaxModule2 = 15;
        public const int MaxModule3 = 25;
        public const int MaxModule4 = 25;
        public const int MaxModule5 = 25;
        public static (int sum, int grade) Calculate(
        int module1, int module2, int module3,
        int module4, int module5, string level)
        {
            if (level != "BU" && level != "PU" && level != "PU+")
                throw new ArgumentException("Некорректный уровень экзамена. Допустимые значения: BU, PU, PU + ");
        if (module1 < 0 || module1 > MaxModule1)
                throw new ArgumentException($"Модуль 1 должен быть от 0 до {MaxModule1}");
            if (module2 < 0 || module2 > MaxModule2)
                throw new ArgumentException($"Модуль 2 должен быть от 0 до {MaxModule2}");
            if (module3 < 0 || module3 > MaxModule3)
                throw new ArgumentException($"Модуль 3 должен быть от 0 до {MaxModule3}");
            int sum = module1 + module2 + module3;
            int maxSum;
            switch (level)
            {
                case "BU":
                    maxSum = MaxModule1 + MaxModule2 + MaxModule3;
                    break;
                case "PU":
                    if (module4 < 0 || module4 > MaxModule4)
                        throw new ArgumentException($"Модуль 4 должен быть от 0 до {MaxModule4}");
                    sum += module4;
                    maxSum = MaxModule1 + MaxModule2 + MaxModule3 + MaxModule4;
                    break;
                case "PU+":
                    if (module4 < 0 || module4 > MaxModule4)
                        throw new ArgumentException($"Модуль 4 должен быть от 0 до {MaxModule4}");
                    if (module5 < 0 || module5 > MaxModule5)
                        throw new ArgumentException($"Модуль 5 должен быть от 0 до {MaxModule5}");
                    sum += module4 + module5;
                    maxSum = MaxModule1 + MaxModule2 + MaxModule3 + MaxModule4 + MaxModule5;
                    break;
                default:
                    throw new ArgumentException("Некорректный уровень экзамена");
            }
            double percent = (double)sum / maxSum * 100;
            int grade = percent switch
            {
                >= 80 => 5,
                >= 60 => 4,
                >= 40 => 3,
                _ => 2
            };
            return (sum, grade);
        }
    }
}