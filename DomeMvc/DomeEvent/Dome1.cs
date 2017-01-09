using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEvent
{
    public class Dome1
    {
        /// <summary>
        /// 第一
        /// 定义委托
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public delegate void MarryHander(string msg);

        /// <summary>
        /// 第二
        /// 定义MarryHander事件
        /// </summary>
        public event MarryHander MarryEvent;

        /// <summary>
        /// 第三
        /// 发布事件
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public void SendMarryEvent(string msg)
        {
            //判断是否绑定事件
            if (MarryEvent != null)
            {
                //返回触发事件的结果
                MarryEvent(msg);
            }
            
        }
    }

    public class OldFriend
    {
        public string Name { get; set; }

        public OldFriend(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 第四，事件处理函数
        /// 定义和MarryHander委托，返回值和方法签名一致
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public void ReceiveMarryEvent(string msg)
        {
            //对事件进行处理
            Console.WriteLine(this.Name + "：已经收到了你的请帖");
        }
    }
}
