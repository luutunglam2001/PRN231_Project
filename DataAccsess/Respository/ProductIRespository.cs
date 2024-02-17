using DataAccsess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Respository
{
    public interface ProductIRespository
    {
        public List<Product> GetAllProduct();
        public List<Product> getBuyIdName(string pname);
        public List<Product> getProductByCate(int? cateId);

        public List<Category> getAllCate();
    }
}
