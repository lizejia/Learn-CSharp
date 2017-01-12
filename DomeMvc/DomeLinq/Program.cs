using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new[] { new { Name = "roman", Age = 3 }, new { Name = "tom", Age = 8 } };

            int[] numbers = new int[5] { 1, 2, 3, 4, 5 };

            //语言集成查询 (LINQ) 是一组技术的名称Language Integrated Query
            //有四个组件
            //1、Linq to Sql
            //2、Linq to DataSet   
            //3、Linq to XML
            //4、Linq to Objects   查询集合数据 数组和List
            //本质：扩展方法（Where，Any）和Lambda表达式组合成LINQ


            //查询表达式，是以form开头，select和group结尾，在他们中间可以写多个where，orderby,join子句
            //from item  in 数据源
            //where 条件
            //select item
            //查询表达式另一种写法：点标记方式(条件少的时候用)
            var y = person.Any(x => x.Name == "roman");

            var yy = person.Where(x => x.Age > 5).Select(i => i.Name);//查询年龄大于5，并且返回属性只有Name




            //查询变量本身只是存储查询命令。 实际的查询执行会延迟到在 foreach 语句中循环访问查询变量时发生。
            //此概念称为“延迟执行”
            //IEnumerable<int>
            var numquerys = from item in numbers
                            where item > 2
                            select item;


            //强制立即执行用.ToArray()和.ToList()
            //List<int>类型
            var numlists = (from item in numbers
                             where item > 2
                             select item).ToList();
            //int[]类型
            var numarrays = (from item in numbers
                             where item > 2
                             select item).ToArray();

            foreach (var item in numquerys)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
