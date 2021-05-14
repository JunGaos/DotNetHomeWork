using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OrderWinForm
{
    public class Customer
    {
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public Customer() { }
        public Customer(string name)
        {
            Name = name;
            PhoneNumber = 123;
            Address = "青岛";
        }
        public Customer(string name, int phone, string address)
        {
            Name = name;
            PhoneNumber = phone;
            Address = address;
        }
        public override string ToString()
        {
            return "姓名:" + Name + " 电话:" + PhoneNumber + " 地址:" + Address;
        }
    }

    public class Goods
    {
        public int GoodId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Goods()
        {
            GoodId = 0;
            Name = "";
            Price = 0.0;
        }
        public Goods(int id, string name, double price)
        {
            GoodId = id;
            Name = name;
            Price = price;
        }
        public override string ToString()
        {
            return "货物：" + Name + " 价格：" + Price + "元";
        }
    }

    public class OrderDetail
    {
        public int OrderItemID { get; set; }
        public Goods Good { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public OrderDetail()
        {
            Goods g = new Goods();
            Good = g;
        }
        public OrderDetail(int id, Goods g, int num)
        {
            OrderItemID = id;
            Good = g;
            Number = num;
            Price = Number * g.Price;
        }
        public override bool Equals(object obj)
        {
            var o = obj as OrderDetail;
            return o != null && o.Good.GoodId == Good.GoodId;
        }
        public override string ToString()
        {

            return "货物名称:" + Good.Name + " 数量:" + Number + " 价格:" + Price;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public class Order
    {
        public int OrderID { get; set; }
        public Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails { set; get; }
        public double TotalPrice { get; }

        public Order() { }
        public Order(int id, Customer c, List<OrderDetail> od)
        {
            OrderID = id;
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
            return o != null && o.OrderID == OrderID;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            string s = "订单ID:" + OrderID + " 订单客户:" + Customer + " 订单总价:" + TotalPrice + "\n";
            foreach (OrderDetail o in OrderDetails) s += o + "\n";
            return s;
        }
        public int CompareTo(object obj)
        {
            if (!(obj is Order))
            {
                throw new System.ArgumentException();
            }
            Order o = (Order)obj;
            if (this.OrderID > o.OrderID) return 1;
            else if (this.OrderID == o.OrderID) return 0;
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
            using (var db = new Model1())
            {
                var orderList = db.Orders.Include("OrderDetails").Include("Customer").ToList();
                foreach (Order order in orderList)
                {
                    foreach (OrderDetail orderDetails in order.OrderDetails)
                    {
                        orderDetails.Good = db.Goods.FirstOrDefault(p => p.GoodId == orderDetails.Good.GoodId);
                    }
                }
                orderList.Sort((p1, p2) => p1.TotalPrice.CompareTo(p2.TotalPrice));
                //Orders.Sort((p1, p2) => p1.OrderID.CompareTo(p2.OrderID));
            }

        }
        public void Add(Order o)
        {

            try
            {
                using (var db = new Model1())
                {
                    if (db.Orders.Contains(o))
                    {
                        Console.WriteLine($"订单已经存在,无法添加.");
                        return;
                    }
                    db.Orders.Add(o);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("添加失败!");
                Console.WriteLine(e.Message);
            }
        }
        public void Delete(int id)
        {
            try
            {
                using (var db = new Model1())
                {
                    var order = db.Orders.FirstOrDefault(p => p.OrderID == id);
                    if (order != null)
                    {
                        db.Orders.Remove(order);
                        db.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("订单不存在!");
                    }
                }
                /*foreach (Order order in Orders)
                {
                    if (order.OrderID == id)
                    {
                        Orders.Remove(order);
                        break;
                    }
                }*/
            }
            catch (Exception e)
            {
                Console.WriteLine("订单不存在，删除失败!");
                Console.WriteLine(e.Message);
            }
        }
        public void Change(int id, Order od)
        {
            try
            {
                using (var db = new Model1())
                {
                    var curOrder = db.Orders.FirstOrDefault(p => p.OrderID == id);
                    od.OrderID = curOrder.OrderID;
                    if (curOrder != null)
                    {
                        db.Orders.Remove(curOrder);
                        db.Orders.Add(od);
                        db.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("订单不存在!");
                    }
                }
                /*foreach (Order order in Orders)
                {
                    if (order.OrderID == id)
                    {
                        Orders.Remove(order);
                        Orders.Add(od);
                        break;
                    }
                }*/
            }
            catch (Exception e)
            {
                Console.WriteLine("订单修改失败!");
                Console.WriteLine(e.Message);
            }
        }
        public Order SearchByID(int info)
        {
            using (var db = new Model1())
            {
                var Orders = db.Orders;
                var result = from od in Orders
                             where od.OrderID == info
                             orderby od.TotalPrice
                             select od;
                return result.FirstOrDefault();
            }
        }
        public List<Order> SerachByGoods(string info)
        {
            using (var db = new Model1())
            {
                var Orders = db.Orders;
                var result = from od in Orders
                             from ode in od.OrderDetails
                             where ode.Good.Name == info
                             orderby od.TotalPrice
                             select od;
                return result.ToList();
            }
            /*var result = from od in Orders
                         from ode in od.OrderDetails
                         where ode.Good.Name == info
                         orderby od.TotalPrice
                         select od;
            return result.ToList();*/
        }
        public List<Order> SerachByCustomer(string info)
        {
            using (var db = new Model1())
            {
                var Orders = db.Orders;
                var result = from od in Orders
                             where od.Customer.Name == info
                             orderby od.TotalPrice
                             select od;
                return result.ToList();
            }
            /*var result = from od in Orders
                         where od.Customer.Name == info
                         orderby od.TotalPrice
                         select od;
            return result.ToList();*/
        }
        public List<Order> SearchByPrice(double info)
        {
            using (var db = new Model1())
            {
                var Orders = db.Orders;
                var result = from od in Orders
                             where od.TotalPrice == info
                             orderby od.TotalPrice
                             select od;
                return result.ToList();
            }
            /*var result = from od in Orders
                         where od.TotalPrice == info
                         orderby od.TotalPrice
                         select od;
            return result.ToList();*/
        }
        public void Write()
        {
            foreach (Order o in Orders)
            {
                Console.WriteLine(o.ToString());
            }
        }
        public void Import(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    Orders = (List<Order>)xs.Deserialize(fs);
                }
                Console.WriteLine("Import已完成");
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }
        }
        public void Export(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                xs.Serialize(fs, Orders);
            }
            Console.WriteLine("Emport已完成");
        }
    }
}
