using System.Collections.Generic;
using System.Linq;

using EntityState = System.Data.Entity.EntityState;

namespace EntityFrameworkDemo
{
    /// <summary>
    /// <para>Too simple CRUD application</para>
    /// <para>CRUD: CREATE, READ, UPDATE, DELETE</para>
    /// </summary>
    public class ProductDal
    {
        public List<Product> GetAll()
        {
            ///<summary>
            /// List all the values in database
            /// </summary>

            using (EtradeContext context = new EtradeContext())
            {
                return context.Products.ToList();
            }
        }

        public List<Product> GetByKey(string key)
        {
            ///<summary>
            /// Most EFFICIENT-WAY
            /// MOST preferred WAY
            /// Direct query to SQL server
            /// </summary>

            using (EtradeContext context = new EtradeContext())
            {
                var query =
                    from prod in context.Products
                    where prod.Name.Contains(key)
                    select prod;
                var result = query.ToList();

                return result;
            }
        }

        public List<Product> GetByUnitPrice(decimal maxPrice, decimal minPrice = 0M)
        {
            ///<summary>
            /// Most EFFICIENT-WAY
            /// MOST preferred WAY
            /// Direct query to SQL server
            /// </summary>

            using (EtradeContext context = new EtradeContext())
            {
                var query =
                    from prod in context.Products
                    where prod.UnitPrice >= minPrice && prod.UnitPrice <= maxPrice
                    select prod;
                var result = query.ToList();

                return result;
            }
        }

        public Product GetById(int id)
        {
            ///<summary>
            /// List all the values in database
            /// </summary>

            using (EtradeContext context = new EtradeContext())
            {
                //var query =
                //    from prod in context.Products
                //    where prod.Id == id
                //    select prod;
                //var result = query.FirstOrDefault();

                //var result = context.Products
                //    .Where(p => p.Id == id)
                //    .FirstOrDefault();   // results null if data is not exist

                // single and happy :D
                var result = context.Products
                    .Where(p => p.Id == id)
                    .SingleOrDefault();  // throws exception if there is more than one data

                return result;
            }
        }

        public void Add(in Product product)
        {
            ///<summary>
            /// Add a new value to Database with "EntityFramework"
            /// </summary>
            using EtradeContext context = new EtradeContext();
            //context.Products.Add(product);
            var entity = context.Entry(product);
            entity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Update(in Product product)
        {
            ///<summary>
            /// Update a new values in Database with "EntityFramework"
            /// </summary>
            using EtradeContext context = new EtradeContext();
            var entity = context.Entry(product);
            entity.State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(in Product product)
        {
            ///<summary>
            /// Update a new values in Database with "SQL"
            /// </summary>
            using EtradeContext context = new EtradeContext();
            var entity = context.Entry(product);
            entity.State = EntityState.Deleted;
            context.SaveChanges();
        }

    }
}
