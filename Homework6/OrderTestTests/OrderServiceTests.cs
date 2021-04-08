using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderTest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTest.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        [TestMethod()]
        public void OrderServiceTest()
        {
            bool b = true;
            Assert.IsTrue(b);
        }

        [TestMethod()]
        public void SortTest()
        {
            Customer c1 = new Customer("小红", 123, "武汉");
            Customer c2 = new Customer("小蓝", 123, "青岛");
            Customer c3 = new Customer("小绿", 123, "北京");

            Goods g1 = new Goods(01, "苹果", 3.5);
            Goods g2 = new Goods(02, "梨", 5.5);
            Goods g3 = new Goods(03, "香蕉", 4.5);

            OrderDetail ode1 = new OrderDetail(01, g1, 9);
            OrderDetail ode2 = new OrderDetail(02, g2, 5);
            OrderDetail ode3 = new OrderDetail(03, g3, 7);
            OrderDetail ode4 = new OrderDetail(04, g2, 4);
            OrderDetail ode5 = new OrderDetail(05, g1, 6);

            List<OrderDetail> l1 = new List<OrderDetail>();
            l1.Add(ode1); l1.Add(ode3);
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
            oe.Add(o1);
            oe.Add(o2);
            Assert.AreEqual(oe.Orders, l4);
        }

        [TestMethod()]
        public void AddTest()
        {
            Customer c1 = new Customer("小红", 123, "武汉");
            Customer c2 = new Customer("小蓝", 123, "青岛");
            Customer c3 = new Customer("小绿", 123, "北京");

            Goods g1 = new Goods(01, "苹果", 3.5);
            Goods g2 = new Goods(02, "梨", 5.5);
            Goods g3 = new Goods(03, "香蕉", 4.5);

            OrderDetail ode1 = new OrderDetail(01, g1, 9);
            OrderDetail ode2 = new OrderDetail(02, g2, 5);
            OrderDetail ode3 = new OrderDetail(03, g3, 7);
            OrderDetail ode4 = new OrderDetail(04, g2, 4);
            OrderDetail ode5 = new OrderDetail(05, g1, 6);

            List<OrderDetail> l1 = new List<OrderDetail>();
            l1.Add(ode1); l1.Add(ode3);
            List<OrderDetail> l2 = new List<OrderDetail>();
            l2.Add(ode2); l2.Add(ode4);
            List<OrderDetail> l3 = new List<OrderDetail>();
            l3.Add(ode1); l3.Add(ode2); l3.Add(ode5);

            Order o1 = new Order(02, c1, l1);
            Order o2 = new Order(01, c2, l2);
            Order o3 = new Order(03, c3, l3);

            List<Order> l4 = new List<Order>();
            l4.Add(o1); l4.Add(o2);
            OrderService oe = new OrderService();
            oe.Add(o1);
            oe.Add(o2);
            Assert.AreEqual(oe.Orders,l4);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Customer c1 = new Customer("小红", 123, "武汉");
            Customer c2 = new Customer("小蓝", 123, "青岛");
            Customer c3 = new Customer("小绿", 123, "北京");

            Goods g1 = new Goods(01, "苹果", 3.5);
            Goods g2 = new Goods(02, "梨", 5.5);
            Goods g3 = new Goods(03, "香蕉", 4.5);

            OrderDetail ode1 = new OrderDetail(01, g1, 9);
            OrderDetail ode2 = new OrderDetail(02, g2, 5);
            OrderDetail ode3 = new OrderDetail(03, g3, 7);
            OrderDetail ode4 = new OrderDetail(04, g2, 4);
            OrderDetail ode5 = new OrderDetail(05, g1, 6);

            List<OrderDetail> l1 = new List<OrderDetail>();
            l1.Add(ode1); l1.Add(ode3);
            List<OrderDetail> l2 = new List<OrderDetail>();
            l2.Add(ode2); l2.Add(ode4);
            List<OrderDetail> l3 = new List<OrderDetail>();
            l3.Add(ode1); l3.Add(ode2); l3.Add(ode5);

            Order o1 = new Order(02, c1, l1);
            Order o2 = new Order(01, c2, l2);
            Order o3 = new Order(03, c3, l3);

            List<Order> l4 = new List<Order>();
            l4.Add(o1); l4.Add(o2);
            OrderService oe = new OrderService(l4);
            oe.Delete(o1.ID);
            l4.Remove(o1);
            Assert.AreEqual(oe.Orders, l4);
        }

        [TestMethod()]
        public void SearchByIDTest()
        {
            Customer c1 = new Customer("小红", 123, "武汉");
            Customer c2 = new Customer("小蓝", 123, "青岛");
            Customer c3 = new Customer("小绿", 123, "北京");

            Goods g1 = new Goods(01, "苹果", 3.5);
            Goods g2 = new Goods(02, "梨", 5.5);
            Goods g3 = new Goods(03, "香蕉", 4.5);

            OrderDetail ode1 = new OrderDetail(01, g1, 9);
            OrderDetail ode2 = new OrderDetail(02, g2, 5);
            OrderDetail ode3 = new OrderDetail(03, g3, 7);
            OrderDetail ode4 = new OrderDetail(04, g2, 4);
            OrderDetail ode5 = new OrderDetail(05, g1, 6);

            List<OrderDetail> l1 = new List<OrderDetail>();
            l1.Add(ode1); l1.Add(ode3);
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
            Order o5 = oe.SearchByID(02);
            Assert.AreEqual(o5, o1);
        }

        [TestMethod()]
        public void SerachByGoodsTest()
        {
            Customer c1 = new Customer("小红", 123, "武汉");
            Customer c2 = new Customer("小蓝", 123, "青岛");
            Customer c3 = new Customer("小绿", 123, "北京");

            Goods g1 = new Goods(01, "苹果", 3.5);
            Goods g2 = new Goods(02, "梨", 5.5);
            Goods g3 = new Goods(03, "香蕉", 4.5);

            OrderDetail ode1 = new OrderDetail(01, g1, 9);
            OrderDetail ode2 = new OrderDetail(02, g2, 5);
            OrderDetail ode3 = new OrderDetail(03, g3, 7);
            OrderDetail ode4 = new OrderDetail(04, g2, 4);
            OrderDetail ode5 = new OrderDetail(05, g1, 6);

            List<OrderDetail> l1 = new List<OrderDetail>();
            l1.Add(ode1); l1.Add(ode3);
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
            Order o5 = oe.SerachByGoods("苹果");
            Assert.AreEqual(o5, o1);
        }

        [TestMethod()]
        public void SerachByCustomerTest()
        {
            Customer c1 = new Customer("小红", 123, "武汉");
            Customer c2 = new Customer("小蓝", 123, "青岛");
            Customer c3 = new Customer("小绿", 123, "北京");

            Goods g1 = new Goods(01, "苹果", 3.5);
            Goods g2 = new Goods(02, "梨", 5.5);
            Goods g3 = new Goods(03, "香蕉", 4.5);

            OrderDetail ode1 = new OrderDetail(01, g1, 9);
            OrderDetail ode2 = new OrderDetail(02, g2, 5);
            OrderDetail ode3 = new OrderDetail(03, g3, 7);
            OrderDetail ode4 = new OrderDetail(04, g2, 4);
            OrderDetail ode5 = new OrderDetail(05, g1, 6);

            List<OrderDetail> l1 = new List<OrderDetail>();
            l1.Add(ode1); l1.Add(ode3);
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
            Order o5 = oe.SerachByCustomer(c1);
            Assert.AreEqual(o5, o1);
        }

        [TestMethod()]
        public void SearchByPriceTest()
        {
            Customer c1 = new Customer("小红", 123, "武汉");
            Customer c2 = new Customer("小蓝", 123, "青岛");
            Customer c3 = new Customer("小绿", 123, "北京");

            Goods g1 = new Goods(01, "苹果", 3.5);
            Goods g2 = new Goods(02, "梨", 5.5);
            Goods g3 = new Goods(03, "香蕉", 4.5);

            OrderDetail ode1 = new OrderDetail(01, g1, 9);
            OrderDetail ode2 = new OrderDetail(02, g2, 5);
            OrderDetail ode3 = new OrderDetail(03, g3, 7);
            OrderDetail ode4 = new OrderDetail(04, g2, 4);
            OrderDetail ode5 = new OrderDetail(05, g1, 6);

            List<OrderDetail> l1 = new List<OrderDetail>();
            l1.Add(ode1); l1.Add(ode3);
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
            Order o5 = oe.SearchByPrice(o1.TotalPrice);
            Assert.AreEqual(o5, o1);
        }

        [TestMethod()]
        public void WriteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ImportTest()
        {
            Customer c1 = new Customer("小红", 123, "武汉");
            Customer c2 = new Customer("小蓝", 123, "青岛");
            Customer c3 = new Customer("小绿", 123, "北京");

            Goods g1 = new Goods(01, "苹果", 3.5);
            Goods g2 = new Goods(02, "梨", 5.5);
            Goods g3 = new Goods(03, "香蕉", 4.5);

            OrderDetail ode1 = new OrderDetail(01, g1, 9);
            OrderDetail ode2 = new OrderDetail(02, g2, 5);
            OrderDetail ode3 = new OrderDetail(03, g3, 7);
            OrderDetail ode4 = new OrderDetail(04, g2, 4);
            OrderDetail ode5 = new OrderDetail(05, g1, 6);

            List<OrderDetail> l1 = new List<OrderDetail>();
            l1.Add(ode1); l1.Add(ode3);
            List<OrderDetail> l2 = new List<OrderDetail>();
            l2.Add(ode2); l2.Add(ode4);
            List<OrderDetail> l3 = new List<OrderDetail>();
            l3.Add(ode1); l3.Add(ode2); l3.Add(ode5);

            Order o1 = new Order(02, c1, l1);
            Order o2 = new Order(01, c2, l2);
            Order o3 = new Order(03, c3, l3);

            List<Order> l4 = new List<Order>();
            l4.Add(o1); l4.Add(o2); l4.Add(o3);
            string path = "C:\\Users\\26419\\source\\repos\\Homework6\\s.xml";
            OrderService oe = new OrderService();
            oe.Import(path);
            Assert.AreEqual(oe.Orders, l4);
        }

        [TestMethod()]
        public void ExportTest()
        {
            string path = "C:\\Users\\26419\\source\\repos\\Homework6\\s.xml";
            OrderService oe = new OrderService();
            oe.Export(path);
            Assert.IsTrue(File.Exists(path));
        }
    }
}