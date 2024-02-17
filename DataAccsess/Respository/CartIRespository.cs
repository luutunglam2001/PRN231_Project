using DataAccsess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Respository
{
    public interface CartIRespository
    {
        public List<Item> getItemsInCart();
        public List<Item> getProductCart(int id);
        public List<Order> getALLOrder();

        public void deleteProductCart(int id);
        public void addorders(Order order);

        public bool Delete(int id);
        public bool deleteCart(int userid, int productId);
        public void addOrderDetail(OrderDetail detail);
        IEnumerable<Order> GetAllOrdersByMember(int memberId);
    }
}
