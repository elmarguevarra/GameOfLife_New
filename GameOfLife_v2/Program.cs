using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {

            var matrix = new bool[3, 3] { { true, false, false }, { true, false, false }, { true, false, false } };
            var iteration = 1;

            var results = EvaluateGameOfLife(matrix, iteration);
            Display(matrix);
        }

        private static void Display(bool[,] results)
        {
            for (int i = 0; i <= results.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= results.GetUpperBound(1); j++)
                {
                    Console.Write(results[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static bool[,] EvaluateGameOfLife(bool[,] matrix, int iteration)
        {
            for (int i = 0; i < iteration; i++)
            {
                int x = matrix.GetUpperBound(0);
                int y = matrix.GetUpperBound(1);

                var yctr = 0;
                var xctr = 0;

                LoopHorizontally(matrix, xctr, yctr);
            }
            return matrix;
        }

        private static void LoopHorizontally(bool[,] matrix, int xctr, int yctr)
        {
            int x = matrix.GetUpperBound(0);
            int y = matrix.GetUpperBound(1);

            for (xctr = 0; xctr <= x; xctr++)
            {
                if (yctr < y)
                {
                    if (matrix[xctr, yctr])
                    {
                        if (matrix[xctr + 1, yctr] == false)
                        {
                            matrix[xctr, yctr] = false;
                        }
                        if (xctr != 0)
                        {
                            if (matrix[xctr - 1, yctr] == false)
                            {
                                matrix[xctr, yctr] = false;
                            }
                        }

                        LoopVertically(matrix, xctr, yctr);
                    }
                }
            }
        }

        private static void LoopVertically(bool[,] matrix, int xctr, int yctr)
        {
            int y = matrix.GetUpperBound(1);

            for (yctr = 0; yctr <= y; yctr++)
            {

                IFCurrentIsAlive(matrix, xctr, yctr);
                IFYisnotZero(matrix, xctr, yctr);

                //if (matrix[xctr, yctr + 1] == false)
                //{
                //    matrix[xctr, yctr] = false;
                //}
                //if (yctr != 0)
                //{
                //    if (matrix[xctr, yctr - 1] == false)
                //    {
                //        matrix[xctr, yctr] = false;
                //    }
                //}
            }
        }

        private static void IFCurrentIsAlive(bool[,] matrix, int xctr, int yctr)
        {
            switch (matrix[xctr, yctr + 1])
            {
                case false:
                    matrix[xctr, yctr] = false;
                    break;
                default:
                    break;
            }
        }

        private static void IFYisnotZero(bool[,] matrix, int xctr, int yctr)
        {
            if (matrix[xctr, yctr + 1] == false)
            {
                matrix[xctr, yctr] = false;
            }
            if (yctr != 0)
            {
                IFCurrentIsAlive(matrix, xctr, yctr);
                if (matrix[xctr, yctr - 1] == false)
                {
                    matrix[xctr, yctr] = false;
                }
            }
        }
    }
}
