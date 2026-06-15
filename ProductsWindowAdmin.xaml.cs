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
using System.Data.Entity;

namespace DemoTest
{
    /// <summary>
    /// Логика взаимодействия для ProductsWindowAdmin.xaml
    /// </summary>
    public partial class ProductsWindowAdmin : Window
    {
        public ProductsWindowAdmin()
        {
            InitializeComponent();

            DiscountComboBox.Items.Add("Скидка: все диапазоны");
            DiscountComboBox.Items.Add("Скидка: 0-12.99%");
            DiscountComboBox.Items.Add("Скидка: 13-16.99%");
            DiscountComboBox.Items.Add("Скидка: 17 и более %");
            DiscountComboBox.SelectedIndex = 0;

            SortComboBox.Items.Add("Без сортировки");
            SortComboBox.Items.Add("Цена по возрастанию");
            SortComboBox.Items.Add("Цена по убыванию");
            SortComboBox.SelectedIndex = 0;

            LoadProducts();
        }

        private void LoadProducts()
        {
            if (ProductsDataGrid == null || SearchTextBox == null || DiscountComboBox == null || SortComboBox == null)
                return;

            try
            {
                using (DemoEntities db = new DemoEntities())
                {
                    var products = db.Product_.Include(x => x.ProductName_).Include(x => x.ProductCategories_).ToList();


                    string search = SearchTextBox.Text.ToLower();

                    if (search != "" && search != "поиск")
                    {
                        products = products.Where(x =>
                            (x.ProductName_ != null && x.ProductName_.Name.ToLower().Contains(search)) ||
                            (x.Description != null && x.Description.ToLower().Contains(search))
                        ).ToList();
                    }


                    if (DiscountComboBox.SelectedIndex == 1)
                        products = products.Where(x => (x.Discount ?? 0) >= 0 && (x.Discount ?? 0) <= 12.99).ToList();

                    if (DiscountComboBox.SelectedIndex == 2)
                        products = products.Where(x => (x.Discount ?? 0) >= 13 && (x.Discount ?? 0) <= 16.99).ToList();

                    if (DiscountComboBox.SelectedIndex == 3)
                        products = products.Where(x => (x.Discount ?? 0) >= 17).ToList();

                    if (SortComboBox.SelectedIndex == 1)
                        products = products.OrderBy(x => x.Price).ToList();

                    if (SortComboBox.SelectedIndex == 2)
                        products = products.OrderByDescending(x => x.Price).ToList();


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

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadProducts();
        }

        private void DiscountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadProducts();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadProducts();
        }

        private void OpenOrdersWindowAdmin()
        {
            OrdersWindowAdmin window = new OrdersWindowAdmin();
            Hide();
            window.ShowDialog();
            Show();
        }


        private void OpenAddWindow()
        {
            AddWindow window = new AddWindow();
            Hide();
            window.ShowDialog();
            Show();
        }

        private void OpenEditWindow()
        {
            EditWindow window = new EditWindow();
            Hide();
            window.ShowDialog();
            Show();
        }


        private void OrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenOrdersWindowAdmin();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenAddWindow();
            // окно добавления
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

            OpenEditWindow();
            // редактирование
        }
    }
}
