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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void OpenProductsWindowGuest()
        {
            ProductsWindowGuest window = new ProductsWindowGuest();
            Hide();
            window.ShowDialog();
            Show();
        }

        private void OpenProductsWindowClient()
        {
            ProductsWindowClient window = new ProductsWindowClient();
            Hide();
            window.ShowDialog();
            Show();
        }

        private void OpenProductsWindowManager()
        {
            ProductsWindowManager window = new ProductsWindowManager();
            Hide();
            window.ShowDialog();
            Show();
        }

        private void OpenProductsWindowAdmin()
        {
            ProductsWindowAdmin window = new ProductsWindowAdmin();
            Hide();
            window.ShowDialog();
            Show();
        }


        private void LoginBtnGuest_Click(object sender, RoutedEventArgs e)
        {
            OpenProductsWindowGuest();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string log = LoginTextBox.Text.Trim();
            string pas = PasswordTextBox.Text.Trim();
           
            
            if (log == "" || pas == "") // проверка ввода
            {
                MessageBox.Show("введите логин и пароль");
                return;
            }
            try
            {
                using (DemoEntities db = new DemoEntities())
                {

                    User_ user = db.User_.FirstOrDefault(x => x.Login == log && x.Password == pas);

                    if (user == null)
                    {
                        MessageBox.Show("Error");
                        return;
                    }


                    string rol = user.UserRole; // проверка роли
                    if (rol == "Администратор")
                    {
                        OpenProductsWindowAdmin();
                    } else if (rol == "Менеджер")
                    {
                        OpenProductsWindowManager();
                    }else if (rol == "Авторизованный клиент")
                    {
                        OpenProductsWindowClient();
                    } else
                    {
                        MessageBox.Show("Error role");
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("ERROR ON CONECTED FOR DB");
            }

            /*проверка по логину и паролю + роль и открытие окон по роли*/
        }

    }
}
