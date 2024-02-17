using DataAccsess.DAO;
using DataAccsess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Respository
{
    public class CartRespository : CartIRespository
    {
        public void addOrderDetail(OrderDetail detail)
        {
            OrdersDAO.Instance.AddOrderDetail(detail);
        }

        public void addorders(Order order)
        {
           OrdersDAO.Instance.AddOrders(order);
        }

        public bool Delete(int id)
        {
           return CartDAO.Instance.Delete(id);
        }

        public bool deleteCart(int userid, int productId)
        {
           return CartDAO.Instance.deleteOrderDetail(userid, productId);
        }

        public void deleteProductCart(int id)
        {
           CartDAO.Instance.OnGetDelete(id);
        }

        public List<Order> getALLOrder()
        {
          return CartDAO.Instance.GetOrders();
        }

        public IEnumerable<Order> GetAllOrdersByMember(int memberId)
        {
            return CartDAO.Instance.findMember(memberId);
        }

        public List<Item> getItemsInCart()
        {
            return CartDAO.Instance.GetItemsInCart();
        }

        public List<Item> getProductCart(int id)
        {
            return CartDAO.Instance.OnGetBuyNow(id);
        }

    
    }
}
