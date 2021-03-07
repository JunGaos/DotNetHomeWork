using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esieve
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("用“埃氏筛法”求得的2~ 100以内的素数如下所示：\n");
            for (int i = 2; i <= 100; i++)
            {
                bool b = false;
                for (int j = 2; j*j <= i; j++)
                {
                    if (i % j == 0)
                    {
                        b = true;
                    }
                }
                if (b)
                {
                    continue;
                }
                Console.WriteLine(i+" ");
            }
            Console.ReadKey();
        }    
    }    
}
