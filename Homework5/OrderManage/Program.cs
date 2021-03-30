using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManage
{
    public class Customer
    {
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public Customer() { }
        public Customer(string name,int phone,string address){
            Name = name;
            PhoneNumber = phone;
            Address = address;
        }
        public override string ToString(){
            return "姓名:"+Name+" 电话:"+PhoneNumber+" 地址:"+Address;
        }
    }

    public class Goods
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Goods(){}
        public Goods(int id, string name, double price)
        {
            ID = id;
            Name = name;
            Price = price;
        }
        public override string ToString()
        {
            return "货物："+Name+" 价格："+Price+"元";
        }
    }

    public class OrderDetail
    {
        public int ID { get; set; }
        public Goods Good { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public OrderDetail(){}
        public OrderDetail(int id, Goods g, int num)
        {
            ID = id;
            Good = g;
            Number = num;
            Price = Number * g.Price;
        }
        public override bool Equals(object obj)
        {
            var o = obj as OrderDetail;
            return o != null && o.Good.ID == Good.ID;
        }
        public override string ToString()
        {

            return "货物名称:"+Good.Name+" 数量:"+Number+" 价格:"+Price;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public class Order
    {
        public int ID { get; set; }
        public Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails{ set; get; }
        public double TotalPrice { get; }

        public Order() { }
        public Order(int id, Customer c, List<OrderDetail> od)
        {
            ID = id;
            Customer = c;
            OrderDetails = od;
            TotalPrice = 0;
            foreach (OrderDetail o in od)
            {
                TotalPrice += o.Price;
            }
        }
        public void AddDetail(OrderDetail orderDetail)
        {
            if (OrderDetails.Contains(orderDetail))
                throw new ApplicationException($"订单项已经存在,无法添加.");
            OrderDetails.Add(orderDetail);
        }
        public void RemoveDetail(OrderDetail orderDetail)
        {
            OrderDetails.Remove(orderDetail);
        }
        public override bool Equals(object obj)
        {
            Order o = obj as Order;
            return o != null && o.ID == ID;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            string s = "订单ID:" + ID + " 订单客户:" + Customer + " 订单总价:" + TotalPrice + "\n";
            foreach (OrderDetail o in OrderDetails) s += o + "\n";
            return s;
        }
        public int CompareTo(object obj)
        {
            if(!(obj is Order))
            {
                throw new System.ArgumentException();
            }
            Order o = (Order)obj;
            if (this.ID > o.ID) return 1;
            else if (this.ID == o.ID) return 0;
            else return -1;
        }
    }
    public class OrderService
    {
        public List<Order> Orders { get; set; }
        public OrderService() { }
        public OrderService(List<Order> orders)
        {
            Orders = orders;
        }
        public void Sort()
        {
            Orders.Sort((p1, p2) => p1.ID.CompareTo(p2.ID));
        }
        public void Add(Order o)
        {
            if (Orders.Contains(o))
                throw new ApplicationException($"订单已经存在,无法添加.");
            Orders.Add(o);
        }
        public void Delete(int id)
        {
            try
            {
                foreach (Order order in Orders)
                {
                    if (order.ID == id)
                    {
                        Orders.Remove(order);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("订单不存在，删除失败!");
                Console.WriteLine(e.Message);
            }
        }
        public void Change(Order od, Customer c)
        {
            try
            {
                int index = Orders.IndexOf(od);
                od.Customer = c;
                Orders[index].Customer = c;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public void Change(Order od, int id)
        {
            try
            {
                int index = Orders.IndexOf(od);
                od.ID = id;
                Orders[index].ID = id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public Order SearchByID(int info)
        {
            var result = from od in Orders
                         where od.ID == info
                         orderby od.TotalPrice
                         select od;
            return result.FirstOrDefault();
        }
        public Order SerachByGoods(string info)
        {
            var result = from od in Orders
                         from ode in od.OrderDetails
                         where ode.Good.Name == info
                         orderby od.TotalPrice
                         select od;
            return result.FirstOrDefault();
        }
        public Order SerachByCustomer(Customer info)
        {
            var result = from od in Orders
                         where od.Customer.Equals(info)
                         orderby od.TotalPrice
                         select od;
            return result.FirstOrDefault();
        }
        public Order SearchByPrice(double info)
        {
            var result = from od in Orders
                         where od.TotalPrice == info
                         orderby od.TotalPrice
                         select od;
            return result.FirstOrDefault();
        }
        public void Write(){
            foreach(Order o in Orders)
            {
                Console.WriteLine(o.ToString());
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Customer c1 = new Customer("小红", 123, "武汉");
            Customer c2 = new Customer("小蓝", 123, "青岛");
            Customer c3 = new Customer("小绿", 123, "北京");

            Goods g1 = new Goods(01,"苹果", 3.5);
            Goods g2 = new Goods(02, "梨", 5.5);
            Goods g3 = new Goods(03, "香蕉", 4.5);

            OrderDetail ode1 = new OrderDetail(01, g1, 9);
            OrderDetail ode2 = new OrderDetail(02, g2, 5);
            OrderDetail ode3 = new OrderDetail(03, g3, 7);
            OrderDetail ode4 = new OrderDetail(04, g2, 4);
            OrderDetail ode5 = new OrderDetail(05, g1, 6);

            List<OrderDetail> l1 = new List<OrderDetail>();
            l1.Add(ode1);l1.Add(ode3);
            List<OrderDetail> l2 = new List<OrderDetail>();
            l2.Add(ode2); l2.Add(ode4);
            List<OrderDetail> l3 = new List<OrderDetail>();
            l3.Add(ode1); l3.Add(ode2); l3.Add(ode5);

            Order o1 = new Order(02, c1, l1);
            Order o2 = new Order(01, c2, l2);
            Order o3 = new Order(03, c3, l3);

            List<Order> l4 = new List<Order>();
            l4.Add(o1); l4.Add(o2); l4.Add(o3);
            OrderService oe = new OrderService(l4);
            Console.WriteLine("当前所有订单如下所示:\n");
            oe.Write();
            Console.WriteLine("按订单号排序后的所有订单:\n");
            oe.Sort();
            oe.Write();
            Console.WriteLine("删除订单编号为02后的订单:\n");
            oe.Delete(02);
            oe.Write();
            Console.WriteLine("查询编号为03的订单:\n");
            Console.WriteLine(oe.SearchByID(03).ToString());
            Console.WriteLine("查询商品是苹果的订单:\n");
            Console.WriteLine(oe.SerachByGoods("苹果").ToString());
            Console.WriteLine("查询小蓝的订单:\n");
            Console.WriteLine(oe.SerachByCustomer(c2).ToString());           
            Console.WriteLine("查询价格为80的订单:\n");
            Console.WriteLine(oe.SearchByPrice(80.0).ToString());
            Console.ReadKey();
        }
    }
}
