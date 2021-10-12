using System;

namespace GameOfLife
{
    class Program
    {

        static void Main(string[] args)
        {
            var matrix = new bool[5, 5] {
                { false, true, false, true, false },
                { false, false, true, true, false },
                { false, true, false, true, false },
                { false, true, false, true, false },
                { false, true, false, true, false }
            };

            //var matrix = new bool[3, 3] {
            //        { false, true, false },
            //        { true, true, true },
            //        { false, true, false },
            //};

            var iteration = 11;
            var results = EvaluateGameOfLife(matrix, iteration);
        }

        public static int[,] EvaluateGameOfLife(bool[,] lifeMatrix, int iteration)
        {
            var nbMaxtrix = new int[lifeMatrix.GetUpperBound(0) + 1, lifeMatrix.GetUpperBound(1) + 1];

            int xLimit = lifeMatrix.GetUpperBound(0);
            int yLimit = lifeMatrix.GetUpperBound(1);

            for (int i = 0; i < iteration; i++)
            {

                for (int xctr = 0; xctr <= xLimit; xctr++)
                {

                    for (int yctr = 0; yctr <= yLimit; yctr++)
                    {
                        var aliveNb = 0;

                        aliveNb = CheckVertically(lifeMatrix, xctr, yctr, xLimit, yLimit, aliveNb);
                        aliveNb = CheckHorizontally(lifeMatrix, xctr, yctr, yLimit, aliveNb);

                        nbMaxtrix[xctr, yctr] = aliveNb;
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Iteration: " + i);
                Display(lifeMatrix);
                LifePerIteration(lifeMatrix, nbMaxtrix);
                Display(nbMaxtrix);
            }
            return nbMaxtrix;
        }

        private static int CheckVertically(bool[,] lifeMatrix, int xctr, int yctr, int xLimit, int yLimit, int aliveNb)
        {
            if (xctr + 1 <= xLimit)
            {
                if (lifeMatrix[xctr + 1, yctr])
                {
                    aliveNb++;
                }
                aliveNb = CheckDiagonally(lifeMatrix, xctr + 1, yctr, yLimit, aliveNb);
            }
            if (xctr != 0)
            {
                if (lifeMatrix[xctr - 1, yctr])
                {
                    aliveNb++;
                }
                aliveNb = CheckDiagonally(lifeMatrix, xctr - 1, yctr, yLimit, aliveNb);
            }
            return aliveNb;
        }

        private static int CheckHorizontally(bool[,] lifeMatrix, int xctr, int yctr, int yLimit, int aliveNb)
        {
            if (yctr + 1 <= yLimit)
            {
                if (lifeMatrix[xctr, yctr + 1])
                {
                    aliveNb++;
                }
            }
            if (yctr != 0)
            {
                if (lifeMatrix[xctr, yctr - 1])
                {
                    aliveNb++;
                }
            }

            return aliveNb;
        }

        private static int CheckDiagonally(bool[,] lifeMatrix, int xctr, int yctr, int yLimit, int aliveNb)
        {
            return CheckHorizontally(lifeMatrix, xctr, yctr, yLimit, aliveNb);
        }

        private static int[,] LifePerIteration(bool[,] lifeMatrix, int[,] nbMaxtrix)
        {
            int xLimit = nbMaxtrix.GetUpperBound(0);
            int yLimit = nbMaxtrix.GetUpperBound(1);

            for (int xctr = 0; xctr <= xLimit; xctr++)
            {
                for (int yctr = 0; yctr <= yLimit; yctr++)
                {
                    if (lifeMatrix[xctr, yctr])
                    {
                        if (nbMaxtrix[xctr, yctr] == 1 || nbMaxtrix[xctr, yctr] == 0 || nbMaxtrix[xctr, yctr] >= 4)
                        {
                            lifeMatrix[xctr, yctr] = false;
                        }
                    }
                    else
                    {
                        if (nbMaxtrix[xctr, yctr] == 3)
                        {
                            lifeMatrix[xctr, yctr] = true;
                        }
                    }


                }
            }

            return nbMaxtrix;
        }

        private static void Display(bool[,] results)
        {
            for (int i = 0; i <= results.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= results.GetUpperBound(1); j++)
                {
                    if (results[i, j])
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write(results[i, j] + "(" + i + "," + j + ")" + " ", Console.ForegroundColor);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void Display(int[,] results)
        {
            for (int i = 0; i <= results.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= results.GetUpperBound(1); j++)
                {
                    Console.Write(results[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
