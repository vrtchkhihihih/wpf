using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DemoTest
{
    /// <summary>
    /// Логика взаимодействия для ProductsWindowClient.xaml
    /// </summary>
    public partial class ProductsWindowClient : Window
    {
        public ProductsWindowClient()
        {
            InitializeComponent();
            LoadProducts();
        }
        private void LoadProducts()
        {
            if (ProductsDataGrid == null)
                return;

            try
            {
                using (DemoEntities db = new DemoEntities())
                {
                    var products = db.Product_.ToList();



                    ProductsDataGrid.ItemsSource = products.Select(x => new
                    {
                        IdProduct = x.IdProduct,
                        Photo = "Фото",
                        ProductArt = x.ProductArt,
                        Name = x.ProductName_.Name,
                        Category = x.ProductCategories_.Category,
                        Description = x.Description,
                        Manufacturer = x.Manufacturer_.ManufactureName,
                        Supplier = x.Supplier_.SupplierName,
                        Price = x.Price ?? 0,
                        Count = x.Count,
                        Quantity = x.Quantity ?? 0,
                        Discount = (x.Discount ?? 0) + "%"
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
