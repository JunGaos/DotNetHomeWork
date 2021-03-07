using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayElement
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a;
            int n;
            Console.Write("请输入进行求值的数组的元素个数:");
            try
            {
                n = int.Parse(Console.ReadLine());
                a = new int[n];
                Console.Write("请依次输入进行求值的数组的各个元素的值（每输入一个数值敲一次回车）:\n");
                for (int i = 0; i < n; i++)
                {
                    a[i]= int.Parse(Console.ReadLine());
                }

                Program p = new Program();
                p.Sort(a);
                Console.Write("数组元素的最大值为：" +a[a.Length-1]+ "\n");
                Console.Write("数组元素的最小值为：" +a[0]+"\n");
                p.Average(a);
                p.Sum(a);
            }
            catch (FormatException)
            {
                Console.Write("输入的不是数字。");
            }            
            Console.ReadKey();
        }
        void Sort(int[] a)
        {
            int temp;
            for(int i = 0; i < a.Length - 1; i++)
            {
                for(int j = a.Length - 1; j > i; j--)
                {
                    if (a[j] < a[j - 1])
                    {
                        temp = a[j];
                        a[j] = a[j - 1];
                        a[j - 1] = temp;
                    }
                }               
            }            
        }
        void Average(int[] a)
        {
            double sum=0.0;
            double n;
            for(int i=0; i < a.Length; i++)
            {
                sum += a[i];
            }
            n = sum / a.Length;
            Console.Write("数组元素的平均值为："+n+"\n");
        }
        void Sum(int[] a)
        {
            int sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i];
            }
            Console.Write("数组元素值的和为："+sum+"\n");
        }
    }
}
