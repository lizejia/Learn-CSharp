using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeExtensionMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            //扩展方法：首先它是方法，它是为了给已经定义的类型增加方法的。
            //在没有扩展方法之前，我们要新增一个方法，需要继承其类型，然后在写你需要增加的方法。
            //如果基类有抽象方法，则还需要重新实现这些抽象方法。而且还改变了该类型名称
            //而且值类型和密封类也不能被继承
            //感觉好麻烦啊啊啊！！！然后扩展方法就出现了
            #region 为什么要有扩展方法

            Person person;
            Person1 person1 = new Person1();
            person = person1;
            person.Eat();
            person.Drink();
            //在没有用扩展方法之前person根本就点不出来Learning方法
            //只有他的派生类Person1的实例才能点出来
            person1.Learning();
            //是不是特别麻烦，而且在书写上也感觉不是Person的扩展。
            #endregion

            #region 定义扩展方法之抽象类扩展

            //扩展的方法
            Ex.Learning(person);
            person.Learning();
            #endregion

            #region 定义扩展方法之值类型扩展

            //扩展的方法
            int i = 4;
            i.Sleep(i);
            #endregion

            #region 定义扩展方法之密封类扩展

            //扩展的方法
            SealTest st = new SealTest();
            st.Speak();
            st.GoOut("dadsa");
            st.GoOutEx();
            #endregion

            //总结扩展方法定义
            //1、扩展方法必须的静态方法
            //2、第一个参数必须是要被扩展的类型
            //3、第一个参数必须加this关键字，不能有其他关键字了
            //4、扩展方法必须定义在非嵌套、非泛型的静态类中


            //扩展方法的调用
            //1、可用(被扩展的类型实例.扩展方法())   推荐这种调用
            //2、可用(包含扩展方法的静态类.扩展方法(被扩展的类型实例))

            //扩展方法调用的优先级
            //1、自己的实例方法=>2、同命名空间下的扩展方法=>3、using命名空间下的扩展方法
            //Note:如果被扩展的类型里定义了和要扩展的方法有一样的方法有一样的方法签名，则只执行自己的实例方法，并且也.不出来扩展方法



            //有人说空引用也可以调用扩展方法，其实是假的，这个假不是说不能调用
            //我们知道调用有两种方式，其实第二种就可以解释空引用的调用扩展方法
            //只不过把空引用变量作为参数传递给静态方法而已
            string str = null;
            Console.WriteLine(str.IsNull()); ;//等价于Ex.IsNull(str)
            Console.WriteLine(Ex.IsNull(str));
            Console.ReadKey();
        }
    }

    /// <summary>
    /// 定义抽象类Person
    /// </summary>
    public abstract class Person
    {
        public virtual void Eat()
        {
            Console.WriteLine("饿了，想吃饭");
        }
        public abstract void Drink();
    }

    /// <summary>
    /// 扩展Person，增加Learning方法
    /// </summary>
    public class Person1 : Person
    {
        /// <summary>
        /// 这个方法不是我想扩展的，但是我还是要写，好累啊
        /// </summary>
        public override void Drink()
        {
            Console.WriteLine("渴了，想喝水。这个方法不是我想扩展的，但是我还是要写，好累啊");
        }

        /// <summary>
        /// 这个才是我要扩展的方法
        /// </summary>
        public void Learning()
        {
            Console.WriteLine("继承正在学习！！！这个是扩展的方法，开心");
        }
    }
    
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class Ex
    {
        public static void Learning(this Person p)
        {
            Console.WriteLine("扩展正在学习！！！这个才是我要扩展的方法，开心");
        }

        public static void Sleep(this int x,int y)
        {
            Console.WriteLine("值类型{0}int的扩展，睡觉啦", y);
        }


        public static void GoOut(this SealTest st,string name)
        {
            Console.WriteLine("密封类的扩展GoOut，我想出去");
        }

        public static void GoOutEx(this SealTest st)
        {
            Console.WriteLine("密封类的扩展GoOutEx，我想出去");
        }


        public static bool IsNull(this string str)
        {
            return null == str;
        }
    }

    /// <summary>
    /// 密封类
    /// </summary>
    public sealed class SealTest
    {
        public void Speak()
        {
            Console.WriteLine("密封类说话啦");
        }
        /// <summary>
        /// 定义无参数的GoOut()
        /// </summary>
        public void GoOut(string name)
        {
            Console.WriteLine(name + "密封类的实例GoOut，我想出去");
        }


    }

}
