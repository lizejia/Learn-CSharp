using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeEvent.PerDefined
{
    public class PreEventArgs:EventArgs
    {
        public string Message { get; set; }
        public PreEventArgs(string msg)
        {
            this.Message = msg;
        }
    }

    public class Bride
    {

        public delegate void Hander(object sender, PreEventArgs e);

        /// <summary>
        /// 第二、定义事件
        /// </summary>
        public event Hander PreEventHandler;

        /// <summary>
        /// 第三、发出通知
        /// </summary>
        /// <param name="msg"></param>
        public void SendPreEvent(string msg)
        {
            if (PreEventHandler != null)
            { 
                
                PreEventHandler(this, new PreEventArgs(msg));
            }
        }
    }

    public class Frie
    {
        public string Name { get; set; }

        public Frie(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 第四，事件处理函数
        /// 定义和EventHandler委托，返回值和方法签名一致
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public void ReceivePreEvent(object sender, PreEventArgs e)
        {
            //输出通知消息
            Console.WriteLine(e.Message);
            //对事件进行处理
            Console.WriteLine(this.Name + "：已经收到了你的请帖");
        }
    }
}
