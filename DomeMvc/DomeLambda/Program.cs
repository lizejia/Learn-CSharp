using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
namespace DomeLambda
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lambda表达式(参数 => 表达式或语句块)

            #region Lambda表达式演变过程
            //C#1.0 最常规的
            Func<string, int> delegate1 = new Func<string, int>(CallBackMethod);
            //C#2.0 用匿名方法
            Func<string, int> delegate2 = delegate (string text)
            {
                return text.Length;
            };
            //C#3.0 用Lambdm 
            Func<string, int> delegate3 = (string text) => text.Length;
            //简化
            Func<string, int> delegate4 = (text) => text.Length;
            //再次简化 
            //注意1、：一个参数的时候可以去掉()，但是多个参数必须带()，并且用逗号隔开
            //注意2、：空括号表示指定零个参数
            //注意3、：有的时候参数必须显示的指定类型，例如没有简化的
            Func<string, int> delegate5 = text => text.Length;
            #endregion

            #region Lambdm表达式的使用---订阅事件

            #region C#3.0之前写法
            Button button1 = new Button();
            button1.Text = "快来点击我";
            button1.Click += delegate (object sender, EventArgs s)
            {
                ReportEvent("button1的Click事件", sender, s);
            };
            button1.KeyPress += delegate (object sender, KeyPressEventArgs s)
            {
                ReportEvent("button1的KeyPress事件", sender, s);
            };
            Form form = new Form();
            form.Name = "在控制台创建窗体";
            form.AutoSize = true;
            form.Controls.Add(button1);
            //运行那个窗体
            Application.Run(form);
            #endregion

            #region C#3.0之后写法
            Button button3 = new Button();
            button1.Text = "快来点击我";
            button1.Click += (object sender, EventArgs s) => ReportEvent("button1的Click事件", sender, s);
            button1.KeyPress += (object sender, KeyPressEventArgs s) => ReportEvent("button1的KeyPress事件", sender, s);
            Form form3 = new Form() { Name = "在3.0控制台创建窗体", AutoSize = true, Controls = { button3 } };
            //运行那个窗体
            Application.Run(form3);
            #endregion

            #endregion

            #region Lambdm表达式除了创建委托外还可以 转换成表达式树(Expression<TDelegate>类)
            //表达式树可以理解为数据结构
            //只不过表达式树是用于表达Lambda表达式的逻辑而已
            //Note：为什么要提出表达式树，为了后面的linq to sql作铺垫

            #region 动态创建一个表达式树
            //表达式的参数
            ParameterExpression a = Expression.Parameter(typeof(int), "a1");
            ParameterExpression b = Expression.Parameter(typeof(int), "b1");
            //表达式的主体部分
            BinaryExpression be = Expression.Add(a, b);
            //构造表达式
            Expression<Func<int, int, int>> ex = Expression.Lambda<Func<int, int, int>>(be, a, b);
            //分析树结构，获取表达式主题部分
            BinaryExpression body = (BinaryExpression)ex.Body;
            //左节点
            ParameterExpression left = (ParameterExpression)body.Left;
            //右节点
            ParameterExpression right = (ParameterExpression)body.Right;
            Console.WriteLine("表达式：{0}", ex);
            Console.WriteLine("主体部分：{0};{1}", body, be);
            Console.WriteLine("左节点：{0};{1}", left, a);
            Console.WriteLine("右节点：{0};{1}", right, b);

            #endregion
            #region Lambdm表达式构造一个表达式树
            Expression<Func<int, int, int>> exlambdm = (int a2, int b2) => a2 + b2;
            //分析树结构，获取表达式主题部分
            BinaryExpression bodylambdm = (BinaryExpression)exlambdm.Body;
            //左节点
            ParameterExpression leftlambdm = (ParameterExpression)bodylambdm.Left;
            //右节点
            ParameterExpression rightlambdm = (ParameterExpression)bodylambdm.Right;
            Console.WriteLine("lambdm表达式：{0};{1}", exlambdm, ex);
            Console.WriteLine("lambdm主体部分：{0};{1}", bodylambdm, body);
            Console.WriteLine("lambdm左节点：{0};{1}", leftlambdm, left);
            Console.WriteLine("lambdm右节点：{0};{1}", rightlambdm, right);
            #endregion

            #region 把表达式树转换成可执行的代码，用Compile()将表达式树编译成委托实例
            int x = ex.Compile().Invoke(10, 20);
            int y = exlambdm.Compile()(11, 22);
            Console.WriteLine("a1、b1的和：{0}", x);
            Console.WriteLine("a2、b2的和：{0}", y);
            #endregion
            #endregion
            Console.ReadKey();
        }
        
        /// <summary>
        /// 要被委托包装的方法
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static int CallBackMethod(string text)
        {
            return text.Length;
        }

        /// <summary>
        /// 记录事件的回调
        /// </summary>
        /// <param name="title"></param>
        /// <param name="sender"></param>
        /// <param name="s"></param>
        private static void ReportEvent(string title, object sender, EventArgs s)
        {
            Console.WriteLine("发生的事件为{0}", title);
            Console.WriteLine("发生事件的对象为{0}", sender);
            Console.WriteLine("发生事件的参数为{0}", s);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
