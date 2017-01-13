using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Linq.Expressions;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace DomeDynamic
{
    class Program
    {
        static void Main(string[] args)
        {
            //详细了解
            //http://blog.csdn.net/learning_hard/article/details/17456641
            #region 引入动态类型
            //在4.0为什么要引入动态类型
            //我们都知道C#是静态类型语言，就算用了隐式类型var,也要必须初始化，让编译器确定其类型
            //引入dynamic关键字来定义动态类型，编译器并不知道其类型，只有在程序运行时，才能被确定
            string str = "20";
            int x = 10;
            //x = str + x;//报错
            x = int.Parse(str) + x; //要想编译成功就要转换
            dynamic strtest = "20";
            //x = strtest + x;



            //好处
            //1、可以减少类型转换的使用
            //2、调用Python等动态语言

            // 引入动态类型之后  
            // 可以在C#语言中与动态语言进行交互  
            // 下面演示在C#中使用动态语言Python 
            ScriptEngine engine = Python.CreateEngine();
            Console.Write("调用Python语言的print函数输出: ");
            // 调用Python语言的print函数来输出  
            engine.Execute("print 'Hello world'");
            #endregion

            #region 动态类型背后的故事
            //这里不的不提DLR(Dynamic Language Runtime)
            //它的作用是什么，一句话：DLR就是帮助C#编译器来识别动态类型
            //那DLR和CLR有什么关系呢，DLR是建立在CLR的基础之上的，DLR是动态语言和C#编译器用来动态执行代码的库，它不具有JIT和GC的功能
            //DLR扮演了什么角色：DLR通过它的绑定器(binder)和调用点(callsite)，元对象来把代码转换为表达式树，
            //*******************然后再把表达式树编译为IL代码，最后由CLR编译为机器可识别代码

            #endregion

            #region 动态类型的约束 1234
            //由于动态类型的不确定性让它不能做很多事
            //1、不能调用扩展方法
            var numbers = Enumerable.Range(10, 10);
            dynamic number = 4;
            //var error = numbers.Take(number);  // 编译时错误
            // 通过下面的方式来解决这个问题  
            // 1. 将动态类型转换为正确的类型  
            var right1 = numbers.Take((int)number);
            // 2. 用调用静态方法的方式来进行调用  
            var right2 = Enumerable.Take(numbers, number);

            //2、委托与动态类型不能做隐式转换
            //dynamic lambdady = x => x + 1;//报错
            //解决这个问题转换
            dynamic lambdady = (Func<int, int>)(xx => xx + 1);
            //3、不能调用静态方法和构造函数
            //dynamic testdy = new dynamic();报错
            //4、类型声明和泛型类型参数(基类不能为dynamic类型、dynamic类型不能为类型参数的约束、不能作为所实现接口的一部分 )

            #endregion

            #region 实现自己的动态行为（ExpandoObject、DynamicObject、IDynamicMetaObjectProvider）
            //ExpandoObject
            dynamic expando = new ExpandoObject();
            expando.Name = "Roman";
            expando.Addmethod = lambdady;
            Console.WriteLine("动态类型名称：{0}", expando.Name);
            Console.WriteLine("动态类型方法：{0}", expando.Addmethod);
            Console.WriteLine("调用动态类型方法：{0}", expando.Addmethod(5));

            //DynamicObject重写
            dynamic dynamicobj = new DynamicType();
            dynamicobj.call = lambdady;
            dynamicobj.call(5);
            dynamicobj.Name = "Roman";
            dynamicobj.Age = "24";

            //实现IDynamicMetaObjectProvider接口
            //由于Dynamic类型在运行时来动态创建对象的，所以对该类型的每个成员的访问都会调用GetMetaObject方法来获得动态对象，
            //然后通过这个动态对象来进行调用，所以实现IDynamicMetaObjectProvider接口，需要实现一个GetMetaObject方法来返回DynamicMetaObject对象
            dynamic dynamicobj2 = new DynamicType2();
            dynamicobj2.Call();
            Console.Read();

            #endregion
            Console.ReadKey();

        }

        //// 基类不能为dynamic 类型  
        //class DynamicBaseType : dynamic
        //{
        //}
        //// dynamic类型不能为类型参数的约束  
        //class DynamicTypeConstrain<T> where T : dynamic
        //{
        //}
        //// 不能作为所实现接口的一部分  
        //class DynamicInterface : IEnumerable<dynamic>
        //{
        //}
    }

    /// <summary>
    /// 实现自己的动态行为DynamicObject重写
    /// </summary>
    class DynamicType :DynamicObject
    {
        // 重写方法，  
        // TryXXX方法表示对对象的动态调用  
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            Console.WriteLine(binder.Name + " 方法正在被调用");
            result = null;
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Console.WriteLine(binder.Name + " 属性被设置，" + "设置的值为： " + value);
            return true;
        }
    }

    /// <summary>
    /// 实现自己的动态行为，实现IDynamicMetaObjectProvider接口
    /// </summary>
    public class DynamicType2 : IDynamicMetaObjectProvider
    {
        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            Console.WriteLine("开始获得元数据......");
            return new Metadynamic(parameter, this);
        }
    }

    // 自定义Metadynamic类  
    public class Metadynamic : DynamicMetaObject
    {
        internal Metadynamic(Expression expression, DynamicType2 value)
            : base(expression, BindingRestrictions.Empty, value)
        {
        }
        // 重写响应成员调用方法  
        public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
        {
            // 获得真正的对象  
            DynamicType2 target = (DynamicType2)base.Value;
            Expression self = Expression.Convert(base.Expression, typeof(DynamicType2));
            var restrictions = BindingRestrictions.GetInstanceRestriction(self, target);
            // 输出绑定方法名  
            Console.WriteLine(binder.Name + " 方法被调用了");
            return new DynamicMetaObject(self, restrictions);
        }
    }
}
