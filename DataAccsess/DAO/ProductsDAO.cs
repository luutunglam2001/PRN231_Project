using DataAccsess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.DAO
{
    public class ProductsDAO
    {
        private static ProductsDAO instance = null;
        private static readonly object instanceLock = new object();
        public List<Category> listC { get; set;} = new List<Category>();


        private ProductsDAO() { }
        public static ProductsDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new ProductsDAO();
                }
                return instance;
            }
        }
        public List<Product> ListProduct()
        {
            List<Product> list = new List<Product>();
            using(var context = new QLMTCContext())
            {
                list = context.Products.ToList();
            }
            return list;
        }
        public List<Product> GetProductByIdName(string searchNameProduct)
        {
            List<Product> pro;
            try
            {
                using (QLMTCContext context = new QLMTCContext())
                {
                    pro = context.Products.Where(a =>
                (string.IsNullOrEmpty(searchNameProduct) || a.ProductName.Contains(searchNameProduct))).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pro;
        }
        public List<Product> GetproductByCate(int? cateId)
        {
            List<Product> pro;
            try
            {
                using (var context = new QLMTCContext())
                {
                    pro = context.Products
                        .Include(p => p.Category)
                        .Where(p => p.CategoryId == cateId)
                       .ToList();
                }
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
            return pro;
        }

        public List<Category> getallCategory()
        {
           // List<Category> listC = new List<Category>();
            using (var context = new QLMTCContext())
            {
                listC = context.Categories.ToList();
            }
            return listC;
        }
    }
}
