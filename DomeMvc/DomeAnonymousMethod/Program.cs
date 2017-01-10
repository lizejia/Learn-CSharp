using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeAnonymousMethod
{
    class Program
    {
        //匿名方法也是方法，当然可以被委托包装，所以用匿名的方式实现委托
        //定义委托
        delegate void CloseDelegate();

        static void Main(string[] args)
        {
            //为什么count为一致加呢，按正常应该是在CreatCloseDelegate()完了后直接释放的
            //因为C#编译器把count变量  变成了一个class的字段，count不在栈上了，而是在托管堆中，
            CloseDelegate test = CreatCloseDelegate();
            Console.WriteLine("调用test");
            test();
            test();



            //正常委托
            CloseDelegate zc = new Friend().Output;
            zc();
            //用匿名方法实例化委托
            CloseDelegate cd = delegate
            {
                Console.WriteLine("用匿名方法实例化委托");
            };
            cd();
            Console.ReadKey();
        }
        /// <summary>
        /// 闭包延长外部变量count的声明周期
        /// </summary>
        /// <returns></returns>
        private static CloseDelegate CreatCloseDelegate()
        { 
            //外部变量
            int count =1;
            //匿名方法实例化委托对象
            CloseDelegate cd = delegate
            {
                Console.WriteLine(count);
                count++;
            };
            //调用委托
            cd();
            return cd;
        }


        public class Friend
        {
            public void Output()
            {
                Console.WriteLine("正常委托！！！");
            }
        }
    }
}
