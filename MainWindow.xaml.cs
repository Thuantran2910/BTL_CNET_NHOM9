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
using System.Data.SqlClient;
using System.Data;

namespace BTL_CNET_NHOM9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-U0SLD00\SQLEXPRESS;Initial Catalog=login;Integrated Security=True");

        public MainWindow()
        {
            InitializeComponent();
        }
        private bool AllowLogin()
        {
            if (Username.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tài khoản", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (password.Password.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
        private void txt_login_Click(object sender, RoutedEventArgs e)
        {
            if (!AllowLogin())
            {
                return;
            }

            try
            {
                string sql = "SELECT * FROM Login WHERE username = '" + Username.Text + "' AND password = '" + password.Password.Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                int count = 0;
                while (rdr.Read())
                {
                    count++;
                    if (rdr.GetString(0).ToString() == "admin" && rdr.GetString(1).ToString() == "admin")
                    {

                        MessageBox.Show("Dang nhap thanh cong trang quan ly");
                        Quanlynghidinh ql = new Quanlynghidinh();
                        this.Close();
                        ql.Show();
                        break;
                    }
                }
                if (count == 0)
                {
                    MessageBox.Show("Tai Khoan Hoac Mat Khau Khong Dung!");
                }
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    

        private void txt_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

