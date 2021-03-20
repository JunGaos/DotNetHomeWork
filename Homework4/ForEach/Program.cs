using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEach
{
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { set; get; }
        public Node(T t)
        {
            Next = null;
            Data = t;
        }
    }
    public class GenericList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public GenericList()
        {
            tail = head = null;
        }
        public Node<T> Head
        {
            get => head;
        }
        public void Add(T t)
        {
            Node<T> n = new Node<T>(t);
            if (tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }
        public void ForEach(Action<T> action)
        {
            for (Node<T> m = head; m.Next != null; m = m.Next)
            {
                action(m.Data);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            GenericList<int> l = new GenericList<int>();
            for (int i = 0; i < 10; i++)
            {
                l.Add(i);
            }
            int sum = 0;
            int max = l.Head.Data;
            int min = l.Head.Data;

            l.ForEach(x => Console.WriteLine(x));
            l.ForEach(x => sum += x);
            l.ForEach(x => { max = max > x ? max : x; });
            l.ForEach(x => { min = min < x ? min : x; });

            Console.WriteLine("最大值是：" + max);
            Console.WriteLine("最小值是：" + min);
            Console.WriteLine("总和是：" + sum);
            Console.ReadKey();
        }
    }
}
