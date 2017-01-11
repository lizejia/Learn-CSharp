using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeSmartCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 隐式类型var
            //隐式类型 var （由于C#是强类型语言）
            //可以用var声明变量和数组
            //var告诉编译器，需要根据变量的值去判定其类型
            var x = 1;

            var arrayint = new[] { 1, 2, 3 };
            var arraystring = new[] { "a", "b" };

            string[] arraystring1 = new string[2] { "a", "b" };
            #endregion


            //对象、集合初始化器{ } Person p = new Person() { };
            //1.要用对象初始化器，这个对象必须要有一个无参的构造函数
            //2.集合初始化器，其本质是调用了对象的Add()方法
            List<int> list = new List<int> { 1, 23, 24 };
      

            #region 匿名类型 通过var和{}来构造
            //构造匿名类型对象
            var Son = new { Name = "Jeck", Age = "5" };
            Console.WriteLine("儿子名字：{0}，年龄：{1}", Son.Name, Son.Age);
            //构造匿名数组

            var array = new[] 
            { 
                new { Name = "Tom", Age = 40 },
                new { Name="Roman", Age = 25 }
            };
            foreach (var item in array)
            {
                Console.WriteLine("名字：{0}，年龄：{1}", item.Name, item.Age);
            }


            #endregion

            Console.ReadKey();
        }
    }
    #region 自动属性
    class Person
    {
        /// <summary>
        /// 自动属性get; set;
        /// 不用定义私有字段
        /// C#编译器自动帮我们生成一个私有字段
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 只读属性，或者不写Set(public string Name { get; })
        /// </summary>
        public string Name { get; private set; }
    }

    public struct TestAutoProperty
    {
        public int Age { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 在结构体中，构造函数必须显示的调用无参构造函数This()
        /// 只有这样C#编译器才知道给所以的字段都被赋值了
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        public TestAutoProperty(string name, int age)
            : this()
        {
            this.Age = age;
            this.Name = name;
        }
    }

    #endregion
}
