using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderWebAPI.Models
{
    public class Order : IComparable<Order>
    {
        [Key]
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string CustomerName { get => (Customer != null) ? Customer.Name : ""; }
        public List<OrderDetail> OrderDetails { set; get; }
        public double TotalPrice { get; }

        public Order()
        {
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
}
