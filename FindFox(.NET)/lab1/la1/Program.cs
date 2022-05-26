using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace la1
{
    class Program
    {
        static void Main()
        {
            Game game = Game.GetInstance();
            int n = 0, m = 0, countFoxes = 0, countShots = 0;
            game.ShowRules();
            Console.WriteLine("Enter width of the plagroung: ");
            m = game.InputValue(m);
            Console.WriteLine("Enter height of the playground: ");
            n = game.InputValue(n);
            Console.WriteLine("Enter count of foxes: ");
            countFoxes = game.InputValue(countFoxes);
            Console.WriteLine("Enter count of shoots: ");
            countShots = game.InputValue(countShots);
            game.App(n, m, countFoxes, countShots);
            Console.Read();
        }
    }
}