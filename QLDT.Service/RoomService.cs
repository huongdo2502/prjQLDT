
using System.Data;
using System.Data.SqlClient;
using QLDT.Data;
using System;

namespace QLDT.Service
{
    public interface IRoomService
    {
        #region tblqldt_room
        /*
            1. Lúc mới mở chương trình -> Xuống db select danh sách và hiển thị lên grid
            2. Lúc insert phòng mới -> Xuống db để lưu
            3. Lúc sửa phong -> Xuống db để lưu
            4. Lúc xóa phòng -> Xuống db để lưu
         */

        DataTable tblqldt_room_get_all();
        DataTable tblqldt_room_get_by_id(string room_nbr);
        DataTable tblqldt_room_insert(string room_nbr, string name, string status, decimal area, int max_qty_man);
        DataTable tblqldt_room_update(string room_nbr, string name, string status, decimal area, int max_qty_man);
        DataTable tblqldt_room_delete(string room_nbr);
        #endregion
    }

    public class RoomService : IRoomService
    {

        private DBHelper _db;

        public RoomService()
        {
            _db = new DBHelper();
        }

        #region tblqldt_room
        /*
            1. Lúc mới mở chương trình -> Xuống db select danh sách và hiển thị lên grid
            2. Lúc insert phòng mới -> Xuống db để lưu
            3. Lúc sửa phong -> Xuống db để lưu
            4. Lúc xóa phòng -> Xuống db để lưu
         */
        public DataTable tblqldt_room_get_all()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_room_get_all]";
            var dt = _db.exec_sql_cmd(cmd);
            return dt;
        }

        public DataTable tblqldt_room_get_by_id(string room_nbr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_room_get_by_id]";

            cmd.Parameters.AddWithValue("@room_nbr", room_nbr ?? (object)DBNull.Value);
            var dt = _db.exec_sql_cmd(cmd);
            return dt;
        }

        public DataTable tblqldt_room_insert(string room_nbr, string name, string status, decimal area, int max_qty_man)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_room_insert]";

            cmd.Parameters.AddWithValue("@room_nbr", room_nbr);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@area", area);
            cmd.Parameters.AddWithValue("@max_qty_man", max_qty_man);

            var dt = _db.exec_sql_cmd(cmd);
            return dt;
        }

        public DataTable tblqldt_room_update(string room_nbr, string name, string status, decimal area, int max_qty_man)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_room_update]";

            cmd.Parameters.AddWithValue("@room_nbr", room_nbr);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@area", area);
            cmd.Parameters.AddWithValue("@max_qty_man", max_qty_man);

            var dt = _db.exec_sql_cmd(cmd);
            return dt;
        }

        public DataTable tblqldt_room_delete(string room_nbr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[p_qldt_room_delete]";

            cmd.Parameters.AddWithValue("@room_nbr", room_nbr);

            var dt = _db.exec_sql_cmd(cmd);
            return dt;
        }
        #endregion
    }
}
