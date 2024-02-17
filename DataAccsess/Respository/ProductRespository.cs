using DataAccsess.DAO;
using DataAccsess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Respository
{
    public class ProductRespository : ProductIRespository
    {
        public List<Category> getAllCate()
        {
           return ProductsDAO.Instance.getallCategory();
        }

        public List<Product> GetAllProduct()
        {
            return ProductsDAO.Instance.ListProduct();
        }
        public List<Product> getBuyIdName(string pname)
        {
            return ProductsDAO.Instance.GetProductByIdName(pname);
        }

        public List<Product> getProductByCate(int? cateId)
        {
           return ProductsDAO.Instance.GetproductByCate(cateId);
        }
    }
}
