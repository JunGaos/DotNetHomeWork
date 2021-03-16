using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shape
{
    public interface IShape
    {
        double GetArea();
        bool JudgeShape();
    }
    public class Square:IShape
    {
        private double length;
        public double Length
        {
            set{length = value;}
            get{return length;}
        }
        public Square() { }
        public Square(double d) {
            this.length = d;
        }
        public double GetArea()
        {
            return length * length;
        }
        public bool JudgeShape()
        {
            return (length > 0);
        }
    }
    public class Rectangle : IShape
    {
        private double length;
        private double width;
        public double Length
        {
            set{ length = value;}
            get{ return length; }
        }
        public double Width
        {
            set{ width = value;}
            get{ return width;}
        }
        public Rectangle() { }
        public Rectangle(double d1,double d2)
        {
            this.length = d1;
            this.width = d2;
        }
        public double GetArea()
        {
            return length * width;
        }
        public bool JudgeShape()
        {
            return (length > 0 && width > 0);
        }
    }
    public class Triangle : IShape
    {
        private double a;
        private double b;
        private double c;
        public double A
        {
            set { a = value; }
            get { return a; }
        }
        public double B
        {
            set { b = value; }
            get { return b; }
        }
        public double C
        {
            set { c = value; }
            get { return c; }
        }
        public Triangle() { }
        public Triangle(double d1, double d2,double d3)
        {
            this.a = d1;
            this.b = d2;
            this.c = d3;
        }
        public double GetArea()
        {
            double p = (a + b + c) / 2;
            double s = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            return s;
        }
        public bool JudgeShape()
        {
            return (a > 0 && b > 0 && c > 0 && a + b > c && a + c > b && c + b > a);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Square s = new Square();
            s.Length = 5.2;
            Rectangle r = new Rectangle();
            r.Length = 3.5;
            r.Width = 2.5;
            Triangle t = new Triangle();
            t.A = 3.0;
            t.B = 4.0;
            t.C = 5.0;
            Console.WriteLine("正方形的面积为：" +s.GetArea());
            if (s.JudgeShape())
            {
                Console.WriteLine("该正方形形状合法。");
            }
            Console.WriteLine("长方形的面积为：" + r.GetArea());
            if (r.JudgeShape())
            {
                Console.WriteLine("该长方形形状合法。");
            }
            Console.WriteLine("三角形的面积为：" + t.GetArea());
            if (t.JudgeShape())
            {
                Console.WriteLine("该三角形形状合法。");
            }
            Console.ReadKey();
        }
    }
}
