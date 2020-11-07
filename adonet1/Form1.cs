using System;
using System.Windows.Forms;

namespace adonet1
{
    public partial class Form1 : Form
    {
        private readonly ProductDal _productdal = new ProductDal();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _Refresh();
        }

        private void _Refresh()
        {
            var results = _productdal.GetAll();
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

        private void refresh_Click(object sender, EventArgs e)
        {
            _Refresh();
        }

        // Add a product
        /// <summary>
        /// Handles the Load event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>

        private void BtnAdd_Click(object sender, EventArgs e)
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

        // Update a product
        /// <summary>
        /// Handles the CellClick event of the dgwProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
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

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            (var Id, _, _, _) = GetDataGridValuesInTheRow();

            Product product = new Product
            {
                Id = Id,
                Name = tbxNameUpdate.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text),
                StockAmount = Convert.ToInt32(tbxStockAmountUpdate.Text),
            };
            _productdal.Update(product);

            _Refresh();
            MessageBox.Show("Updated");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            (var Id, _, _, _) = GetDataGridValuesInTheRow();
            _productdal.Delete(Id);

            _Refresh();
            MessageBox.Show("Deleted");
        }

        private void gbxUpdate_Enter(object sender, EventArgs e)
        {

        }

        private void dgwProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
