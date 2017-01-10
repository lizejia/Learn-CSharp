using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Compare<int>.CompareGeneric(10, 20));
            Console.WriteLine(Compare<string>.CompareGeneric("OOO", "10"));
            Console.WriteLine("******************TEST性能*************************");
            TestGeneric();
            TestNoGeneric();
            Console.WriteLine("--结论：为何非泛型会运行时间长！！！--");
            Console.WriteLine("--因为ArrayList.Add()的参数是object，需要进行装箱操作--");
            Console.WriteLine("******************TEST性能*************************");
            #region 类型参数T

            //未绑定泛型和已构造泛型（开发类型、密封类型）
            //Type.ContainsGenericParameters判断类型对象是否包含未被实际类型代替的类型参数，如果存在，true（开放类型）,否则false
            Type t = typeof(Compare<>);
            Console.WriteLine("是否开放类型" + t.ContainsGenericParameters);
            t = typeof(Dictionary<,>);
            Console.WriteLine("是否开放类型" + t.ContainsGenericParameters);
            t = typeof(Dictionary<int,string>);
            Console.WriteLine("是否开放类型" + t.ContainsGenericParameters);
            t = typeof(DictionaryStringKey<string>);
            Console.WriteLine("是否开放类型" + t.ContainsGenericParameters);
            #endregion 类型参数T

            #region 泛型的静态字段和静态函数问题
            StaticField<int>.field = "1";
            StaticField<int>.field = "11";
            StaticField<string>.field = "2";
            StaticField<Guid>.field = "3";

            //每一次赋值都改变其值
            NoGenericStaticField.field = "非泛型类静态字段1";
            NoGenericStaticField.field = "非泛型类静态字段2";
            NoGenericStaticField.field = "非泛型类静态字段3";

            StaticField<int>.Output();
            StaticField<string>.Output();
            StaticField<Guid>.Output();

            NoGenericStaticField.Output();

            Console.WriteLine("--结论：我们都知道类类型的静态字段不管创建多少实例，这个静态字段都只存在一个,静态构造函数只会执行一次。而每一个封闭的泛型都有属于自己的静态字段，静态构造函数！！！");
            #endregion 

            #region 类型参数推断（类型推断只适用于泛型方法）
            int n1 = 1;
            int n2 = 2;
            //不使用类型推断
            //genericMethod<int>(ref n1, ref n2);
            //Console.WriteLine("n1值" + n1);
            //Console.WriteLine("n2值" + n2);
            //使用类型推断
            genericMethod(ref n1, ref n2);
            Console.WriteLine("n1值" + n1);
            Console.WriteLine("n2值" + n2);

            #endregion

            Console.WriteLine("------------参数类型的约束------------------");
            Console.WriteLine("*****引用类型约束：1、where T：class------放在最前面");
            Console.WriteLine("*公共无参构造函数：2、where T：new()------放在最后面");
            Console.WriteLine("**********值类型：3、where T：struct-----不包括可空类型");
            Console.WriteLine("****转换类型约束：4、where T：基类名（指定的类型参数必须是基类或者派生自基类的子类）");
            Console.WriteLine("********************where T:接口名（指定的类型实参必须是接口或实现该接口的类）");
            Console.WriteLine("********************where T:U(T提供的类型实参必须是U提供的类型实参或派生于U提供的类型实参)-----");
            Console.ReadKey();
        }
        /// <summary>
        /// 类型参数推断（类型推断只适用于泛型方法）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        private static void genericMethod<T>(ref T t1, ref T t2)
        {
            T t = t1;
            Console.WriteLine("****");
            Console.WriteLine(t);
            Console.WriteLine(t1);
            Console.WriteLine(t2);
            t1 = t2;
            Console.WriteLine("****");
            Console.WriteLine(t);
            Console.WriteLine(t1);
            Console.WriteLine(t2);
            t2 = t;
            Console.WriteLine("****");
            Console.WriteLine(t);
            Console.WriteLine(t1);
            Console.WriteLine(t2);
        }

        #region TEST性能
        /// <summary>
        /// 测试泛型，运行时间
        /// </summary>
        static void TestGeneric()
        {
            //初始化秒表
            Stopwatch stopwatch = new Stopwatch();
            List<int> list = new List<int>();
            //开始计时
            stopwatch.Start();
            for (int i = 0; i < 10000000; i++)
			{
                list.Add(i);
			}
            //结束计时
            stopwatch.Stop();
            //输出所用的时间
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("泛型:" + elapsedTime);
        }
        /// <summary>
        /// 测试非泛型，运行时间
        /// </summary>
        static void TestNoGeneric()
        {
            //初始化秒表
            Stopwatch stopwatch = new Stopwatch();
            ArrayList list = new ArrayList();
            //开始计时
            stopwatch.Start();
            for (int i = 0; i < 10000000; i++)
            {
                list.Add(i);
            }
            //结束计时
            stopwatch.Stop();
            //输出所用的时间
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("非泛型:" + elapsedTime);
        }
        #endregion TEST性能
    }
    #region 泛型的静态字段和静态函数问题

    /// <summary>
    /// 泛型类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StaticField<T>
    {
        static StaticField()
        {
            Console.WriteLine("泛型的静态构造函数被执行了，T为" + typeof(T).Name);
        }

        public static string field;

        public static void Output()
        {
            Console.WriteLine(field + ":" + typeof(T).Name);
        }
    }

    /// <summary>
    /// 非泛型类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NoGenericStaticField
    {
        static NoGenericStaticField()
        {
            Console.WriteLine("非泛型的静态构造函数被执行了");
        }
        public static string field;

        public static void Output()
        {
            Console.WriteLine(field);
        }
    }
    #endregion 类型参数T

    /// <summary>
    /// 声明开放泛型类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DictionaryStringKey<T> : Dictionary<int, T>
    {

    }


    /// <summary>
    /// Compare<T>泛型类 
    /// </summary>
    /// <typeparam name="T">T为类型参数,where是类型参数的约束，意思就是说T类型必须实现IComparable接口</typeparam>
    public class Compare<T> where T : IComparable
    {
        public static T CompareGeneric(T t1,T t2)
        {
            if (t1.CompareTo(t2) > 0)
            {
                return t1;
            }
            else
            {
                return t2;
            }
        }
    }



}
