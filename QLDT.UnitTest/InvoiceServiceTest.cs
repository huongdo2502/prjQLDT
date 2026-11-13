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
    public class InvoiceServiceTest
    {
        #region declare 
        private DBHelper _db;

        private IRoomService _roomService;
        private IContractService _contractService;
        private InvoiceService _invoiceService;
        private IValidationService _validationService;
        #endregion

        #region set value
        #endregion

        #region ham tao
        public InvoiceServiceTest()
        {
            _db = new DBHelper();

            _roomService = new RoomService();
            _contractService = new ContractService();
            _invoiceService = new InvoiceService();
            _validationService = new ValidationService();
        }
        #endregion

        #region tblqldt_invoice

        #region tblqldt_invoice_get_all

        #region tblqldt_invoice_get_all_with_data
        [TestMethod]
        public void tblqldt_invoice_get_all_with_data_select_month_year()
        {
            /*
                Quy định:
                    Người dùng chọn tháng và năm.
                    Hệ thống chỉ hiển thị danh sách phiếu thu của đúng tháng, năm đã chọn.
            */

            // Xóa dữ liệu
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                        DELETE FROM tblqldt_member;
                        DELETE FROM tblqldt_contract;
                        DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            // Insert hợp đồng
            _contractService.tblqldt_contract_insert(
                "HD001", "P001",
                1000000, 3000000,
                4000, 10000,
                15000, 15000);

            // Insert phiếu thu tháng 6/2026
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr, billing_month, billing_year,
            room_price,
            old_electric_reading, new_electric_reading,
            electric_price, electric_amount,
            old_water_reading, new_water_reading,
            water_price, water_amount,
            trash_fee, security_fee,
            total_amount,
            payment_status,
            collection_date,
            is_checkout
        )
        VALUES
        (
            'HD001',
            6,
            2026,
            3000000,
            0,20,
            4000,80000,
            0,5,
            10000,50000,
            15000,15000,
            3210000,
            'CHUA_THU',
            NULL,
            0
        );";

            _db.exec_sql_cmd(cmd);

            // Act
            DataTable dt = _invoiceService.tblqldt_invoice_get_all(6, 2026);

            // Assert
            Assert.AreEqual(1, dt.Rows.Count);
            Assert.AreEqual("HD001", dt.Rows[0]["contract_nbr"].ToString());
            Assert.AreEqual(6, Convert.ToInt32(dt.Rows[0]["billing_month"]));
            Assert.AreEqual(2026, Convert.ToInt32(dt.Rows[0]["billing_year"]));
        }

        [TestMethod]
        public void tblqldt_invoice_get_all_with_data_default_previous_month()
        {
            /*
                Quy định:
                    1. Nếu ngày hiện tại <= 10 → mặc định hiển thị phiếu thu của tháng trước.
                    2. Nếu ngày hiện tại > 10 → mặc định hiển thị phiếu thu của tháng hiện tại.
            */

            // Xóa dữ liệu
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                        DELETE FROM tblqldt_member;
                        DELETE FROM tblqldt_contract;
                        DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            // Insert hợp đồng
            _contractService.tblqldt_contract_insert(
                "HD001", "P001",
                1000000, 3000000,
                4000, 10000,
                15000, 15000);

            // Tạo phiếu thu tháng trước (06/2026)
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr, billing_month, billing_year,
            room_price,
            old_electric_reading, new_electric_reading,
            electric_price, electric_amount,
            old_water_reading, new_water_reading,
            water_price, water_amount,
            trash_fee, security_fee,
            total_amount,
            payment_status,
            collection_date,
            is_checkout
        )
        VALUES
        (
            'HD001',
            6,
            2026,
            3000000,
            0,10,
            4000,40000,
            0,5,
            10000,50000,
            15000,15000,
            3120000,
            'CHUA_THU',
            NULL,
            0
        );";
            _db.exec_sql_cmd(cmd);

            // Giả lập ngày hiện tại là 10/07/2026
            DateTime current = new DateTime(2026, 7, 10);
            DateTime last = current.AddMonths(-1);

            // Act
            DataTable dt = _invoiceService.tblqldt_invoice_get_all(0, 0, current);

            // Assert
            Assert.AreEqual(1, dt.Rows.Count);
            Assert.AreEqual(last.Month, Convert.ToInt32(dt.Rows[0]["billing_month"]));
            Assert.AreEqual(last.Year, Convert.ToInt32(dt.Rows[0]["billing_year"]));
        }

        [TestMethod]
        public void tblqldt_invoice_get_all_with_data_default_current_month()
        {
            /*
                Quy định:
                    1. Nếu ngày hiện tại <= 10 → mặc định hiển thị phiếu thu của tháng trước.
                    2. Nếu ngày hiện tại > 10 → mặc định hiển thị phiếu thu của tháng hiện tại.
            */

            // Xóa dữ liệu
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                        DELETE FROM tblqldt_member;
                        DELETE FROM tblqldt_contract;
                        DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Insert phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.50M, 5);

            // Insert hợp đồng
            _contractService.tblqldt_contract_insert(
                "HD001", "P001",
                1000000, 3000000,
                4000, 10000,
                15000, 15000);

            // Giả lập ngày hiện tại là 20/07/2026
            DateTime current = new DateTime(2026, 7, 20);

            // Insert phiếu thu tháng hiện tại (07/2026)
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr, billing_month, billing_year,
            room_price,
            old_electric_reading, new_electric_reading,
            electric_price, electric_amount,
            old_water_reading, new_water_reading,
            water_price, water_amount,
            trash_fee, security_fee,
            total_amount,
            payment_status,
            collection_date,
            is_checkout
        )
        VALUES
        (
            'HD001',
            7,
            2026,
            3000000,
            0,10,
            4000,40000,
            0,5,
            10000,50000,
            15000,15000,
            3120000,
            'CHUA_THU',
            NULL,
            0
        );";

            _db.exec_sql_cmd(cmd);

            // Act
            DataTable dt = _invoiceService.tblqldt_invoice_get_all(0, 0, current);

            // Assert
            Assert.AreEqual(1, dt.Rows.Count);         
            Assert.AreEqual(current.Month, Convert.ToInt32(dt.Rows[0]["billing_month"]));
            Assert.AreEqual(current.Year, Convert.ToInt32(dt.Rows[0]["billing_year"]));
        }

        #endregion

        #region tblqldt_invoice_get_all_without_data

        [TestMethod]
        public void tblqldt_invoice_get_all_without_data_select_month_year()
        {
            /*
                Quy định:
                    1. Không có dữ liệu phiếu thu.
                    2. Người dùng chọn tháng và năm cần xem.

                Kết quả:
                    - Trả về danh sách phiếu thu rỗng.
            */

            // Clear dữ liệu
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;                               
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            var dt = _invoiceService.tblqldt_invoice_get_all(6, 2026);

            // Assert
            Assert.IsNotNull(dt);
            Assert.AreEqual(0, dt.Rows.Count);
        }

        [TestMethod]
        public void tblqldt_invoice_get_all_without_data_default_previous_month()
        {
            /*
                Quy định:
                    1. Không có dữ liệu phiếu thu.
                    2. Người dùng không chọn tháng và năm.
                    3. Ngày hiện tại trước ngày 10.

                Kết quả:
                    - Mặc định lấy dữ liệu phiếu thu của tháng trước.
                    - Không có dữ liệu được trả về.
            */

            // Clear dữ liệu
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;                               
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Giả lập ngày 9/07/2026 => lấy dữ liệu tháng 6
            var dt = _invoiceService.tblqldt_invoice_get_all(0, 0, new DateTime(2026, 7, 9));

            // Assert
            Assert.IsNotNull(dt);
            Assert.AreEqual(0, dt.Rows.Count);
        }

        [TestMethod]
        public void tblqldt_invoice_get_all_without_data_default_current_month()
        {
            /*
               Quy định:
                   1. Không có dữ liệu phiếu thu.
                   2. Người dùng không chọn tháng và năm.
                   3. Ngày hiện tại từ ngày 10 trở đi.

               Kết quả:
                   - Mặc định lấy dữ liệu phiếu thu của tháng hiện tại.
                   - Không có dữ liệu được trả về.
           */

            // Clear dữ liệu
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;                               
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Giả lập ngày 20/07/2026 => lấy dữ liệu tháng 7
            var dt = _invoiceService.tblqldt_invoice_get_all(0, 0, new DateTime(2026, 7, 20));

            // Assert
            Assert.IsNotNull(dt);
            Assert.AreEqual(0, dt.Rows.Count);
        }

        #endregion

        #endregion

        #region tblqldt_create_invoice

        [TestMethod]
        public void tblqldt_invoice_create_no_contract()
        {
            /*
              Quy định
                1. Không có hợp đồng trong hệ thống.
                2. Người dùng thực hiện lập phiếu thu.

              Kết quả mong đợi
                1. Không lập phiếu thu.
                2. Hiển thị thông báo:
                   "Không có hợp đồng để lập phiếu thu."
            */

            SqlCommand cmd = new SqlCommand();

            // Xóa dữ liệu
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);

            try
            {
                // Act
                _invoiceService.tblqldt_invoice_create(DateTime.Now.Month, DateTime.Now.Year);
                Assert.Fail("Phải báo lỗi khi không có hợp đồng.");
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual("Không có hợp đồng để lập phiếu thu.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_create_first_time_current_month_success()
        {
            /*
                Quy định
                    1. Chưa có phiếu thu nào.
                    2. Có ít nhất 1 hợp đồng còn hiệu lực.
                    3. Lần đầu lập phiếu thu của tháng hiện tại.

                Kết quả mong đợi
                    1. Lập phiếu thu thành công.
                    2. Tạo 01 phiếu thu.
            */

            SqlCommand cmd = new SqlCommand();

            // Xóa dữ liệu
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);

            // Tạo hợp đồng
            // Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 3000000, 4000, 10000, 15000, 15000);

            // Lập phiếu thu tháng hiện tại
            _invoiceService.tblqldt_invoice_create(DateTime.Now.Month, DateTime.Now.Year);

            // Kiểm tra đã tạo 1 phiếu thu
            DataTable dt = _invoiceService.tblqldt_invoice_get_all(DateTime.Now.Month, DateTime.Now.Year, null);

            Assert.AreEqual(1, dt.Rows.Count);

            Assert.AreEqual("HD001", dt.Rows[0]["contract_nbr"].ToString());
            Assert.AreEqual(DateTime.Now.Month, Convert.ToInt32(dt.Rows[0]["billing_month"]));
            Assert.AreEqual(DateTime.Now.Year, Convert.ToInt32(dt.Rows[0]["billing_year"]));
            Assert.AreEqual("CHUA_THU", dt.Rows[0]["payment_status"].ToString());
        }

        [TestMethod]
        public void tblqldt_invoice_create_first_time_invalid_month()
        {
            /*
                Quy định
                    1. Chưa có phiếu thu nào.
                    2. Có hợp đồng còn hiệu lực.
                    3. Lần đầu chỉ được lập phiếu thu của tháng hiện tại.

                Kết quả mong đợi
                    1. Không lập phiếu thu.
                    2. Báo lỗi:
                       "Lần đầu chỉ được lập phiếu thu của tháng hiện tại."
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
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 3000000, 4000, 10000, 15000, 15000);

            // Cho hợp đồng có hiệu lực
            cmd.CommandText = @"UPDATE tblqldt_contract
                                SET start_date = '2026-01-01'
                                WHERE contract_nbr='HD001'";
            _db.exec_sql_cmd(cmd);

            // Chọn tháng trước tháng hiện tại
            DateTime invalidMonth = DateTime.Now.AddMonths(-1);

            try
            {
                _invoiceService.tblqldt_invoice_create(invalidMonth.Month, invalidMonth.Year);
                Assert.Fail("Phải báo lỗi khi lần đầu lập phiếu không phải tháng hiện tại.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Lần đầu chỉ được lập phiếu thu của tháng hiện tại.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_create_no_contract_need_create()
        {
            /*
                Quy định
                    1. Có ít nhất 1 hợp đồng còn hiệu lực.
                    2. Đã lập phiếu thu cho tất cả hợp đồng của tháng hiện tại.
                    3. Người dùng tiếp tục nhấn "Tạo phiếu thu".

                Kết quả mong đợi
                    1. Không tạo thêm phiếu thu.
                    2. Báo lỗi:
                       "Không có hợp đồng cần lập phiếu thu."
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
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 3000000, 4000, 10000, 15000, 15000);

            // Cho hợp đồng có hiệu lực
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE tblqldt_contract
                                SET start_date = '2026-01-01'
                                WHERE contract_nbr='HD001'";
            _db.exec_sql_cmd(cmd);

            // Lập phiếu thu lần đầu
            _invoiceService.tblqldt_invoice_create(DateTime.Now.Month, DateTime.Now.Year);

            try
            {
                // Tiếp tục lập phiếu thu cùng tháng
                _invoiceService.tblqldt_invoice_create(DateTime.Now.Month, DateTime.Now.Year);
                Assert.Fail("Phải báo không còn hợp đồng cần lập phiếu thu.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Không có hợp đồng cần lập phiếu thu trong tháng này.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_create_has_contract_need_create()
        {
            /*            
                Quy định
                    1. Có ít nhất 01 hợp đồng còn hiệu lực.
                    2. Đã lập phiếu thu cho tất cả hợp đồng của tháng hiện tại.
                    3. Sau đó có thêm hợp đồng có hiệu lực.
                    4. Người dùng tiếp tục nhấn "Tạo phiếu thu".

                Kết quả mong đợi
                    1. Chỉ tạo thêm phiếu thu cho hợp đồng mới.
                    2. Không tạo lại phiếu thu của hợp đồng đã có.
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
            _roomService.tblqldt_room_insert("P002", "Phòng 2", "TRONG", 20.5M, 5);

            // Tạo hợp đồng HD001
            _contractService.tblqldt_contract_insert("HD001", "P001",800000, 1000000, 4000, 10000, 15000, 15000);

            // Lập phiếu thu lần đầu
            _invoiceService.tblqldt_invoice_create(DateTime.Now.Month, DateTime.Now.Year);

            // Phát sinh hợp đồng HD002
            _contractService.tblqldt_contract_insert("HD002", "P002", 500000, 1000000, 4000, 10000, 15000, 15000);

            // Lập phiếu thu lần hai cho HD002
            _invoiceService.tblqldt_invoice_create(DateTime.Now.Month, DateTime.Now.Year);

            // Kiểm tra
            DataTable dt = _invoiceService.tblqldt_invoice_get_all(
                DateTime.Now.Month,
                DateTime.Now.Year,
                null);

            Assert.AreEqual(2, dt.Rows.Count);
            Assert.AreEqual("HD001", dt.Rows[0]["contract_nbr"].ToString());
            Assert.AreEqual("HD002", dt.Rows[1]["contract_nbr"].ToString());
        }

        [TestMethod]
        public void tblqldt_invoice_create_invalid_create_future_month()
        {
            /*
                Quy định
                    1. Đã tồn tại phiếu thu của tháng hiện tại.
                    2. Người dùng chỉ được phép lập phiếu thu của tháng kế tiếp.
                    3. Người dùng chọn lập phiếu thu của tháng tương lai (không phải tháng hiện tại và cũng không phải tháng kế tiếp).

                Kết quả mong đợi
                    1. Không lập phiếu thu.
                    2. Hiển thị thông báo:
                       "Chỉ được lập phiếu thu của tháng hiện tại và tháng kế tiếp."
            */

            // Clear dữ liệu
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
            _contractService.tblqldt_contract_insert(
                "HD001", "P001",
                1000000, 3000000, 4000, 10000, 15000, 15000);

            // Tạo phiếu thu của tháng hiện tại
            DateTime current = DateTime.Now;

            cmd.CommandText = $@"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr,billing_month,billing_year,
            room_price,
            old_electric_reading,new_electric_reading,
            electric_price,electric_amount,
            old_water_reading,new_water_reading,
            water_price,water_amount,
            trash_fee,security_fee,total_amount,
            payment_status,collection_date,is_checkout
        )
        VALUES
        (
            'HD001',
            {current.Month},
            {current.Year},
            1000000,
            100,120,
            4000,80000,
            10,12,
            10000,20000,
            15000,15000,
            1130000,
            'DA_THU',
            GETDATE(),
            0
        )";
            _db.exec_sql_cmd(cmd);

            // Tháng tương lai (sau tháng kế tiếp)
            DateTime invalid = DateTime.Now.AddMonths(2);

            try
            {
                _invoiceService.tblqldt_invoice_create(
                    invalid.Month,
                    invalid.Year);

                Assert.Fail("Không được phép lập phiếu thu của tháng tương lai.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Chỉ được lập phiếu thu của tháng hiện tại và tháng kế tiếp.",
                    ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_create_previous_month_not_input_reading()
        {
            /*
                Quy định:
                    1. Có phòng.
                    2. Có hợp đồng còn hiệu lực.
                    3. Đã có phiếu thu tháng trước.
                    4. Tháng trước chưa nhập đủ chỉ số điện hoặc nước.
                    5. Người dùng lập phiếu thu tháng kế tiếp.

                Kết quả:
                    1. Không tạo phiếu thu.
                    2. Báo lỗi:
                       "Phiếu thu tháng trước chưa nhập đầy đủ chỉ số điện hoặc nước."
            */

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
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 3000000, 4000, 10000, 15000, 15000);

            // Tạo phiếu tháng trước nhưng CHƯA nhập điện nước
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (
                contract_nbr, billing_month, billing_year, room_price, old_electric_reading, new_electric_reading, electric_price, electric_amount,
                old_water_reading, new_water_reading, water_price, water_amount, trash_fee, security_fee, total_amount,payment_status, collection_date, is_checkout
            )
            VALUES
            (
                'HD001', 7, 2026, 1000000, 100, NULL, 4000, NULL, 10, NULL, 10000, NULL, 15000, 15000, NULL, 'CHUA_THU', NULL, 0
            )";
            _db.exec_sql_cmd(cmd);

            try
            {
                // Lập phiếu tháng kế tiếp
                _invoiceService.tblqldt_invoice_create(8, 2026);
                Assert.Fail("Phải báo lỗi chưa nhập đủ chỉ số điện nước.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Phiếu thu tháng trước chưa nhập đầy đủ chỉ số điện hoặc nước.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_create_next_month_success()
        {
            /*
                Quy định:
                    1. Có phòng.
                    2. Có hợp đồng còn hiệu lực.
                    3. Đã có phiếu thu tháng trước.
                    4. Tháng trước đã nhập đủ chỉ số điện nước.
                    5. Lập phiếu thu tháng kế tiếp.

                Kết quả:
                    Tạo phiếu thu thành công.
            */

            // Clear dữ liệu
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
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 3000000, 4000, 10000, 15000, 15000);

            // Tạo phiếu thu tháng trước (07/2026)
            // Đã nhập đủ chỉ số
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr, billing_month, billing_year, room_price, old_electric_reading, new_electric_reading, electric_price,
            electric_amount, old_water_reading, new_water_reading, water_price, water_amount, trash_fee, security_fee,
            total_amount, payment_status, collection_date, is_checkout
        )
        VALUES
        (
            'HD001', 7,2026, 1000000, 100,120,4000,80000, 10, 15, 10000,50000, 15000, 15000, 1160000, 'DA_THU','2026-07-10', 0          
        )";
            _db.exec_sql_cmd(cmd);

            // Tạo phiếu tháng 8
            var result = _invoiceService.tblqldt_invoice_create(8, 2026);

            // Kiểm tra tạo thành công
            Assert.IsNotNull(result);

            // Kiểm tra database có invoice tháng 8
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"SELECT COUNT(*)
                                FROM tblqldt_invoice
                                WHERE contract_nbr = 'HD001'
                                  AND billing_month = 8
                                  AND billing_year = 2026";
            var dt = _db.exec_sql_cmd(cmd);
            Assert.AreEqual(1, Convert.ToInt32(dt.Rows[0][0]));
        }
        #endregion

        #region tblqldt_update_invoice
        [TestMethod]
        public void tblqldt_invoice_update_invoice_not_exists()
        {
            /*
                Quy định:
                    1. Phiếu thu không tồn tại.

                Kết quả:
                    Báo lỗi "Phiếu thu không tồn tại."
            */

            // Dọn dẹp dữ liệu cũ
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;"
            };

            _db.exec_sql_cmd(cmd);

            // Tạo phòng mẫu
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);

            // Tạo hợp đồng mẫu (ví dụ cho 'HD999')
            _contractService.tblqldt_contract_insert("HD999", "P001", 1000000, 3000000, 4000, 10000, 15000, 15000);

            // Không insert vào bảng tblqldt_invoice để đảm bảo phiếu thu không tồn tại
            // Thực thi và kiểm tra ngoại lệ
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD999",      // Hợp đồng có tồn tại nhưng không có phiếu thu tương ứng
                    12, 2030, 0, 0, 0, 0, "CHUA_THU", false);
                Assert.Fail("Phiếu thu không tồn tại.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Phiếu thu không tồn tại.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_update_future_month_fail()
        {
            /*
                Quy định:
                    1. Có hợp đồng.
                    2. Có phiếu thu của tháng sau.
                    3. Người dùng cập nhật phiếu thu.

                Kết quả:
                    - Không cho phép cập nhật.
                    - Báo lỗi:
                      "Chỉ được cập nhật phiếu thu của tháng hiện tại."
            */

            SqlCommand cmd = new SqlCommand();

            // Xóa dữ liệu
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"
        DELETE FROM tblqldt_invoice;
        DELETE FROM tblqldt_member;
        DELETE FROM tblqldt_contract;
        DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Tạo phòng
            _roomService.tblqldt_room_insert(
                "P001",
                "Phòng 101",
                "TRONG",
                20.5M,
                5);

            // Tạo hợp đồng
            _contractService.tblqldt_contract_insert(
                "HD001",
                "P001",
                1000000,
                1000000,
                4000,
                10000,
                15000,
                15000);

            // Tạo phiếu thu tháng sau (08/2026)
            cmd.CommandText = @"
    INSERT INTO tblqldt_invoice
    (
        contract_nbr,
        billing_month,
        billing_year,
        room_price,
        old_electric_reading,
        new_electric_reading,
        electric_price,
        electric_amount,
        old_water_reading,
        new_water_reading,
        water_price,
        water_amount,
        trash_fee,
        security_fee,
        total_amount,
        payment_status,
        collection_date,
        is_checkout
    )
    VALUES
    (
        'HD001',
        8,
        2026,
        1000000,
        100,
        150,
        4000,
        200000,
        50,
        80,
        10000,
        300000,
        15000,
        15000,
        1530000,
        'CHUA_THU',
        NULL,
        0
    );";

            _db.exec_sql_cmd(cmd);

            try
            {
                // Giả lập hôm nay là 15/07/2026
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    8,
                    2026,
                    100,
                    160,
                    50,
                    90,
                    "CHUA_THU",
                    false,
                    new DateTime(2026, 7, 15));

                Assert.Fail("Phải báo lỗi khi cập nhật phiếu thu tháng tương lai.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Chỉ được cập nhật phiếu thu của tháng hiện tại.",
                    ex.Message.Trim());
            }

            // Kiểm tra dữ liệu không thay đổi
            cmd.CommandText = @"
        SELECT
            new_electric_reading,
            new_water_reading,
            payment_status
        FROM tblqldt_invoice
        WHERE contract_nbr = 'HD001'
          AND billing_month = 8
          AND billing_year = 2026;";

            DataTable dt = _db.exec_sql_cmd(cmd);

            Assert.AreEqual(150, Convert.ToInt32(dt.Rows[0]["new_electric_reading"]));
            Assert.AreEqual(80, Convert.ToInt32(dt.Rows[0]["new_water_reading"]));
            Assert.AreEqual("CHUA_THU", dt.Rows[0]["payment_status"].ToString());
        }

        #region tblqldt_invoice_update_past_invoice
        [TestMethod]
        public void tblqldt_invoice_update_past_invoice_change_electric_fail()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng quá khứ.
                    2. Thay đổi chỉ số điện mới.

                Kết quả:
                    Báo lỗi "Không được sửa chỉ số điện của phiếu thu trong quá khứ."
            */

            // 1. Dọn dẹp dữ liệu cũ
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @"DELETE FROM tblqldt_invoice;
                            
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;"
            };
            _db.exec_sql_cmd(cmd);

            // 2. Tạo phòng mẫu
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);

            // 3. Tạo hợp đồng mẫu
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 3000000, 4000, 10000, 15000, 15000);

            // 4. Tạo phiếu thu tháng 2/2026 (Quá khứ so với thời điểm hiện tại)
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr, billing_month, billing_year, room_price,
            old_electric_reading, new_electric_reading, electric_price, electric_amount,
            old_water_reading, new_water_reading, water_price, water_amount,
            trash_fee, security_fee, total_amount, payment_status, collection_date, is_checkout
        )
        VALUES
        (
            'HD001', 2, 2026, 1000000,
            100, 120, 4000, 80000,
            10, 15, 10000, 50000,
            15000, 15000, 1160000, 'DA_THU', '2026-07-10', 0
        )";

            _db.exec_sql_cmd(cmd);

            // 5. Thực thi và kiểm tra ngoại lệ
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    2,
                    2026,
                    100,    
                    130,    
                    10,     
                    15,     
                    "DA_THU",
                    false);

                Assert.Fail("Phải báo lỗi không được sửa chỉ số điện của phiếu thu trong quá khứ.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Không được sửa chỉ số điện của phiếu thu trong quá khứ.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_update_past_invoice_change_water_fail()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng quá khứ.
                    2. Thay đổi chỉ số nước mới.

                Kết quả:
                    Báo lỗi "Không được sửa chỉ số nước của phiếu thu trong quá khứ."
            */

            // 1. Dọn dẹp dữ liệu cũ
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;"
            };

            _db.exec_sql_cmd(cmd);

            // 2. Tạo phòng mẫu
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);

            // 3. Tạo hợp đồng mẫu
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 3000000, 4000, 10000, 15000, 15000);

            // 4. Tạo phiếu thu tháng 2/2026 (Quá khứ so với thời điểm hiện tại)
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', 2, 2026, 1000000,
             100, 120, 4000, 80000,
             10, 15, 10000, 50000,
             15000, 15000, 1160000,
             'DA_THU', '2026-07-10', 0)";
            _db.exec_sql_cmd(cmd);

            // 5. Thực thi và kiểm tra ngoại lệ
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    2,
                    2026,
                    100,    
                    120,    
                    10,     
                    18,     
                    "DA_THU",
                    false);

                Assert.Fail("Phải báo lỗi không được sửa chỉ số nước của phiếu thu trong quá khứ.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Không được sửa chỉ số nước của phiếu thu trong quá khứ.", ex.Message.Trim());


            }
        }

        [TestMethod]
        public void tblqldt_invoice_update_past_invoice_is_checkout_fail()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng quá khứ.
                    2. Thực hiện trả phòng (is_checkout = 1).

                Kết quả:
                    Báo lỗi "Không được trả phòng đối với phiếu thu trong quá khứ."
            */

            // 1. Dọn dẹp dữ liệu cũ
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @"
                    DELETE FROM tblqldt_invoice;
                    DELETE FROM tblqldt_member;
                    DELETE FROM tblqldt_contract;
                    DELETE FROM tblqldt_room;"
            };

            _db.exec_sql_cmd(cmd);

            // 2. Tạo phòng mẫu
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);

            // 3. Tạo hợp đồng mẫu
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 3000000, 4000, 10000, 15000, 15000);

            // 4. Tạo phiếu thu tháng 2/2026 (Quá khứ so với thời điểm hiện tại)
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', 2, 2026, 1000000,
             100, 120, 4000, 80000,
             10, 15, 10000, 50000,
             15000, 15000, 1160000,
             'DA_THU', '2026-07-10', 0)";

            _db.exec_sql_cmd(cmd);

            // 5. Thực thi và kiểm tra ngoại lệ
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    2,
                    2026,
                    100,    
                    120,    
                    10,     
                    15,     
                    "DA_THU",
                    true);  // is_checkout = 1 (thực hiện trả phòng)

                Assert.Fail("Phải báo lỗi không được trả phòng đối với phiếu thu trong quá khứ.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Không được trả phòng đối với phiếu thu trong quá khứ.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_update_past_invoice_DA_THU_to_DA_THU_fail()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng quá khứ.
                    2. Trạng thái thanh toán trong DB là 'DA_THU' và cập nhật giữ nguyên 'DA_THU' (hoặc thực hiện sửa đổi dữ liệu không hợp lệ đối với phiếu đã thu ở quá khứ).

                Kết quả:
                    Báo lỗi tương ứng theo business logic.
            */

            // 1. Dọn dẹp dữ liệu cũ
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;"
            };
            _db.exec_sql_cmd(cmd);

            // 2. Tạo phòng mẫu
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);

            // 3. Tạo hợp đồng mẫu
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 3000000, 4000, 10000, 15000, 15000);

            // 4. Tạo phiếu thu tháng 2/2026 với trạng thái 'DA_THU'
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', 2, 2026, 1000000,
             100, 120, 4000, 80000,
             10, 15, 10000, 50000,
             15000, 15000, 1160000,
             'DA_THU', '2026-07-10', 0)";

            _db.exec_sql_cmd(cmd);

            // 5. Thực thi và kiểm tra ngoại lệ
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    2,
                    2026,
                    100,    
                    120,    
                    10,     
                    15,     
                    "DA_THU", 
                    false);

                Assert.Fail("Phải báo lỗi khi cập nhật phiếu thu đã thanh toán trong quá khứ.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Phiếu thu đã được thanh toán trước đó. Vui lòng tải lại dữ liệu.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_update_past_invoice_DA_THU_thu_to_CHUA_THU_fail()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng quá khứ và đang ở trạng thái 'DA_THU'.
                    2. Cố gắng chuyển đổi trạng thái thanh toán từ 'DA_THU' sang 'CHUA_THU'.

                Kết quả:
                    Báo lỗi tương ứng theo quy định nghiệp vụ (ví dụ: Không được chuyển trạng thái phiếu thu đã thanh toán về chưa thanh toán trong quá khứ).
            */

            // 1. Dọn dẹp dữ liệu cũ
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;"
            };

            _db.exec_sql_cmd(cmd);

            // 2. Tạo phòng mẫu
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);

            // 3. Tạo hợp đồng mẫu
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 3000000, 4000, 10000, 15000, 15000);

            // 4. Tạo phiếu thu tháng 2/2026 với trạng thái 'DA_THU'
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', 2, 2026, 1000000,
             100, 120, 4000, 80000,
             10, 15, 10000, 50000,
             15000, 15000, 1160000,
             'DA_THU', '2026-07-10', 0)";

            _db.exec_sql_cmd(cmd);

            // 5. Thực thi và kiểm tra ngoại lệ khi đổi trạng thái từ 'DA_THU' sang 'CHUA_THU'
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    2,
                    2026,
                    100,    
                    120,    
                    10,     
                    15,     
                    "CHUA_THU",
                    false);

                Assert.Fail("Phải báo lỗi không được chuyển trạng thái phiếu thu đã thanh toán về chưa thanh toán.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Không được chuyển trạng thái phiếu thu đã thanh toán sang chưa thanh toán.", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_update_past_invoice_CHUA_THU_to_CHUA_THU_fail()
        {
            /*
                Quy định:
                    1. Phiếu thu trong cơ sở dữ liệu có trạng thái 'CHUA_THU'.
                    2. Cố gắng cập nhật trạng thái giữ nguyên hoặc không hợp lệ theo quy định.

                Kết quả:
                    Báo lỗi "Phiếu thu chưa được thanh toán. Chỉ được phép cập nhật sang trạng thái "Đã thu"."
            */

            // 1. Dọn dẹp dữ liệu cũ
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @"
                    DELETE FROM tblqldt_invoice;
                    DELETE FROM tblqldt_member;
                    DELETE FROM tblqldt_contract;
                    DELETE FROM tblqldt_room;"
            };

            _db.exec_sql_cmd(cmd);

            // 2. Tạo phòng mẫu
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);

            // 3. Tạo hợp đồng mẫu
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 3000000, 4000, 10000, 15000, 15000);

            // 4. Tạo phiếu thu tháng 2/2026 với trạng thái 'CHUA_THU'
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', 2, 2026, 1000000,
             100, 120, 4000, 80000,
             10, 15, 10000, 50000,
             15000, 15000, 1160000,
             'CHUA_THU', NULL, 0)";

            _db.exec_sql_cmd(cmd);

            // 5. Thực thi và kiểm tra ngoại lệ
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    2,
                    2026,
                    100,    
                    120,    
                    10,     
                    15,    
                    "CHUA_THU", 
                    false);

                Assert.Fail("Phiếu thu chưa được thanh toán. Chỉ được phép cập nhật sang trạng thái \"Đã thu\".");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Phiếu thu chưa được thanh toán. Chỉ được phép cập nhật sang trạng thái \"Đã thu\".", ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_update_past_invoice_CHUA_THU_to_DA_THU_success()
        {
            /*
                Quy định:
                    1. Phiếu thu trong cơ sở dữ liệu có trạng thái 'CHUA_THU'.
                    2. Cập nhật trạng thái hợp lệ sang 'DA_THU'.

                Kết quả:
                    Cập nhật thành công phiếu thu sang trạng thái 'DA_THU'.
            */

            // 1. Dọn dẹp dữ liệu cũ
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;"
            };

            _db.exec_sql_cmd(cmd);

            // 2. Tạo phòng mẫu
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);

            // 3. Tạo hợp đồng mẫu
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 3000000, 4000, 10000, 15000, 15000);

            // 4. Tạo phiếu thu tháng 2/2026 với trạng thái 'CHUA_THU'
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', 2, 2026, 1000000,
             100, 120, 4000, 80000,
             10, 15, 10000, 50000,
             15000, 15000, 1160000,
             'CHUA_THU', NULL, 0)";

            _db.exec_sql_cmd(cmd);

            // 5. Thực thi cập nhật sang 'DA_THU'
            _invoiceService.tblqldt_invoice_update("HD001", 2, 2026, 100, 120, 10, 15, "DA_THU", false);

            // 6. Kiểm tra database có invoice tháng 2
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"SELECT COUNT(*)
                                FROM tblqldt_invoice
                                WHERE contract_nbr = 'HD001'
                                  AND billing_month = 2
                                  AND billing_year = 2026";
            var dt = _db.exec_sql_cmd(cmd);
            Assert.AreEqual(1, Convert.ToInt32(dt.Rows[0][0]));
        }
        #endregion

        #region tblqldt_invoice_update_invoice_previous_month_invoice
        [TestMethod]
        public void tblqldt_invoice_update_previous_month_current_month_paid_change_reading_fail()
        {
            /*
                Quy định:
                    1. Có phòng đang thuê.
                    2. Có hợp đồng đang hiệu lực.
                    3. Có phiếu thu tháng trước của hợp đồng.
                    4. Phiếu thu tháng trước chưa thanh toán.
                    5. Có phiếu thu tháng hiện tại.
                    6. Phiếu thu tháng hiện tại đã thanh toán.
                    7. Người dùng cập nhật lại chỉ số điện hoặc nước
                       của phiếu thu tháng trước.

                Kết quả:
                    - Không cho phép cập nhật chỉ số điện nước.
                    - Báo lỗi:
                      "Phiếu thu tháng hiện tại đã thanh toán,
                       không được cập nhật chỉ số điện nước của phiếu thu tháng trước."
            */


            SqlCommand cmd = new SqlCommand();

            // Xóa dữ liệu cũ
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 1. Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M,5);

            // 2. Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Tạo phiếu thu tháng trước
            // 06/2026
            // CHUA_THU
            
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr,
            billing_month,
            billing_year,
            room_price,
            old_electric_reading,
            new_electric_reading,
            electric_price,
            electric_amount,
            old_water_reading,
            new_water_reading,
            water_price,
            water_amount,
            trash_fee,
            security_fee,
            total_amount,
            payment_status,
            collection_date,
            is_checkout
        )
        VALUES
        (
            'HD001',
            6,
            2026,
            1000000,
            100,
            150,
            4000,
            200000,
            50,
            80,
            10000,
            300000,
            15000,
            15000,
            1530000,
            'CHUA_THU',
            NULL,
            0
        );";
            _db.exec_sql_cmd(cmd);

            // 4. Tạo phiếu thu tháng hiện tại
            // 07/2026
            // DA_THU    
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr,
            billing_month,
            billing_year,
            room_price,
            old_electric_reading,
            new_electric_reading,
            electric_price,
            electric_amount,
            old_water_reading,
            new_water_reading,
            water_price,
            water_amount,
            trash_fee,
            security_fee,
            total_amount,
            payment_status,
            collection_date,
            is_checkout
        )
        VALUES
        (
            'HD001',
            7,　
            2026,
            1000000,
            150,
            200,
            4000,
            200000,
            80,
            100,
            10000,
            200000,
            15000,
            15000,
            1430000,
            'DA_THU',
            GETDATE(),
            0
        );";
            _db.exec_sql_cmd(cmd);

            // 5. Update phiếu thu tháng trước
            // Thay đổi chỉ số điện nước           
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    6, 

                    2026,
                    100,    // old electric
                    160,    // new electric thay đổi
                    50,     // old water
                    90,     // new water thay đổi
                    "CHUA_THU",
                    false,
                    new DateTime(2026,7,15)
                );
                Assert.Fail(
                    "Phải báo lỗi vì phiếu thu tháng hiện tại đã thanh toán.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Phiếu thu tháng hiện tại đã thanh toán, không được cập nhật chỉ số điện nước của phiếu thu tháng trước.",
                    ex.Message.Trim());
            }

            // 6. Kiểm tra dữ liệu không bị thay đổi
            cmd.CommandText = @"SELECT new_electric_reading,
                                       new_water_reading
                                FROM tblqldt_invoice
                                WHERE contract_nbr='HD001'
                                    AND billing_month=6
                                    AND billing_year=2026;";
            DataTable dt = _db.exec_sql_cmd(cmd);
            Assert.AreEqual(150, Convert.ToInt32(dt.Rows[0]["new_electric_reading"]));
            Assert.AreEqual(80, Convert.ToInt32(dt.Rows[0]["new_water_reading"]));
        }

        [TestMethod]
        public void tblqldt_invoice_update_previous_month_paid_to_unpaid_fail()
        {
            /*
                Quy định:
                    1. Có hợp đồng.
                    2. Có phiếu thu tháng trước đã thanh toán.
                    3. Cập nhật trạng thái từ DA_THU sang CHUA_THU.

                Kết quả:
                    - Không cho phép cập nhật.
                    - Báo lỗi:
                      "Không được chuyển trạng thái phiếu thu từ Đã thu sang Chưa thu."
            */

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"
        DELETE FROM tblqldt_invoice;
        DELETE FROM tblqldt_member;
        DELETE FROM tblqldt_contract;
        DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);


            // Tạo phòng
            _roomService.tblqldt_room_insert(
                "P001",
                "Phòng 101",
                "TRONG",
                20.5M,
                5);


            // Tạo hợp đồng
            _contractService.tblqldt_contract_insert(
                "HD001",
                "P001",
                1000000,
                1000000,
                4000,
                10000,
                15000,
                15000);


            // Tạo phiếu thu tháng trước đã thu
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr,
            billing_month,
            billing_year,
            room_price,
            old_electric_reading,
            new_electric_reading,
            electric_price,
            electric_amount,
            old_water_reading,
            new_water_reading,
            water_price,
            water_amount,
            trash_fee,
            security_fee,
            total_amount,
            payment_status,
            collection_date,
            is_checkout
        )
        VALUES
        (
            'HD001',
            6,
            2026,
            1000000,
            100,
            150,
            4000,
            200000,
            50,
            80,
            10000,
            300000,
            15000,
            15000,
            1530000,
            'DA_THU',
            GETDATE(),
            0
        );";

            _db.exec_sql_cmd(cmd);

            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    6,
                    2026,
                    100,
                    150,
                    50,
                    80,
                    "CHUA_THU",
                    false,
                    new DateTime(2026, 7, 15));

                Assert.Fail("Phải báo lỗi khi chuyển ĐÃ THU sang CHƯA THU.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Không được chuyển trạng thái phiếu thu từ \"Đã thu\" sang \"Chưa thu\".",
                    ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_update_previous_month_already_paid_fail()
        {
            /*
                Quy định:
                    1. Có hợp đồng.
                    2. Có phiếu thu tháng trước.
                    3. Phiếu thu tháng trước đã thanh toán.
                    4. Người dùng tiếp tục gửi trạng thái DA_THU.

                Kết quả:
                    - Không cập nhật.
                    - Báo lỗi:
                      "Phiếu thu đã được thanh toán trước đó. Vui lòng tải lại dữ liệu."
            */

            SqlCommand cmd = new SqlCommand();

            // Xóa dữ liệu
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"
        DELETE FROM tblqldt_invoice;
        DELETE FROM tblqldt_member;
        DELETE FROM tblqldt_contract;
        DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);

            // Tạo phòng
            _roomService.tblqldt_room_insert(
                "P001",
                "Phòng 101",
                "TRONG",
                20.5M,
                5);

            // Tạo hợp đồng
            _contractService.tblqldt_contract_insert(
                "HD001",
                "P001",
                1000000,
                1000000,
                4000,
                10000,
                15000,
                15000);

            // Insert phiếu thu tháng trước đã thanh toán
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr,
            billing_month,
            billing_year,
            room_price,
            old_electric_reading,
            new_electric_reading,
            electric_price,
            old_water_reading,
            new_water_reading,
            water_price,
            trash_fee,
            security_fee,
            total_amount,
            payment_status,
            collection_date,
            is_checkout
        )
        VALUES
        (
            'HD001',
            6,
            2026,
            1000000,
            100,
            150,
            4000,
            50,
            80,
            10000,
            15000,
            15000,
            1530000,
            'DA_THU',
            GETDATE(),
            0
        );";
            _db.exec_sql_cmd(cmd);

            // Update lại trạng thái DA_THU
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    6,
                    2026,

                    100,
                    150,

                    50,
                    80,

                    "DA_THU",

                    false);


                Assert.Fail(
                    "Phải báo lỗi vì phiếu thu đã thanh toán trước đó.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Phiếu thu đã được thanh toán trước đó. Vui lòng tải lại dữ liệu.",
                    ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_update_previous_month_unpaid_change_reading_success()
        {
            /*
                Quy định:
                    1. Có hợp đồng.
                    2. Phiếu thu tháng trước ở trạng thái CHUA_THU.
                    3. Phiếu thu tháng hiện tại ở trạng thái CHUA_THU.
                    4. Chỉ số điện mới >= chỉ số điện cũ.
                    5. Chỉ số nước mới >= chỉ số nước cũ.
                    6. Người dùng cập nhật chỉ số điện, nước của tháng trước.

                Kết quả:
                    1. Cập nhật thành công.
                    2. Tính lại tiền điện.
                    3. Tính lại tiền nước.
                    4. Tính lại tổng tiền.
                    5. Cập nhật chỉ số đầu kỳ của phiếu thu tháng hiện tại.
            */

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);

            // Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000,　15000,　15000);

            // Phiếu thu tháng trước chưa thu
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr,
            billing_month,
            billing_year,
            room_price,
            old_electric_reading,
            new_electric_reading,
            electric_price,
            old_water_reading,
            new_water_reading,
            water_price,
            trash_fee,
            security_fee,
            total_amount,
            payment_status,
            is_checkout
        )
        VALUES
        (
            'HD001',
            6,
            2026,
            1000000,
            100,
            150,
            4000,
            50,
            80,
            10000,
            15000,
            15000,
            1530000,
            'CHUA_THU',
            0
        );";

            _db.exec_sql_cmd(cmd);

            // Phiếu thu tháng hiện tại chưa thu
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr,
            billing_month,
            billing_year,
            room_price,
            old_electric_reading,
            new_electric_reading,
            electric_price,
            old_water_reading,
            new_water_reading,
            water_price,
            trash_fee,
            security_fee,
            total_amount,
            payment_status,
            is_checkout
        )
        VALUES
        (
            'HD001',
            7,
            2026,
            1000000,
            150,
            200,
            4000,
            80,
            100,
            10000,
            15000,
            15000,
            1430000,
            'CHUA_THU',
            0
        );";

            _db.exec_sql_cmd(cmd);


            // Update chỉ số tháng trước
            var result = _invoiceService.tblqldt_invoice_update(
                "HD001",
                6,
                2026,
                100,
                160,
                50,
                90,
                "CHUA_THU",
                false,
                new DateTime(2026, 7, 15));


            // Kiểm tra cập nhật thành công
            Assert.IsNotNull(result);

            // Kiểm tra dữ liệu sau update
            cmd.CommandText = @"
        SELECT 
            new_electric_reading,
            new_water_reading,
            electric_amount,
            water_amount,
            total_amount
        FROM tblqldt_invoice
        WHERE contract_nbr='HD001'
        AND billing_month=6
        AND billing_year=2026;";

            DataTable dt = _db.exec_sql_cmd(cmd);

            Assert.AreEqual(160, Convert.ToInt32(dt.Rows[0]["new_electric_reading"]));
            Assert.AreEqual(90, Convert.ToInt32(dt.Rows[0]["new_water_reading"]));

            Assert.AreEqual(240000, Convert.ToDecimal(dt.Rows[0]["electric_amount"]));
            Assert.AreEqual(400000, Convert.ToDecimal(dt.Rows[0]["water_amount"]));
            Assert.AreEqual(1170000, Convert.ToDecimal(dt.Rows[0]["total_amount"]));
        }
     
        [TestMethod]
        public void tblqldt_invoice_update_previous_month_electric_reading_invalid_fail()
        {
            /*
                Quy định:
                    1. Có hợp đồng.
                    2. Có phiếu thu tháng trước chưa thanh toán.
                    3. Chỉ số điện mới nhỏ hơn chỉ số điện cũ.

                Kết quả:
                    - Không cập nhật.
                    - Báo lỗi:
                      "Chỉ số điện mới phải lớn hơn hoặc bằng chỉ số điện cũ."
            */

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"
        DELETE FROM tblqldt_invoice;
        DELETE FROM tblqldt_member;
        DELETE FROM tblqldt_contract;
        DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);


            // Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);

            // Tạo hợp đồng
            _contractService.tblqldt_contract_insert(
                "HD001",
                "P001",
                1000000,
                1000000,
                4000,
                10000,
                15000,
                15000);


            // Phiếu thu tháng trước chưa thanh toán
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr,
            billing_month,
            billing_year,
            room_price,
            old_electric_reading,
            new_electric_reading,
            electric_price,
            old_water_reading,
            new_water_reading,
            water_price,
            trash_fee,
            security_fee,
            total_amount,
            payment_status,
            is_checkout
        )
        VALUES
        (
            'HD001',
            6,
            2026,
            1000000,
            100,
            150,
            4000,
            50,
            80,
            10000,
            15000,
            15000,
            1530000,
            'CHUA_THU',
            0
        );";

            _db.exec_sql_cmd(cmd);


            try
            {
                // Điện mới < điện cũ
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    6,
                    2026,
                    100,
                    90,
                    50,
                    80,
                    "CHUA_THU",
                    false,
                    new DateTime(2026, 7, 15));

                Assert.Fail(
                    "Phải báo lỗi khi chỉ số điện mới nhỏ hơn chỉ số điện cũ.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Chỉ số điện mới phải lớn hơn hoặc bằng chỉ số điện cũ.",
                    ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_update_previous_month_water_reading_invalid_fail()
        {
            /*
                Quy định:
                    1. Có hợp đồng.
                    2. Có phiếu thu tháng trước chưa thanh toán.
                    3. Chỉ số nước mới nhỏ hơn chỉ số nước cũ.

                Kết quả:
                    - Không cập nhật.
                    - Báo lỗi:
                      "Chỉ số nước mới phải lớn hơn hoặc bằng chỉ số nước cũ."
            */

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"
        DELETE FROM tblqldt_invoice;
        DELETE FROM tblqldt_member;
        DELETE FROM tblqldt_contract;
        DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);


            // Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);


            // Tạo hợp đồng
            _contractService.tblqldt_contract_insert(
                "HD001",
                "P001",
                1000000,
                1000000,
                4000,
                10000,
                15000,
                15000);


            // Phiếu thu tháng trước chưa thanh toán
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr,
            billing_month,
            billing_year,
            room_price,
            old_electric_reading,
            new_electric_reading,
            electric_price,
            old_water_reading,
            new_water_reading,
            water_price,
            trash_fee,
            security_fee,
            total_amount,
            payment_status,
            is_checkout
        )
        VALUES
        (
            'HD001',
            6,
            2026,
            1000000,
            100,
            150,
            4000,
            50,
            80,
            10000,
            15000,
            15000,
            1530000,
            'CHUA_THU',
            0
        );";

            _db.exec_sql_cmd(cmd);


            try
            {
                // Nước mới < nước cũ
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    6,
                    2026,
                    100,
                    150,
                    50,
                    40,
                    "CHUA_THU",
                    false,
                    new DateTime(2026,7,15));

                Assert.Fail(
                    "Phải báo lỗi khi chỉ số nước mới nhỏ hơn chỉ số nước cũ.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Chỉ số nước mới phải lớn hơn hoặc bằng chỉ số nước cũ.",
                    ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_update_previous_month_checkout_unpaid_fail()
        {
            /*
                Quy định:
                    1. Có hợp đồng.
                    2. Có phiếu thu tháng trước chưa thanh toán.
                    3. Người dùng chọn trả phòng.

                Kết quả:
                    - Không cho trả phòng.
                    - Không cập nhật is_checkout.
                    - Báo lỗi:
                      "Không thể trả phòng khi phiếu thu chưa thanh toán."
            */


            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"
        DELETE FROM tblqldt_invoice;
        DELETE FROM tblqldt_member;
        DELETE FROM tblqldt_contract;
        DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);


            // Tạo phòng
            _roomService.tblqldt_room_insert(
                "P001",
                "Phòng 101",
                "TRONG",
                20.5M,
                5);


            // Tạo hợp đồng
            _contractService.tblqldt_contract_insert(
                "HD001",
                "P001",
                1000000,
                1000000,
                4000,
                10000,
                15000,
                15000);

            // Phiếu thu tháng trước chưa thanh toán
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (
                contract_nbr,
                billing_month,
                billing_year,

                room_price,

                old_electric_reading,
                new_electric_reading,
                electric_price,
                electric_amount,

                old_water_reading,
                new_water_reading,
                water_price,
                water_amount,

                trash_fee,
                security_fee,
                total_amount,

                payment_status,
                collection_date,
                is_checkout
            )
            VALUES
            (
                'HD001',
                6,
                2026,
                1000000,
                100,
                150,
                4000,
                200000,
                50,
                80,
                10000,
                300000,
                15000,
                15000,
                1530000,
                'CHUA_THU',
                NULL,
                0
            );";

            _db.exec_sql_cmd(cmd);
            try
            {
                // Người dùng chọn trả phòng
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    6,
                    2026,
                    100,
                    150,
                    50,
                    80,
                    "CHUA_THU",
                    true,
                    new DateTime(2026, 7, 15));


                Assert.Fail(
                    "Phải báo lỗi khi trả phòng nhưng phiếu thu chưa thanh toán.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Không thể trả phòng khi phiếu thu chưa thanh toán.",
                    ex.Message.Trim());
            }
        }

        [TestMethod]
        public void tblqldt_invoice_update_previous_month_payment_success_sync_current_month()
        {
            /*
                Quy định:
                    1. Có hợp đồng.
                    2. Phiếu thu tháng trước chưa thanh toán.
                    3. Người dùng chuyển trạng thái sang DA_THU.
                    4. Chỉ số điện nước hợp lệ.
                    5. Phiếu thu tháng hiện tại chưa thanh toán.

                Kết quả:
                    - Phiếu thu tháng trước chuyển sang DA_THU.
                    - Cập nhật ngày thu.
                    - Đồng bộ chỉ số sang tháng hiện tại.
            */

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"
        DELETE FROM tblqldt_invoice;
        DELETE FROM tblqldt_member;
        DELETE FROM tblqldt_contract;
        DELETE FROM tblqldt_room;";

            _db.exec_sql_cmd(cmd);


            // Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);


            // Tạo hợp đồng
            _contractService.tblqldt_contract_insert(
                "HD001",
                "P001",
                1000000,
                1000000,
                4000,
                10000,
                15000,
                15000);

            // Phiếu thu tháng trước chưa thu
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr,
            billing_month,
            billing_year,
            room_price,
            old_electric_reading,
            new_electric_reading,
            electric_price,
            electric_amount,
            old_water_reading,
            new_water_reading,
            water_price,
            water_amount,
            trash_fee,
            security_fee,
            total_amount,
            payment_status,
            collection_date,
            is_checkout
        )
        VALUES
        (
            'HD001',
            6,
            2026,
            1000000,
            100,
            150,
            4000,
            200000,
            50,
            80,
            10000,
            300000,
            15000,
            15000,
            1530000,
            'CHUA_THU',
            NULL,
            0
        );";

            _db.exec_sql_cmd(cmd);


            // Phiếu thu tháng hiện tại chưa thu
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr,
            billing_month,
            billing_year,
            room_price,
            old_electric_reading,
            new_electric_reading,
            electric_price,
            electric_amount,
            old_water_reading,
            new_water_reading,
            water_price,
            water_amount,
            trash_fee,
            security_fee,
            total_amount,
            payment_status,
            collection_date,
            is_checkout
        )
        VALUES
        (
            'HD001',
            7,
            2026,
            1000000,
            150,
            200,
            4000,
            200000,
            80,
            100,
            10000,
            200000,
            15000,
            15000,
            1430000,
            'CHUA_THU',
            NULL,
            0
        );";

            _db.exec_sql_cmd(cmd);


            // Thu tiền tháng trước
            _invoiceService.tblqldt_invoice_update(
                "HD001",
                6,
                2026,
                100,
                150,
                50,
                80,
                "DA_THU",
                false);


            // Kiểm tra tháng trước đã thu
            cmd.CommandText = @"
        SELECT 
            payment_status,
            collection_date
        FROM tblqldt_invoice
        WHERE contract_nbr = 'HD001'
          AND billing_month = 6
          AND billing_year = 2026;";

            DataTable dt = _db.exec_sql_cmd(cmd);

            Assert.AreEqual(
                "DA_THU",
                dt.Rows[0]["payment_status"].ToString());

            Assert.IsFalse(
                Convert.IsDBNull(dt.Rows[0]["collection_date"]));


            // Kiểm tra đồng bộ sang tháng hiện tại
            cmd.CommandText = @"
        SELECT 
            old_electric_reading,
            old_water_reading
        FROM tblqldt_invoice
        WHERE contract_nbr = 'HD001'
          AND billing_month = 7
          AND billing_year = 2026;";

            dt = _db.exec_sql_cmd(cmd);

            Assert.AreEqual(
                150,
                Convert.ToInt32(dt.Rows[0]["old_electric_reading"]));

            Assert.AreEqual(
                80,
                Convert.ToInt32(dt.Rows[0]["old_water_reading"]));
        }

        [TestMethod]
        public void tblqldt_invoice_update_previous_month_checkout_fail()
        {
            /*
                Quy định:
                    1. Có hợp đồng.
                    2. Phiếu thu đang cập nhật thuộc tháng trước.
                    3. Người dùng chọn trả phòng.

                Kết quả:
                    - Không cho trả phòng.
                    - Chỉ được trả phòng trên phiếu thu tháng hiện tại.
            */

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);

            // Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // Phiếu thu tháng trước
            cmd.CommandText = @"
        INSERT INTO tblqldt_invoice
        (
            contract_nbr,
            billing_month,
            billing_year,
            room_price,
            old_electric_reading,
            new_electric_reading,
            electric_price,
            electric_amount,
            old_water_reading,
            new_water_reading,
            water_price,
            water_amount,
            trash_fee,
            security_fee,
            total_amount,
            payment_status,
            collection_date,
            is_checkout
        )
        VALUES
        (
            'HD001',
            6,
            2026,
            1000000,
            100,
            150,
            4000,
            200000,
            50,
            80,
            10000,
            300000,
            15000,
            15000,
            1530000,
            'DA_THU',
            GETDATE(),
            0
        );";

            _db.exec_sql_cmd(cmd);

            try
            {
                // Trả phòng trên phiếu tháng trước
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    6,
                    2026,
                    100,
                    150,
                    50,
                    80,
                    "DA_THU",
                    true,
                    new DateTime(2026, 7, 15));


                Assert.Fail(
                    "Phải báo lỗi vì chỉ được trả phòng trên phiếu thu tháng hiện tại.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Chỉ được trả phòng trên phiếu thu của tháng hiện tại.",
                    ex.Message.Trim());
            }
        }
        #endregion

        #region tblqldt_invoice_update_current_month_invoice()

        [TestMethod]
        public void tblqldt_invoice_update_current_month_paid_edit_fail()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng hiện tại.
                    2. Phiếu thu đã thanh toán.
                    3. Người dùng không chọn trả phòng.

                Kết quả:
                    - Không cho phép chỉnh sửa phiếu thu.
                    - Báo lỗi:
                      "Phiếu thu đã thanh toán, không được chỉnh sửa."
            */

            SqlCommand cmd = new SqlCommand();

            // Xóa dữ liệu cũ
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 1. Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 1", "TRONG", 20.5M, 5);

            // 2. Tạo hợp đồng

            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Tạo phiếu thu tháng hiện tại đã thanh toán
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', MONTH(GETDATE()), YEAR(GETDATE()), 1000000,
             100, 150, 4000, 200000,
             50, 80, 10000, 300000,
             15000, 15000, 1530000,
             'DA_THU', GETDATE(), 0)";

            _db.exec_sql_cmd(cmd);

            // 4. Cập nhật phiếu thu
            try
            {
                _invoiceService.tblqldt_invoice_update("HD001",
                    DateTime.Now.Month,
                    DateTime.Now.Year,
                    100, 160,
                    50, 90,
                    "DA_THU",
                    false);

                Assert.Fail("Phải báo lỗi vì phiếu thu đã thanh toán.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Phiếu thu đã thanh toán, không được chỉnh sửa.", ex.Message.Trim());
            }

            // 5. Kiểm tra dữ liệu không bị thay đổi
            cmd.CommandText = @"SELECT new_electric_reading, new_water_reading
                                FROM tblqldt_invoice
                                WHERE contract_nbr='HD001'
                                  AND billing_month = MONTH(GETDATE())
                                  AND billing_year = YEAR(GETDATE());";
            DataTable dt = _db.exec_sql_cmd(cmd);

            Assert.AreEqual(150, Convert.ToInt32(dt.Rows[0]["new_electric_reading"]));
            Assert.AreEqual(80, Convert.ToInt32(dt.Rows[0]["new_water_reading"]));
        }

        [TestMethod]
        public void tblqldt_invoice_update_current_month_paid_checkout_previous_unpaid_fail()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng hiện tại.
                    2. Phiếu thu đã thanh toán.
                    3. Người dùng chọn trả phòng.
                    4. Còn phiếu thu các tháng trước chưa thanh toán.

                Kết quả:
                    - Không cho phép trả phòng.
                    - Báo lỗi:
                      "Không thể trả phòng vì còn phiếu thu các tháng trước chưa thanh toán."
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();         
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 1. Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);

            // 2. Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Tạo phiếu thu tháng trước - CHUA_THU
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', MONTH(DATEADD(MONTH,-1,GETDATE())), YEAR(DATEADD(MONTH,-1,GETDATE())), 1000000,
             100, 150, 4000, 200000,
             50, 80, 10000, 300000,
             15000, 15000, 1530000,
             'CHUA_THU', NULL, 0)";
            _db.exec_sql_cmd(cmd);

            // 4. Tạo phiếu thu tháng hiện tại - DA_THU
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', MONTH(GETDATE()), YEAR(GETDATE()), 1000000,
             150, 200, 4000, 200000,
             80, 100, 10000, 200000,
             15000, 15000, 1430000,
             'DA_THU', GETDATE(), 0)";
            _db.exec_sql_cmd(cmd);

            // 5. Trả phòng
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    DateTime.Now.Month,
                    DateTime.Now.Year,
                    150,
                    200,
                    80,
                    100,
                    "DA_THU",
                    true);

                Assert.Fail("Phải báo lỗi vì còn phiếu thu tháng trước chưa thanh toán.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Không thể trả phòng vì còn phiếu thu các tháng trước chưa thanh toán.",
                    ex.Message.Trim());
            }

            // 6. Kiểm tra dữ liệu không thay đổi
            cmd.CommandText = @"
        SELECT is_checkout
        FROM tblqldt_invoice
        WHERE contract_nbr='HD001'
          AND billing_month = MONTH(GETDATE())
          AND billing_year = YEAR(GETDATE());";

            DataTable dt = _db.exec_sql_cmd(cmd);

            Assert.AreEqual(false, Convert.ToBoolean(dt.Rows[0]["is_checkout"]));
        }

        [TestMethod]
        public void tblqldt_invoice_update_current_month_paid_checkout_success()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng hiện tại.
                    2. Phiếu thu đã thanh toán.
                    3. Người dùng chọn trả phòng.
                    4. Không còn phiếu thu các tháng trước chưa thanh toán.

                Kết quả:
                    - Trả phòng thành công.
                    - Cập nhật ngày kết thúc hợp đồng.
                    - Chuyển phòng sang trạng thái TRONG.
                    - Chuyển toàn bộ thành viên sang CHUYEN_DI.
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();           
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 1. Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);

            // 2. Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Thêm thành viên
            _contractService.tblqldt_member_insert("TV001", "HD001", "Nguyễn Văn A", true, "079123456789", new DateTime(2000, 1, 1), "NAM", "0901234567", "Bình Dương", "DANG_O");

            // 4. Tạo phiếu thu tháng hiện tại đã thanh toán
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', MONTH(GETDATE()), YEAR(GETDATE()), 1000000,
             100, 150, 4000, 200000,
             50, 80, 10000, 300000,
             15000, 15000, 1530000,
             'DA_THU', GETDATE(), 0)";
                        
            _db.exec_sql_cmd(cmd);

            // 5. Trả phòng
            DataTable dt = _invoiceService.tblqldt_invoice_update(
                "HD001",
                DateTime.Now.Month,
                DateTime.Now.Year,
                100,
                150,
                50,
                80,
                "DA_THU",
                true);

            Assert.AreEqual(1, Convert.ToInt32(dt.Rows[0]["status"]));
            Assert.AreEqual("Trả phòng thành công.", dt.Rows[0]["msg"].ToString());

            // 6. Kiểm tra hợp đồng
            cmd.CommandText = @"SELECT end_date
                                FROM tblqldt_contract
                                WHERE contract_nbr = 'HD001';";
            DataTable dtContract = _db.exec_sql_cmd(cmd);

            Assert.IsNotNull(dtContract.Rows[0]["end_date"]);
            Assert.AreEqual( DateTime.Today,
                Convert.ToDateTime(dtContract.Rows[0]["end_date"]).Date);

            // 7. Kiểm tra trạng thái phòng
            cmd.CommandText = @"SELECT status
                                FROM tblqldt_room
                                WHERE room_nbr = 'P001';";
            DataTable dtRoom = _db.exec_sql_cmd(cmd);

            Assert.AreEqual("TRONG", dtRoom.Rows[0]["status"].ToString());

            // 8. Kiểm tra thành viên
            cmd.CommandText = @"SELECT status, is_primary_tenant
                                FROM tblqldt_member
                                WHERE contract_nbr = 'HD001';";
            DataTable dtMember = _db.exec_sql_cmd(cmd);

            Assert.AreEqual("CHUYEN_DI", dtMember.Rows[0]["status"].ToString());
            Assert.AreEqual(false, Convert.ToBoolean(dtMember.Rows[0]["is_primary_tenant"]));

            // 9. Kiểm tra phiếu thu
            cmd.CommandText = @"SELECT is_checkout
                                FROM tblqldt_invoice
                                WHERE contract_nbr = 'HD001'
                                    AND billing_month = MONTH(GETDATE())
                                    AND billing_year = YEAR(GETDATE());";
            DataTable dtInvoice = _db.exec_sql_cmd(cmd);
            Assert.AreEqual(true, Convert.ToBoolean(dtInvoice.Rows[0]["is_checkout"]));
        }

        [TestMethod]
        public void tblqldt_invoice_update_current_month_unpaid_checkout_fail()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng hiện tại.
                    2. Phiếu thu chưa thanh toán.
                    3. Người dùng chưa thu tiền.
                    4. Người dùng chọn trả phòng.

                Kết quả:
                    - Không cho phép trả phòng.
                    - Báo lỗi:
                      "Không thể trả phòng khi phiếu thu chưa thanh toán."
            */

            SqlCommand cmd = new SqlCommand();

            // Xóa dữ liệu cũ
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 1. Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);

            // 2. Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Thêm thành viên
            _contractService.tblqldt_member_insert("TV001", "HD001", "Nguyễn Văn A", true, "079123456789", new DateTime(2000, 1, 1), "NAM", "0901234567", "Bình Dương", "DANG_O");

            // 4. Tạo phiếu thu tháng hiện tại chưa thanh toán
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', MONTH(GETDATE()), YEAR(GETDATE()), 1000000,
             100, 150, 4000, 200000,
             50, 80, 10000, 300000,
             15000, 15000, 1530000,
             'CHUA_THU', NULL, 0)";
            _db.exec_sql_cmd(cmd);

            // 5. Chọn trả phòng khi chưa thanh toán
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    DateTime.Now.Month,
                    DateTime.Now.Year,
                    100,
                    150,
                    50,
                    80,
                    "CHUA_THU",
                    true);

                Assert.Fail("Phải báo lỗi vì phiếu thu chưa thanh toán.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Không thể trả phòng khi phiếu thu chưa thanh toán.", ex.Message.Trim());
            }

            // 6. Kiểm tra dữ liệu không thay đổi
            cmd.CommandText = @"
        SELECT is_checkout
        FROM tblqldt_invoice
        WHERE contract_nbr = 'HD001'
          AND billing_month = MONTH(GETDATE())
          AND billing_year = YEAR(GETDATE());";

            DataTable dt = _db.exec_sql_cmd(cmd);

            Assert.AreEqual(false, Convert.ToBoolean(dt.Rows[0]["is_checkout"]));
        }

        [TestMethod]
        public void tblqldt_invoice_update_current_month_unpaid_new_electric_reading_less_than_old_fail()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng hiện tại.
                    2. Phiếu thu chưa thanh toán.
                    3. Chỉ số điện mới nhỏ hơn chỉ số điện cũ.

                Kết quả:
                    - Không cập nhật phiếu thu.
                    - Báo lỗi:
                      "Chỉ số điện mới phải lớn hơn hoặc bằng chỉ số điện cũ."
            */

            SqlCommand cmd = new SqlCommand();

            // Xóa dữ liệu cũ
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;                             
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 1. Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);

            // 2. Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Tạo phiếu thu tháng hiện tại
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', MONTH(GETDATE()), YEAR(GETDATE()), 1000000,
             100, 150, 4000, 200000,
             50, 80, 10000, 300000,
             15000, 15000, 1530000,
             'CHUA_THU', NULL, 0)";
            _db.exec_sql_cmd(cmd);

            // 4. Cập nhật phiếu thu
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    DateTime.Now.Month,
                    DateTime.Now.Year,
                    100,    // old electric
                    99,     // new electric < old electric
                    50,     // old water
                    80,     // new water
                    "CHUA_THU",
                    false);

                Assert.Fail("Phải báo lỗi vì chỉ số điện mới nhỏ hơn chỉ số điện cũ.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Chỉ số điện mới phải lớn hơn hoặc bằng chỉ số điện cũ.",
                    ex.Message.Trim());
            }

            // Kiểm tra dữ liệu không thay đổi
            cmd.CommandText = @"SELECT new_electric_reading
                                FROM tblqldt_invoice
                                WHERE contract_nbr='HD001'
                                   AND billing_month = MONTH(GETDATE())
                                   AND billing_year = YEAR(GETDATE());";
            DataTable dt = _db.exec_sql_cmd(cmd);

            Assert.AreEqual(150, Convert.ToInt32(dt.Rows[0]["new_electric_reading"]));
        }

        [TestMethod]
        public void tblqldt_invoice_update_current_month_unpaid_new_water_reading_less_than_old_fail()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng hiện tại.
                    2. Phiếu thu chưa thanh toán.
                    3. Chỉ số nước mới nhỏ hơn chỉ số nước cũ.

                Kết quả:
                    - Không cập nhật phiếu thu.
                    - Báo lỗi:
                      "Chỉ số nước mới phải lớn hơn hoặc bằng chỉ số nước cũ."
            */

            SqlCommand cmd = new SqlCommand();

            // Xóa dữ liệu cũ
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;                              
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 1. Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);

            // 2. Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Tạo phiếu thu tháng hiện tại
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', MONTH(GETDATE()), YEAR(GETDATE()), 1000000,
             100, 150, 4000, 200000,
             50, 80, 10000, 300000,
             15000, 15000, 1530000,
             'CHUA_THU', NULL, 0)";
          
            _db.exec_sql_cmd(cmd);

            // 4. Cập nhật phiếu thu
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    DateTime.Now.Month,
                    DateTime.Now.Year,
                    100,
                    150,
                    50,
                    49,     // new water < old water
                    "CHUA_THU",
                    false);

                Assert.Fail("Phải báo lỗi vì chỉ số nước mới nhỏ hơn chỉ số nước cũ.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Chỉ số nước mới phải lớn hơn hoặc bằng chỉ số nước cũ.",
                    ex.Message.Trim());
            }
            // Kiểm tra dữ liệu không thay đổi
            cmd.CommandText = @"
        SELECT new_electric_reading
        FROM tblqldt_invoice
        WHERE contract_nbr='HD001'
          AND billing_month = MONTH(GETDATE())
          AND billing_year = YEAR(GETDATE());";

            DataTable dt = _db.exec_sql_cmd(cmd);

            Assert.AreEqual(150, Convert.ToInt32(dt.Rows[0]["new_electric_reading"]));
        }

        [TestMethod]
        public void tblqldt_invoice_update_current_month_unpaid_update_success()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng hiện tại.
                    2. Phiếu thu chưa thanh toán.
                    3. Người dùng chưa thu tiền.
                    4. Chỉ số điện và nước hợp lệ.

                Kết quả:
                    - Cập nhật chỉ số điện, nước.
                    - Tính lại tiền điện, nước và tổng tiền.
                    - Cập nhật phiếu thu thành công.
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 1. Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);

            // 2. Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Tạo phiếu thu tháng hiện tại
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', MONTH(GETDATE()), YEAR(GETDATE()), 1000000,
             100, 150, 4000, 200000,
             50, 80, 10000, 300000,
             15000, 15000, 1530000,
             'CHUA_THU', NULL, 0)";
            
            _db.exec_sql_cmd(cmd);

            // 4. Cập nhật phiếu thu
            DataTable actual = _invoiceService.tblqldt_invoice_update(
                "HD001",
                DateTime.Now.Month,
                DateTime.Now.Year,
                100,
                180,
                50,
                90,
                "CHUA_THU",
                false);

            Assert.AreEqual(1, Convert.ToInt32(actual.Rows[0]["status"]));
            Assert.AreEqual("Cập nhật phiếu thu thành công.", actual.Rows[0]["msg"].ToString());

            // 5. Kiểm tra dữ liệu
            cmd.CommandText = @"SELECT new_electric_reading,
                                        electric_amount,
                                        new_water_reading,
                                        water_amount,
                                        total_amount          
                                FROM tblqldt_invoice
                                WHERE contract_nbr='HD001'
                                  AND billing_month = MONTH(GETDATE())
                                  AND billing_year = YEAR(GETDATE());";
            DataTable dt = _db.exec_sql_cmd(cmd);

            Assert.AreEqual(180, Convert.ToInt32(dt.Rows[0]["new_electric_reading"]));
            Assert.AreEqual(320000, Convert.ToDecimal(dt.Rows[0]["electric_amount"]));
            Assert.AreEqual(90, Convert.ToInt32(dt.Rows[0]["new_water_reading"]));
            Assert.AreEqual(400000, Convert.ToDecimal(dt.Rows[0]["water_amount"]));

            // Tổng tiền:
            // 1.000.000 + 320.000 + 400.000 + 15.000 + 15.000 = 1.750.000
            Assert.AreEqual(1750000, Convert.ToDecimal(dt.Rows[0]["total_amount"]));
        }

        [TestMethod]
        public void tblqldt_invoice_update_current_month_collect_success()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng hiện tại.
                    2. Phiếu thu chưa thanh toán.
                    3. Người dùng xác nhận thu tiền.
                    4. Không chọn trả phòng.
                    5. Chỉ số điện và nước hợp lệ.

                Kết quả:
                    - Thu tiền thành công.
                    - Cập nhật trạng thái DA_THU.
                    - Cập nhật ngày thu tiền.
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();         
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 1. Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);

            // 2. Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Tạo phiếu thu tháng hiện tại chưa thanh toán
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', MONTH(GETDATE()), YEAR(GETDATE()), 1000000,
             100, 150, 4000, 200000,
             50, 80, 10000, 300000,
             15000, 15000, 1530000,
             'CHUA_THU', NULL, 0)";
          
            _db.exec_sql_cmd(cmd);

            // 4. Thu tiền
            DataTable actual = _invoiceService.tblqldt_invoice_update(
                "HD001",
                DateTime.Now.Month,
                DateTime.Now.Year,
                100,
                180,
                50,
                90,
                "DA_THU",
                false);

            Assert.AreEqual(1, Convert.ToInt32(actual.Rows[0]["status"]));
            Assert.AreEqual("Thu tiền và cập nhật phiếu thành công.", actual.Rows[0]["msg"].ToString());

            // 5. Kiểm tra dữ liệu
            cmd.CommandText = @"SELECT
                                    new_electric_reading,
                                    new_water_reading,
                                    electric_amount,
                                    water_amount,
                                    total_amount,
                                    payment_status,
                                    collection_date,
                                    is_checkout
                                FROM tblqldt_invoice
                                WHERE contract_nbr = 'HD001'
                                  AND billing_month = MONTH(GETDATE())
                                  AND billing_year = YEAR(GETDATE());";
            DataTable dt = _db.exec_sql_cmd(cmd);

            Assert.AreEqual(180, Convert.ToInt32(dt.Rows[0]["new_electric_reading"]));
            Assert.AreEqual(90, Convert.ToInt32(dt.Rows[0]["new_water_reading"]));
            Assert.AreEqual(320000M, Convert.ToDecimal(dt.Rows[0]["electric_amount"]));
            Assert.AreEqual(400000M, Convert.ToDecimal(dt.Rows[0]["water_amount"]));

            // 1.000.000 + 320.000 + 400.000 + 15.000 + 15.000
            Assert.AreEqual(1750000M, Convert.ToDecimal(dt.Rows[0]["total_amount"]));
            Assert.AreEqual("DA_THU", dt.Rows[0]["payment_status"].ToString());
            Assert.IsNotNull(dt.Rows[0]["collection_date"]);
            Assert.AreEqual(false, Convert.ToBoolean(dt.Rows[0]["is_checkout"]));
        }

        [TestMethod]
        public void tblqldt_invoice_update_current_month_collect_checkout_previous_unpaid_fail()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng hiện tại.
                    2. Phiếu thu chưa thanh toán.
                    3. Người dùng xác nhận thu tiền.
                    4. Người dùng chọn trả phòng.
                    5. Còn phiếu thu các tháng trước chưa thanh toán.

                Kết quả:
                    - Không cho phép trả phòng.
                    - Báo lỗi:
                      "Không thể trả phòng vì còn phiếu thu các tháng trước chưa thanh toán."
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();          
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 1. Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);

            // 2. Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Tạo phiếu thu tháng trước - CHUA_THU
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', MONTH(DATEADD(MONTH,-1,GETDATE())), YEAR(DATEADD(MONTH,-1,GETDATE())), 1000000,
             100, 150, 4000, 200000,
             50, 80, 10000, 300000,
             15000, 15000, 1530000,
             'CHUA_THU', NULL, 0)";

            _db.exec_sql_cmd(cmd);

            // 4. Tạo phiếu thu tháng hiện tại - CHUA_THU
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', MONTH(GETDATE()), YEAR(GETDATE()), 1000000,
             150, 200, 4000, 200000,
             80, 100, 10000, 200000,
             15000, 15000, 1430000,
             'CHUA_THU', NULL, 0)";
            
            _db.exec_sql_cmd(cmd);

            // 5. Thu tiền và trả phòng
            try
            {
                _invoiceService.tblqldt_invoice_update(
                    "HD001",
                    DateTime.Now.Month,
                    DateTime.Now.Year,
                    150,
                    200,
                    80,
                    100,
                    "DA_THU",
                    true);

                Assert.Fail("Phải báo lỗi vì còn phiếu thu các tháng trước chưa thanh toán.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(
                    "Không thể trả phòng vì còn phiếu thu các tháng trước chưa thanh toán.",
                    ex.Message.Trim());
            }

            // 6. Kiểm tra phiếu thu hiện tại chưa bị cập nhật
            cmd.CommandText = @"
        SELECT payment_status,
               is_checkout,
               collection_date
        FROM tblqldt_invoice
        WHERE contract_nbr = 'HD001'
          AND billing_month = MONTH(GETDATE())
          AND billing_year = YEAR(GETDATE());";

            DataTable dt = _db.exec_sql_cmd(cmd);

            Assert.AreEqual("CHUA_THU", dt.Rows[0]["payment_status"].ToString());
            Assert.AreEqual(false, Convert.ToBoolean(dt.Rows[0]["is_checkout"]));
            Assert.AreEqual(DBNull.Value, dt.Rows[0]["collection_date"]);
        }

        [TestMethod]
        public void tblqldt_invoice_update_current_month_collect_checkout_success()
        {
            /*
                Quy định:
                    1. Phiếu thu thuộc tháng hiện tại.
                    2. Phiếu thu chưa thanh toán.
                    3. Người dùng xác nhận thu tiền.
                    4. Người dùng chọn trả phòng.
                    5. Không còn phiếu thu các tháng trước chưa thanh toán.

                Kết quả:
                    - Thu tiền thành công.
                    - Trả phòng thành công.
                    - Cập nhật ngày kết thúc hợp đồng.
                    - Chuyển phòng sang trạng thái TRONG.
                    - Chuyển toàn bộ thành viên sang CHUYEN_DI.
            */

            // Xóa dữ liệu cũ
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM tblqldt_invoice;
                                DELETE FROM tblqldt_member;
                                DELETE FROM tblqldt_contract;
                                DELETE FROM tblqldt_room;";
            _db.exec_sql_cmd(cmd);

            // 1. Tạo phòng
            _roomService.tblqldt_room_insert("P001", "Phòng 101", "TRONG", 20.5M, 5);

            // 2. Tạo hợp đồng
            _contractService.tblqldt_contract_insert("HD001", "P001", 1000000, 1000000, 4000, 10000, 15000, 15000);

            // 3. Thêm thành viên
            _contractService.tblqldt_member_insert("TV001", "HD001", "Nguyễn Văn A", true, "079123456789", new DateTime(2000, 1, 1), "NAM", "0901234567", "Bình Dương", "DANG_O");

            // 4. Tạo phiếu thu tháng hiện tại - CHUA_THU
            cmd.CommandText = @"
            INSERT INTO tblqldt_invoice
            (contract_nbr, billing_month, billing_year, room_price,
             old_electric_reading, new_electric_reading, electric_price, electric_amount,
             old_water_reading, new_water_reading, water_price, water_amount,
             trash_fee, security_fee, total_amount,
             payment_status, collection_date, is_checkout)
            VALUES
            ('HD001', MONTH(GETDATE()), YEAR(GETDATE()), 1000000,
             100, 150, 4000, 200000,
             50, 80, 10000, 300000,
             15000, 15000, 1530000,
             'CHUA_THU', NULL, 0)";
            _db.exec_sql_cmd(cmd);

            // 5. Thu tiền và trả phòng
            DataTable actual = _invoiceService.tblqldt_invoice_update(
                "HD001",
                DateTime.Now.Month,
                DateTime.Now.Year,
                100,
                180,
                50,
                90,
                "DA_THU",
                true);

            Assert.AreEqual(1, Convert.ToInt32(actual.Rows[0]["status"]));
            Assert.AreEqual("Thu tiền và cập nhật phiếu thành công.", actual.Rows[0]["msg"].ToString());

            // 6. Kiểm tra phiếu thu
            cmd.CommandText = @"SELECT payment_status,
                                    collection_date,
                                    is_checkout                                  
                                FROM tblqldt_invoice
                                WHERE contract_nbr = 'HD001'
                                  AND billing_month = MONTH(GETDATE())
                                  AND billing_year = YEAR(GETDATE());";
            DataTable dtInvoice = _db.exec_sql_cmd(cmd);

            Assert.AreEqual("DA_THU", dtInvoice.Rows[0]["payment_status"].ToString());
            Assert.AreEqual(true, Convert.ToBoolean(dtInvoice.Rows[0]["is_checkout"]));
            Assert.IsNotNull(dtInvoice.Rows[0]["collection_date"]);

            // 7. Kiểm tra hợp đồng
            cmd.CommandText = @"SELECT end_date
                                FROM tblqldt_contract
                                WHERE contract_nbr = 'HD001';";
            DataTable dtContract = _db.exec_sql_cmd(cmd);

            Assert.IsNotNull(dtContract.Rows[0]["end_date"]);
            Assert.AreEqual(DateTime.Today, Convert.ToDateTime(dtContract.Rows[0]["end_date"]).Date);

            // 8. Kiểm tra trạng thái phòng
            cmd.CommandText = @"SELECT status
                                FROM tblqldt_room
                                WHERE room_nbr = 'P001';";
            DataTable dtRoom = _db.exec_sql_cmd(cmd);

            Assert.AreEqual("TRONG", dtRoom.Rows[0]["status"].ToString());

            // 9. Kiểm tra thành viên
            cmd.CommandText = @"SELECT status, is_primary_tenant
                                FROM tblqldt_member
                                WHERE contract_nbr = 'HD001';";
            DataTable dtMember = _db.exec_sql_cmd(cmd);

            Assert.AreEqual("CHUYEN_DI", dtMember.Rows[0]["status"].ToString());
            Assert.AreEqual(false, Convert.ToBoolean(dtMember.Rows[0]["is_primary_tenant"]));
        }

        #endregion 
         
        #endregion

        #endregion

    }
}
