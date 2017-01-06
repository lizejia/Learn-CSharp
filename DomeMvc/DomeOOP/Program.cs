using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Sheep sheep = new Sheep();
            sheep.Age = 2;
            Console.WriteLine(sheep.Age);
            Console.WriteLine(sheep.Eat());



            Console.ReadKey();
        }
    }
}
