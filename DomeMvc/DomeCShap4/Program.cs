using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeCShap4
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 可选参数和命名参数
            //可选参数的四个条件
            //1、可选参数必须在必选参数后面
            //2、可选参数的默认值必须为常量  如：数字、字符串、枚举、null、coust成员
            //3、可选参数不能是参数数组params
            //4、不能用ref和out关键字修饰
            Print(1);
            Print(2, "jerry");
            Print(2, "jerry", Sexx.Male);


            //用命名实参(命名实参的具体名称必须和方法参数中的变量一致)
            //如果我们想省略第二个参数怎么办
            Print(3, s: Sexx.Male);
            Print(Age: 3, Name: "tom", s: Sexx.Male);

            #endregion


            #region 泛型的协变性out、逆变性in
            #region 协变性out
            //就是类型参数T可以从派生类隐式的转化成基类
            List<object> objectList = new List<object>();
            List<Program> programList = new List<Program>();
            objectList.AddRange(programList);//可以编译通过
            //programList.AddRange(objectList);//不能编译通过
            //通过F12可以看出
            //因为AddRange(IEnumerable<T> collection)中的IEnumerable<T>参数，该接口定义为IEnumerable<out T>
            #endregion
            #region 逆变性in
            //就是类型参数T可以从基类隐式的转化成派生类

            IComparer<object> objComparer = new TestComparer();
            IComparer<Program> proComparer = new TestComparer();

            //objectList.Sort(proComparer);//不能编译通过
            //通过F12可以看出
            //因为IComparer<T>，该接口定义为IComparer<in T>
            programList.Sort(objComparer);
            #endregion


            //Note
            //1、协变和逆变只能是引用类型(因为可变性存在引用转换的过程的，值类型变量存储的就是对象本身，并不是对象的引用)
            //2、只有接口我委托才支持协变和逆变
            //3、必须显示的用in和out
            //4、委托的可变性不要在多播委托中使用
            //因为多播委托是通过委托链来实现的，如果存在协变或者逆变行为，+=起来，
            //Delegate.Combine()又必须要求参数必须类型一致，就会无法确定创建什么类型的委托了
            //但是可以通过强制转换解决此问题

            //从IL看出
            //+=和-=的本质是调用Delegate.Combine()和Delegate.Remove()方法
            Func<string> stringfun = () => "";

            Func<object> temfun = new Func<object>(stringfun);

            Func<object> objfun = () => new object();
            Func<object> combined = objfun + temfun;
            #endregion
            Console.ReadKey();
        }
        #region 可选参数和命名参数
        static void Print(int Age, string Name = "roman", Sexx s = Sexx.Female)
        {
            Console.WriteLine("Age={0},Name={1},性别={2}", Age, Name, s);
        }

        public enum Sexx
        {
            Male,
            Female
        }

        #endregion
    }
    public class TestComparer : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            return x.ToString().CompareTo(y.ToString());
        }
    }
}
