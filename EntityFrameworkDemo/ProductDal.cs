using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Microsoft.EntityFrameworkCore;
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
