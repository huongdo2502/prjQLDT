
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
    public class RoomServiceTest
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
        public RoomServiceTest()
        {
            _db = new DBHelper();

            _roomService = new RoomService();
            _contractService = new ContractService();
            _validationService = new ValidationService();
        }
        #endregion

        #region tblqldt_room

        #region tblqldt_room_get_all
        [TestMethod]
        public void tblqldt_room_get_all_with_data()
        {
            SqlCommand cmd = new SqlCommand();

            // Xóa dữ liệu
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                                delete from tblqldt_member;
                                delete from tblqldt_contract;
                                delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Insert dữ liệu phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);
            _roomService.tblqldt_room_insert("P002", "Phòng 2", "TRONG", 20.50M, 5);

            // Gọi hàm lấy danh sách phòng
            dt = _roomService.tblqldt_room_get_all();

            // Kiểm tra số dòng dữ liệu trả về
            Assert.AreEqual(2, dt.Rows.Count);
        }

        [TestMethod]
        public void tblqldt_room_get_all_without_data()
        {
            SqlCommand cmd = new SqlCommand();

            // Xóa dữ liệu
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                                delete from tblqldt_member;
                                delete from tblqldt_contract;
                                delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            // Gọi hàm lấy danh sách phòng
            dt = _roomService.tblqldt_room_get_all();

            // Kiểm tra số dòng dữ liệu trả về
            Assert.AreEqual(0, dt.Rows.Count);
        }
        #endregion

        #region click chon 1 dong -> chi liet ke ma khong test
        #endregion

        #region tblqldt_room_insert
        [TestMethod]
        public void tblqldt_room_insert_chk_data_user_input()
        {
            /*
                Quy định:
                    room_nbr
                        1. Bắt buộc phải nhập.
                        2. Mã phòng tối thiểu 1 ký tự.
                        3. Mã phòng tối đa 10 ký tự.

                    name
                        1. Bắt buộc phải nhập.
                        2. Tên phòng tối thiểu 1 ký tự.
                        3. Tên phòng tối đa 200 ký tự.

                    status
                        1. Bắt buộc phải nhập.
                        2. Chỉ được phép là TRONG 

                    area
                        1. Diện tích không âm
                        1. Diện tích từ 15 đến 50 m².

                    max_qty_man
                        1. Số người ở không âm
                        2. Số người ở tối thiểu 1.
                        3. Số người ở tối đa 5.
            */

            // Khởi tạo dữ liệu hợp lệ
            var vm = new tblqldt_room_insert_vm();
            vm.room_nbr = "P001";
            vm.name = "Phòng 1";
            vm.status = "TRONG";
            vm.area = 20.50M;
            vm.max_qty_man = 5;

            // room_nbr
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
                Assert.AreEqual("Mã phòng tối đa 10 kí tự, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            // name
            vm.room_nbr = "P001";
            try
            {
                vm.name = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập tên phòng", ex.Message.Trim());
            }

            try
            {
                vm.name = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập tên phòng", ex.Message.Trim());
            }

            try
            {
                vm.name = new string('A', 201);
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tên phòng tối đa 200 kí tự, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            // status_01
            vm.name = "Phòng 1";
            try
            {
                vm.status = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Trạng thái phòng không được để trống", ex.Message.Trim());
            }

            try
            {
                vm.status = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Trạng thái phòng không được để trống", ex.Message.Trim());
            }

            //try
            //{
            //    vm.status = "DANG_THUE";
            //    _validationService.Validate(vm);
            //}
            //catch (Exception ex)
            //{
            //    Assert.AreEqual("Trạng thái phòng phải là TRONG", ex.Message.Trim());
            //}

            // status_02
            //vm.name = "Phòng 1";

            //try
            //{
            //    vm.status = null;
            //    _validationService.Validate(vm);
            //}
            //catch (Exception ex)
            //{
            //    Assert.AreEqual("Trạng thái phòng không được để trống", ex.Message.Trim());
            //}

            //try
            //{
            //    vm.status = "";
            //    _validationService.Validate(vm);
            //}
            //catch (Exception ex)
            //{
            //    Assert.AreEqual("Trạng thái phòng không được để trống", ex.Message.Trim());
            //}

            ////try
            ////{
            ////    vm.status = "KHONG_CHO_THUE";
            ////    _validationService.Validate(vm);
            ////}
            ////catch (Exception ex)
            ////{
            ////    Assert.AreEqual("Trạng thái phòng phải là TRONG", ex.Message.Trim());
            ////}

            // area
            vm.status = "TRONG";
            try
            {
                vm.area = -1;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Diện tích tối thiểu 15 m^2 và tối đa 50 m^2, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            try
            {
                vm.area = 14;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Diện tích tối thiểu 15 m^2 và tối đa 50 m^2, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            try
            {
                vm.area = 51;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Diện tích tối thiểu 15 m^2 và tối đa 50 m^2, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            // max_qty_man
            vm.area = 20.50M;
            try
            {
                vm.max_qty_man = -1;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Số người ở tối thiểu là 1 và tối đa là 5, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            try
            {
                vm.max_qty_man = 0;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Số người ở tối thiểu là 1 và tối đa là 5, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            try
            {
                vm.max_qty_man = 6;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Số người ở tối thiểu là 1 và tối đa là 5, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            // Tất cả dữ liệu hợp lệ
            vm.max_qty_man = 5;
            try
            {
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Test sau cùng, có 1 lỗi không xác định cụ thể: " + ex.Message, ex.Message);
            }
        }

        [TestMethod]
        public void tblqldt_room_insert_room_nbr_already_exists()
        {
            /*
                Quy định:
                    1. Kiểm tra mã phòng đã tồn tại.
                    2. Nếu mã phòng đã tồn tại thì không được thêm.
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Thêm phòng P001
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            // Thêm lại phòng P001
            try
            {
                _roomService.tblqldt_room_insert("P001", "Phòng 2", "TRONG", 25.00M, 4);
                Assert.Fail("Phải báo lỗi mã phòng đã tồn tại.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã phòng đã tồn tại, vui lòng kiểm tra lại", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_room_insert_chk_status_DANG_THUE_fail()
        {
            /*
                Quy định:
                    1. Kiểm tra trạng thái <> TRONG
                    2. Nếu <> TRONG -> Thông báo: Trạng thái phải là "TRONG"
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            try
            {
                _roomService.tblqldt_room_insert("P001", "Phòng 1", "DANG_THUE", 25.00M, 4);
                Assert.Fail("Phải báo lỗi thêm phòng thì trạng thái phòng không được khác trống.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Trạng thái phòng phải là TRONG", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_room_insert_chk_status_KHONG_CHO_THUE_fail()
        {
            /*
                Quy định:
                    1. Kiểm tra trạng thái <> TRONG
                    2. Nếu <> TRONG -> Thông báo: Trạng thái phải là "TRONG"
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            try
            {
                _roomService.tblqldt_room_insert("P001", "Phòng 2", "KHONG_CHO_THUE", 25.00M, 4);
                Assert.Fail("Phải báo lỗi thêm phòng trạng thái phòng không được khác trống.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Trạng thái phòng phải là TRONG", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_room_insert_information_success()
        {
            // 1. Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            // Kiểm tra dữ liệu sau insert
            DataTable dtAfter = _roomService.tblqldt_room_get_by_id("P001");

            Assert.AreEqual(1, dtAfter.Rows.Count);
            Assert.AreEqual("P001", dtAfter.Rows[0]["room_nbr"].ToString());
            Assert.AreEqual("Phòng 1", dtAfter.Rows[0]["name"].ToString());
            Assert.AreEqual("TRONG", dtAfter.Rows[0]["status"].ToString());
            Assert.AreEqual(20.50M, Convert.ToDecimal(dtAfter.Rows[0]["area"]));
            Assert.AreEqual(5, Convert.ToInt32(dtAfter.Rows[0]["max_qty_man"]));
        }
        #endregion

        #region tblqldt_room_update
        [TestMethod]
        public void tblqldt_room_update_chk_data_user_input()
        {
            /*
                Quy định:
                    room_nbr
                        1. Bắt buộc phải nhập.
                        2. Mã phòng tối thiểu 1 ký tự.
                        3. Mã phòng tối đa 10 ký tự.

                    name
                        1. Bắt buộc phải nhập.
                        2. Tên phòng tối thiểu 1 ký tự.
                        3. Tên phòng tối đa 200 ký tự.

                    status
                        1. Bắt buộc phải nhập.
                        2. Chỉ được phép là TRONG hoặc KHONG_CHO_THUE.

                    area
                        1. Diện tích không âm
                        1. Diện tích từ 15 đến 50 m².

                    max_qty_man
                        1. Số người ở không âm
                        2. Số người ở tối thiểu 1.
                        3. Số người ở tối đa 5.
            */

            // Khởi tạo dữ liệu hợp lệ
            var vm = new tblqldt_room_update_vm();
            vm.room_nbr = "P001";
            vm.name = "Phòng 1";
            vm.status = "TRONG"; // hoặc vm.status = "KHONG_CHO_THUE"
            vm.area = 20.50M;
            vm.max_qty_man = 5;

            // room_nbr
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
                vm.room_nbr = "P012345678910";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã phòng tối đa 10 kí tự, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            // name
            vm.room_nbr = "P001";
            try
            {
                vm.name = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập tên phòng", ex.Message.Trim());
            }

            try
            {
                vm.name = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Vui lòng nhập tên phòng", ex.Message.Trim());
            }

            try
            {
                vm.name = new string('A', 201);
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Tên phòng tối đa 200 kí tự, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            // status
            vm.name = "Phòng 1";
            try
            {
                vm.status = null;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Trạng thái phòng không được để trống", ex.Message.Trim());
            }

            try
            {
                vm.status = "";
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Trạng thái phòng không được để trống", ex.Message.Trim());
            }

            //try
            //{
            //    vm.status = "DANG_THUE";
            //    _validationService.Validate(vm);
            //}
            //catch (Exception ex)
            //{
            //    Assert.AreEqual("Trạng thái phòng phải là 'TRONG' hoặc 'KHONG_CHO_THUE'", ex.Message.Trim());
            //}

            // area
            vm.status = "TRONG";
            try
            {
                vm.area = -1;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Diện tích tối thiểu 15 m^2 và tối đa 50 m^2, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            try
            {
                vm.area = 10;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Diện tích tối thiểu 15 m^2 và tối đa 50 m^2, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            try
            {
                vm.area = 51;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Diện tích tối thiểu 15 m^2 và tối đa 50 m^2, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            // max_qty_man
            vm.area = 20.50M;

            try
            {
                vm.max_qty_man = -1;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Số người ở tối thiểu là 1 và tối đa là 5, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            try
            {
                vm.max_qty_man = 0;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Số người ở tối thiểu là 1 và tối đa là 5, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            try
            {
                vm.max_qty_man = 6;
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Số người ở tối thiểu là 1 và tối đa là 5, vui lòng kiểm tra lại", ex.Message.Trim());
            }

            // Tất cả dữ liệu hợp lệ
            vm.max_qty_man = 5;
            try
            {
                _validationService.Validate(vm);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Test sau cùng, có 1 lỗi không xác định cụ thể: " + ex.Message, ex.Message);
            }
        }

        [TestMethod]
        public void tblqldt_room_update_room_nbr_not_exists()
        {
            /*
                Quy định:
                    1. Kiểm tra mã phòng có tồn tại hay không.
                    2. Nếu mã phòng không tồn tại -> báo lỗi.
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);

            // Cập nhật phòng không tồn tại
            try
            {
                _roomService.tblqldt_room_update("P002", "Phòng 2", "TRONG", 20.50M, 5);
                Assert.Fail("Phải báo lỗi mã phòng không tồn tại.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Mã phòng không tồn tại", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_room_update_fail_when_room_status_is_DANG_THUE()
        {
            /*
                Quy định:
                    Nếu phòng đang ở trạng thái DANG_THUE thì không cho phép cập nhật.
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Thêm phòng đang thuê bằng SQL trực tiếp
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.CommandText = @"INSERT INTO tblqldt_room (room_nbr, name, status, area, max_qty_man) 
                          VALUES ('P001', N'Phòng 1', 'DANG_THUE', 20.50, 5);";
            _db.exec_sql_cmd(cmdInsert);

            // Thực hiện cập nhật
            try
            {
                _roomService.tblqldt_room_update("P001", "Phòng này không sửa", "TRONG", 20.50M, 5);
                Assert.Fail("Phải báo lỗi vì phòng đang được thuê.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Phòng đang được thuê không thể chỉnh sửa thông tin", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_room_update_success_when_room_status_is_TRONG()
        {
            /*
                Quy định:
                    1. Phòng tồn tại.
                    2. Cập nhật:
                        - name
                        - status
                        - area
                        - max_qty_man
                    3. Kiểm tra dữ liệu trước update.
                    4. Kiểm tra dữ liệu sau update.
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
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            // Kiểm tra dữ liệu trước update
            DataTable dtBefore = _roomService.tblqldt_room_get_by_id("P001");

            Assert.AreEqual(1, dtBefore.Rows.Count);
            Assert.AreEqual("P001", dtBefore.Rows[0]["room_nbr"].ToString());
            Assert.AreEqual("Phòng 1", dtBefore.Rows[0]["name"].ToString());
            Assert.AreEqual("TRONG", dtBefore.Rows[0]["status"].ToString());
            Assert.AreEqual(20.50M, Convert.ToDecimal(dtBefore.Rows[0]["area"]));
            Assert.AreEqual(5, Convert.ToInt32(dtBefore.Rows[0]["max_qty_man"]));

            // Update
            _roomService.tblqldt_room_update("P001", "Phòng XO", "KHONG_CHO_THUE", 38.46M, 4);

            // Kiểm tra dữ liệu sau update
            DataTable dtAfter = _roomService.tblqldt_room_get_by_id("P001");

            Assert.AreEqual(1, dtAfter.Rows.Count);
            Assert.AreEqual("P001", dtAfter.Rows[0]["room_nbr"].ToString());
            Assert.AreEqual("Phòng XO", dtAfter.Rows[0]["name"].ToString());
            Assert.AreEqual("KHONG_CHO_THUE", dtAfter.Rows[0]["status"].ToString());
            Assert.AreEqual(38.46M, Convert.ToDecimal(dtAfter.Rows[0]["area"]));
            Assert.AreEqual(4, Convert.ToInt32(dtAfter.Rows[0]["max_qty_man"]));
        }

        [TestMethod]
        public void tblqldt_room_update_success_when_room_status_is_KHONG_CHO_THUE()
        {
            /*
                Quy định:
                    1. Phòng tồn tại.
                    2. Cập nhật:
                        - name
                        - status
                        - area
                        - max_qty_man
                    3. Kiểm tra dữ liệu trước update.
                    4. Kiểm tra dữ liệu sau update.
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);

            // Insert đúng nghiệp vụ
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            // Kiểm tra dữ liệu trước update
            DataTable dtBefore = _roomService.tblqldt_room_get_by_id("P001");

            Assert.AreEqual("TRONG", dtBefore.Rows[0]["status"].ToString());

            // Chuẩn bị dữ liệu cho test
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE tblqldt_room
                                SET status = 'KHONG_CHO_THUE'
                                WHERE room_nbr = 'P001';";
            _db.exec_sql_cmd(cmd);

            // Thực hiện update cần test
            _roomService.tblqldt_room_update("P001", "Phòng XO", "TRONG", 38.46M, 4);

            // Kiểm tra dữ liệu sau update
            // Kiểm tra dữ liệu sau update
            DataTable dtAfter = _roomService.tblqldt_room_get_by_id("P001");

            Assert.AreEqual(1, dtAfter.Rows.Count);
            Assert.AreEqual("P001", dtAfter.Rows[0]["room_nbr"].ToString());
            Assert.AreEqual("Phòng XO", dtAfter.Rows[0]["name"].ToString());
            Assert.AreEqual("TRONG", dtAfter.Rows[0]["status"].ToString());
            Assert.AreEqual(38.46M, Convert.ToDecimal(dtAfter.Rows[0]["area"]));
            Assert.AreEqual(4, Convert.ToInt32(dtAfter.Rows[0]["max_qty_man"]));
        }
        #endregion

        #region tblqldt_room_delete
        [TestMethod]
        public void tblqldt_room_delete_room_nbr_not_exists()
        {
            /*              
                Quy định:
                    Nếu mã phòng không tồn tại -> báo lỗi
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

            try
            {
                _roomService.tblqldt_room_delete("PXX"); //truyền mã phòng không tôn tại trong db

            }
            catch (Exception ex)
            {
                Assert.AreEqual("Không tìm thấy phòng", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_room_delete_fail_when_room_status_is_DANG_THUE()
        {
            /*
                Quy định:
                    1. Trạng thái phòng DANG_THUE
                    2. Phòng DANG_THUE -> thông báo: không được xóa
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert trực tiếp bằng SQL 
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.CommandText = @"INSERT INTO tblqldt_room (room_nbr, name, status, area, max_qty_man) 
                              VALUES ('P001', N'Phòng 1', 'DANG_THUE', 20.50, 5);";
            _db.exec_sql_cmd(cmdInsert);

            // Thực hiện xóa và bắt lỗi
            try
            {
                _roomService.tblqldt_room_delete("P001");
                Assert.Fail("Phải báo lỗi vì phòng đang thuê không được xóa.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Phòng đang thuê hoặc không cho thuê không được xóa", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_room_delete_fail_when_room_status_is_KHONG_CHO_THUE()
        {
            /*
               Quy định:
                   1. Trạng thái phòng KHONG_CHO_THUE
                   2. Phòng KHONG_CHO_THUE-> thông báo: không được xóa
           */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                        delete from tblqldt_member;
                        delete from tblqldt_contract;
                        delete from tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert trực tiếp bằng SQL 
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.CommandType = CommandType.Text;
            cmdInsert.CommandText = @"INSERT INTO tblqldt_room (room_nbr, name, status, area, max_qty_man) 
                              VALUES ('P001', N'Phòng 1', 'KHONG_CHO_THUE', 20.50, 5);";
            _db.exec_sql_cmd(cmdInsert);

            // Thực hiện xóa và bắt lỗi
            try
            {
                _roomService.tblqldt_room_delete("P001");
                Assert.Fail("Phải báo lỗi vì phòng đang thuê không được xóa.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Phòng đang thuê hoặc không cho thuê không được xóa", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_room_delete_fail_when_room_has_history_contract()
        {
            /*             
                Quy định:
                   Phòng đã từng có hợp đồng -> thông báo: "Phòng đã từng có hợp đồng không thể xóa"
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"delete from tblqldt_invoice;
                                delete from tblqldt_member;
                                delete from tblqldt_contract;
                                delete from tblqldt_room;";
            var dt = _db.exec_sql_cmd(cmd);

            //Insert phòng ở trạng thái "TRONG"
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            //Insert dữ liệu giả lập phòng đã từng có hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 800000, 1000000, 4000, 10000, 15000, 15000);

            try
            {
                _roomService.tblqldt_room_delete("P001");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Phòng đã từng có hợp đồng không thể xóa", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_room_delete_success()
        {
            /*              
               Quy định:                
                  Phòng Trống, chưa từng phát sinh hợp đồng  -> được phép xóa
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

            // Thực hiện xóa
            _roomService.tblqldt_room_delete("P001");

            // Lấy kết quả trên UI, kiểm tra xem có trả về 0 dòng
            dt = _roomService.tblqldt_room_get_all();
            Assert.AreEqual(dt.Rows.Count, 0);
        }
        #endregion

        #endregion
    }
}












