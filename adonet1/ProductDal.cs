using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace adonet1
{
    /// <summary>
    /// <para>Too simple CRUD application</para>
    /// <para>CRUD: CREATE, READ, UPDATE, DELETE</para>
    /// </summary>
    class ProductDal
    {
        // connect to the database
        // DESKTOP-ET2MV41
        private const string _connectionString =
            @"server=DESKTOP-Q3V23GG;
              initial catalog=ETrade;
              integrated security=true";
        // remote connection
        //"integrate security=false;
        //uid=<UserId>;
        //password=<password>"
        private readonly SqlConnection _connection = new SqlConnection(_connectionString);
        private readonly SqlCommand _command = new SqlCommand();

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        private void SetCommand(in string CommandText)
        {
            _command.CommandText = CommandText;
            _command.Connection = _connection;
            //var numberOfEfectedRows = _command.ExecuteNonQuery();
        }
        private void AddWithValue(in Product product)
        {
            _command.Parameters.AddWithValue("@name", product.Name);
            _command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            _command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
        }

        public List<Product> GetAll()
        {
            ///<summary>
            /// List all the values in database
            /// </summary>

            ConnectionControl();

            // get the values with "SQL"
            const string query1 =
                "SELECT* FROM Products";
            //SqlCommand command = new SqlCommand(query1, connection: _connection);
            SetCommand(query1);
            SqlDataReader reader = _command.ExecuteReader();

            // create return value
            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                var id = reader["Id"];
                var name = reader["Name"];
                var unitPrice = reader["UnitPrice"];
                var stockPrice = reader["StockAmount"];

                Product product = new Product
                {
                    Id = Convert.ToInt32(id),
                    Name = name.ToString(),
                    UnitPrice = Convert.ToDecimal(unitPrice),
                    StockAmount = Convert.ToInt32(stockPrice),
                };

                products.Add(product);
            }

            // Close all of them when work is done. (!!! but not here !!!!)
            reader.Close();
            //_connection.Close();

            return products;
        }

        public DataTable GetAll2()
        {
            /// <summary>
            /// List all the values in database
            /// </summary>

            ConnectionControl();

            // get the values with "SQL"
            const string query1 =
                "SELECT* FROM Products";
            //SqlCommand command = new SqlCommand(query1, connection: _connection);
            SetCommand(query1);
            SqlDataReader reader = _command.ExecuteReader();

            // create return value
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            // Close all
            reader.Close();
            //_connection.Close();

            return dataTable;
        }

        public int Add(in Product product)
        {
            ///<summary>
            /// Add a new value to Database with "SQL"
            /// </summary>
            ConnectionControl();
            const string query1 =
                "INSERT INTO Products VALUES(@name, @unitPrice, @stockAmount)";

            //SqlCommand command = new SqlCommand(query1, _connection);
            AddWithValue(product);

            SetCommand(query1);
            var numberOfEfectedRows = _command.ExecuteNonQuery();
            _command.Parameters.Clear();

            return numberOfEfectedRows;
        }

        public int Update(in Product product)
        {
            ///<summary>
            /// Update a new values in Database with "SQL"
            /// </summary>
            ConnectionControl();
            const string query1 =
                "UPDATE Products SET Name=@name, UnitPrice=@unitPrice, StockAmount=@stockAmount WHERE Id=@id";

            //SqlCommand command = new SqlCommand(query1, _connection);
            AddWithValue(product);
            _command.Parameters.AddWithValue("@id", product.Id);

            SetCommand(query1);
            int numberOfEfectedRows = _command.ExecuteNonQuery();
            _command.Parameters.Clear();

            return numberOfEfectedRows;
        }

        public int Delete(int id)
        {
            ///<summary>
            /// Update a new values in Database with "SQL"
            /// </summary>
            ConnectionControl();
            const string query1 =
                "DELETE FROM Products WHERE Id=@id";

            //SqlCommand command = new SqlCommand(query1, _connection);
            _command.Parameters.AddWithValue("@id", id);

            SetCommand(query1);
            int numberOfEfectedRows = _command.ExecuteNonQuery();
            _command.Parameters.Clear();

            return numberOfEfectedRows;
        }

    }
}
