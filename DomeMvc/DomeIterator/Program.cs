using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeIterator
{
    class Program
    {
        static void Main(string[] args)
        {

            Friends fs = new Friends();
            //第一步fs调用GetEnumerator()
            //第二步in调用MoveNext()
            //第三步Friend返回Current
            //然后循环第二步和第三步
            foreach (Friend item in fs)
            {
                Console.WriteLine(item.Name);
            }
            Console.ReadKey();
        }


        public class Friend 
        {
            public string Name { get; set; }
            public Friend(string name)
            {
                this.Name = name;
            }
        }

        public class Friends : IEnumerable
        {
            private Friend[] friendarray;
            public Friends()
            {
                friendarray = new Friend[]
                {
                    new Friend("张三"),
                    new Friend("李四"),
                    new Friend("王麻子")
                };
            }
            /// <summary>
            /// 索引器
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public Friend this[int index]
            {
                get { return friendarray[index]; }
            }

            public int Count
            {
                get { return friendarray.Length; }
            }

            /// <summary>
            /// 实现IEnumerable接口方法
            /// 要获得迭代器必须实现IEnumerable.GetEnumerator()方法
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                //在C#1.0中实现一个迭代器
                //return new FriendIterator(this);

                //在C#2.0中实现一个迭代器
                for (int i = 0; i < friendarray.Length; i++)
                {
                    yield return friendarray[i];
                }
                
            }
        }

        /// <summary>
        /// 在C#1.0中实现一个迭代器
        /// 必须实现IEnumerator的Current()、Reset()和MoveNext()
        /// </summary>
        public class FriendIterator : IEnumerator
        {
            private readonly Friends friends;
            private int index;
            private Friend current;

            public FriendIterator(Friends _friends)
            {
                this.friends = _friends;
                this.index = 0;
            }


            public object Current
            {
                get { return this.current; }
            }

            public bool MoveNext()
            {
                if (index + 1 > friends.Count)
                {
                    return false;
                }
                else
                {
                    this.current = friends[index];
                    index++;
                    return true;
                }
            }

            public void Reset()
            {
                index = 0;
            }
        }

    }
}
