using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeFactor
{
    class Program
    {
        static void Main(string[] args)
        {
            int n,m;
            string s;
            bool b = true;
            try
            {
                Console.Write("请输入要分解的数：");
                s = Console.ReadLine();
                n = int.Parse(s);
                m = n;
                Console.Write("输入数据的所有素数因子为：");
                Program p=new Program();
                for(int i = 1; i*i <= m; i++)//输出其所有素数因子
                {
                    if (p.Factor(ref n,i,ref b))
                    {
                        Console.Write(i+" ");
                    }
                }
                if (b)//判断数字本身是否为素数
                {
                    Console.Write(m+"\n");
                }                
            }
            catch (FormatException)
            {
                Console.Write("输入的不是数字。");
            }
            Console.ReadKey();
        }
        bool Factor(ref int n, int i, ref bool b)
        {
            if (i == 1)
            {
                return true;
            }
            else
            {
                if (n % i != 0)
                {
                    return false;
                }
                else
                {
                    b = false;
                    while (n % i == 0)
                    {
                        n = n / i;
                    }
                    return true;
                }
                
            }
        }
    }
}
