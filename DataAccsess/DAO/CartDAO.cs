using DataAccsess.Helper;
using DataAccsess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.DAO
{
    public class CartDAO
    {
        private static CartDAO instance = null;
        private static readonly object instanceLock = new object();
        private readonly IHttpContextAccessor _httpContextAccessor;
        public List<Item> cart { get; set; }
       
        public List<Order> orders = new List<Order>();
        private readonly QLMTCContext context = new QLMTCContext();
        private CartDAO(IHttpContextAccessor contextAccessor) {
            _httpContextAccessor = contextAccessor;
        }
        private CartDAO()
        {

        }
        public static CartDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new CartDAO();
                }
                return instance;
            }
        }
        public List<Order> GetOrders()
        {
            var productModel = new QLMTCContext();
            orders = context.Orders.ToList();
            return orders;
            
        }

        public List<Item> GetItemsInCart()
        {
            return cart;
        }

        public List<Item> OnGetBuyNow(int id)
        {
            var productModel = new QLMTCContext();
            //cart = SessionHelper.GetObjectFormJson<List<Item>>(_httpContextAccessor.HttpContext.Session, "cart");
            if (cart == null)
            {
                cart = new List<Item>();
                Item item = new Item
                {
                    Product = productModel.Products.SingleOrDefault(x => x.ProductId == id),
                    Quantity = 1
                };
                cart.Add(item);
                //SessionHelper.SetObjectAsJson(_httpContextAccessor.HttpContext.Session, "cart", cart);
            }
            else
            {
                int index = Exits(cart, id);
                if (index == -1)
                {
                    cart.Add(new Item
                    {
                        Product = productModel.Products.SingleOrDefault(x => x.ProductId == id),
                        Quantity = 1
                    });
                }
                else
                {
                    cart[index].Quantity++;
                }
               //SessionHelper.SetObjectAsJson(_httpContextAccessor.HttpContext.Session, "cart", cart);
            }
           return cart;
        }
        private int Exits(List<Item> cart, int id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId == id)
                {
                    return i;
                }
            }
            return -1;
        }
        public void OnGetDelete(int id)
        {
            // cart = SessionHelper.GetObjectFormJson<List<Item>>(_httpContextAccessor.HttpContext.Session, "cart");
            int index = Exits(cart, id);
            cart.RemoveAt(index);
            //SessionHelper.SetObjectAsJson(_httpContextAccessor.HttpContext.Session, "cart", cart);
        }
        public bool deleteOrderDetail(int orderId, int productId)
        {
            try
            {
                IEnumerable<OrderDetail> odd = context.OrderDetails.Where(x => x.OrderId == orderId && x.ProductId == productId).ToList();
                if (odd != null)
                {
                    context.OrderDetails.RemoveRange(odd);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order does not have detail!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
        public IEnumerable<Order> findMember(int userId)
        {
            var listOrder = new List<Order>();
            try
            {
                listOrder = context.Orders.Include(o => o.OrderDetails).Where(x => x.CustomerId == userId).ToList();
                if (listOrder is null)
                {
                    throw new Exception("User does not have order!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrder;
        }
        public bool Delete(int id)
        {
            try
            {
                //Product pr = new Product();
                List<Item> product = new List<Item>();
                int index = product.FindIndex(item => item.Product.ProductId == id);
                if(index >= 0)
                {
                    product.RemoveAt(index);
                    return true;
                }
                /*using (QLMTCContext context = new QLMTCContext())
                {
                    Product author = context.Products.ToList().FirstOrDefault(s => s.ProductId == id);
                    if (author != null)
                    {
                        context.Products.Remove(author);
                        //context.SaveChanges();
                        return true;
                    }

                }*/
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
        public void OnPostCreate(int id)
        {
            cart = SessionHelper.GetObjectFormJson<List<Item>>(_httpContextAccessor.HttpContext.Session, "cart");
            string user = _httpContextAccessor.HttpContext.Session.GetString("UserName") ?? "";
            Account acc = context.Accounts.FirstOrDefault(x => x.Usename == user);
             context.Orders.Add(new Order()
            {
                CustomerId = acc.CustomerId,
                OrderDate = DateTime.Now,

            });
            context.SaveChanges();
        }
    }
}
