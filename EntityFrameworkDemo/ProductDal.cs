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
