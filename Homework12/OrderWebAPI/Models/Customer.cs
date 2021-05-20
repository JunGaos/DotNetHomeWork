using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderWebAPI.Models
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
}
