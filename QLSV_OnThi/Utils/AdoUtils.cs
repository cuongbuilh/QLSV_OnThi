using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV_OnThi.Utils
{
    class AdoUtils
    {

        // Sử dụng trực tiếp database server 
        public const string SERVER = "101.96.66.219,8013";
        public const string DATABASE = "QLSinhVien";
        public const string USERNAME = "sa";
        public const string PASSWORD = "Admin1234";

        public SqlConnection cnn;
        public SqlCommand cmd;
        public DataTable data;
        public SqlDataAdapter adapter;

        public void Connect()
        {
            string connectionTemplate =
                @"Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
            string connectionString = String.Format(connectionTemplate, SERVER, DATABASE, USERNAME, PASSWORD);
            cnn = new SqlConnection(connectionString);
            cnn.Open();
        }

        public void Disconnect()
        {
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
        }


        public DataTable GetDataTable(string Sql)
        {
            Connect();
            adapter = new SqlDataAdapter(Sql, cnn);
            data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public void Excute(string sql)
        {
            Connect();
            cmd = new SqlCommand(sql, cnn);
            cmd.ExecuteNonQuery();
            Disconnect();
        }

        public SqlDataReader ExcuteReader(string sql)
        {
            Connect();
            cmd = new SqlCommand(sql, cnn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            // Disconnect();
            return dataReader;
        }
    }
}