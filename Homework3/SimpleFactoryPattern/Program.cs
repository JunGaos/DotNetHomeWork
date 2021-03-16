using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shape;

namespace SimpleFactoryPattern
{
    public class ShapeFactory
    {
        public static IShape GetRandomShape(int seed)
        {
            Random r = new Random(seed);
            int i = r.Next(0, 3);
            IShape shape = null;
            switch (i)
            {
                case 0:
                    shape = new Square(r.NextDouble() + r.Next(0, 5));
                    Console.WriteLine("创建正方形。");
                    break;
                case 1:
                    shape = new Rectangle(r.NextDouble() + r.Next(0, 5), r.NextDouble() + r.Next(0, 5));
                    Console.WriteLine("创建长方形。");
                    break;
                case 2:
                    shape = new Triangle(r.NextDouble() + r.Next(0, 5), r.NextDouble() + r.Next(0, 5), r.NextDouble() + r.Next(0, 5));
                    Console.WriteLine("创建三角形。");
                    break;
            }
            return shape;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            double allArea=0.0;
            for(int i = 0; i < 10; i++)
            {
                IShape s=ShapeFactory.GetRandomShape(i);
                if (s.JudgeShape())
                {
                    allArea += s.GetArea();
                }
            }
            Console.WriteLine("随机创建的十个图形总面积（剔除不合法图形）为：" + allArea);
            Console.ReadKey();
        }
    }
}
