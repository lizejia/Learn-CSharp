using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexer
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            person[0] = 1;
            person["00011"] = "hello word";
            Console.WriteLine(person[0]);
            Console.WriteLine(person["00011"]);


            Point point = new Point(123,321);
            //Point point;
            //Console.WriteLine(point);
            //point.X = 1;
            //point.Y = 2;
            Console.WriteLine(point.x);
            Console.WriteLine(point.y);
            Console.ReadKey();
        }
    }

    class Person 
    {
        private Hashtable stringarray = new Hashtable();
        public string this[string index]
        {
            get
            {
                return stringarray[index].ToString();
            }
            set 
            {
                //stringarray[index] = value;
                stringarray.Add(index, value);
            }
        }

        private int[] intarray = new int[10];
        public int this  [int index]
        {
            get
            {
                return intarray[index];
            }
            set
            {
                intarray[index] = value;
            }
        }
    }

    struct Point {
        public int x;
        public int y;
        //public int X { get { return x; } set { x = value; } }
        //public int Y { get { return y; } set { y = value; } }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
