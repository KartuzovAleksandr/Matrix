using System;

namespace MatrixMxN
{
    class Matrix
    {
        int[,] data = new int[1, 1];

        int m = 1;
        int n = 1;

        public Matrix(int m, int n)
        {
            Generate(m, n);
        }
        public void Generate(int m, int n)
        {
            this.m = m;
            this.n = n;
            data = new int[m, n]; //создаем пустой массив
            Random random = new Random();
            //заполняем массив данными
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    data[i, j] = random.Next(10, 100);
                }
        }
        public int[,] Transpose()
        {
            int[,] transpose = new int[n, m];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    transpose[j, i] = data[i, j];
                }
            }
            return transpose;
        }
        public float Average()
        {
            if (data == null)
            {
                Console.WriteLine("Матрица не существует!");
                return -1;
            }

            float sum = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sum += data[i, j];
                }
            }
            return sum / (data.GetLength(0) * data.GetLength(1));
        }
        public string GetInfo(bool isTranspose, bool needAlInfo)
        {
            string matrix = isTranspose ? "----Транспонированная матрица------\n" : "----Исходная матрица------\n";
            var array = isTranspose ? Transpose() : data;

            int row = isTranspose ? n : m;
            int col = isTranspose ? m : n;

            if (needAlInfo)
            {
                matrix += isTranspose ? $"Размеры: {n}x{m}\n" : $"Размеры: {m}x{n}\n";
                matrix += $"Среднее значение {Average()} \n";
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    matrix += array[i, j].ToString() + "\t";
                }
                matrix += "\n";
            }
            return matrix;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int m = 0;
            int n = 0;
            int errors = 0;
            while ((m == 0) && (n == 0))
            {
                Console.WriteLine("Введите желаемую размерность матрицы в формате MхN, где M и N - целые числа");
                Console.WriteLine("Например, 10x5");
                Console.Write("Размерность матрицы: ");
                string[] strings = Console.ReadLine().Split('x');
                if ((strings.Length < 2) || (int.TryParse(strings[0], out m) == false) || (int.TryParse(strings[1], out n) == false))
                {
                    errors++;
                    Console.WriteLine($"Допущено ошибок ввода: {errors}");
                    if ((errors > 1) && (errors < 4))
                    {
                        Console.Beep();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Не издевайся надо мной! Пиши так: целое число, затем - маленький ИКС (английская раскладка!), затем - опять целое число. Пробелы ставить не надо");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    else
                        if (errors == 4)
                    {
                        Console.Beep();
                        Console.Beep();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ты безнадежен, человек. Закрой программу и иди домой");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                }
            }
            if (errors < 4)
            {
                Matrix matrix = new Matrix(m, n);
                Console.WriteLine(matrix.GetInfo(false, true));
                Console.WriteLine(matrix.GetInfo(true, false));
            }
        }
    }
}