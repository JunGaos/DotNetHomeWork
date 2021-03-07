using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToeplitzMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] a;
            int m, n;
            Console.WriteLine("请依次输入矩阵的行数和列数：");
            m = int.Parse(Console.ReadLine());
            n = int.Parse(Console.ReadLine());
            a = new int[m, n];
            Console.WriteLine("请依次输入每一个数据：");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = int.Parse(Console.ReadLine());
                }
            }
            Program p = new Program();
            if (p.WheatherToeplitz(a, m, n))
            {
                Console.WriteLine("这是一个托普利茨矩阵.");
            }
            else
            {
                Console.WriteLine("这不是一个托普利茨矩阵.");
            }
            Console.ReadKey();
        }
        bool WheatherToeplitz(int[,] a, int m, int n)
        {
            for (int i = 0; i < m - 1; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    if (a[i, j] != a[i + 1, j + 1])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
