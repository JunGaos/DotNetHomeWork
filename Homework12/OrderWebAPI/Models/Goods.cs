using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderWebAPI.Models
{
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
}
