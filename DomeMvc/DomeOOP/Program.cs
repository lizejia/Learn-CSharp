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

            //提问B b = new B();输出什么？
            B b = new B();
            //有继承关系时；实例化子类时的顺序是：
            //1、子类静态字段
            //2、子类静态构造函数
            //3、子类实例字段
            //4、父类静态字段
            //5、父类静态构造函数
            //6、父类实例字段
            //7、父类实例构造函数
            //8、子类实例构造函数
            //补充说明：
            //1、构造引用类型的对象时，调用实例构造方法之前，为对象分配的内存总是先被归零，构造器没有显式重写字段，字段的值为0或者null
            //2、原则上讲，类中的字段应该在实例构造方法内初始化。C#编译器提供了简化的语法，允许在变量定义的时候初始化。但在幕后，C#会把这部分代码搬到构造方法内部。
            //    因此，这里存在代码膨胀的问题。多个字段在定义时初始化，同时存在多个构造方法，每个构造方法都会把这些字段初始化的代码搬到自己的内部，这样造成代码的膨胀。
            //    为了避免这样情况，可以把这些字段的初始化放到一个无参构造方法内，其他的构造方法显式调用无参构造方法。
            //3、初始化类的字段有两种方法，①使用简化语法，在定义的时候初始化；② 在构造方法内初始化。使用简化语法初始化的代码，会被搬到构造方法内。
            //    特别注意，在生成的IL中，父类构造方法会夹在 ①和②之间。因此，实例化子类的时候，会先执行①，再执行父类构造方法，然后执行②。
            //    现在问题来了，假如在父类构造方法内，调用虚方法，虚方法回调子类的方法，子类方法使用字段，这时候字段的值是简化语法初始化的值
            Console.ReadKey();
        }
    }
    /// <summary>
    /// 父类
    /// </summary>
    class A
    {
        public A()
        {
            PrintFields();
        }
        public virtual void PrintFields() { }
    }
    /// <summary>
    /// 子类
    /// </summary>
    class B : A
    {
        int x = 1;
        int y;

        public B()
        {
            y = -1;
        }

        public override void PrintFields()
        {
            Console.WriteLine("x={0},y={1}", x, y);
        }
    }
}
