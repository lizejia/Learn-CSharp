using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DomeRecursion
{
    class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        //static void Main(string[] args)
        //{
        //    Test(0);
        //    Console.ReadKey();
        //}

        /// <summary>
        /// 递归就是方法自己调用自己
        /// </summary>
        /// <param name="sysNo"></param>
        static void Test(int sysNo)
        {
            sysNo++;
            if (sysNo < 10)
            {
                Test(sysNo);
            }
            Console.WriteLine(sysNo);
        }
    }
}
