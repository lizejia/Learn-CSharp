using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeFileOperation
{
    class Program
    {

        //

        /// <summary>
        /// 文件操作核心类
        /// 1、File静态和FileInfo实例，可以直接操作硬盘上的文件
        /// 2、Directory静态和DirectoryInfo实例，包含了用来创建，移动，删除和枚举所有目录或子目录的成员
        /// 3、Stream（流）：可以理解为内存中的字节序列，操作有查找、读取、写入；
        /// 4、Stream的四个常用派生类FileStream对文件流的操作；NetworkStream网络通信基础信息流；
        ///                     MemoryStream对内存数据进行读写操作；GZipStream用于压缩和解压缩的数据
        /// 5、用读写器对流中的数据进行操作
        /// 5.1、文本读写器 TextReader和TextWriter
        /// 5.2、字符串读写器 StringReader和StringWriter
        /// 5.3、二进制读写器 BinaryReader和BinaryWriter
        /// 5.4、流读写器 StreamReader和StreamWriter
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            FileStream FSRead = new FileStream(@"E:\Code\16\TL\01_Requirement\线框图\20141229\catgory1.html", FileMode.Open);
            FileStream FSWrite = new FileStream(@"E:\catgory1.html", FileMode.Create);

            //二进制字节流
            byte[] buffer = new byte[1024*30];
            int length = 0;
            do
            {
                length = FSRead.Read(buffer, 0, buffer.Length);

                FSWrite.Write(buffer, 0, length);
            } while (length == buffer.Length);


            //关闭文件流
            FSRead.Dispose();
            FSWrite.Dispose();
            Console.WriteLine("操作完成");
            Console.ReadKey();
        }
    }
}
