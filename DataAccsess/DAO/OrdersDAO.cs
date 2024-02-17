using DataAccsess.Helper;
using DataAccsess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.DAO
{
    public class OrdersDAO
    {
        private readonly QLMTCContext _context = new();

        public List<Item> cart;
        public List<Order> orders = new List<Order>();
        public List<OrderDetail> orderDetail;
        [BindProperty]
        public Order order { get; set; }

        private static OrdersDAO instance = null;
        private static readonly object instanceLock = new object();
        
      
        public static OrdersDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new OrdersDAO();
                }
                return instance;
            }
        }
        public void AddOrders(Order order)
        {

            try
            {
                using (QLMTCContext context = new QLMTCContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (QLMTCContext context = new QLMTCContext())
                {
                    context.OrderDetails.Add(orderDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
