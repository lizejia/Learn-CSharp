using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeNullable
{
    class Program
    {

        static void Main(string[] args)
        {
            #region Nullable<T>属性和方法的使用
            //Nullable 和Nullable<T>
            int? i = null;  //?问好就表示int类型可以为null  等价于Nullable<int> i = null
            Console.WriteLine("i值：" + i);
            //Console.WriteLine("i类型：" + i.GetType());
            Program p=new Program();

            int? y = 10;
            Console.WriteLine("有值情况");
            p.Display(y);

            Console.WriteLine("无值情况");
            Nullable<int> z = new Nullable<int>();
            p.Display(z);

            #endregion

            #region ??空合并操作符
            //??空合并操作符---如果左边的不为null,返回左边，负责返回右边
            Console.WriteLine();
            Console.WriteLine("??运算符操作");

            int? nullable = null;
            int? hasvalue = 2;

            Console.WriteLine("返回右边值：{0}", nullable ?? hasvalue);
            nullable = 1;
            Console.WriteLine("返回左边值：{0}", nullable ?? hasvalue);

            Console.WriteLine("引用类型");
            string isnull = null;
            string nonull = "你好右边";

            Console.WriteLine("返回右边值：{0}", isnull ?? nonull);
            isnull = "你好左边";
            Console.WriteLine("返回左边值：{0}", isnull ?? nonull);
            #endregion

            //可空类型的装箱和拆箱
            //1、Nullable<T> 的类型都是System.int32
            //2、装箱操作后的引用类型，用GetType获取还是System.int32
            //3、当可空类型装箱后，在拆箱成非可空类型，报错。因为非可空类型没有null值
            //4、没有值得可空类型不能调用任何函数
            p.BoxedAndUnboxen();
            Console.ReadLine();
        }

        /// <summary>
        /// Nullable<T>属性和方法的使用
        /// </summary>
        /// <param name="nullable"></param>
        public void Display(int? nullable)
        {
            //用HasValue判断可空对象是否有值
            Console.WriteLine("可空类型nullable是否有值：" + nullable.HasValue);
            if (nullable.HasValue)
            {
                Console.WriteLine("值：" + nullable.Value);
            }
            //GetValueOrDefault()
            //如果有值，返回值
            //如果没有，返回默认值0
            Console.WriteLine("GetValueOrDefault():{0}", nullable.GetValueOrDefault());

            //GetValueOrDefault(T)
            //如果有值，返回值
            //如果没有，返回T
            Console.WriteLine("GetValueOrDefault(T)重载:{0}", nullable.GetValueOrDefault(3));

            //GetHashCode()
            //如果有值，返哈希代码
            //如果没有，返回0
            Console.WriteLine("GetHashCode():{0}", nullable.GetHashCode());

            
        }

        /// <summary>
        /// 装箱拆箱
        /// </summary>
        public void BoxedAndUnboxen()
        {
            Nullable<int> y = 5;
            int? x = null;
            Console.WriteLine("装箱前，y类型{0}", y.GetType());

            //没有值得可空类型调用函数时，会报错。
            //因为在调用函数之前，编译器会对可空类型进行装箱操作，变成空引用。
            //然后就抛出了空引用异常
            //Console.WriteLine("x类型{0}", x.GetType());
            //Console.WriteLine("x为空，null直接赋值");
            object obj = y;
            Console.WriteLine("装箱后，y类型{0}", y.GetType());
            Console.WriteLine("obj类型{0}", obj.GetType());
            Console.WriteLine("obj值为：{0}", obj);

            int value = (int)obj;
            x = (int?)obj;
            Console.WriteLine("拆箱后，obj类型{0}", obj.GetType());
            Console.WriteLine("拆箱后，value类型{0}", value.GetType());
            Console.WriteLine("拆箱后，x类型{0}", x.GetType());



            Nullable<int> isnull = null;

            //没有值得可空类型调用函数时，会报错。
            //isnull.GetType();

            object objj = isnull;
            Console.WriteLine("值为：" + objj);
            int unboxed = (int)objj;

            Console.WriteLine();
        }
    }
}
