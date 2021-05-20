using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Models;

namespace OrderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext orderDB;
        public OrderController(OrderContext context)
        {
            orderDB = context;
        }
        // GET api/order
        [HttpGet]
        public ActionResult<List<Order>> Get()
        {
            IQueryable<Order> query= orderDB.Orders;
            return query.ToList();

        }

        // GET api/order/5
        [HttpGet("{id}")]
        public ActionResult<List<Order>> Get(string id)
        {
            IQueryable<Order> query = orderDB.Orders.Include(o => o.OrderDetails.Select(d => d.Goods)).Include("Customer").
                    Where(order => order.OrderId == id);
            List<Order> orderList = query.ToList();
            orderList.Sort();
            if (orderList != null)
            {
                return NotFound();
            }
            return orderList;
        }

        // POST api/order
        [HttpPost]
        public ActionResult<Order> Post(Order order)
        {
            try
            {
                order.Customer = null;
                order.OrderDetails.ForEach(i => i.Goods = null);
                orderDB.Orders.Add(order);
                orderDB.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return order;
        }

        // PUT api/order/5
        [HttpPut("{id}")]
        public ActionResult<Order> Put(string id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest("Id can not be modified!");
            }
            try
            {
                orderDB.Entry(order).State = EntityState.Modified;
                orderDB.SaveChanges();
            }
            catch (Exception e)
            {
                string error = e.Message;
                return BadRequest(error);
            }
            return NoContent();
        }

        // DELETE api/order/5
        [HttpDelete("{id}")]
        public ActionResult<Order> Delete(string id)
        {
            try
            {
                Order od = orderDB.Orders.FirstOrDefault(o => o.OrderId == id);
                if (od != null)
                {
                    orderDB.Orders.Remove(od);
                    orderDB.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }
    }
}
