using MathNet.Numerics.LinearAlgebra;
using System;

namespace level_network_adjustment
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu.MainMenu();
            while (true)
            {
                //获取ConsoleKeyInfo并不显示输入按键字符
                ConsoleKeyInfo key = Console.ReadKey(true);

                //Esc退出
                switch (key.Key)
                {
                    case ConsoleKey.F1:
                        Menu.ConditionMenu();
                        break;
                    case ConsoleKey.F2:
                        Menu.IndirectAdjustment();
                        break;
                    case ConsoleKey.F3:
                        Menu.Help();
                        break;
                    case ConsoleKey.F4:
                        Environment.Exit(0);
                        break;


                }

            }
        }


    }
    class Menu
    {
        public static void MainMenu()
        {
            Console.WriteLine("Conditions(F1)\t\tIndirect(F2)\tHelp(F3)\tExit(F4)\n");
            Console.WriteLine();
        }
        public static void ConditionMenu()
        {

            Console.Clear();
            Console.WriteLine("Input Coefficient Matrix Called 'A' ");
            Matrix<double> A = ModifyMatrix.Input();
            Console.WriteLine("Input Co-factor Matrix Called 'Q'");
            Matrix<double> Q = ModifyMatrix.Input();
            Console.WriteLine("Input Constant Matrix Called 'W'");
            Matrix<double> W = ModifyMatrix.Input();
            Adjustment adjustment = new Adjustment();
            var result = adjustment.ConditionAdjustment(A, Q, W);
            Console.Clear();
            Console.WriteLine("The Condition Adjustment result is :");
            Console.WriteLine(result);

        }
        public static void IndirectAdjustment()
        {
            Console.Clear();
            Console.WriteLine("Input Coefficient Matrix Called 'B' ");
            Matrix<double> B = ModifyMatrix.Input();
            Console.WriteLine("Input Weight Matrix Called 'P'");
            Matrix<double> P = ModifyMatrix.Input();
            Console.WriteLine("Input Constant Matrix Called 'l'");
            Matrix<double> l = ModifyMatrix.Input();
            Adjustment adjustment = new Adjustment();
            var result = adjustment.IndirectAdjustment(B, P, l);
            Console.Clear();
            Console.WriteLine("The Indirect Adjustment result is :");
            Console.WriteLine(result);

        }
        public static void Help()
        {

            Console.WriteLine("\tIf you can't solve problem,please check your matrix.");
            Console.WriteLine("\t\t\t\tBy Icye 2017.12.17");

        }
    }

    class ModifyMatrix
    {
        /// <summary>
        /// matrix class
        /// input or modify
        /// </summary>
        /// <returns></returns>
        public static Matrix<double> Input()
        {
            int row = 0;
            int col = 0;
            var M = Matrix<double>.Build;
            Console.WriteLine("Please input rows:");
            row = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Please input columns:");
            col = Convert.ToInt16(Console.ReadLine());

            var matrix = M.Dense(row, col);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    double input;
                    Console.Write("Enter value for ({0},{1}): ", i, j);
                    while (!double.TryParse(Console.ReadLine(), out input))
                    {
                        Console.Write("Enter correct value for ({0},{1}): ", i, j);
                    }
                    matrix[i, j] = input;
                }
            }
            Console.Clear();
            Console.WriteLine("The matrix is :");
            Console.WriteLine(matrix);
            return matrix;
        }
        public static Matrix<double> Modify(Matrix<double> matrix)
        {
            int row = 0;
            int col = 0;
            double input;
            Console.Write("Please input rows: ");
            row = Convert.ToInt16(Console.ReadLine());
            Console.Write("Please input columns: ");
            col = Convert.ToInt16(Console.ReadLine());
            Console.Write("The old value is: ");
            Console.WriteLine(matrix[row, col]);
            Console.Write("Input the new value : ");
            while (!double.TryParse(Console.ReadLine(), out input))
            {
                Console.Write("Enter correct value for ({0},{1}): ", row, col);
            }
            matrix[row, col] = input;
            Console.Clear();
            Console.Write("The new matrix is: ");
            Console.WriteLine(matrix.ToMatrixString());
            return matrix;
        }
    }
    class Adjustment
    {
        public Matrix<double> ConditionAdjustment(Matrix<double> A, Matrix<double> Q, Matrix<double> W)
        {
            var NAA = A * Q * A.Transpose();
            var K = -NAA.Inverse() * W; ;
            var V = Q * A.Transpose() * K;
            return V;
        }
        public Matrix<double> IndirectAdjustment(Matrix<double> B, Matrix<double> P, Matrix<double> l)
        {
            var NBB = B.Transpose() * P * B;
            var W = B.Transpose() * P * l;
            var x = NBB.Inverse() * W;
            var V = B * x - l;
            return V;
        }
    }
}