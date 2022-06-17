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
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Data;


namespace BTL_CNET_NHOM9
{

    public partial class Quanlynghidinh : Window
    {
        SqlConnection dbcon = new SqlConnection(@"Data Source=DESKTOP-U0SLD00\SQLEXPRESS;Initial Catalog=NghiDinhDB;Integrated Security=True");

        public Quanlynghidinh()
        {
            InitializeComponent();
            loadGird();
            

        }

        public void loadGird()
        {
            SqlCommand cmd = new SqlCommand("select * from DLNgDinh9", dbcon);
            DataTable dt = new DataTable();
            dbcon.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            dbcon.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }
        public bool isValid()
        {
            if (txt_dieu.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỂN ĐIỀU", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_nd_dieu.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN NỘI DUNG ĐIỀU", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_khoan.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN KHOẢN", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_ndk.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN NỘI DUNG KHOẢN", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_mpt.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN MỨC PHẠT TRÊN", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_mpd.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN MỨC PHẠT DƯỚI", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("BẠN CÓ CHẮC CHẮN MUỐN THÊM KHÔNG? ", "THÊM DỮ LIỆU", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (isValid())
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO DLNgDinh9 VALUES (@DIEU, @NOIDUNGDIEU, @KHOAN, @NOIDUNGKHOAN, @MUCPHATTREN, @MUCPHATDUOI)", dbcon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@DIEU", txt_dieu.Text);
                        cmd.Parameters.AddWithValue("@NOIDUNGDIEU", txt_nd_dieu.Text);
                        cmd.Parameters.AddWithValue("@KHOAN", txt_khoan.Text);
                        cmd.Parameters.AddWithValue("@NOIDUNGKHOAN", txt_ndk.Text);
                        cmd.Parameters.AddWithValue("@MUCPHATTREN", txt_mpt.Text);
                        cmd.Parameters.AddWithValue("@MUCPHATDUOI", txt_mpd.Text);
                        dbcon.Open();
                        cmd.ExecuteNonQuery();
                        dbcon.Close();
                        loadGird();
                        MessageBox.Show("THÊM THÀNH CÔNG", "ĐÃ LƯU", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearData();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                dbcon.Close();

            }
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("BẠN CÓ CHẮC CHẮN MUỐN SỬA KHÔNG? ", "SỬA DỮ LIỆU", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (isValid())
                {
                    dbcon.Open();
                    SqlCommand cmd = new SqlCommand("update DLNgDinh9 set Điều = '" + txt_dieu.Text + "' Nội dung điều = '" + txt_nd_dieu.Text + "' Khoản = '" + txt_khoan.Text + "'Nội dung khoản = '" + txt_ndk.Text + "' Mức phạt trên = '" + txt_mpt + "' Mức phạt dưới = '" + txt_mpd + "'WHERE ID = '" + search_txt.Text + "' ");
                    try
                    {
                        cmd.Connection = dbcon;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("ĐÃ SỬA THÀNH CÔNG", "ĐÃ LƯU", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("KHÔNG SỬA ĐƯỢC \nKIỂM TRA LẠI ID");
                    }
                    finally
                    {
                        dbcon.Close();
                        ClearData();
                        loadGird();
                    }
                }
            }
            else
            {
                dbcon.Close();
            }

        }


        private void btn_Delete_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("BẠN CÓ CHẮC CHẮN MUỐN XÓA KHÔNG? ", "XÓA DỮ LIỆU", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                dbcon.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM DLNgDinh9 WHERE ID = " + search_txt.Text + " ", dbcon);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("ĐÃ XÓA THÀNH CÔNG", "ĐÃ LƯU", MessageBoxButton.OK, MessageBoxImage.Information);
                    dbcon.Close();
                    ClearData();
                    loadGird();
                    dbcon.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("KHÔNG XÓA ĐƯỢC \nKIỂM TRA LẠI ID");
                }
                finally
                {
                    dbcon.Close();
                }
            }
            else
            {
                dbcon.Close();
            }
        }


        public void ClearData()
        {
            txt_dieu.Clear();
            txt_nd_dieu.Clear();
            txt_khoan.Clear();
            txt_ndk.Clear();
            txt_mpt.Clear();
            txt_mpd.Clear();
            search_txt.Clear();

        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            ClearData();

        }



        private void txt_Search_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM DLNgDinh9 WHERE Điều = '" + int.Parse(txt_dieu.Text) + "'");
                DataTable dt = new DataTable();
                dbcon.Open();
                cmd.Connection = dbcon;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                datagrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dbcon.Close();
            }

        }

    }
}
    

        
    








