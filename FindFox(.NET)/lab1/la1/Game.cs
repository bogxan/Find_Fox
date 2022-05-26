using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace la1
{
    public class Game
    {
        private static Game _instance;
        private Game() { }
        public static Game GetInstance()
        {
            if (_instance==null)
            {
                _instance = new Game();
            }
            return _instance;
        }
        public static int x;
        public static int y;

        public void ShowBoard(int[,] arr)
        {
            for (int i = 0; i < arr.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < arr.GetUpperBound(0) + 1; j++)
                {
                    Console.Write($"{arr[i, j]}\t");
                }
                Console.WriteLine();
            }
        }

        public void InitFoxes(int[,] boardFoxes, int[,] hiddingFoxes, int countFoxes)
        {
            Random random = new Random();
            while (countFoxes > 0)
            {
                int x = random.Next(0, boardFoxes.GetUpperBound(0) + 1);
                int y = random.Next(0, boardFoxes.GetUpperBound(1) + 1);
                for (int i = 0; i < boardFoxes.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < boardFoxes.GetUpperBound(1) + 1; j++)
                    {
                        if (x == i && y == j)
                        {
                            boardFoxes[i, j] += 1;
                            hiddingFoxes[i, j] = boardFoxes[i, j];
                        }
                    }
                }
                countFoxes--;
            }

        }

        public void InitBoard(int[,] array)
        {
            for (int i = 0; i < (array.GetUpperBound(0) + 1); i++)
            {
                for (int j = 0; j < (array.GetUpperBound(1) + 1); j++)
                {
                    array[i, j] = 0;
                }
            }
        }

        public void InputCoordinates(int a, int b)
        {
            Console.WriteLine("Enter coordinates of fox: ");
            Console.WriteLine("X: ");
            x = InputValue(a);
            Console.WriteLine("Y: ");
            y = InputValue(b);
        }

        public void ShowLeftInfo(int countFoxes, int countShots)
        {
            Console.WriteLine($"Count of the leftover fox - {countFoxes}");
            Console.WriteLine($"Count of the leftover shoots - {countShots}");
        }

        public void ShowHorzVertInfo(int countHorz, int countVert)
        {
            Console.WriteLine($"By horizontal left {countHorz} foxes");
            Console.WriteLine($"By vertical left {countVert} foxes");
        }

        public bool FindFox(int[,] playGround, int[,] boardFoxes, int x, int y)
        {
            bool result = false;
            if (boardFoxes[x, y] > 0)
            {
                boardFoxes[x, y] -= 1;
                playGround[x, y] += 1;
                Console.WriteLine($"You found fox!!! Coordinates of found fox: ({x}; {y})");
                result = true;
            }
            return result;
        }

        public void FindHorzVert(int[,] boardFoxes, int x, int y)
        {
            int countHorz = 0, countVert = 0;
            for (int fx = 0; fx < boardFoxes.GetUpperBound(0) + 1; fx++)
            {
                if (boardFoxes[fx, y] > 0) countVert += boardFoxes[fx, y];
            }
            for (int fy = 0; fy < boardFoxes.GetUpperBound(1) + 1; fy++)
            {
                if (boardFoxes[x, fy] > 0) countHorz += boardFoxes[x, fy];
            }
            ShowHorzVertInfo(countHorz, countVert);
        }

        public void App(int n, int m, int countFoxes, int countShots)
        {
            if (countFoxes != 0 && countShots != 0 && n != 0 && m != 0)
            {
                int[,] hiddingFoxes = new int[n, m];
                int[,] playGround = new int[n, m];
                int[,] boardFoxes = new int[n, m];
                Console.WriteLine("This is a playground: \n");
                InitBoard(playGround);
                InitBoard(boardFoxes);
                InitBoard(hiddingFoxes);
                InitFoxes(boardFoxes, hiddingFoxes, countFoxes);
                ShowBoard(playGround);
                while (countFoxes >= 0 || countShots > 0)
                {
                    x = y = 0;
                    InputCoordinates(x, y);
                    if (FindFox(playGround, boardFoxes, x, y)) countFoxes--;
                    countShots--;
                    ShowBoard(playGround);
                    ShowLeftInfo(countFoxes, countShots);
                    FindHorzVert(boardFoxes, x, y);
                    if (countShots == 0 && countFoxes != 0)
                    {
                        Console.WriteLine("You loose!");
                        break;
                    }
                    if (countFoxes == 0)
                    {
                        Console.WriteLine("You won!");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Some of entered values is 0!!!");
            }
        }

        public int InputValue(int k)
        {
            do
            {
                try
                {
                    k = int.Parse(Console.ReadLine());
                    if (k < 0) throw new FormatException();
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Entered value is not valid number!");
                }
            }
            while (true);
            return k;
        }

        public void ShowRules()
        {
            Console.WriteLine("Welcome in the game 'Find fox'!\n" +
                "Rules: \n" +
                "- you should enter width and height of the playground (can't be 0);\n" +
                "- you should enter count of foxes and shoots that you have (can't be 0);\n" +
                "- if you find fox - cell will have value '5'\n" +
                "- if count of the foxes is 0 - you will win\n" +
                "- if count of the shoots is 0 - you will loose\n");
        }
    }
}
