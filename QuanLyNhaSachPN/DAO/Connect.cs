using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSachPN.DAO
{
    class Connect
    {
        //string constr = @"Data Source=DESKTOP-2MC26TB\SQLEXPRESS;Initial Catalog=QLNSPN;Integrated Security=True";
        string constr = @"Data Source=DESKTOP-NGEA4UC\SQLEXPRESS;Initial Catalog=QLNSPN;Integrated Security=True";
        SqlConnection conn;
        public Connect()
        {
            conn = new SqlConnection(constr);
        }
        public DataSet LayDuLieu(string truyvan)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter dap = new SqlDataAdapter(truyvan, conn);
                dap.Fill(ds);
                return ds;
            }
            catch
            {
                return null;
            }
        }
        public bool ThucThi(string query)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query);
                int r = cmd.ExecuteNonQuery();
                conn.Close();
                return r > 0;
            }
            catch
            {
                return true;
            }
        }
    }
}
