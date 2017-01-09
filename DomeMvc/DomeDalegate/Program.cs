using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeDalegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();


            //第一步，声明委托变量
            //MyDelegate d;
            //第二步，实例化委托类型，并且传递需要包装的方法
            //d = new MyDelegate(p.Say);
            //第三步，将委托作为方法参数，传递给方法
            //p.CallDelegate(d,18,"Roman");


            #region Test1
            Console.WriteLine("***********Test2无返回值的委托****用委托对象显示包装实例方法**********");
            MyDelegate d = new MyDelegate(p.Say);
            p.CallDelegate(d,18,"Roman");

            Console.WriteLine("***********Test2无返回值的委托****用委托显示包装静态方法**********");
            MyDelegate dstatic = new MyDelegate(SayStatic);
            dstatic(1832131, "Roman222222");
            new MyDelegate(SayStatic).Invoke(00000000000000000000,"000000000000000");
            p.CallDelegate(dstatic, 18, "Roman");

            Console.WriteLine("***********Test2无返回值的委托****用委托隐示包装实例方法和静态方法**********");
            p.CallDelegate(p.Say, 20, "jack");
            p.CallDelegate(SayStatic, 20, "jack");
            Console.WriteLine("**************************");

            Console.WriteLine("***********委托链+-**********");

            MyDelegate delegatechain = null;
            delegatechain += d;
            delegatechain += dstatic;
            delegatechain(50, "方法1：老了，来个委托链吧");

            d += dstatic;
            d(51, "方法2：年轻，来个委托链吧！");
            Console.WriteLine("***********注意：不同委托（返回值，方法参数，任意不同）不能加减成委托链**********");

            //GreetingDelegate DG = new GreetingDelegate(p.ChineseGreeting);
            //DG += d;

            #endregion

            Console.WriteLine("**************************");
            Console.WriteLine("***********Test2有返回值的委托****隐示包装实例方法**************");

            string x = p.Greeting("Roman", p.ChineseGreeting);
            string y = p.Greeting("Roman", p.EnlishGreeting);
            Console.WriteLine(x + "," + y);
            //p.Greeting("Roman", p.ChineseGreeting);
            //p.Greeting("Roman", p.EnlishGreeting);
            Console.ReadKey();

        }
        #region Test1
        /// <summary>
        /// 1、定义委托
        /// </summary>
        /// <param name="age"></param>
        /// <param name="name"></param>
        public delegate void MyDelegate(int age, string name);

        /// <summary>
        /// 2、定义需要被委托包装的方法（必须返回类型，参数的个数，顺序，类型一致）
        /// 实例方法
        /// </summary>
        /// <param name="age"></param>
        /// <param name="name"></param>
        public void Say(int age, string name)
        {
            Console.WriteLine("姓名：" + name);
            Console.WriteLine("年龄：" + age);
        }

        /// <summary>
        /// 2、定义需要被委托包装的方法（必须返回类型，参数的个数，顺序，类型一致）
        /// 静态方法
        /// </summary>
        /// <param name="age"></param>
        /// <param name="name"></param>
        public static void SayStatic(int age, string name)
        {
            Console.WriteLine("静态姓名：" + name);
            Console.WriteLine("静态年龄：" + age);
        }

        /// <summary>
        /// 调用委托
        /// </summary>
        /// <param name="myDelegate"></param>
        public void CallDelegate(MyDelegate myDelegate,int age, string name)
        {
            //显示调用
            myDelegate.Invoke(age, name);
            //隐式调用
            //myDelegate(18, "roman");
        }
        #endregion

        /// <summary>
        /// 定义委托类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public delegate string GreetingDelegate(string name);
        
        /// <summary>
        /// 定义需要被委托包装的方法（返回值，方法签名要一致）
        /// </summary>
        /// <param name="name"></param>
        public string EnlishGreeting(string name)
        {
            //Console.WriteLine("Hello" + name);
            return "Hello" + name;
        }

        /// <summary>
        /// 定义需要被委托包装的方法（返回值，方法签名要一致）
        /// </summary>
        /// <param name="name"></param>
        public string ChineseGreeting(string name)
        {
            //Console.WriteLine("你好" + name);

            return "你好" + name;
        }

        /// <summary>
        /// 调用委托
        /// </summary>
        /// <param name="name"></param>
        /// <param name="greetingDelegate"></param>
        public string Greeting(string name, GreetingDelegate greetingDelegate)
        {
            //greetingDelegate.Invoke(name);
            return greetingDelegate.Invoke(name);
        }

    }
}
