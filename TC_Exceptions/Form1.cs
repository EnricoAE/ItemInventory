using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TC_Exceptions
{
    public partial class frmAddProduct : Form
    {
        class NumberFormatExcception : Exception
        {
            public NumberFormatExcception(string quantity) : base(quantity) { }
        }
        class StringFormatException : Exception
        {
            public StringFormatException(string name) : base(name) { }
        }
        class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string price) : base(price) { }
        }

        private int _Quantity;
        private double _SellPrice;
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        BindingSource showProductList;
        public frmAddProduct()
        {
            InitializeComponent();
            showProductList = new BindingSource();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            _ProductName = Product_Name(txtProductName.Text);
            _Category = cbCategory.Text;
            _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            _Quantity = Quantity(txtQuantity.Text);
            _SellPrice = SellingPrice(txtSellPrice.Text);
            showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
            _ExpDate, _SellPrice, _Quantity, _Description));
            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewProductList.DataSource = showProductList;
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]
            {
                "Beverages", "Bread / Bakery",
                "Carned / Jarred Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Other",

            };
            foreach (string variableName in ListOfProductCategory)
            {
                cbCategory.Items.Add(variableName);
            }
        }




        public string Product_Name(string name)
        {
            try
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    throw new StringFormatException(name);
                }
            }
            catch (StringFormatException sfe)
            {
                MessageBox.Show("String Format input in product name." + sfe.Message);

            }
            finally
            {
                Console.WriteLine("Input string only in product name");
            }
            return name;
        }
        public int Quantity(string qty)
        {
            try
            {
                if (!Regex.IsMatch(qty, @"^[0-9]"))
                {
                    throw new NumberFormatExcception(qty);
                }

            }
            catch (NumberFormatExcception nfe)
            {
                MessageBox.Show("Numnber format input in quantity. " + nfe.Message);
            }
            return Convert.ToInt32(qty);
        }




        public double SellingPrice(string price)
        {
            try
            {
                if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                {
                    throw new CurrencyFormatException(price);
                }

            }
            catch (CurrencyFormatException cfe)
            {
                MessageBox.Show(" Currency format input in price " + cfe.Message);
            }
            return Convert.ToDouble(price);
        }



    }
    class ProductClass
    {
        private int _Quantity;
        private double _SellingPrice;
        private string _ProductName, _Category, _ManufacturingDate, _ExpirationDate, _Description;
        public ProductClass(string ProductName, string Category, string MfgDate, string ExpDate,
        double Price, int Quantity, string Description)
        {
            this._Quantity = Quantity;
            this._SellingPrice = Price;
            this._ProductName = ProductName;
            this._Category = Category;
            this._ManufacturingDate = MfgDate;
            this._ExpirationDate = ExpDate;
            this._Description = Description;
        }
        public string productName
        {
            get
            {
                return this._ProductName;
            }
            set
            {
                this._ProductName = value;
            }
        }
        public string category
        {
            get
            {
                return this._Category;
            }
            set
            {
                this._Category = value;
            }
        }
        public string manufacturingDate
        {
            get
            {
                return this._ManufacturingDate;
            }
            set
            {
                this._ManufacturingDate = value;
            }
        }
        public string expirationDate
        {
            get
            {
                return this._ExpirationDate;
            }
            set
            {
                this._ExpirationDate = value;
            }
        }
        public string description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }
        public int quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this._Quantity = value;
            }
        }
        public double sellingPrice
        {
            get
            {
                return this._SellingPrice;
            }
            set
            {
                this._SellingPrice = value;
            }
        }
    }
}
