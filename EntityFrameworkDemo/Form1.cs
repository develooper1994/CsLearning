using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkDemo
{
    public partial class Form1 : Form
    {
        ProductDal _productdal = new ProductDal();

        private void _Refresh()
        {
            var results = _productdal.GetAll();
            dgwProducts.DataSource = results;
        }
        private void SearchProductsBAD(string key)
        {
            ///<summary>
            /// Not good for speed and memory.
            /// Get all data from database and load into memory
            /// and then FILTER all these GARBAGE.
            ///
            /// VERY INEFFICIENT
            /// </summary>
            var results = _productdal.GetAll();
            var query =
                from result in results
                let lowName = result.Name.ToLower()
                where lowName.Contains(key.ToLower())  // key.ToLower()
                select result;
            var queryImmediate = query.ToList();

            dgwProducts.DataSource = queryImmediate;
        }
        private void SearchProductsGOOD(string key)
        {
            var results = _productdal.GetByKey(key);
            dgwProducts.DataSource = results;
        }
        private (int Id, string Name, string UnitPrice, string StockAmount) GetDataGridValuesInTheRow()
        {
            //DataGridViewCellCollection cells = dgwProducts.CurrentRow.Cells;

            const int IdIdx = 0, NameIdx = 1, UnitPriceIdx = 2, StockAmountIdx = 3;

            var Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[IdIdx].Value);
            var Name = dgwProducts.CurrentRow.Cells[NameIdx].Value.ToString();
            var UnitPrice = dgwProducts.CurrentRow.Cells[UnitPriceIdx].Value.ToString();
            var StockAmount = dgwProducts.CurrentRow.Cells[StockAmountIdx].Value.ToString();

            return (Id: Id, Name: Name, UnitPrice: UnitPrice, StockAmount: StockAmount);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _Refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var prod = new Product
            {
                Name = tbxName.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                StockAmount = Convert.ToInt32(tbxStockAmount.Text),
            };
            _productdal.Add(prod);

            _Refresh();
            MessageBox.Show("Message Added!");
        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
             There is Ttoo many events. I looked at which one is good for me.
            To debug events
            MessageBox.Show("dgwProducts_CellClick");
            */

            (_, string Name, string UnitPrice, string StockAmount) = GetDataGridValuesInTheRow();

            tbxNameUpdate.Text = Name;
            tbxUnitPriceUpdate.Text = UnitPrice;
            tbxStockAmountUpdate.Text = StockAmount;
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            _Refresh();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            (var Id, _, _, _) = GetDataGridValuesInTheRow();

            var prod = new Product
            {
                Id = Id,
                Name = tbxNameUpdate.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text),
                StockAmount = Convert.ToInt32(tbxStockAmountUpdate.Text),
            };
            _productdal.Update(prod);

            _Refresh();
            MessageBox.Show("Message Added!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            (var Id, _, _, _) = GetDataGridValuesInTheRow();

            Product prod = new Product
            {
                Id = Id,
                Name = tbxNameUpdate.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text),
                StockAmount = Convert.ToInt32(tbxStockAmountUpdate.Text),
            };
            _productdal.Delete(prod);

            _Refresh();
            MessageBox.Show("Message Added!");
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            //SearchProductsBAD(tbxSearch.Text);
            SearchProductsGOOD(tbxSearch.Text);
            //MessageBox.Show(tbxSearch.Text);
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            var result = _productdal.GetById(3);
            dgwProducts.DataSource = result;
        }
    }
}
