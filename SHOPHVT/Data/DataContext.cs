using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SHOPHVT.Models;

namespace SHOPHVT.Data
{
    public class DataContext
    {
        private List<Customer> customers = new List<Customer>();

        public List<Customer> Customers
        {
            get
            {
                UpdateData();
                return customers;
            }
        }

        private SqlAccess _sqlAccess = new SqlAccess();

        public DataContext()
        {
            UpdateData();
        }

        public void UpdateData()
        {
            DataTable dataTable = _sqlAccess.GetTable("SELECT * FROM KhachHang");
            if (dataTable.Rows.Count == 0)
            {
                return;
            }
            customers.Clear();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Customer customer = new Customer()
                {
                    ID = Convert.ToInt32(dataTable.Rows[i]["khachHangID"].ToString()),
                    FullName = dataTable.Rows[i]["hoTen"].ToString(),
                    Birthday = DateTime.Parse(dataTable.Rows[i]["ngaySinh"].ToString()),
                    Gender = dataTable.Rows[i]["gioiTinh"].ToString(),
                    PhoneNumber = dataTable.Rows[i]["soDienThoai"].ToString(),
                    Email = dataTable.Rows[i]["email"].ToString(),
                    Address = dataTable.Rows[i]["diaChi"].ToString(),
                    Username = dataTable.Rows[i]["tenDangNhap"].ToString(),
                    Password = dataTable.Rows[i]["matKhau"].ToString()
                };
                customers.Add(customer);
            }
        }

        public void SaveCustomer(Customer customer)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("khachHangID", customer.ID),
                new SqlParameter("hoTen", customer.FullName),
                new SqlParameter("ngaySinh", customer.Birthday),
                new SqlParameter("gioiTinh", customer.Gender),
                new SqlParameter("soDienThoai", customer.PhoneNumber),
                new SqlParameter("email", customer.Email),
                new SqlParameter("diaChi", customer.Address),
                new SqlParameter("matKhau", customer.Password)
            };
            _sqlAccess.Execute("ProcUpdateKhachHang", parameters);
        }
    }
}