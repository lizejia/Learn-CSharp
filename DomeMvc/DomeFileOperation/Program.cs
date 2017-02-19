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
