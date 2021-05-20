using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderWebAPI.Models
{
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
}
