using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEvent
{
    public class BrideGroom
    {
        /// <summary>
        /// 第一
        /// 定义委托
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public delegate string MarryHander(string msg);

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
        public string SendMarryEvent(string msg)
        {
            //判断是否绑定事件
            if (MarryEvent != null)
            {
                //返回触发事件的结果
                string relust =  MarryEvent(msg);
                return relust;
            }
            return "MarryEvent事件没有绑定！！！";
        }
    }

    public class Friend
    {
        public string Name { get; set; }

        public Friend(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 第四，事件处理函数
        /// 定义和MarryHander委托，返回值和方法签名一致
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string ReceiveMarryEvent(string msg)
        {
            //输出消息
            Console.WriteLine(msg);

            //对事件进行处理
            string result = this.Name + "：已经收到了你的请帖";
            return result;
        }
    }
}
