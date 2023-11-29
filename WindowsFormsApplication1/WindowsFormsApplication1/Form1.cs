using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKhoaApp
{
    public partial class FormQuanLyKhoa : Form
    {
        private string connectionString = "Data Source=A209PC03;Initial Catalog=QL_Khoa;Integrated Security=True;";

        public FormQuanLyKhoa()
        {
            InitializeComponent();
            LoadDataIntoDataGridView(); // Tải dữ liệu vào DataGridView khi form khởi động
        }

        private void LoadDataIntoDataGridView()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Khoa";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                txtMaKhoa.Text = selectedRow.Cells["MaSanPham"].Value.ToString();
                txtTenKhoa.Text = selectedRow.Cells["TenSanPham"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string masanpham = txtMaKhoa.Text;
            string tensanpham = txtTenKhoa.Text;

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(masanpham) || string.IsNullOrWhiteSpace(tensanpham))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES (@MaKhoa, @TenKhoa)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@MaKhoa", masanpham);
                        cmd.Parameters.AddWithValue("@TenKhoa", tensanpham);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm dữ liệu thành công.");
                            txtMaKhoa.Clear();
                            txtTenKhoa.Clear();
                            LoadDataIntoDataGridView(); // Cập nhật DataGridView sau khi thêm dữ liệu
                        }
                        else
                        {
                            MessageBox.Show("Không có dòng nào được thêm vào cơ sở dữ liệu.");
}
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                }
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maKhoa = txtMaKhoa.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Khoa WHERE MaKhoa = @MaKhoa";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    cmd.ExecuteNonQuery();
                }
            }

            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            LoadDataIntoDataGridView(); // Cập nhật DataGridView sau khi xóa dữ liệu
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maKhoa = txtMaKhoa.Text; 
            string tenKhoa = txtTenKhoa.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Khoa SET TenKhoa = @TenKhoa WHERE MaKhoa = @MaKhoa";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaKhoa", maKhoa);
                    cmd.Parameters.AddWithValue("@TenKhoa", tenKhoa);
                    cmd.ExecuteNonQuery();
                }
            }

            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            LoadDataIntoDataGridView(); // Cập nhật DataGridView sau khi sửa dữ liệu

        }

        private void FormQuanLyKhoa_Load(object sender, EventArgs e)
        {

        }
    }
}

    

        
        
        