using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeNew
{
    class Program
    {
        static void Main(string[] args)
        {
            //1）new 运算符：用于创建对象和调用构造函数。这种大家都比较熟悉，没什么好说的了。
            //2）new 修饰符：在用作修饰符时，new 关键字可以显式隐藏从基类继承的成员。
            //3）new 约束：用于在泛型声明中约束可能用作类型参数的参数的类型。 
            BetterPhone BP = new BetterPhone();

            BP.Show();
            Console.ReadKey();


        }
    }


    public class Phone
    {
        public void Show()
        {
            Console.WriteLine("我是父类");
        }
    }

    public class BetterPhone:Phone
    {
        public new void Show()
        {
            Console.WriteLine("我是子类");
        }
    }
}
