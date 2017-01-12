using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomeEvent.PerDefined;


namespace DomeEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            //从IL看出
            //+=和-=的本质是调用Delegate.Combine()和Delegate.Remove()方法

            #region Dome1test
            Dome1 dome1 = new Dome1();
            OldFriend oldfriend1 = new OldFriend("Tom");
            OldFriend oldfriend2 = new OldFriend("Roman");
            OldFriend oldfriend3 = new OldFriend("Jack");

            dome1.MarryEvent += new Dome1.MarryHander(oldfriend1.ReceiveMarryEvent);

            dome1.MarryEvent += new Dome1.MarryHander(oldfriend2.ReceiveMarryEvent);
            Console.WriteLine("*****************Dome1test开始************************");
            dome1.SendMarryEvent("朋友们，我要结婚了，准时参加啊啊啊！！！");
            Console.WriteLine("*****************取消事件************************");
            dome1.MarryEvent -= new Dome1.MarryHander(oldfriend2.ReceiveMarryEvent);
            dome1.SendMarryEvent("朋友们，我要结婚了，准时参加啊啊啊！！！");

            Console.WriteLine("*****************Dome1test结束************************");
            #endregion

            #region 自定义委托

            Console.WriteLine("*****************自定义委托开始************************");
            //初始化新郎官
            BrideGroom bridegroom = new BrideGroom();
            //初始化朋友
            Friend friend1 = new Friend("Tom");
            Friend friend2 = new Friend("Roman");
            Friend friend3 = new Friend("Jack");

            //实例化委托类型，并且传递需要包装的方法
            BrideGroom.MarryHander marryhander1 = new BrideGroom.MarryHander(friend1.ReceiveMarryEvent);
            
            //Console.WriteLine(marryhander1.Invoke("dsadsadsad"));
            BrideGroom.MarryHander marryhander2 = new BrideGroom.MarryHander(friend2.ReceiveMarryEvent);
            marryhander1 += marryhander2;


            List<string> list = new List<string>();
            Delegate[] myDelegates = marryhander1.GetInvocationList();
            foreach (var myDelegate in myDelegates)
            {
                BrideGroom.MarryHander m1 = (BrideGroom.MarryHander)myDelegate;//注意此处需强转
                list.Add(m1.Invoke("结婚啦"));//等价于list.Add(m1(2));
            }
            foreach (var i in list)
            {
                Console.WriteLine(i);
            }


            //+=订阅事件
            bridegroom.MarryEvent += marryhander1;

            //发出通知
            //Console.WriteLine(marryhander1.Invoke("结婚啦"));
            string msg = bridegroom.SendMarryEvent("朋友们，我要结婚了，准时参加啊啊啊！！！");

            Console.WriteLine(msg);

            Console.WriteLine("****结论：发现输出的是最后的绑定事件，但是每次都输出了两次消息，说明只是后一条返回值覆盖了前一个返回值！！！****");
            Console.WriteLine("****思考：在事件处理函数中ReceiveMarryEvent，需要循环取出每一次的返回值，才能显示全部显示，（现在还没有想到怎么去循环取出????）****");
            Console.WriteLine("*****************自定义委托结束************************");
            #endregion


            #region 预定义委托类型
            Console.WriteLine("*****************预定义委托开始************************");
            //初始化新郎官
            DomeEvent.PerDefined.Bride bride = new DomeEvent.PerDefined.Bride();
            //初始化朋友
            Frie riend1 = new Frie("Tom");
            Frie riend2 = new Frie("Roman");
            Frie riend3 = new Frie("Jack");

            bride.PreEventHandler += new Bride.Hander(riend1.ReceivePreEvent);
            bride.PreEventHandler += new Bride.Hander(riend2.ReceivePreEvent);

            bride.SendPreEvent("我是新娘，我要结婚啦");
            Console.WriteLine("*****************预定义委托结束************************");
            #endregion
            Console.ReadKey();
        }

    }
}
