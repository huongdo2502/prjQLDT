using System;
using System.ComponentModel.DataAnnotations;

namespace QLDT.UI.ViewModel
{
    #region tblqldt_contract
    public class tblqldt_contract_insert_vm
    {
        [Required(ErrorMessage = "Vui lòng nhập mã hợp đồng")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Mã hợp đồng phải từ 1 đến 10 ký tự")]
        public string contract_nbr { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã phòng")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Mã phòng phải từ 1 đến 10 ký tự")]
        public string room_nbr { get; set; }

        public DateTime? start_date { get; set; }

        public DateTime? end_date { get; set; }

        [Range(0, 99999999, ErrorMessage = "Tiền cọc phải nằm trong khoảng từ 0 đến 99999999")]
        public decimal? deposit { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá phòng")]
        [Range(1, 99999999, ErrorMessage = "Giá phòng phải nằm trong khoảng từ 1 đến 99999999")]
        public decimal? room_price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá điện")]
        [Range(1, 9999, ErrorMessage = "Giá điện phải nằm trong khoảng 1 đến 9999")]
        public decimal? electric_price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá nước")]
        [Range(1, 99999, ErrorMessage = "Giá nước phải nằm trong khoảng 1 đến 99999")]
        public decimal? water_price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiền rác")]
        [Range(1, 99999, ErrorMessage = "Tiền rác phải nằm trong khoảng 1 đến 99999")]
        public decimal? trash_fee { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiền an ninh")]
        [Range(1, 99999, ErrorMessage = "Tiền an ninh nằm trong khoảng 1 đến 99999")]
        public decimal? security_fee { get; set; }
    }

    public class tblqldt_contract_update_vm
    {
        [Required(ErrorMessage = "Vui lòng nhập mã hợp đồng")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Mã hợp đồng phải từ 1 đến 10 ký tự")]
        public string contract_nbr { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã phòng")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Mã phòng phải từ 1 đến 10 ký tự")]
        public string room_nbr { get; set; }

        public DateTime? start_date { get; set; }

        public DateTime? end_date { get; set; }

        [Range(0, 99999999, ErrorMessage = "Tiền cọc phải nằm trong khoảng từ 0 đến 99999999")]
        public decimal? deposit { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá phòng")]
        [Range(1, 99999999, ErrorMessage = "Giá phòng phải nằm trong khoảng từ 1 đến 99999999")]
        public decimal? room_price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá điện")]
        [Range(1, 9999, ErrorMessage = "Giá điện phải nằm trong khoảng 1 đến 9999")]
        public decimal? electric_price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá nước")]
        [Range(1, 99999, ErrorMessage = "Giá nước phải nằm trong khoảng 1 đến 99999")]
        public decimal? water_price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiền rác")]
        [Range(1, 99999, ErrorMessage = "Tiền rác phải nằm trong khoảng 1 đến 99999")]
        public decimal? trash_fee { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiền an ninh")]
        [Range(1, 99999, ErrorMessage = "Tiền an ninh nằm trong khoảng 1 đến 99999")]
        public decimal? security_fee { get; set; }
    }
    #endregion

    #region tblqldt_member
    public class tblqldt_member_insert_vm
    {
        [Required(ErrorMessage = "Vui lòng nhập mã thành viên")]
        [MaxLength(10, ErrorMessage = "Mã thành viên tối đa 10 ký tự")]
        public string member_nbr { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hợp đồng")]
        [MaxLength(10, ErrorMessage = "Mã hợp đồng tối đa 10 ký tự")]
        public string contract_nbr { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [MaxLength(200, ErrorMessage = "Họ tên tối đa 200 ký tự")]
        public string full_name { get; set; }

        public bool is_primary_tenant { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập CCCD")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "CCCD phải gồm đúng 12 số")]
        public string national_id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        public DateTime? birth_date { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        [RegularExpression("^(Nam|Nữ)$", ErrorMessage = "Giới tính phải là Nam hoặc Nữ")]
        public string gender { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải gồm 10 số")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [MaxLength(500, ErrorMessage = "Địa chỉ tối đa 500 ký tự")]
        public string address { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn trạng thái")]
        [RegularExpression("^(DANG_O|CHO_NHAN_PHONG|CHUYEN_DI)$",
            ErrorMessage = "Trạng thái thành viên phải là DANG_O hoặc DANG_CHO_PHONG")]
        public string status { get; set; }
    }

    public class tblqldt_member_update_vm
    {
        [Required(ErrorMessage = "Vui lòng nhập mã thành viên")]
        [MaxLength(10, ErrorMessage = "Mã thành viên tối đa 10 ký tự")]
        public string member_nbr { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hợp đồng")]
        [MaxLength(10, ErrorMessage = "Mã hợp đồng tối đa 10 ký tự")]
        public string contract_nbr { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [MaxLength(200, ErrorMessage = "Họ tên tối đa 200 ký tự")]
        public string full_name { get; set; }

        public bool is_primary_tenant { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập CCCD")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "CCCD phải gồm đúng 12 số")]
        public string national_id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        public DateTime? birth_date { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        [RegularExpression("^(Nam|Nữ)$", ErrorMessage = "Giới tính phải là Nam hoặc Nữ")]
        public string gender { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải gồm 10 số")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [MaxLength(500, ErrorMessage = "Địa chỉ tối đa 500 ký tự")]
        public string address { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn trạng thái")]
        [RegularExpression("^(DANG_O|CHO_NHAN_PHONG|CHUYEN_DI)$", 
            ErrorMessage = "Trạng thái thành viên phải là DANG_O hoặc CHUYEN_DI")]
        public string status { get; set; }
    }
    #endregion
}
