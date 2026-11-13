
using System;
using System.Data;
using System.Data.SqlClient;

namespace QLDT.Data
{
    public interface IDBHelper
    {
        DataTable exec_sql_cmd(SqlCommand command);
    }

    public class DBHelper : IDBHelper
    {
        private SqlConnection _sqlConnection;
        private string _connectionString = string.Empty;
        public DBHelper()
        {
            _connectionString = "Server = DESKTOP-QB-971; Database = QLDT; Integrated Security = True";
            //ConfigurationManager.ConnectionStrings["SQLDBSERVER"].ConnectionString.ToString();
            _sqlConnection = new SqlConnection(_connectionString);
        }

        public DataTable exec_sql_cmd(SqlCommand command)
        {
            DataTable dt = new DataTable();
            try
            {
                if (_sqlConnection.State == ConnectionState.Closed)
                {
                    _sqlConnection.Open();
                }
                command.Connection = _sqlConnection;
                SqlDataAdapter da = new SqlDataAdapter(command);            
                da.Fill(dt);
                _sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sqlConnection.Close();
            }
            return dt;
        }
    }
}
