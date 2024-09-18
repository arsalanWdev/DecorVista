using DecorVista.DataAccess.Data;
using DecorVista.DataAccess.Repository.IRepository;
using DecorVista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorVista.DataAccess.Repository
{
 
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Product obj)
        {
            var objfromdb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objfromdb != null)
            {
                objfromdb.Title = obj.Title;
                objfromdb.SKU = obj.SKU;

                objfromdb.Price = obj.Price;
                objfromdb.Price50 = obj.Price50;
                objfromdb.ListPrice = obj.ListPrice;
                objfromdb.Price100 = obj.Price100;
                objfromdb.Description = obj.Description;
                objfromdb.CategoryId = obj.CategoryId;
                objfromdb.Author = obj.Author;
                if (obj.ImageUrl != null)
                {
                    objfromdb.ImageUrl = obj.ImageUrl;
                }


            }

        }
    }
}
