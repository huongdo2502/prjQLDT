using System;
using System.Data;
using System.Data.SqlClient;
using QLDT.Data;

namespace QLDT.Service
{
    public interface IInvoiceService
    {
        DataTable tblqldt_invoice_get_all(int billing_month, int billing_year, DateTime? current_date = null);

        DataTable tblqldt_invoice_create(int billing_month, int billing_year);

        DataTable tblqldt_invoice_update(
            string contract_nbr,
            int billing_month,
            int billing_year,
            int old_electric_reading,
            int new_electric_reading,
            int old_water_reading,
            int new_water_reading,
            string payment_status,
            bool is_checkout,
            DateTime? current_date = null);

        DataTable tblqldt_contract_get_all_invoice();

    }

    public class InvoiceService : IInvoiceService
    {
        private DBHelper _db;

        public InvoiceService()
        {
            _db = new DBHelper();
        }

        #region tblqldt_invoice
        /*
            1. Lúc mới mở chương trình -> Xuống db select danh sách và hiển thị lên grid
            2. Lúc insert phòng mới -> Xuống db để lưu
            3. Lúc sửa phong -> Xuống db để lưu
            4. Lúc xóa phòng -> Xuống db để lưu
         */
        public DataTable tblqldt_invoice_get_all(int billing_month, int billing_year, DateTime? current_date = null)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "p_qldt_invoice_get_all";

            cmd.Parameters.AddWithValue("@billing_month", billing_month);
            cmd.Parameters.AddWithValue("@billing_year", billing_year);

            if (current_date.HasValue)
            {
                cmd.Parameters.AddWithValue("@current_date", current_date.Value);
            }

            return _db.exec_sql_cmd(cmd);
        }

        public DataTable tblqldt_invoice_create(int billing_month, int billing_year)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "p_qldt_invoice_create";

            cmd.Parameters.AddWithValue("@billing_month", billing_month);
            cmd.Parameters.AddWithValue("@billing_year", billing_year);

            return _db.exec_sql_cmd(cmd);
        }

        public DataTable tblqldt_invoice_update(
    string contract_nbr,
    int billing_month,
    int billing_year,
    int old_electric_reading,
    int new_electric_reading,
    int old_water_reading,
    int new_water_reading,
    string payment_status,
    bool is_checkout,
    DateTime? current_date = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "p_qldt_invoice_update";

            cmd.Parameters.AddWithValue("@contract_nbr", contract_nbr);
            cmd.Parameters.AddWithValue("@billing_month", billing_month);
            cmd.Parameters.AddWithValue("@billing_year", billing_year);
            cmd.Parameters.AddWithValue("@old_electric_reading", old_electric_reading);
            cmd.Parameters.AddWithValue("@new_electric_reading", new_electric_reading);
            cmd.Parameters.AddWithValue("@old_water_reading", old_water_reading);
            cmd.Parameters.AddWithValue("@new_water_reading", new_water_reading);
            cmd.Parameters.AddWithValue("@payment_status", payment_status);
            cmd.Parameters.AddWithValue("@is_checkout", is_checkout);

            if (current_date.HasValue)
                cmd.Parameters.AddWithValue("@current_date", current_date.Value);
            else
                cmd.Parameters.AddWithValue("@current_date", DBNull.Value);

            return _db.exec_sql_cmd(cmd);
        }

        public DataTable tblqldt_contract_get_all_invoice()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "p_qldt_contract_get_all_invoice";

            return _db.exec_sql_cmd(cmd);
        }
    }


        #endregion
    }

