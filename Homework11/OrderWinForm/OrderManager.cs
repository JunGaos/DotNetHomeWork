using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public string Id { get; set; }
        public string Address { get; set; }
        public Customer() { }
        public Customer(string name)
        {
            Name = name;
            Address = "青岛";
        }
        public Customer(string name, string id, string address)
        {
            Name = name;
            Id = id;
            Address = address;
        }
        public override string ToString()
        {
            return "姓名:" + Name + " ID:" + Id + " 地址:" + Address;
        }
        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null &&
                   Id == customer.Id &&
                   Name == customer.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = 1479869798;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }

    public class Goods
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Goods()
        {
            Id = Guid.NewGuid().ToString();
        }
        public Goods(string name, double price)
        {
            Name = name;
            Price = price;
        }
        public override string ToString()
        {
            return "货物：" + Name + " 价格：" + Price + "元";
        }
        public override bool Equals(object obj)
        {
            var goods = obj as Goods;
            return goods != null &&
                   Id == goods.Id &&
                   Name == goods.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = 1479869798;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }

    public class OrderDetail
    {
        [Key]
        public string Id { get; set; }

        public int Index { get; set; } //序号

        public string GoodsId { get; set; }

        public Goods Goods { get; set; }

        public String GoodsName { get => Goods != null ? this.Goods.Name : ""; }

        public double UnitPrice { get => Goods != null ? this.Goods.Price : 0.0; }

        public string OrderId { get; set; }

        public int Number { get; set; }
        public OrderDetail()
        {
            Id = Guid.NewGuid().ToString();
        }
        public OrderDetail(int index, Goods goods, int number)
        {
            this.Index = index;
            this.Goods = goods;
            this.Number = number;
        }
        public double TotalPrice
        {
            get => Goods == null ? 0.0 : Goods.Price * Number;
        }
        public override bool Equals(object obj)
        {
            var item = obj as OrderDetail;
            return item != null &&
                   GoodsName == item.GoodsName;
        }
        public override string ToString()
        {

            return $"[No.:{Index},goods:{GoodsName},number:{Number},totalPrice:{TotalPrice}]";
        }
        public override int GetHashCode()
        {
            var hashCode = -2127770830;
            hashCode = hashCode * -1521134295 + Index.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GoodsName);
            hashCode = hashCode * -1521134295 + Number.GetHashCode();
            return hashCode;
        }
    }
    public class Order:IComparable<Order>
    {
        [Key]
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string CustomerName { get => (Customer != null) ? Customer.Name : ""; }
        public List<OrderDetail> OrderDetails { set; get; }
        public double TotalPrice { get; }

        public Order() {
            OrderId = Guid.NewGuid().ToString();
            OrderDetails = new List<OrderDetail>();
        }
        public Order(string orderId, Customer customer, List<OrderDetail> od) : this()
        {
            this.OrderId = orderId;
            this.Customer = customer;
            this.OrderDetails = od;
            TotalPrice = 0;
            foreach (OrderDetail o in od)
            {
                TotalPrice += o.TotalPrice;
            }
        }
        public void AddDetail(OrderDetail orderDetail)
        {
            if (OrderDetails.Contains(orderDetail))
                throw new ApplicationException($"添加错误：订单项{orderDetail.GoodsName} 已经存在!");
            OrderDetails.Add(orderDetail);
        }
        public void RemoveDetail(OrderDetail orderDetail)
        {
            OrderDetails.Remove(orderDetail);
        }
        public override bool Equals(object obj)
        {
            var order = obj as Order;
            return order != null &&
                   OrderId == order.OrderId;
        }
        public override int GetHashCode()
        {
            var hashCode = -531220479;
            hashCode = hashCode * -1521134295 + OrderId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CustomerName);
            return hashCode;
        }
        public override string ToString()
        {
            string s = "订单ID:" + OrderId + " 订单客户:" + Customer + " 订单总价:" + TotalPrice + "\n";
            foreach (OrderDetail o in OrderDetails) s += o + "\n";
            return s;
        }
        public int CompareTo(Order obj)
        {
            if (obj == null) return 1;
            return this.OrderId.CompareTo(obj.OrderId);
        }
    }
    public class OrderService
    {
        //public List<Order> Orders { get; set; }
        //public OrderService() { }
        public OrderService()
        {
            using (var m = new Model1())
            {
                if (m.Goods.Count() == 0)
                {
                    m.Goods.Add(new Goods("苹果", 100.0));
                    m.Goods.Add(new Goods("梨", 200.0));
                    m.SaveChanges();
                }
                if (m.Customers.Count() == 0)
                {
                    m.Customers.Add(new Customer("A"));
                    m.Customers.Add(new Customer("B"));
                    m.SaveChanges();
                }
            }
        }
        public List<Order> Orders
        {
            get
            {
                using (var m = new Model1())
                {
                    return m.Orders.Include(o => o.OrderDetails.Select(d => d.Goods)).Include("Customer").
                      ToList<Order>();
                }
            }
        }

        public Order GetOrder(string id)
        {
            using (var m = new Model1())
            {
                return m.Orders.Include(o => o.OrderDetails.Select(d => d.Goods)).Include("Customer")
                  .SingleOrDefault(o => o.OrderId == id);
            }
        }

        //避免级联添加或修改Customer和Goods
        private static void FixOrder(Order newOrder)
        {
            newOrder.CustomerId = newOrder.Customer.Id;
            newOrder.Customer = null;
            newOrder.OrderDetails.ForEach(d => {
                d.GoodsId = d.Goods.Id;
                d.Goods = null;
            });
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
                        orderDetails.Goods = db.Goods.FirstOrDefault(p => p.Id == orderDetails.Goods.Id);
                    }
                }
                orderList.Sort((p1, p2) => p1.TotalPrice.CompareTo(p2.TotalPrice));
                //Orders.Sort((p1, p2) => p1.OrderID.CompareTo(p2.OrderID));
            }

        }
        public void Add(Order order)
        {
            FixOrder(order);
            using (var m = new Model1())
            {
                m.Entry(order).State = EntityState.Added;
                m.SaveChanges();
            }
        }
        public void Delete(string id)
        {
            /*try
            {
                using (var db = new Model1())
                {
                    var order = db.Orders.FirstOrDefault(p => p.OrderId == id);
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
            }
            catch (Exception e)
            {
                Console.WriteLine("订单不存在，删除失败!");
                Console.WriteLine(e.Message);
            }*/
            using (var m = new Model1())
            {
                var order = m.Orders.Include("Details").SingleOrDefault(o => o.OrderId == id);
                if (order == null) return;
                m.OrderDetails.RemoveRange(order.OrderDetails);
                m.Orders.Remove(order);
                m.SaveChanges();
            }
        }
        public void Change(string id, Order od)
        {
            try
            {
                using (var db = new Model1())
                {
                    var curOrder = db.Orders.FirstOrDefault(p => p.OrderId == id);
                    od.OrderId = curOrder.OrderId;
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
        public List<Order> SearchByID(string info)
        {
            /*using (var db = new Model1())
            {
                var Orders = db.Orders;
                var result = from od in Orders
                             where od.OrderId == info
                             orderby od.TotalPrice
                             select od;
                return result.FirstOrDefault();
            }*/
            using (var m = new Model1())
            {
                return m.Orders.Include(o => o.OrderDetails.Select(d => d.Goods)).Include("Customer").
                    Where(order => order.OrderId == info).ToList();
            }
        }
        public List<Order> SerachByGoods(string info)
        {
            /*using (var db = new Model1())
            {
                var Orders = db.Orders;
                var result = from od in Orders
                             from ode in od.OrderDetails
                             where ode.Goods.Name == info
                             orderby od.TotalPrice
                             select od;
                return result.ToList();
            }*/
            using (var m = new Model1())
            {
                var query = m.Orders.Include(o => o.OrderDetails.Select(d => d.Goods)).Include("Customer")
                    .Where(order => order.OrderDetails.Any(item => item.Goods.Name == info));
                return query.ToList();
            }
        }
        public List<Order> SerachByCustomer(string info)
        {
            /*using (var db = new Model1())
            {
                var Orders = db.Orders;
                var result = from od in Orders
                             where od.Customer.Name == info
                             orderby od.TotalPrice
                             select od;
                return result.ToList();
            }*/
            using (var m = new Model1())
            {
                return m.Orders.Include(o => o.OrderDetails.Select(d => d.Goods)).Include("Customer")
                  .Where(order => order.Customer.Name == info).ToList();
            }
        }
        public object SearchByPrice(double info)
        {
            /*using (var db = new Model1())
            {
                var Orders = db.Orders;
                var result = from od in Orders
                             where od.TotalPrice == info
                             orderby od.TotalPrice
                             select od;
                return result.ToList();
            }*/
            using (var m = new Model1())
            {
                return m.Orders.Include(o => o.OrderDetails.Select(d => d.Goods)).Include("Customer")
                .Where(order => order.OrderDetails.Sum(d => d.Number * d.Goods.Price) > info)
                .ToList();
            }
        }
        public void UpdateOrder(Order newOrder)
        {
            Delete(newOrder.OrderId);
            Add(newOrder);
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
            /*XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
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
            }*/
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (var m = new Model1())
                {
                    List<Order> temp = (List<Order>)xs.Deserialize(fs);
                    temp.ForEach(order => {
                        if (m.Orders.SingleOrDefault(o => o.OrderId == order.OrderId) == null)
                        {
                            FixOrder(order);
                            m.Orders.Add(order);
                        }
                    });
                    m.SaveChanges();
                }
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
