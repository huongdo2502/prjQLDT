using System;
using System.Data;
using System.Data.SqlClient;
using QLDT.Data;

namespace QLDT.Service
{
    public interface IContractService
    {
        #region tblqldt_contract 
        DataTable tblqldt_contract_get_all();

        DataTable tblqldt_contract_get_by_id(string contract_nbr);

        DataTable tblqldt_room_get_available();

        DataTable tblqldt_room_get_all_contract();
        
        DataTable tblqldt_contract_insert
        (
            string contract_nbr, 
            string room_nbr,
            decimal? deposit, 
            decimal room_price,
            decimal electric_price, 
            decimal water_price, 
            decimal trash_fee, 
            decimal security_fee
        );

        DataTable tblqldt_contract_update
        (
            string contract_nbr,
            string room_nbr,
            decimal? deposit, 
            decimal room_price,
            decimal electric_price, 
            decimal water_price, 
            decimal trash_fee, 
            decimal security_fee
        );

        DataTable tblqldt_contract_delete(string contract_nbr);
        #endregion

        #region tblqldt_member 
        DataTable tblqldt_member_get_all();

        DataTable tblqldt_member_get_by_id(string member_nbr);

        DataTable tblqldt_contract_get_for_member();

        DataTable tblqldt_member_insert
        (
            string member_nbr, 
            string contract_nbr,
            string txt_full_name,
            bool is_primary_tenant,
            string national_id,
            DateTime birth_date, 
            string genle, string phone,
            string adress,
            string status
        );

        DataTable tblqldt_member_update
        (
            string member_nbr,
            string contract_nbr,
            string txt_full_name,
            bool is_primary_tenant,
            string national_id,
            DateTime birth_date,
            string genle,
            string phone,
            string address,
            string status
        );

        DataTable tblqldt_member_delete(string member_nbr);

        #endregion
    }

    public class ContractService : IContractService
    {

        private DBHelper _db;

        public ContractService()
        {
            _db = new DBHelper();
        }

        #region tblqldt_contract
        public DataTable tblqldt_contract_get_all()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_contract_get_all]";
            var dt = _db.exec_sql_cmd(cmd);
            return dt;
        }

        public DataTable tblqldt_contract_get_by_id(string contract_nbr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_contract_get_by_id]";

            cmd.Parameters.AddWithValue("@contract_nbr", contract_nbr);
            var dt = _db.exec_sql_cmd(cmd);
            return dt;
        }

        public DataTable tblqldt_room_get_available()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_room_get_available]";

            return _db.exec_sql_cmd(cmd);
        }

        public DataTable tblqldt_room_get_all_contract()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_room_get_all_contract]";

            return _db.exec_sql_cmd(cmd);
        }

        public DataTable tblqldt_contract_insert(
            string contract_nbr,
            string room_nbr,          
            decimal? deposit,
            decimal room_price,
            decimal electric_price,
            decimal water_price,
            decimal trash_fee,
            decimal security_fee)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_contract_insert]";

            cmd.Parameters.AddWithValue("@contract_nbr", contract_nbr);
            cmd.Parameters.AddWithValue("@room_nbr", room_nbr);

            if (deposit.HasValue)
                cmd.Parameters.AddWithValue("@deposit", deposit.Value);
            else
                cmd.Parameters.AddWithValue("@deposit", DBNull.Value);

            cmd.Parameters.AddWithValue("@room_price", room_price);
            cmd.Parameters.AddWithValue("@electric_price", electric_price);
            cmd.Parameters.AddWithValue("@water_price", water_price);
            cmd.Parameters.AddWithValue("@trash_fee", trash_fee);
            cmd.Parameters.AddWithValue("@security_fee", security_fee);

            return _db.exec_sql_cmd(cmd);
        }

        public DataTable tblqldt_contract_update(
            string contract_nbr,
            string room_nbr,
            decimal? deposit,
            decimal room_price,
            decimal electric_price,
            decimal water_price,
            decimal trash_fee,
            decimal security_fee)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_contract_update]";

            cmd.Parameters.AddWithValue("@contract_nbr", contract_nbr);
            cmd.Parameters.AddWithValue("@room_nbr", room_nbr);

            if (deposit.HasValue)
                cmd.Parameters.AddWithValue("@deposit", deposit.Value);
            else
                cmd.Parameters.AddWithValue("@deposit", DBNull.Value);

            cmd.Parameters.AddWithValue("@room_price", room_price);
            cmd.Parameters.AddWithValue("@electric_price", electric_price);
            cmd.Parameters.AddWithValue("@water_price", water_price);
            cmd.Parameters.AddWithValue("@trash_fee", trash_fee);
            cmd.Parameters.AddWithValue("@security_fee", security_fee);

            return _db.exec_sql_cmd(cmd);
        }

        public DataTable tblqldt_contract_delete(string contract_nbr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_contract_delete]";

            cmd.Parameters.AddWithValue("@contract_nbr", contract_nbr);

            return _db.exec_sql_cmd(cmd);
        }
        #endregion

        #region tblqldt_member 
        public DataTable tblqldt_member_get_all()
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_member_get_all]";
            var dt = _db.exec_sql_cmd(cmd);
            return dt;
        }

        public DataTable tblqldt_member_get_by_id(string member_nbr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "p_qldt_member_get_by_id";

            cmd.Parameters.AddWithValue("@member_nbr", member_nbr);

            return _db.exec_sql_cmd(cmd);
        }

        public DataTable tblqldt_contract_get_for_member()
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_contract_get_for_member]";
            var dt = _db.exec_sql_cmd(cmd);
            return dt;
        }

        public DataTable tblqldt_member_insert
        (
               string member_nbr,
               string contract_nbr,
               string full_name,
               bool is_primary_tenant,
               string national_id,
               DateTime birth_date,
               string gender,
               string phone,
               string address,
               string status
        )
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_member_insert]";

            cmd.Parameters.AddWithValue("@member_nbr", member_nbr);
            cmd.Parameters.AddWithValue("@contract_nbr", contract_nbr);
            cmd.Parameters.AddWithValue("@full_name", full_name);
            cmd.Parameters.AddWithValue("@is_primary_tenant", is_primary_tenant);
            cmd.Parameters.AddWithValue("@national_id", national_id);
            cmd.Parameters.AddWithValue("@birth_date", birth_date);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@status", status);

            var dt = _db.exec_sql_cmd(cmd);
            return dt;
        }

        public DataTable tblqldt_member_update
        (
            string member_nbr,
            string contract_nbr,
            string full_name,
            bool is_primary_tenant,
            string national_id,
            DateTime birth_date,
            string gender,
            string phone,
            string address,
            string status
        )
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_member_update]";

            cmd.Parameters.AddWithValue("@member_nbr", member_nbr);
            cmd.Parameters.AddWithValue("@contract_nbr", contract_nbr);
            cmd.Parameters.AddWithValue("@full_name", full_name);
            cmd.Parameters.AddWithValue("@is_primary_tenant", is_primary_tenant);
            cmd.Parameters.AddWithValue("@national_id", national_id);
            cmd.Parameters.AddWithValue("@birth_date", birth_date);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@address", address);
           cmd.Parameters.AddWithValue("@status", status);

            var dt = _db.exec_sql_cmd(cmd);
            return dt;
        }

        public DataTable tblqldt_member_delete(string member_nbr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_member_delete]";

            cmd.Parameters.AddWithValue("@member_nbr", member_nbr);

            var dt = _db.exec_sql_cmd(cmd);
            return dt;
        }

        #endregion
    }
}
