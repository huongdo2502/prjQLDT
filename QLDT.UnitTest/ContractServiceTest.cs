
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLDT.Data;
using QLDT.Service;
using QLDT.UI.ViewModel;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QLDT.UnitTest
{
    [TestClass]
    public class ContractServiceTest
    {
        #region declare 
        private DBHelper _db;

        private IRoomService _roomService;
        private IContractService _contractService;
        private IValidationService _validationService;
        #endregion

        #region set value
        #endregion

        #region ham tao
        public ContractServiceTest()
        {
            _db = new DBHelper();

            _roomService = new RoomService();
            _contractService = new ContractService();
            _validationService = new ValidationService();
        }
        #endregion

        #region tblqldt_contract

        #region tblqldt_contract_get_all
        [TestMethod]
        public void tblqldt_contract_get_all_with_data()
        {
            // Xóa dữ liệu
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                                delete from tblqldt_member;
                                delete from tblqldt_contract;
                                delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert dữ liệu phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _roomService.tblqldt_room_insert("P002", "Phòng 2", "TRONG", 20.50M, 5);
            _roomService.tblqldt_room_insert("P003", "Phòng 3", "TRONG", 20.50M, 5);

            // Insert dữ liệu hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000M, 1000000M, 4000M, 10000M, 15000M, 15000M);
            _contractService.tblqldt_contract_insert("HD002", "P002", 800000M, 1000000M, 4000M, 10000M, 15000M, 15000M);
            _contractService.tblqldt_contract_insert("HD003", "P003", 800000M, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Gọi hàm lấy danh sách hợp đồng
            dt = _contractService.tblqldt_contract_get_all();

            // Kiểm tra số dòng dữ liệu trả về
            Assert.AreEqual(3, dt.Rows.Count);
        }

        [TestMethod]
        public void tblqldt_contract_get_all_without_data()
        {
            // Xóa dữ liệu
            SqlCommand cmd = new SqlCommand();           
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                                delete from tblqldt_member;
                                delete from tblqldt_contract;
                                delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Gọi hàm lấy danh sách hợp đồng
            dt = _contractService.tblqldt_contract_get_all();

            // Kiểm tra không có dữ liệu
            Assert.AreEqual(0, dt.Rows.Count);
        }

        #endregion

        #region tblqldt_contract_insert
        [TestMethod]
        public void tblqldt_contract_insert_chk_data_user_input()
        {
            /*
                Quy định:
                    contact_nbr
                        1. Bắt buộc phải nhập (Giá trị không rỗng: not null)    
                        2. Mã hợp đồng tối thiểu 1 kí tự
                        3. Mã hợp đồng tối đa 10 kí tự

                    room_nbr
                        1. Bắt buộc phải nhập (Giá trị không rỗng: not null)    
                        2. Mã phòng tối thiểu 1 kí tự
                        3. Mã phòng tối đa 10 kí tự
                
                    deposit
                        1. Tiền cọc không âm
                        2. Tiền cọc tối đa là 99999999

                    room_price
                        1. Bắt buộc phải nhập (Giá trị không rỗng: not null)      
                        2. Giá phòng không âm
                        3. Giá phòng tối đa 99999999

                    electric_price
                        1. Bắt buộc phải nhập (Giá trị không rỗng: not null)      
                        2. Giá điện không âm
                        3. Giá điện tối đa 9999

                    water_price
                       1. Bắt buộc phải nhập (Giá trị không rỗng: not null)      
                       2. Giá nước không âm
                       3. Giá nước tối đa 99999

                    trash_free
                       1. Bắt buộc phải nhập (Giá trị không rỗng: not null)      
                       2. Tiền rác không âm
                       3. Tiền rác tối đa 99999
                
                    security_free
                       1. Bắt buộc phải nhập (Giá trị không rỗng: not null)      
                       2. Tiền an ninh không âm
                       3. Tiền an ninh tối đa 99999
             */

            // Khởi tạo giá trị hợp lệ trước
            var vm = new tblqldt_contract_insert_vm();
            vm.contract_nbr = "HD001";
            vm.room_nbr = "P001";
            vm.deposit = 800000;
            vm.room_price = 1000000;
            vm.electric_price = 4000;
            vm.water_price = 10000;
            vm.trash_fee = 10000;
            vm.security_fee = 15000;

            // contract_nbr
            try
            {
                vm.contract_nbr = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập mã hợp đồng\n", ex.Message);
            }
            try
            {
                vm.contract_nbr = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập mã hợp đồng\n", ex.Message);
            }

            try
            {
                vm.contract_nbr = "HD0123456789";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã hợp đồng tối đa 10 ký tự\n", ex.Message);
            }

            // room_nbr
            vm.contract_nbr = "HD001";
            try
            {
                vm.room_nbr = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập mã phòng", ex.Message.Trim());
            }
            try
            {
                vm.room_nbr = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập mã phòng", ex.Message.Trim());
            }
            try
            {
                vm.room_nbr = "P0123456789";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã phòng tối đa 10 ký tự", ex.Message.Trim());
            }

            // deposit
            vm.room_nbr = "P001";
            try
            {
                vm.deposit = -800000;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tiền cọc không được âm\n", ex.Message);
            }
            try
            {
                vm.deposit = 999999999;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tiền cọc tối đa là 99999999", ex.Message);
            }

            // room_price
            vm.deposit = 800000;
            try
            {
                vm.room_price = -1000000;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Giá phòng phải lớn hơn 0", ex.Message.Trim());
            }
            try
            {
                vm.room_price = 999999999;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Giá phòng tối đa là 99999999 ", ex.Message);
            }

            // electric_price
            vm.room_price = 1000000;
            try
            {
                vm.electric_price = -4000;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Giá điện phải lớn hơn 0", ex.Message.Trim());
            }
            try
            {
                vm.electric_price = 99999;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Giá điện tối đa là 9999", ex.Message);
            }

            // water_price
            vm.electric_price = 4000;
            try
            {
                vm.water_price = -10000;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Giá nước phải lớn hơn 0", ex.Message.Trim());
            }
            try
            {
                vm.water_price = 999999;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Giá nước tối đa là 99999", ex.Message);
            }

            // trash_fee
            vm.water_price = 10000;
            try
            {
                vm.trash_fee = -15000;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tiền rác phải lớn hơn 0", ex.Message.Trim());
            }
            try
            {
                vm.trash_fee = 99999;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tiền rác tối đa là 99999", ex.Message);
            }

            // security_fee
            vm.trash_fee = 10000;
            try
            {
                vm.security_fee = -15000;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tiền an ninh phải lớn hơn 0", ex.Message.Trim());
            }
            try
            {
                vm.security_fee = 99999;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tiền an ninh tối đa là 99999", ex.Message);
            }
            
            // Tất cả dữ liệu hợp lệ
            vm.security_fee = 15000;
            try
            {
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Test sau cùng, có 1 lỗi không xác định cụ thể:" + ex.Message, ex.Message);
            }
        }

        [TestMethod]
        public void tblqldt_contract_insert_contract_nbr_already_exists()
        {
            /*              
                Quy định:
                   1. Kiểm tra xem Mã hợp đồng nhập vào đã tồn tại trong hệ thống chưa.
                   2. Nếu mã hợp đồng đã tồn tại -> báo lỗi
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                                delete from tblqldt_member;
                                delete from tblqldt_contract;
                                delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert sẵn phòng P001 và hợp đồng HD002 vào db
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            // Khởi tạo dữ liệu hợp lệ 
            var vm = new tblqldt_contract_insert_vm();          
            vm.room_nbr = "P002";
            vm.deposit = 800000;
            vm.room_price = 1000000;
            vm.electric_price = 4000;
            vm.water_price = 10000;
            vm.trash_fee = 15000;
            vm.security_fee = 15000;

            try
            {
                vm.contract_nbr = "HD001"; // Cố tình làm trùng mã
                _validationService.Validate(vm);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã hợp đồng đã tồn tại", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_contract_insert_fail_when_room_status_is_DANG_THUE()
        {
            /*
                Quy định:
                    1. Kiểm tra trạng thái của phòng.
                    2. Chỉ cho phép lập hợp đồng khi phòng có trạng thái "TRONG".
                    3. Nếu phòng có trạng thái "DANG_THUE" hoặc "KHONG_CHO_THUE" thì không được lập hợp đồng.
                    4. Thông báo: "Phòng đang thuê hoặc không cho thuê, không thể lập hợp đồng".
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                                delete from tblqldt_member;
                                delete from tblqldt_contract;
                                delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert phòng trực tiếp bằng SQL 
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.CommandText = @"INSERT INTO tblqldt_room (room_nbr, name, status, area, max_qty_man) 
                              VALUES ('P002', N'Phòng 2', 'DANG_THUE', 20.50, 5);";
            _db.exec_sql_cmd(cmdInsert);

            // Thực hiện lập hợp đồng cho phòng đang thuê và bắt lỗi từ SQL Procedure
            try
            {
                _contractService.tblqldt_contract_insert("HD001", "P002", 800000, 1000000, 4000, 10000, 15000, 15000);
                Assert.Fail("Phải báo lỗi vì phòng đang thuê không được lập hợp đồng.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Phòng đang thuê hoặc không cho thuê, không thể lập hợp đồng", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_contract_insert_fail_when_room_status_is_KHONG_CHO_THUE()
        {
            /*
                Quy định:
                    1. Chỉ cho phép lập hợp đồng khi phòng có trạng thái "TRONG".
                    2. Trường hợp kiểm thử: phòng có trạng thái "KHONG_CHO_THUE".
                    3. Kết quả mong đợi: Không lập được hợp đồng.
                    4. Thông báo: "Phòng đang thuê hoặc không cho thuê, không thể lập hợp đồng".
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert phòng trực tiếp bằng SQL 
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.CommandText = @"INSERT INTO tblqldt_room (room_nbr, name, status, area, max_qty_man) 
                              VALUES ('P004', N'Phòng 4', 'KHONG_CHO_THUE', 20.50, 5);";
            _db.exec_sql_cmd(cmdInsert);

            // Thực hiện lập hợp đồng cho phòng không cho thuê và bắt lỗi từ SQL Procedure
            try
            {
                _contractService.tblqldt_contract_insert("HD004", "P004", 800000, 1000000, 4000, 10000, 15000, 15000);
                Assert.Fail("Phải báo lỗi vì phòng không cho thuê không được lập hợp đồng.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Phòng đang thuê hoặc không cho thuê, không thể lập hợp đồng", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_contract_insert_without_deposit_zero_should_success()
        {
            /*
             Quy định:
                1. Cho phép lập hợp đồng khi không nhập tiền đặt cọc.
                2. Nếu không nhập tiền đặt cọc thì giá trị deposit và start_date được lưu là NULL.
                3. Trạng thái phòng vẫn giữ nguyên là "TRONG".
             */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert phòng
            _roomService.tblqldt_room_insert("P006", "Phòng 6", "TRONG", 20.50M, 5);

            // Insert hợp đồng (không đặt cọc)
            DataTable dtInsert = _contractService.tblqldt_contract_insert("HD006", "P006", null, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Trả kết quả trên UI là 1 dòng dữ liệu
            Assert.AreEqual(1, dtInsert.Rows.Count);
            Assert.AreEqual("1", dtInsert.Rows[0]["status"].ToString());

            // Kiểm tra dữ liệu hợp đồng
            DataTable dtContract = _contractService.tblqldt_contract_get_by_id("HD006");

            Assert.AreEqual(1, dtContract.Rows.Count);
            Assert.AreEqual("HD006", dtContract.Rows[0]["contract_nbr"].ToString());
            Assert.AreEqual("P006", dtContract.Rows[0]["room_nbr"].ToString());
            Assert.IsTrue(dtContract.Rows[0].IsNull("deposit")); // Không đặt cọc
            Assert.AreEqual(1000000M, Convert.ToDecimal(dtContract.Rows[0]["room_price"]));
            Assert.AreEqual(4000M, Convert.ToDecimal(dtContract.Rows[0]["electric_price"]));
            Assert.AreEqual(10000M, Convert.ToDecimal(dtContract.Rows[0]["water_price"]));
            Assert.AreEqual(15000M, Convert.ToDecimal(dtContract.Rows[0]["trash_fee"]));
            Assert.AreEqual(15000M, Convert.ToDecimal(dtContract.Rows[0]["security_fee"]));

            // Kiểm tra trạng thái phòng
            DataTable dtRoom = _roomService.tblqldt_room_get_by_id("P006");

            Assert.AreEqual(1, dtRoom.Rows.Count);
            Assert.AreEqual("P006", dtRoom.Rows[0]["room_nbr"].ToString());
            Assert.AreEqual("TRONG", dtRoom.Rows[0]["status"].ToString());
        }

        [TestMethod]
        public void tblqldt_contract_insert_contract_success()
        {
            /*
              Quy định:
                1.Chỉ cho phép lập hợp đồng khi phòng có trạng thái "TRONG".
                2.Nếu có nhập tiền đặt cọc thì lập hợp đồng thành công.
                3.Sau khi lập hợp đồng thành công, trạng thái phòng được cập nhật từ "TRONG" sang "DANG_THUE".
            */

            // Clear dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice; 
                        delete from tblqldt_member; 
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert phòng
            _roomService.tblqldt_room_insert("P005", "Phòng 5", "TRONG", 20.50M, 5);

            // Insert hợp đồng
            DataTable dtInsert = _contractService.tblqldt_contract_insert("HD005", "P005", 800000M, 1000000M, 4000M, 10000M, 15000M, 15000M);

            Assert.AreEqual(1, dtInsert.Rows.Count);
            Assert.AreEqual("1", dtInsert.Rows[0]["status"].ToString());

            // Lấy dữ liệu hợp đồng từ DB
            DataTable dtContract = _contractService.tblqldt_contract_get_by_id("HD005");

            // Assert kiểm tra hợp đồng
            Assert.AreEqual(1, dtContract.Rows.Count);
            Assert.AreEqual("HD005", dtContract.Rows[0]["contract_nbr"].ToString());
            Assert.AreEqual("P005", dtContract.Rows[0]["room_nbr"].ToString());
            Assert.AreEqual(800000M, Convert.ToDecimal(dtContract.Rows[0]["deposit"]));
            Assert.AreEqual(1000000M, Convert.ToDecimal(dtContract.Rows[0]["room_price"]));
            Assert.AreEqual(4000M, Convert.ToDecimal(dtContract.Rows[0]["electric_price"]));
            Assert.AreEqual(10000M, Convert.ToDecimal(dtContract.Rows[0]["water_price"]));
            Assert.AreEqual(15000M, Convert.ToDecimal(dtContract.Rows[0]["trash_fee"]));
            Assert.AreEqual(15000M, Convert.ToDecimal(dtContract.Rows[0]["security_fee"]));

            // Lấy dữ liệu phòng từ DB để kiểm tra xem phòng P005 đã đổi trạng thái chưa
            DataTable dtRoom = _roomService.tblqldt_room_get_by_id("P005");

            // Assert kiểm tra phòng
            Assert.AreEqual(1, dtRoom.Rows.Count);
            Assert.AreEqual("P005", dtRoom.Rows[0]["room_nbr"].ToString());
            Assert.AreEqual("DANG_THUE", dtRoom.Rows[0]["status"].ToString());
        }

        #endregion

        #region tblqldt_contract_update
        [TestMethod]
        public void tblqldt_contract_update_chk_data_user_input()
        {
            /*
                 Quy định:
                    contact_nbr
                        1. Bắt buộc phải nhập
                        2. Mã hợp đồng tối thiểu 1 kí tự
                        3. Mã hợp đồng tối đa 10 kí tự

                    room_nbr
                        1. Bắt buộc phải nhập: Không rỗng
                        2. Mã phòng tối thiểu 1 kí tự
                        3. Mã phòng tối đa 10 kí tự

                    deposit
                        1. Tiền cọc không âm
                        2. Tiền cọc tối đa là 99999999

                    room_price
                        1. Bắt buộc phải nhập (Giá trị không rỗng: not null)      
                        2. Giá phòng không âm
                        3. Giá phòng tối đa 99999999

                    electric_price
                        1. Bắt buộc phải nhập (Giá trị không rỗng: not null)      
                        2. Giá điện không âm
                        3. Giá điện tối đa 9999

                    water_price
                        1. Bắt buộc phải nhập (Giá trị không rỗng: not null)      
                        2. Giá điện không âm
                        3. Giá điện tối đa 99999

                    trash_fee
                        1. Bắt buộc phải nhập (Giá trị không rỗng: not null)      
                        2. Giá điện không âm
                        3. Giá điện tối đa 99999

                    security_fee
                        1. Bắt buộc phải nhập (Giá trị không rỗng: not null)      
                        2. Giá điện không âm
                        3. Giá điện tối đa 99999
             */

            // Khởi tạo giá trị hợp lệ trước
            var vm = new tblqldt_contract_insert_vm();
            vm.contract_nbr = "HD001";
            vm.room_nbr = "P001";
            vm.deposit = 800000;
            vm.room_price = 1000000;
            vm.electric_price = 4000;
            vm.water_price = 10000;
            vm.trash_fee = 15000;
            vm.security_fee = 15000;

            // contract_nbr
            try
            {
                vm.contract_nbr = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập mã hợp đồng\n", ex.Message);
            }

            try
            {
                vm.contract_nbr = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập mã hợp đồng\n", ex.Message);
            }
            try
            {
                vm.contract_nbr = "HD0123456789";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã hợp đồng tối đa 10 ký tự\n", ex.Message);
            }

            // room_nbr
            vm.contract_nbr = "HD001";
            try
            {
                vm.room_nbr = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập mã phòng\n", ex.Message);
            }
            try
            {
                vm.room_nbr = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập mã phòng\n", ex.Message);
            }
            try
            {
                vm.room_nbr = "P0123456789";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã phòng tối đa 10 ký tự\n", ex.Message);
            }

            // depoit
            vm.room_nbr = "P001";
            try
            {
                vm.deposit = -800000;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tiền cọc không được âm\n", ex.Message);
            }
            try
            {
                vm.deposit = 999999999;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tiền cọc tối đa là 99999999", ex.Message);
            }

            // room_price
            vm.deposit = 1000000;
            try
            {
                vm.room_price = -1000000;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Giá phòng phải lớn hơn 0", ex.Message.Trim());
            }
            try
            {
                vm.room_price = 999999999;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Giá phòng tối đa là 99999999 ", ex.Message);
            }

            // electric_price
            vm.room_price = 1000000;
            try
            {
                vm.electric_price = -4000;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Giá điện phải lớn hơn 0", ex.Message.Trim());
            }
            try
            {
                vm.electric_price = 99999;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Giá điện tối đa là 9999 ", ex.Message);
            }

            // water_price
            vm.electric_price = 4000;
            try
            {
                vm.water_price = -10000;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Giá nước phải lớn hơn 0", ex.Message.Trim());
            }
            try
            {
                vm.water_price = 999999;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Giá nước tối đa là 99999 ", ex.Message);
            }

            // trash_fee
            vm.water_price = 10000;
            try
            {
                vm.trash_fee = 15000;
                
                vm.trash_fee = -10000;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tiền rác phải lớn hơn 0", ex.Message.Trim());
            }
            try
            {
                vm.trash_fee = 999999;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tiền rác tối đa là 99999 ", ex.Message);
            }

            // security_fee
            vm.trash_fee = 15000;
            try
            {
                vm.security_fee = -15000;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tiền an ninh phải lớn hơn 0", ex.Message.Trim());
            }
            try
            {
                vm.security_fee = 999999;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tiền an ninh tối đa là 99999 ", ex.Message);
            }

            // Tất cả các cột hợp lệ
            vm.security_fee = 15000;
            try
            {
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Test sau cùng, có 1 lỗi không xác định cụ thể:" + ex.Message, ex.Message);
            }
        }
      
        [TestMethod]
        public void tblqldt_contract_update_contract_not_exists()
        {
            /*  
                Quy định:
                    Kiểm tra Mã Hợp Đồng có tồn tại hay không -> 
                    Nếu không, thông báo: "Dữ liệu không tồn tại để cập nhật"
            */

            // Xóa sạch dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert sẵn phòng P001 và hợp đồng HD001 vào db
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            // Khởi tạo dữ liệu hợp lệ
            var vm = new tblqldt_contract_insert_vm();
            vm.contract_nbr = "HD001";
            vm.room_nbr = "P001";
            vm.deposit = 800000;
            vm.room_price = 1000000;
            vm.electric_price = 4000;
            vm.water_price = 10000;
            vm.trash_fee = 15000;
            vm.security_fee = 15000;

            try
            {
                vm.contract_nbr = "HD002";
                _validationService.Validate(vm);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("Không tìm thấy hợp đồng", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_contract_update_is_end_date()
        {
            /*  
                Quy định:
                    1. Kiểm tra hợp đồng có ngày kết thúc chưa 
                    2. Nếu có ngày kết thức, thông báo: Hợp đồng đã kết thúc không cập nhật cập nhật"
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                                delete from tblqldt_member;
                                delete from tblqldt_contract;
                                delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert sẵn phòng P001 và hợp đồng HD001 vào db
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            // Insert phiếu thu với is_checkout = 1
            SqlCommand cmdInvoice = new SqlCommand();
            cmdInvoice.CommandType = CommandType.Text;
            cmdInvoice.CommandText = @"INSERT INTO tblqldt_invoice 
        (
            contract_nbr, billing_month, billing_year, room_price, 
            old_electric_reading, new_electric_reading, electric_price, electric_amount, 
            old_water_reading, new_water_reading, water_price, water_amount, 
            trash_fee, security_fee, 
            total_amount, 
            payment_status, collection_date, is_checkout
        ) 
        VALUES 
        (
            'HD001', 7, 2026, 1000000, 
            0, 0, 4000, 0, 
            0, 0, 10000, 0, 
            15000, 15000, 
            1000000,
            'DA_THU', '2026-07-21', 1
        );";
            _db.exec_sql_cmd(cmdInvoice);

            // Khởi tạo dữ liệu hợp lệ
            var vm = new tblqldt_contract_update_vm();
            vm.contract_nbr = "HD001";
            vm.room_nbr = "P001";
            vm.deposit = 800000;
            vm.room_price = 1000000;
            vm.electric_price = 4000;
            vm.water_price = 10000;
            vm.trash_fee = 15000;
            vm.security_fee = 15000;

            try
            {
                vm.deposit = 500000; //truyền dữ liệu muốn update vào
                _validationService.Validate(vm);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("Hợp đồng đã kết thúc không cập nhật", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_contract_update_add_deposit_and_change_room_status()
        {
            /*
                Quy định:
                    1. Hợp đồng chưa đóng tiền cọc (deposit = 0).
                    2. Cập nhật bổ sung tiền cọc.
                    3. Tiền cọc được cập nhật thành công.
                    4. Trạng thái phòng chuyển từ TRONG -> DANG_THUE.
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Tạo phòng và hợp đồng chưa đặt cọc
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            _contractService.tblqldt_contract_insert("HD001", "P001", 0, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Kiểm tra dữ liệu trước khi update
            DataTable dtBeforeContract = _contractService.tblqldt_contract_get_by_id("HD001");
            Assert.AreEqual(1, dtBeforeContract.Rows.Count);
            Assert.AreEqual(0M, Convert.ToDecimal(dtBeforeContract.Rows[0]["deposit"]));

            DataTable dtBeforeRoom = _roomService.tblqldt_room_get_by_id("P001");
            Assert.AreEqual("TRONG", dtBeforeRoom.Rows[0]["status"].ToString());

            // Thực hiện update
            _contractService.tblqldt_contract_update("HD001", "P001", 900000M, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Kiểm tra dữ liệu hợp đồng sau update
            DataTable dtContract = _contractService.tblqldt_contract_get_by_id("HD001");

            Assert.AreEqual(1, dtContract.Rows.Count);
            Assert.AreEqual("HD001", dtContract.Rows[0]["contract_nbr"].ToString());
            Assert.AreEqual(900000M, Convert.ToDecimal(dtContract.Rows[0]["deposit"]));

            // Kiểm tra trạng thái phòng
            DataTable dtRoom = _roomService.tblqldt_room_get_by_id("P001");

            Assert.AreEqual(1, dtRoom.Rows.Count);
            Assert.AreEqual("P001", dtRoom.Rows[0]["room_nbr"].ToString());
            Assert.AreEqual("DANG_THUE", dtRoom.Rows[0]["status"].ToString());
        }

        [TestMethod]
        public void tblqldt_contract_update_information_success()
        {
            /*
                Quy định:
                    1. Hợp đồng đã có tiền cọc (deposit = 800000).
                    2. Cập nhật lại thông tin hợp đồng:
                       - deposit
                       - room_price
                       - electric_price
                       - water_price
                       - trash_fee
                       - security_fee
                    3. Kiểm tra dữ liệu hợp đồng được cập nhật đúng.
                    4. Trạng thái phòng vẫn là DANG_THUE.
            */

            // Clear dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice; 
                        delete from tblqldt_member; 
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            // Insert hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000M, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Kiểm tra thông tin trước khi update
            DataTable dtBeforeContract = _contractService.tblqldt_contract_get_by_id("HD001");
            DataTable dtBeforeRoom = _roomService.tblqldt_room_get_by_id("P001");

            Assert.AreEqual(800000M, Convert.ToDecimal(dtBeforeContract.Rows[0]["deposit"]));
            Assert.AreEqual(1000000M, Convert.ToDecimal(dtBeforeContract.Rows[0]["room_price"]));
            Assert.AreEqual(4000M, Convert.ToDecimal(dtBeforeContract.Rows[0]["electric_price"]));
            Assert.AreEqual(10000M, Convert.ToDecimal(dtBeforeContract.Rows[0]["water_price"]));
            Assert.AreEqual(15000M, Convert.ToDecimal(dtBeforeContract.Rows[0]["trash_fee"]));
            Assert.AreEqual(15000M, Convert.ToDecimal(dtBeforeContract.Rows[0]["security_fee"]));

            Assert.AreEqual("DANG_THUE", dtBeforeRoom.Rows[0]["status"].ToString());

            // Update hợp đồng
            DataTable dtUpdate = _contractService.tblqldt_contract_update("HD001","P001", 111111M, 1999999M, 3999M, 9999M, 14999M, 15999M);
            Assert.AreEqual(1, dtUpdate.Rows.Count);

            // Lấy dữ liệu hợp đồng từ DB
            DataTable dtContract = _contractService.tblqldt_contract_get_by_id("HD001");

            // Assert kiểm tra hợp đồng sau update
            Assert.AreEqual(1, dtContract.Rows.Count);
            Assert.AreEqual("HD001", dtContract.Rows[0]["contract_nbr"].ToString());
            Assert.AreEqual("P001", dtContract.Rows[0]["room_nbr"].ToString());
            Assert.AreEqual(111111M, Convert.ToDecimal(dtContract.Rows[0]["deposit"]));
            Assert.AreEqual(1999999M, Convert.ToDecimal(dtContract.Rows[0]["room_price"]));
            Assert.AreEqual(3999M, Convert.ToDecimal(dtContract.Rows[0]["electric_price"]));
            Assert.AreEqual(9999M, Convert.ToDecimal(dtContract.Rows[0]["water_price"]));
            Assert.AreEqual(14999M, Convert.ToDecimal(dtContract.Rows[0]["trash_fee"]));
            Assert.AreEqual(15999M, Convert.ToDecimal(dtContract.Rows[0]["security_fee"]));

            // Lấy dữ liệu phòng từ DB để kiểm tra xem phòng P001 đã đổi trạng thái chưa
            DataTable dtRoom = _roomService.tblqldt_room_get_by_id("P001");

            // Assert kiểm tra phòng
            Assert.AreEqual(1, dtRoom.Rows.Count);
            Assert.AreEqual("P001", dtRoom.Rows[0]["room_nbr"].ToString());
            Assert.AreEqual("DANG_THUE", dtRoom.Rows[0]["status"].ToString());
        }

        #endregion

        #region tblqldt_contract_delete
        [TestMethod]
        public void tblqldt_contract_delete_contract_not_exists()
        {
            /*        
                Quy định:
                    Kiểm tra xem mã đồng có tồn tại ? -> không tồn tại để xóa
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                                delete from tblqldt_member;
                                delete from tblqldt_contract;
                                delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert sẵn phòng P001 và hợp đồng HD001 vào db
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            try
            {
                _contractService.tblqldt_contract_delete("P002");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Không tìm thấy mã hợp đồng", ex.Message);
            }
        }

        [TestMethod]
        public void tblqldt_contract_delete_has_deposit()
        {
            /*      
                Quy định :
                    Kiểm tra xem hợp đồng đã có tiền cọc -> Không xóa hợp đồng đã coc cọc
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert sẵn phòng P001 và hợp đồng HD001 vào db
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            try
            {
                _contractService.tblqldt_contract_delete("HD001");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Hợp đồng đang thuê không thể xóa", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_contract_delete_room_checked_out()
        {
            /*    
                Quy định:
                   Kiểm tra xem hợp đồng đã trả phòng -> Không xóa hợp đồng đã trả phòng
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert sẵn phòng P001 và hợp đồng HD001 vào db 
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            // Insert phiếu thu với is_checkout = 1
            SqlCommand cmdInvoice = new SqlCommand();
            cmdInvoice.CommandType = CommandType.Text;
            cmdInvoice.CommandText = @"INSERT INTO tblqldt_invoice 
        (
            contract_nbr, billing_month, billing_year, room_price, 
            old_electric_reading, new_electric_reading, electric_price, electric_amount, 
            old_water_reading, new_water_reading, water_price, water_amount, 
            trash_fee, security_fee, 
            total_amount, 
            payment_status, collection_date, is_checkout
        ) 
        VALUES 
        (
            'HD001', 7, 2026, 1000000, 
            0, 0, 4000, 0, 
            0, 0, 10000, 0, 
            15000, 15000, 
            1000000,
            'DA_THU', '2026-07-21', 1
        );";
            _db.exec_sql_cmd(cmdInvoice);

            try
            {
                _contractService.tblqldt_contract_delete("HD001");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Hợp đồng đã trả phòng, không thể xóa", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_contract_delete_no_deposit_has_member()
        {
            /*          
                Quy định:
                    Kiểm tra xem hợp đồng chưa có tiền cọc nhưng đã có thành viên thì không cho phép xóa          
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert sẵn phòng P001
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            // Insert hợp đồng HD001 với tiền cọc bằng null (chưa cọc)
            _contractService.tblqldt_contract_insert("HD001", "P001", null, 1000000, 4000, 10000, 15000, 15000);

            // Insert trực tiếp thành viên bằng SQL 
            SqlCommand cmdMember = new SqlCommand();
            cmdMember.CommandType = CommandType.Text;
            cmdMember.CommandText = @"
        INSERT INTO dbo.tblqldt_member
        (
            member_nbr, contract_nbr, full_name, is_primary_tenant, 
            national_id, birth_date, gender, phone, address, status
        )
        VALUES
        (
            'TV001', 'HD001', N'Bùi Anh Quân', 1, 
            '098900000011', '1999-12-12', N'Nam', '0989000011', N'An Giang', 'CHO_NHAN_PHONG'
        );";
            _db.exec_sql_cmd(cmdMember);

            try
            {                
                _contractService.tblqldt_contract_delete("HD001");            
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Hợp đồng chưa cọc nhưng đã có thành viên. Vui lòng xóa thành viên trước", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_contract_delete_success_no_deposit_no_member() 
        { 
            /*          
                Quy định:
                    Kiểm tra xem hợp đồng chưa cọc và chưa có thành viên thì được phép xóa thành công          
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert sẵn phòng P001
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            // Insert hợp đồng HD001 với tiền cọc bằng null (chưa cọc) và  chưa có thành viên nào
            _contractService.tblqldt_contract_insert("HD001", "P001", null, 1000000, 4000, 10000, 15000, 15000);

            // Thực hiện xóa
              _contractService.tblqldt_contract_delete("HD001");

            // Lấy kết quả trên UI, kiểm tra xem có trả về 0 dòng
            dt = _contractService.tblqldt_contract_get_all();         
            Assert.AreEqual(dt.Rows.Count, 0);
        }

        #endregion

        #endregion

        #region tblqldt_member

        #region tblqldt_member_get_all
        [TestMethod]
        public void tblqldt_member_get_all_with_data()
        {
            // Xóa dữ liệu
            SqlCommand cmd = new SqlCommand();           
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                                delete from tblqldt_member;
                                delete from tblqldt_contract;
                                delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert dữ liệu phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            // Insert dữ liệu hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000M, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Insert dữ liệu thành viên
            _contractService.tblqldt_member_insert("TV001", "HD001", "Nguyễn Hoàng Lâm", true, "098900000011", new DateTime(2000, 11, 21), "Nam", null, "Bình Dương", "DANG_O");

            // Gọi hàm và lấy kết quả trên UI, kết quả số dòng trả về là 1
            dt = _contractService.tblqldt_member_get_all();
            Assert.AreEqual(dt.Rows.Count, 1);
        }

        [TestMethod]
        public void tblqldt_member_get_all_without_data()
        {
            // Xóa dữ liệu
            SqlCommand cmd = new SqlCommand();           
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                                delete from tblqldt_member;
                                delete from tblqldt_contract;
                                delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Không insert dữ liệu, gọi hàm luôn
            dt = _contractService.tblqldt_member_get_all();

            // Lấy kết quả trên UI, kiểm tra xem có trả về 0 dòng
            Assert.AreEqual(dt.Rows.Count, 0);
        }

        #endregion

        #region tblqldt_member_insert
        [TestMethod]
        public void tblqldt_member_insert_member_chk_data_user_input()
        {
            /*
                Quy định:
                    member_nbr
                        1. Bắt buộc nhập
                        2. Tối thiểu 1 ký tự
                        3. Tối đa 10 ký tự

                    contract_nbr
                        1. Bắt buộc nhập
                        2. Tối thiểu 1 ký tự
                        3. Tối đa 10 ký tự

                    full_name
                        1. Bắt buộc nhập
                        2. Tối thiểu 1 ký tự
                        3. Tối đa 200 ký tự

                    is_primary_tenant
                        1. Bắt buộc nhập

                    national_id
                        1. Bắt buộc nhập
                        2. Đúng 12 số

                    phone
                        1. Đúng 10 số (nếu có nhập)

                    birth_date
                        1. Bắt buộc nhập
                        2. Nhỏ hơn ngày hiện tại

                    gender
                        1. Bắt buộc nhập

                    address
                        1. Bắt buộc nhập
                        2. Tối đa 500 ký tự

                    status
                        1. Bắt buộc nhập
                        2. Tối đa 30 ký tự
            */

            // Khởi tạo dữ liệu hợp lệ
            var vm = new tblqldt_member_insert_vm();
            vm.member_nbr = "TV001";
            vm.contract_nbr = "HD001";
            vm.full_name = "Nguyễn Minh Khang";
            vm.is_primary_tenant = true;
            vm.national_id = "012345678901";
            vm.phone = "0989000011";
            vm.birth_date = new DateTime(2000, 12, 12);
            vm.gender = "Nam";
            vm.address = "Bình Dương";
            vm.status = "DANG_O"; //"DANG_CHO_PHONG"
          
            // member_nbr
            try
            {
                vm.member_nbr = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập mã thành viên", ex.Message.Trim());
            }

            try
            {
                vm.member_nbr = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập mã thành viên", ex.Message.Trim());
            }

            try
            {
                vm.member_nbr = "TV0123456789";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã thành viên tối đa 10 ký tự", ex.Message.Trim());
            }

            // contract_nbr
            vm.member_nbr = "TV001";

            try
            {
                vm.contract_nbr = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng chọn hợp đồng", ex.Message.Trim());
            }

            try
            {
                vm.contract_nbr = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng chọn hợp đồng", ex.Message.Trim());
            }

            try
            {
                vm.contract_nbr = "HD0123456789";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã hợp đồng tối đa 10 ký tự", ex.Message.Trim());
            }
           
            // full_name
            vm.contract_nbr = "HD001";

            try
            {
                vm.full_name = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập họ tên", ex.Message.Trim());
            }

            try
            {
                vm.full_name = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập họ tên", ex.Message.Trim());
            }

            try
            {
                vm.full_name = new string('A', 201);
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Họ tên tối đa 200 ký tự", ex.Message.Trim());
            }
            

            // national_id
            vm.full_name = "Nguyễn Minh Khang";

            try
            {
                vm.national_id = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập CCCD", ex.Message.Trim());
            }

            try
            {
                vm.national_id = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập CCCD", ex.Message.Trim());
            }

            try
            {
                vm.national_id = "0123456789";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("CCCD phải gồm đúng 12 số", ex.Message.Trim());
            }
          
            // phone
            vm.national_id = "012345678901";

            try
            {
                vm.phone = "098900001";//9 SỐ
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Số điện thoại phải gồm 10 số", ex.Message.Trim());
            }
            try
            {
                vm.phone = "09890000111";//11 SỐ
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Số điện thoại phải gồm 10 số", ex.Message.Trim());
            }

            // birth_date
            vm.phone = "0989000011";

            try
            {
                vm.birth_date = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập ngày sinh", ex.Message.Trim());
            }
            try
            {
                vm.birth_date = DateTime.Today.AddDays(1);
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Ngày sinh phải nhỏ hơn ngày hiện tại", ex.Message.Trim());
            }
        
            // gender
            vm.birth_date = new DateTime(2000, 12, 12);

            try
            {
                vm.gender = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng chọn giới tính", ex.Message.Trim());
            }
           
            // address
            vm.gender = "Nam";

            try
            {
                vm.address = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập địa chỉ", ex.Message.Trim());
            }

            try
            {
                vm.address = new string('A', 501);
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Địa chỉ tối đa 500 ký tự", ex.Message.Trim());
            }
         
            // status
            vm.address = "Bình Dương";

            try
            {
                vm.status = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng chọn trạng thái", ex.Message.Trim());
            }

            try
            {
                vm.status = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng chọn trạng thái", ex.Message.Trim());
            }
            try
            {
                vm.status = "AAAAA";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Trạng thái thành viên phải là DANG_O hoặc DANG_CHO_PHONG", ex.Message.Trim());
            }

            // Tất cả dữ liệu hợp lệ
            vm.status = "DANG_O";

            try
            {
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Test sau cùng, có 1 lỗi không xác định cụ thể:" + ex.Message, ex.Message);
            }
            
        }

        [TestMethod]
        public void tblqldt_member_insert_member_nbr_already_exists()
        {
            /*
                Quy định:
                    1. Mã thành viên đã tồn tại.
                    2. Không được thêm.
                    3. Báo: "Mã thành viên đã tồn tại"
            */

            // Xóa dữ liệu
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);

            // Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);

            // Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000M, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Thêm thành viên lần 1
            _contractService.tblqldt_member_insert("TV001", "HD001", "Nguyễn Diễm Quỳnh", true, "012345678901", new DateTime(2000, 12, 12),
                "Nữ", "0989000001", "Bình Dương", "DANG_O");

            // Thêm lại cùng mã TV001
            try
            {
                _contractService.tblqldt_member_insert("TV001", "HD001", "Nguyễn Văn A", false, "098765432101", new DateTime(2000, 1, 1),
                    "Nam", "0989888888", "Bình Dương", "DANG_O");
                Assert.Fail("Phải báo lỗi mã thành viên đã tồn tại.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã thành viên đã tồn tại", ex.Message);
            }
        }

        [TestMethod]
        public void tblqldt_member_insert_fail_when_contract_nbr_not_exists()
        {
            /*
                Quy định:
                    1. Mã hợp đồng không tồn tại.
                    2. Không được thêm thành viên.
                    3. Báo: "Mã hợp đồng không tồn tại".
            */

            // Xóa dữ liệu
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                        DELETE FROM tblqldt_member;
                        DELETE FROM tblqldt_contract;
                        DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);

            // Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);

            // Không tạo hợp đồng HD001

            try
            {
                _contractService.tblqldt_member_insert(
                    "TV001",
                    "HD001",                  // Không tồn tại
                    "Nguyễn Văn A",
                    true,
                    "012345678901",
                    new DateTime(2000, 1, 1),
                    "Nam",
                    "0989000001",
                    "Bình Dương",
                    "DANG_O");

                Assert.Fail("Phải báo lỗi mã hợp đồng không tồn tại.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã hợp đồng không tồn tại", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_insert_contract_already_returned_room()
        {
            /*
                Quy định:
                    1. Ngày kết thúc được lấy là ngày trả phòng bên phiếu thu.
                    2. Nếu phiếu thu ghi nhận đã có ngày trả phòng -> Hợp đồng đã trả phòng.
                    3. Không cho thêm thành viên và báo: "Hợp đồng đã trả phòng không thêm thành viên"
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                        DELETE FROM tblqldt_member;
                        DELETE FROM tblqldt_contract;
                        DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);

            // Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);

            // Tạo hợp đồng (Bên hợp đồng không lưu ngày kết thúc)
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000M, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Tạo phiếu thu VÀ truyền vào "ngày trả phòng" (ví dụ: ngày 24/07/2026)
            SqlCommand cmdInvoice = new SqlCommand();
            cmdInvoice.CommandType = CommandType.Text;
            cmdInvoice.CommandText = @"INSERT INTO tblqldt_invoice 
        (
            contract_nbr, billing_month, billing_year, room_price, 
            old_electric_reading, new_electric_reading, electric_price, electric_amount, 
            old_water_reading, new_water_reading, water_price, water_amount, 
            trash_fee, security_fee, 
            total_amount, 
            payment_status, collection_date, is_checkout
        ) 
        VALUES 
        (
            'HD001', 7, 2026, 1000000, 
            0, 0, 4000, 0, 
            0, 0, 10000, 0, 
            15000, 15000, 
            1000000,
            'DA_THU', '2026-07-21', 1
        );";
            _db.exec_sql_cmd(cmdInvoice);

            // Thêm thành viên -> Hệ thống kiểm tra thấy phiếu thu có ngày trả phòng -> Văng lỗi
            try
            {
                _contractService.tblqldt_member_insert("TV001", "HD001", "Nguyễn Văn A", true, "012345678901", new DateTime(2000, 12, 12),
                                        "Nam", "0989000001", "Bình Dương", "DANG_O");
                Assert.Fail("Phải báo lỗi hợp đồng đã trả phòng không thêm thành viên");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Hợp đồng đã trả phòng không thêm thành viên", ex.Message);
            }
        }

        [TestMethod]
        public void tblqldt_member_insert_national_id_already_exists()
        {
            /*
                Quy định:
                    1. CCCD đã tồn tại trong hệ thống.
                    2. Không cho phép thêm thành viên.
                    3. Báo lỗi: "CCCD đã tồn tại"
                */

            // Xóa dữ liệu
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);

            // Chuẩn bị dữ liệu nền (Phòng và Hợp đồng)
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000M, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Thêm thành viên
            DataTable dt = _contractService.tblqldt_member_insert("TV001", "HD001", "Nguyễn Diễm Quỳnh", true, "012345678901",
                new DateTime(2000, 12, 12), "Nữ", "0989000001", "Bình Dương", "DANG_O");

            // Thêm thành viên lần 2 với CÙNG số CCCD (Phải báo lỗi)
            try
            {
                _contractService.tblqldt_member_insert("TV002", "HD001", "Trần Thanh Tuyền", false, "012345678901", 
                    new DateTime(2001, 2, 2), "Nữ", "0912345678", "Hà Nội", "DANG_O");
                Assert.Fail("Phải báo lỗi CCCD đã tồn tại.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("CCCD đã tồn tại", ex.Message);
            }
        }

        [TestMethod]
        public void tblqldt_member_insert_primary_tenant_already_exists()
        {
            /*
                Quy định:
                    1. Hợp đồng đã có chủ hộ (is_primary_tenant = 1).
                    2. Thêm một thành viên khác với is_primary_tenant = 1 cho cùng hợp đồng đó.
                    3. Không cho phép thêm và báo lỗi: "Hợp đồng đã có chủ hộ" ạn).
            */

            // Xóa dữ liệu
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);

            // Chuẩn bị dữ liệu nền (Phòng và Hợp đồng)
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000M, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Thêm chủ hộ lần 1 (Thành công - is_primary_tenant = true)
            _contractService.tblqldt_member_insert("TV001", "HD001", "Nguyễn Diễm Quỳnh",true, "012345678901",
                new DateTime(2000, 12, 12), "Nữ", "0989000001", "Bình Dương", "DANG_O");

            // Thêm thành viên thứ 2 nhưng CŨNG ĐẶT LÀM CHỦ HỘ cho cùng hợp đồng HD001 (Phải báo lỗi)
            try
            {
                _contractService.tblqldt_member_insert("TV002", "HD001", "Trần Thu Bình", true, // Cố tình đặt là chủ hộ thứ 2
                   "012345678902", new DateTime(2001, 2, 2), "Nữ", "0912345678", "Hà Nội", "DANG_O");
                Assert.Fail("Phải báo lỗi hợp đồng đã có chủ hộ.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Hợp đồng đã có chủ hộ", ex.Message);
            }
        }

        [TestMethod]
        public void tblqldt_member_insert_birth_date_in_future_throws_error()
        {
            /*
               Quy định:
                   1. Ngày sinh lớn hơn ngày hiện tại (ở tương lai).
                   2. Không cho phép thêm thành viên.
                   3. Báo lỗi: "Ngày sinh không được lớn hơn ngày hiện tại."
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert phòng và hợp đồng nền
            _roomService.tblqldt_room_insert("P009", "Phòng 9", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD009", "P009", 500000M, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Thêm thành viên với NGÀY SINH Ở TƯƠNG LAI (Phải báo lỗi)
            try
            {
                _contractService.tblqldt_member_insert(
                    "TV003",
                    "HD009",
                    "Nguyễn Hoàng Tương Lai",
                    true,
                    "012345678909",
                    DateTime.Now.AddDays(1), // Ngày sinh ở tương lai 
                    "Nam",
                    "0989000999",
                    "Hà Nội",
                    "DANG_O"
                );

                Assert.Fail("Phải báo lỗi ngày sinh không được lớn hơn ngày hiện tại.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Ngày sinh không được lớn hơn ngày hiện tại.", ex.Message);
            }
        }

        [TestMethod]
        public void tblqldt_member_insert_primary_tenant_under_16_years_old()
        {
            /*
                Quy định:
                    1. Chủ hộ phải từ đủ 16 tuổi.
                    2. Thêm thành viên làm chủ hộ.
                    3. Thành viên chưa đủ 16 tuổi.

                    Kết quả:
                    Báo lỗi "Chủ hộ phải từ đủ 16 tuổi."
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Tạo phòng
            _roomService.tblqldt_room_insert("P008", "Phòng 8", "TRONG", 20.5M, 5);

            // Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD008", "P008", 800000M, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // 3. Thêm chủ hộ chưa đủ 16 tuổi
            try
            {
                _contractService.tblqldt_member_insert(
                    "TV008",
                    "HD008",
                    "Nguyễn Đức An",
                    true,                       // Là chủ hộ
                    "012345678909",
                    DateTime.Today.AddYears(-15), // Mới 15 tuổi
                    "Nam",
                    "0989000009",
                    "Bình Dương",
                    "DANG_O");

                Assert.Fail("Phải báo lỗi chủ hộ chưa đủ 16 tuổi.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Chủ hộ phải từ đủ 16 tuổi.", ex.Message);
            }
        }

        [TestMethod]
        public void tblqldt_member_insert_exceed_room_max_quantity_5_persons_throws_error()
        {
            /*
                Quy định:
                    1. Kiểm tra số lượng người ở tối đa của phòng (@max_qty_man = 5).
                    2. Thêm đủ 5 thành viên.
                    3. Thêm thành viên thứ 6 vượt quá giới hạn tối đa.

                    Kết quả mong đợi:
                    Báo lỗi "Phòng đã đủ số người tối đa."
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Tạo phòng với giới hạn tối đa là 5 người
            _roomService.tblqldt_room_insert("P005", "Phòng 5 Người", "TRONG", 30.0M, 5); // max_qty_man = 5

            // Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD005", "P005", 1000000M, 2000000M, 4000M, 10000M, 15000M, 15000M);

            // Thêm lần lượt 5 thành viên (chạm mốc tối đa)
            for (int i = 1; i <= 5; i++)
            {
                _contractService.tblqldt_member_insert(
                    $"TV00{i}",
                    "HD005",
                    $"Thành Viên {i}",
                    i == 1, // Người đầu tiên là chủ hộ
                    $"01234567890{i}",
                    new DateTime(1990 + i, 1, 1),
                    "Nam",
                    $"090900000{i}",
                    "Hà Nội",
                    "DANG_O");
            }

            // Cố gắng thêm thành viên thứ 6 và kiểm tra bắt lỗi
            try
            {
                _contractService.tblqldt_member_insert(
                    "TV006",
                    "HD005",
                    "Thành Viên Thứ Sáu",
                    false,
                    "012345678906",
                    new DateTime(1998, 6, 6),
                    "Nam",
                    "0909000006",
                    "Đà Nẵng",
                    "DANG_O");

                Assert.Fail("Phải báo lỗi phòng đã đủ số người tối đa.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Phòng đã đủ số người tối đa.", ex.Message);
            }
        }

        [TestMethod]
        public void tblqldt_member_insert_should_set_status_DANG_O_when_contract_has_deposit()
        {
            /*
               Quy định:
                   1. Hợp đồng có đặt cọc (deposit > 0).
                   2. Thêm thành viên thành công.
                   3. Trạng thái thành viên là DANG_O.
            */

            // 1. Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert phòng
            _roomService.tblqldt_room_insert("P007", "Phòng 7", "TRONG", 20.50M, 5);

            // Insert hợp đồng có cọc (deposit = 500000)
            _contractService.tblqldt_contract_insert("HD007", "P007", 500000M, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Thêm thành viên
            DataTable dtInsert = _contractService.tblqldt_member_insert("M001", "HD007", "Nguyễn Văn A", true, "012345678901",
                new DateTime(2000, 1, 1), "Nam", "0987654321", "Hà Nội", "DANG_O");

            // Kiểm tra kết quả trả về và trạng thái trong DB
            DataTable dtMember = _contractService.tblqldt_member_get_by_id("M001");

            Assert.AreEqual(1, dtMember.Rows.Count);
            Assert.AreEqual("M001", dtMember.Rows[0]["member_nbr"].ToString());
            Assert.AreEqual("HD007", dtMember.Rows[0]["contract_nbr"].ToString());
            Assert.AreEqual("DANG_O", dtMember.Rows[0]["status"].ToString());
        }

        [TestMethod]
        public void tblqldt_member_insert_should_set_status_CHO_NHAN_PHONG_when_contract_not_deposit()
        {
            /*
               Quy định:
                   1. Hợp đồng KHÔNG cọc (deposit = NULL hoặc 0).
                   2. Thêm thành viên thành công.
                   3. Trạng thái thành viên là CHO_NHAN_PHONG.
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert phòng
            _roomService.tblqldt_room_insert("P008", "Phòng 8", "TRONG", 20.50M, 5);

            // Insert hợp đồng KHÔNG CỌC (deposit = null)
            _contractService.tblqldt_contract_insert("HD008", "P008", null, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Thêm thành viên
            DataTable dtInsert = _contractService.tblqldt_member_insert("M002", "HD008", "Trần Thị B", true, "012345678902",
                new DateTime(2001, 2, 2), "Nữ", "0912345678", "Hà Nội", "CHO_NHAN_PHONG");

            // Kiểm tra kết quả trả về và trạng thái trong DB
            DataTable dtMember = _contractService.tblqldt_member_get_by_id("M002");

            Assert.AreEqual(1, dtMember.Rows.Count);
            Assert.AreEqual("M002", dtMember.Rows[0]["member_nbr"].ToString());
            Assert.AreEqual("HD008", dtMember.Rows[0]["contract_nbr"].ToString());
            Assert.AreEqual("CHO_NHAN_PHONG", dtMember.Rows[0]["status"].ToString());
        }

        #endregion

        #region tblqldt_member_update
        [TestMethod]
        public void tblqldt_member_update_member_chk_data_user_input()
        {
            /*
             Quy định
                member_nbr
                    1. Bắt buộc nhập
                    2. Tối thiểu 1 ký tự
                    3. Tối đa 10 ký tự

                contract_nbr
                    1. Bắt buộc nhập
                    2. Tối thiểu 1 ký tự
                    3. Tối đa 10 ký tự

                full_name
                    1. Bắt buộc nhập
                    2. Tối thiểu 1 ký tự
                    3. Tối đa 200 ký tự

                is_primary_tenant
                    1. Bắt buộc nhập

                national_id
                    1. Bắt buộc nhập
                    2. Đúng 12 số

                phone
                    1. Đúng 10 số (nếu có nhập)

                birth_date
                    1. Bắt buộc nhập
                    2. Nhỏ hơn ngày hiện tại

                gender
                    1. Bắt buộc nhập

                address
                    1. Bắt buộc nhập
                    2. Tối đa 500 ký tự

                status
                    1. Bắt buộc nhập
                    2. Tối đa 30 ký tự
            */

            // Khởi tạo dữ liệu hợp lệ

            var vm = new tblqldt_member_update_vm();
            vm.member_nbr = "TV001";
            vm.contract_nbr = "HD001";
            vm.full_name = "Nguyễn Minh Khang";
            vm.is_primary_tenant = true;
            vm.national_id = "098900000011";
            vm.phone = "0123456780";
            vm.birth_date = new DateTime(2000, 12, 12);
            vm.gender = "Nam";
            vm.address = "Bình Dương";
            vm.status = "DANG_O"; //"DANG_CHO_PHONG"

            // member_nbr

            try
            {
                vm.member_nbr = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập mã thành viên", ex.Message.Trim());
            }

            try
            {
                vm.member_nbr = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập mã thành viên", ex.Message.Trim());
            }

            try
            {
                vm.member_nbr = "TV0123456789";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã thành viên tối đa 10 ký tự", ex.Message.Trim());
            }

            // contract_nbr
            vm.member_nbr = "TV001";

            try
            {
                vm.contract_nbr = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng chọn hợp đồng", ex.Message.Trim());
            }

            try
            {
                vm.contract_nbr = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng chọn hợp đồng", ex.Message.Trim());
            }

            try
            {
                vm.contract_nbr = "HD0123456789";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã hợp đồng tối đa 10 ký tự", ex.Message.Trim());
            }

            // full_name
            vm.contract_nbr = "HD001";

            try
            {
                vm.full_name = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập họ tên", ex.Message.Trim());
            }

            try
            {
                vm.full_name = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập họ tên", ex.Message.Trim());
            }

            try
            {
                vm.full_name = new string('A', 201);
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Họ tên tối đa 200 ký tự", ex.Message.Trim());
            }


            // national_id
            vm.full_name = "Nguyễn Minh Khang";

            try
            {
                vm.national_id = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập CCCD", ex.Message.Trim());
            }

            try
            {
                vm.national_id = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập CCCD", ex.Message.Trim());
            }

            try
            {
                vm.national_id = "09890000001";//11 số
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("CCCD phải gồm đúng 12 số", ex.Message.Trim());
            }

            try
            {
                vm.national_id = "0989000000111"; //13 số
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("CCCD phải gồm đúng 12 số", ex.Message.Trim());
            }

            // phone
            vm.national_id = "012345678901";

            try
            {
                vm.phone = "012345678"; //9 số
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Số điện thoại phải gồm 10 số", ex.Message.Trim());
            }

            try
            {
                vm.phone = "01234567890"; //11 số
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Số điện thoại phải gồm 10 số", ex.Message.Trim());
            }

            vm.phone = "0989000011";

            // birth_date
            try
            {
                vm.birth_date = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập ngày sinh", ex.Message.Trim());
            }
            try
            {
                vm.birth_date = DateTime.Today.AddDays(1);
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Ngày sinh phải nhỏ hơn ngày hiện tại", ex.Message.Trim());
            }

            // gender
            vm.birth_date = new DateTime(2000, 12, 12);
            try
            {
                vm.gender = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng chọn giới tính", ex.Message.Trim());
            }
            try
            {
                vm.gender = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng chọn giới tính", ex.Message.Trim());
            }

            // address
            vm.gender = "Nam";
            try
            {
                vm.address = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập địa chỉ", ex.Message.Trim());
            }
            try
            {
                vm.address = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập địa chỉ", ex.Message.Trim());
            }

            try
            {
                vm.address = new string('A', 501);
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Địa chỉ tối đa 500 ký tự", ex.Message.Trim());
            }

            // status
            //vm.address = "Bình Dương";

            //try
            //{
            //    vm.status = "";
            //    _validationService.Validate(vm);
            //}
            //catch (Exception ex)
            //{
            //    Assert.AreEqual("Vui lòng chọn trạng thái", ex.Message.Trim());
            //}

            //try
            //{
            //    vm.status = null;
            //    _validationService.Validate(vm);
            //}
            //catch (Exception ex)
            //{
            //    Assert.AreEqual("Vui lòng chọn trạng thái", ex.Message.Trim());
            //}
            //try
            //{
            //    vm.status = "AAAAA";
            //    _validationService.Validate(vm);
            //}
            //catch (Exception ex)
            //{
            //    Assert.AreEqual("Trạng thái thành viên phải là DANG_O hoặc CHUYEN_DI", ex.Message.Trim());
            //}

            //Tất cả dữ liệu hợp lệ
            //vm.status = "DANG_O";

            
            vm.address = "Bình Dương";
            try
            {
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Test sau cùng, có 1 lỗi không xác định cụ thể:" + ex.Message, ex.Message);
            }

        }

        [TestMethod]
        public void tblqldt_member_update_member_nbr_not_exists()
        {
            /*
                Quy định:
                    1. Tồn tại phòng P001.
                    2. Tồn tại hợp đồng HD001 thuộc phòng P001.
                    3. Không tồn tại thành viên có mã TV999.

                    Kiểm tra:
                    Cập nhật thông tin thành viên với mã TV999.

                    Kết quả mong đợi:
                    Hệ thống báo lỗi: "Mã thành viên không tồn tại để cập nhật".
            */

            // 1. Xóa sạch dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                        DELETE FROM tblqldt_member;
                        DELETE FROM tblqldt_contract;
                        DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);

            // 2. Tạo Phòng trước
            _roomService.tblqldt_room_insert(
                "P001",
                "Phòng 01",
                "TRONG",
                25.0M,
                4);

            // 3. Tạo Hợp đồng gắn với Phòng
            _contractService.tblqldt_contract_insert(
                "HD001",
                "P001",
                500000M,
                1000000M,
                4000M,
                10000M,
                15000M,
                15000M);

            // 4. (BỎ QUA BƯỚC INSERT THÀNH VIÊN ĐỂ GIẢ LẬP KHÔNG TỒN TẠI)

            // 5. Tiến hành gọi hàm Update với mã "TV999" không có thật trong DB
            try
            {
                _contractService.tblqldt_member_update(
                    "TV999",                    // Mã thành viên KHÔNG TỒN TẠI
                    "HD001",
                    "Nguyễn Văn Mới",
                    true,
                    "012345678901",
                    new DateTime(2000, 1, 1),
                    "Nam",
                    "0909999999",
                    "Hồ Chí Minh",
                    "DANG_O");

                // Nếu lệnh trên chạy mà không bắn lỗi -> Test Fail
                Assert.Fail("Phải báo lỗi mã thành viên không tồn tại.");
            }
            catch (Exception ex)
            {
                // Bắt được lỗi đúng như kỳ vọng
                Assert.AreEqual("Mã thành viên không tồn tại để cập nhật", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_update_contract_is_end_date()
        {
            /*
  
                Quy định:
                    1. Tồn tại phòng P001.
                    2. Tồn tại hợp đồng HD001 thuộc phòng P001.
                    3. Hợp đồng HD001 đã được trả phòng (phiếu thu có is_checkout = 1, hệ thống đã cập nhật ngày kết thúc hợp đồng).
                    4. Tồn tại thành viên TV001 thuộc hợp đồng HD001.

                    Kiểm tra: Cập nhật thông tin thành viên TV001.
                    Kết quả mong đợi: Hệ thống báo lỗi: "Hợp đồng đã trả phòng không thể cập nhật thành viên".
            */

            //Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            //Insert sẵn phòng P001 và hợp đồng HD001 vào db
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            // 4. Insert thành viên thuộc hợp đồng này
            _contractService.tblqldt_member_insert(
                "TV001",
                "HD001",
                "Nguyễn Đức Hùng",
                true,
                "012345678901",
                new DateTime(2000, 1, 1),
                "Nam",
                "0909000001",
                "Hà Nội",
                "DANG_O");

            //Insert phiếu thu với is_checkout = 1
            SqlCommand cmdInvoice = new SqlCommand();
            cmdInvoice.CommandType = CommandType.Text;
            cmdInvoice.CommandText = @"INSERT INTO tblqldt_invoice 
        (
            contract_nbr, billing_month, billing_year, room_price, 
            old_electric_reading, new_electric_reading, electric_price, electric_amount, 
            old_water_reading, new_water_reading, water_price, water_amount, 
            trash_fee, security_fee, 
            total_amount, 
            payment_status, collection_date, is_checkout
        ) 
        VALUES 
        (
            'HD001', 7, 2026, 1000000, 
            0, 0, 4000, 0, 
            0, 0, 10000, 0, 
            15000, 15000, 
            1000000,
            'DA_THU', '2026-07-21', 1
        );";
            _db.exec_sql_cmd(cmdInvoice);

            SqlCommand cmdUpdateContract = new SqlCommand();
            cmdUpdateContract.CommandType = CommandType.Text;
            cmdUpdateContract.CommandText = @"UPDATE tblqldt_contract 
                                            SET end_date = GETDATE()
                                            WHERE contract_nbr = 'HD001'";
            _db.exec_sql_cmd(cmdUpdateContract);

            // 5. Tiến hành gọi hàm Update và kiểm tra ngoại lệ
            try
            {
                _contractService.tblqldt_member_update(
                    "TV001",
                    "HD001",
                    "Nguyễn Đức Đông Hùng", //dòng cập nhật
                    true,
                    "012345678901",
                    new DateTime(2000, 1, 1),
                    "Nam",
                    "0909999999",
                    "Hồ Chí Minh",
                    "DANG_O");

                Assert.Fail("Phải báo lỗi hợp đồng đã trả phòng, không thể cập nhật.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Hợp đồng đã trả phòng không thể cập nhật thành viên", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_update_national_id_already_exists()
        {
            /*
                Quy định:
                    1. Không cho phép hai thành viên có cùng CCCD.
                    2. Khi cập nhật thành viên, nếu CCCD đã tồn tại ở thành viên khác thì không được cập nhật.
                    3. Hiển thị thông báo: "CCCD đã tồn tại."
            */

            // 1. Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 2. Insert phòng và hợp đồng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Insert Thành viên thứ nhất (TV001) với CCCD số 1
            _contractService.tblqldt_member_insert(
                "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "DANG_O");

            // 4. Insert Thành viên thứ hai (TV002) với CCCD số 2
            _contractService.tblqldt_member_insert(
                "TV002", "HD001", "Trần Văn B", false, "098765432109",
                new DateTime(2001, 1, 1), "Nam", "0909000002", "Hà Nội", "DANG_O");

            // 5. Cập nhật thành viên TV001 nhưng đổi CCCD trùng với TV002 (098765432109)
            try
            {
                _contractService.tblqldt_member_update(
                    "TV001",
                    "HD001",
                    "Nguyễn Văn A Sửa",
                    true,
                    "098765432109", // <-- Trùng CCCD với TV002
                    new DateTime(2000, 1, 1),
                    "Nam",
                    "0909999999",
                    "Hồ Chí Minh",
                    "DANG_O");

                Assert.Fail("Phải báo lỗi CCCD đã tồn tại.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("CCCD đã tồn tại.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_update_birth_date_invalid()
        {
            /*
            Quy định :
                1. Ngày sinh của thành viên bắt buộc phải nhỏ hơn ngày hiện tại(không được bằng hoặc lớn hơn ngày hôm nay).
                2. Điều kiện chặn: @birth_date >= CAST(GETDATE() AS DATE).
                3. Thông báo lỗi yêu cầu: "Ngày sinh phải nhỏ hơn ngày hiện tại."
            */

            // 1. Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 2. Insert phòng và hợp đồng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Insert thành viên hợp lệ ban đầu
            _contractService.tblqldt_member_insert(
                "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "DANG_O");

            // 4. Gọi hàm Update với ngày sinh không hợp lệ (Ngày hiện tại hoặc tương lai)
            try
            {
                _contractService.tblqldt_member_update(
                    "TV001",
                    "HD001",
                    "Nguyễn Văn A",
                    true,
                    "012345678901",
                    DateTime.Today, // <-- Ngày sinh bằng ngày hiện tại (vi phạm điều kiện >= GETDATE())
                    "Nam",
                    "0909000001",
                    "Hà Nội",
                    "DANG_O");

                Assert.Fail("Phải báo lỗi ngày sinh phải nhỏ hơn ngày hiện tại.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Ngày sinh phải nhỏ hơn ngày hiện tại.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_update_primary_tenant_under_16()
        {
            /*
                Quy định :
                   1. Nếu thành viên là chủ hộ (@is_primary_tenant = 1), thành viên đó bắt buộc phải từ đủ 16 tuổi trở lên tính đến thời điểm hiện tại.
                   2. Điều kiện chặn: @is_primary_tenant = 1 AND DATEADD(YEAR, 16, @birth_date) > CAST(GETDATE() AS DATE).
                   3. Thông báo lỗi yêu cầu: "Chủ hộ phải từ đủ 16 tuổi."
            */

            // 1. Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 2. Insert phòng và hợp đồng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Insert thành viên ban đầu hợp lệ
            _contractService.tblqldt_member_insert(
                "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "DANG_O");

            // 4. Gọi hàm Update với vai trò là Chủ hộ (is_primary_tenant = true) nhưng ngày sinh chưa đủ 16 tuổi (ví dụ sinh năm nay trừ đi 5 năm)
            try
            {
                _contractService.tblqldt_member_update(
                    "TV001",
                    "HD001",
                    "Nguyễn Văn A",
                    true, // <-- Là chủ hộ
                    "012345678901",
                    DateTime.Today.AddYears(-5), // <-- Mới 5 tuổi, chưa đủ 16 tuổi
                    "Nam",
                    "0909000001",
                    "Hà Nội",
                    "DANG_O");

                Assert.Fail("Phải báo lỗi chủ hộ phải từ đủ 16 tuổi.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Chủ hộ phải từ đủ 16 tuổi.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_update_cannot_remove_primary_tenant()
        {
            /*
             Quy định:
                 1. Không cho phép bỏ quyền chủ hộ của thành viên đang sinh sống nếu chưa chỉ định thành viên khác thay thế.
                 2. Điều kiện chặn: @old_primary_tenant = 1 AND @is_primary_tenant = 0 AND @status <> 'CHUYEN_DI'.
                 3. Thông báo lỗi yêu cầu: "Vui lòng chọn thành viên khác làm chủ hộ trước khi bỏ quyền chủ hộ."
             */

            // 1. Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 2. Insert phòng và hợp đồng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Insert thành viên ban đầu LÀ CHỦ HỘ (is_primary_tenant = true)
            _contractService.tblqldt_member_insert(
                "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "DANG_O");

            // 4. Gọi hàm Update: Bỏ tích chủ hộ (is_primary_tenant = false) nhưng status vẫn là DANG_O
            try
            {
                _contractService.tblqldt_member_update(
                    "TV001",
                    "HD001",
                    "Nguyễn Văn A",
                    false, // <-- Bỏ quyền chủ hộ
                    "012345678901",
                    new DateTime(2000, 1, 1),
                    "Nam",
                    "0909000001",
                    "Hà Nội",
                    "DANG_O"); // <-- Trạng thái khác 'CHUYEN_DI'

                Assert.Fail("Phải báo lỗi vui lòng chọn thành viên khác làm chủ hộ trước khi bỏ quyền chủ hộ.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng chọn thành viên khác làm chủ hộ trước khi bỏ quyền chủ hộ.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_update_already_moved_out()
        {
            /*
             Quy định
             - Thành viên đã ở trạng thái 'CHUYEN_DI' thì dữ liệu xem như đã đóng
             - Không cập nhật thông tin
             - Thông báo lỗi yêu cầu: "Thành viên đã chuyển đi không được cập nhật"

            */
            // 1. Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // 2. Insert phòng và hợp đồng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Insert thành viên ban đầu (trạng thái DANG_O)
            _contractService.tblqldt_member_insert(
                "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "DANG_O");

            // 4. Ép trạng thái thành viên thành 'CHUYEN_DI' trực tiếp trong DB
            SqlCommand cmdUpdateContract = new SqlCommand();
            cmdUpdateContract.CommandType = CommandType.Text;
            cmdUpdateContract.CommandText = @"UPDATE tblqldt_member
                                      SET status = 'CHUYEN_DI'
                                      WHERE member_nbr = 'TV001'";
            _db.exec_sql_cmd(cmdUpdateContract);

            // 5. Gọi hàm Update và kiểm tra ngoại lệ
            try
            {
                _contractService.tblqldt_member_update(
                    "TV001", "HD001", "Nguyễn BBBBBB", true, "012345678901",
                    new DateTime(2000, 1, 1), "Nam", "0909999999", "Hồ Chí Minh", "CHUYEN_DI");

                Assert.Fail("Phải báo lỗi Thành viên đã chuyển đi không được cập nhật");
            }
            catch (Exception ex)
            {
                // Khớp chính xác thông báo lỗi từ RAISERROR trong SQL
                Assert.AreEqual("Thành viên đã chuyển đi không được cập nhật", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_update_pending_to_moved_out()
        {
            /*
                Quy định:
                    1. Không cho phép cập nhật trạng thái của thành viên từ CHO_NHAN_PHONG sang CHUYEN_DI.
                    2. Thành viên phải nhận phòng trước khi thực hiện chuyển đi.
                    3. Hiển thị thông báo: "Thành viên chờ nhận phòng không thể chuyển đi".
            */

            // 1. Xóa dữ liệu cũ 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 2. Insert phòng và hợp đồng (tiền cọc = 0)
            _roomService.tblqldt_room_insert("P008", "Phòng 8", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD008", "P008", null, 1000000, 4000, 10000, 15000, 15000);

            // 3. Insert thành viên ở trạng thái chờ nhận phòng
            _contractService.tblqldt_member_insert(
                "TV008", "HD008", "Nguyễn Văn A", true, "012345678901",
                new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "CHO_NHAN_PHONG");

            try
            {
                _contractService.tblqldt_member_update(
                    "TV008", "HD008", "Nguyễn Văn A", true, "012345678901",
                    new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "CHUYEN_DI"); 

                Assert.Fail("Phải báo lỗi thành viên chờ nhận phòng không thể chuyển đi");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Thành viên chờ nhận phòng không thể chuyển đi", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_update_pending_to_living_no_deposit()
        {
            /*
             QUY ĐỊNH :
                1. Thành viên đang ở trạng thái chờ nhận phòng ('CHO_NHAN_PHONG') khi muốn chuyển sang trạng thái đang ở ('DANG_O') thì hợp đồng bắt buộc phải đã đóng tiền cọc (tiền cọc > 0).
                2. Nếu tiền cọc bằng 0 hoặc null, hệ thống từ chối cập nhật trạng thái.
                3. Thông báo lỗi yêu cầu: "Hợp đồng chưa nhận cọc, không thể chuyển sang trạng thái đang ở."
             */

            // 1. Xóa dữ liệu cũ 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 2. Insert phòng và hợp đồng với tiền cọc = 0
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", null, 1000000, 4000, 10000, 15000, 15000);

            // 3. Insert thành viên ban đầu ở trạng thái chờ nhận phòng
            _contractService.tblqldt_member_insert(
                "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "CHO_NHAN_PHONG");

            // 4. Gọi hàm Update chuyển sang trạng thái đang ở ('DANG_O') khi chưa đóng cọc 
            try
            {
                _contractService.tblqldt_member_update(
                    "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                    new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "DANG_O");

                Assert.Fail("Phải báo lỗi hợp đồng chưa nhận cọc, không thể chuyển sang trạng thái đang ở.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Hợp đồng chưa nhận cọc, không thể chuyển sang trạng thái đang ở.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_update_move_out_when_replacement_primary_tenant_exists_success()
        {
            /*
                Quy định:
                    1. Có hợp đồng.
                    2. Hợp đồng có 2 thành viên đang ở.
                    3. Thành viên TV001 là chủ hộ.
                    4. Thành viên TV002 là thành viên thường.
                    5. Chuyển quyền chủ hộ từ TV001 sang TV002.
                    6. TV001 chuyển đi.

                Kết quả:
                    - Chuyển quyền chủ hộ thành công.
                    - TV001 có trạng thái CHUYEN_DI.
                    - TV001 không còn là chủ hộ.
                    - TV002 là chủ hộ.
            */

            // 1. Xóa dữ liệu cũ 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);
            // 1. Tạo phòng
            _roomService.tblqldt_room_insert(
                "P001", "Phòng 1", "TRONG", 20, 4);

            // 2. Tạo hợp đồng
            _contractService.tblqldt_contract_insert(
                "HD001", "P001", 1000000,
                1000000, 3500, 15000,
                20000, 10000);

            // 3. Thêm chủ hộ
            _contractService.tblqldt_member_insert(
                "TV001", "HD001", "Nguyễn Văn A",
                true,
                "012345678901",
                new DateTime(1995, 1, 1),
                "Nam",
                "0909000001",
                "Hà Nội",
                "DANG_O");

            // 4. Thêm thành viên
            _contractService.tblqldt_member_insert(
                "TV002", "HD001", "Nguyễn Văn B",
                false,
                "012345678902",
                new DateTime(1998, 1, 1),
                "Nam",
                "0909000002",
                "Hà Nội",
                "DANG_O");

            // 5. Chuyển quyền chủ hộ sang TV002
            _contractService.tblqldt_member_update(
                "TV002", "HD001", "Nguyễn Văn B",
                true,
                "012345678902",
                new DateTime(1998, 1, 1),
                "Nam",
                "0909000002",
                "Hà Nội",
                "DANG_O");

            // 6. Chủ hộ cũ chuyển đi
            DataTable dt = _contractService.tblqldt_member_update(
                "TV001", "HD001", "Nguyễn Văn A",
                true,
                "012345678901",
                new DateTime(1995, 1, 1),
                "Nam",
                "0909000001",
                "Hà Nội",
                "CHUYEN_DI");

            // 7. Kiểm tra kết quả
            Assert.AreEqual(1, Convert.ToInt32(dt.Rows[0]["status"]));
            Assert.AreEqual("Cập nhật thành công.", dt.Rows[0]["msg"].ToString());

        }

        [TestMethod]
        public void tblqldt_member_update_last_living_member_to_moved_out()
        {
            /*
                Quy định :
                   1. Hợp đồng bắt buộc phải duy trì ít nhất một thành viên đang ở ('DANG_O').
                   2. Không cho phép thành viên cuối cùng duy nhất trong hợp đồng chuyển trạng thái thành chuyển đi ('CHUYEN_DI').
                   3. Thông báo lỗi yêu cầu: "Hợp đồng phải còn ít nhất một thành viên đang ở."
            */

            // 1. Xóa dữ liệu cũ 
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 2. Insert phòng và hợp đồng 
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Chỉ có duy nhất 1 thành viên TV001 trong hợp đồng ở trạng thái đang ở ('DANG_O')
            _contractService.tblqldt_member_insert(
                "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "DANG_O");

            // 4. Gọi hàm Update cho người cuối cùng chuyển đi và kiểm tra ngoại lệ
            try
            {
                _contractService.tblqldt_member_update(
                    "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                    new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "CHUYEN_DI"); // Vi phạm vì là người cuối cùng

                Assert.Fail("Phải báo lỗi hợp đồng phải còn ít nhất một thành viên đang ở.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Hợp đồng phải còn ít nhất một thành viên đang ở.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_update_pending_to_living_with_deposit_success()
        {
            /*
             Quy định :
                1. Ban đầu hợp đồng được tạo với tiền cọc = null
                2. Cập nhật tiền cọc
                3. Thành viên ở trạng thái 'CHO_NHAN_PHONG' update chuyển sang 'DANG_O' thành công
             */

            // 1. Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 2. Insert phòng và hợp đồng ban đầu (tiền cọc = 0)
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", null, 1000000, 4000, 10000, 15000, 15000);

            // 3. Insert thành viên ở trạng thái chờ nhận phòng
            _contractService.tblqldt_member_insert(
                "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "CHO_NHAN_PHONG");

            // 4. Giả lập hành động bổ sung/cập nhật tiền cọc cho hợp đồng lên > 0 trực tiếp trong DB
            SqlCommand cmdUpdateDeposit = new SqlCommand();
            cmdUpdateDeposit.CommandType = CommandType.Text;
            cmdUpdateDeposit.CommandText = @"UPDATE tblqldt_contract 
                                     SET deposit = 500000 
                                     WHERE contract_nbr = 'HD001'";
            _db.exec_sql_cmd(cmdUpdateDeposit);

            // 5. Gọi hàm Update thành viên sang 'DANG_O' thành công vì hợp đồng lúc này đã có cọc
            try
            {
                _contractService.tblqldt_member_update(
                    "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                    new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "DANG_O");
            }
            catch (Exception ex)
            {
                Assert.Fail("Lẽ ra phải chuyển sang đang ở thành công sau khi đã có cọc nhưng lại báo lỗi: " + ex.Message);
            }
        }

        [TestMethod]
        public void tblqldt_member_update_living_to_moved_out_not_primary_success()
        {
            /*
                Quy định :
                  1. Thành viên đang ở trạng thái 'DANG_O' có thể chuyển sang 'CHUYEN_DI' thành công.
                  2. Trường hợp thành viên này KHÔNG phải là chủ hộ (is_primary_tenant = false).
                  3. Hợp đồng vẫn đang trong thời hạn hiệu lực (chưa trả phòng).
                  4. Miễn là trong hợp đồng vẫn còn ít nhất một thành viên khác đang ở thì việc chuyển đi sẽ thành công

             */

            // 1. Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 2. Insert phòng và hợp đồng đang có hiệu lực (end_date là NULL hoặc chưa tới hạn)
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Insert 2 thành viên đang ở: 
            // - TV001 là chủ hộ (true)
            // - TV002 KHÔNG phải là chủ hộ (false) -> TV cần test chuyển đi
            _contractService.tblqldt_member_insert(
                "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "DANG_O");

            _contractService.tblqldt_member_insert(
                "TV002", "HD001", "Nguyễn Văn B", false, "012345678902",
                new DateTime(2001, 1, 1), "Nam", "0909000002", "Hà Nội", "DANG_O");

            // 4. Gọi hàm Update cho TV002 (không phải chủ hộ) chuyển sang 'CHUYEN_DI' thành công
            try
            {
                _contractService.tblqldt_member_update(
                    "TV002", "HD001", "Nguyễn Văn B", false, "012345678902",
                    new DateTime(2001, 1, 1), "Nam", "0909000002", "Hà Nội", "CHUYEN_DI");
            }
            catch (Exception ex)
            {
                Assert.Fail("Lẽ ra thành viên không phải chủ hộ phải chuyển đi thành công nhưng lại báo lỗi: " + ex.Message);
            }
        }

        #endregion

        #region tblqldt_member_delete
        [TestMethod]
        public void tblqldt_member_delete_not_found_data_fail()
        {
            /*            
                Quy định:
                    Nếu mã thành viên không tồn tại -> báo lỗi
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                                delete from tblqldt_member;
                                delete from tblqldt_contract;
                                delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert dữ liệu
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 0, 1000000M, 4000M, 10000M, 15000M, 15000M);
            _contractService.tblqldt_member_insert(
                "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "DANG_O"
            );

            try
            {
                _contractService.tblqldt_member_delete("TV_KHONG_TON_TAI"); // truyền mã thành viên không tồn tại trong db
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã thành viên không tồn tại", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_delete_contract_ended_fail()
        {
            /*            
                Quy định:
                    Nếu hợp đồng đã kết thúc (đã trả phòng) -> không thể xóa thành viên -> báo lỗi
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert phòng và hợp đồng với deposit = 0 để đi qua được điều kiện cọc
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            // Thêm thành viên với trạng thái "CHUYEN_DI" khi đã trả phòng
            _contractService.tblqldt_member_insert(
              "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
              new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "DANG_O"
           );

            // Insert phiếu thu trả phòng
            SqlCommand cmdInvoice = new SqlCommand();
            cmdInvoice.CommandType = CommandType.Text;
            cmdInvoice.CommandText = @"
            INSERT INTO tblqldt_invoice
            (
                contract_nbr, billing_month, billing_year, room_price, old_electric_reading, new_electric_reading,
                electric_price, electric_amount, old_water_reading, new_water_reading, water_price, water_amount,
                trash_fee, security_fee, total_amount, payment_status, collection_date, is_checkout
            )
            VALUES
            ('HD001', 7, 2026, 1000000, 0, 0, 4000, 0, 0, 0, 10000, 0, 15000, 15000, 1030000, 'DA_THU', GETDATE(), 1);";

            _db.exec_sql_cmd(cmdInvoice);

            try
            {
                _contractService.tblqldt_member_delete("TV001");
                Assert.Fail("Lẽ ra phải báo lỗi hợp đồng đã trả phòng.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Hợp đồng đã trả phòng không thể xóa thành viên.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_delete_contract_has_deposit_fail()
        {
            /*
                Quy định:
                    1. Hợp đồng đã thu tiền cọc (deposit > 0).
                    2. Hợp đồng đang có hiệu lực (chưa trả phòng).
                    3. Không được phép xóa thành viên.
                    4. Thông báo lỗi:
                       "Hợp đồng đã thu tiền cọc, không thể xóa thành viên."
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert phòng 
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            // Insert hợp đồng có cọc
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000M, 1000000M, 4000M, 10000M, 15000M, 15000M);

            // Insert thành viên
            _contractService.tblqldt_member_insert("TV001", "HD001", "Nguyễn Văn A", true, "012345678901", new DateTime(2000, 1, 1),
                "Nam", "0909000001", "Hà Nội", "DANG_O");
         
            try
            {
                _contractService.tblqldt_member_delete("TV001");
                Assert.Fail("Lẽ ra phải báo lỗi.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Hợp đồng đã thu tiền cọc, không thể xóa thành viên.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_member_delete_success()
        {
            /*
                 Quy định:
                     1. Hợp đồng chưa thu tiền cọc (deposit = NULL hoặc = 0).
                     2. Thành viên đang ở trạng thái CHO_NHAN_PHONG.
                     3. Cho phép xóa thành viên.
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert phòng và hợp đồng với deposit = 0 (chưa cọc)
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _contractService.tblqldt_contract_insert("HD001", "P001", null, 1000000, 4000, 10000, 15000, 15000);

            // Insert thành viên thuộc hợp đồng chưa cọc
            _contractService.tblqldt_member_insert(
                "TV001", "HD001", "Nguyễn Văn A", true, "012345678901",
                new DateTime(2000, 1, 1), "Nam", "0909000001", "Hà Nội", "CHO_NHAN_PHONG"
            );
            
            // Thực hiện xóa
            _contractService.tblqldt_member_delete("TV001");

            // Lấy kết quả trên UI, kiểm tra xem có trả về 0 dòng
            dt = _contractService.tblqldt_member_get_all();
            Assert.AreEqual(dt.Rows.Count, 0);
        }
        #endregion

        #endregion
    }
}








