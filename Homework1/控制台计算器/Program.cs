using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 控制台计算器
{
    class Program
    {
        static void Main(string[] args)
        {
            int n1, n2;
            string s1, s2, s3;
            try
            {
                Console.Write("请输入要计算的第一个数：");
                s1 = Console.ReadLine();
                n1 = int.Parse(s1);
                Console.Write("请输入要计算的第二个数：");
                s2 = Console.ReadLine();
                n2 = int.Parse(s2);
                Console.Write("请输入运算符：");
                s3 = Console.ReadLine();
                switch (s3)
                {
                    case "+":
                        Console.WriteLine($"{n1}+{n2}={n1 + n2}");
                        break;
                    case "-":
                        Console.WriteLine($"{n1}-{n2}={n1 - n2}");
                        break;
                    case "*":
                        Console.WriteLine($"{n1}*{n2}={n1 * n2}");
                        break;
                    case "/":
                        Console.WriteLine($"{n1}/{n2}={n1 / n2}");
                        break;
                    default:
                        throw new ArithmeticException();

                }
            }
            catch (FormatException)
            {
                Console.Write("输入的不是数字");
            }
            catch (ArithmeticException)
            {
                Console.Write("输入的运算符不合法。");
            }
            Console.ReadKey();
        }
    }
}
