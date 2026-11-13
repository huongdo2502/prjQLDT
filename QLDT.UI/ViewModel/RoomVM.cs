
using System.ComponentModel.DataAnnotations;

namespace QLDT.UI.ViewModel
{
    #region tblqldt_room 
    public class tblqldt_room_insert_vm
    {
        [Required(ErrorMessage = "Vui lòng nhập mã phòng")]
        [MaxLength(10, ErrorMessage = "Mã phòng tối đa 10 kí tự, vui lòng kiểm tra lại")]
        public string room_nbr { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên phòng")]
        [MaxLength(200, ErrorMessage = "Tên phòng tối đa 200 kí tự, vui lòng kiểm tra lại")]
        public string name { get; set; }

        [Required(ErrorMessage = "Trạng thái phòng không được để trống")]
        [RegularExpression("^TRONG", ErrorMessage = "Trạng thái phòng phải là TRONG")]
        public string status { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập diện tích")]
        [Range(15, 50, ErrorMessage = "Diện tích tối thiểu 15 m^2 và tối đa 50 m^2, vui lòng kiểm tra lại")]
        public decimal? area { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số người ở tối đa")]
        [Range(1, 5, ErrorMessage = "Số người ở tối thiểu là 1 và tối đa là 5, vui lòng kiểm tra lại")]
        public int? max_qty_man { get; set; }
    }

    public class tblqldt_room_update_vm
    {
        //[Required(ErrorMessage = "Vui lòng nhập mã phòng")]
        //[MaxLength(10, ErrorMessage = "Mã phòng tối đa 10 kí tự, vui lòng kiểm tra lại")]

        public string room_nbr { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên phòng")]
        [MaxLength(200, ErrorMessage = "Tên phòng tối đa 200 kí tự, vui lòng kiểm tra lại")]
        public string name { get; set; }

        [Required(ErrorMessage = "Trạng thái phòng không được để trống")]
        [RegularExpression("^(TRONG|KHONG_CHO_THUE)$",
            ErrorMessage = "Trạng thái phòng phải là 'TRONG' hoặc 'KHONG_CHO_THUE'")]
        public string status { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập diện tích")]
        [Range(15, 50, ErrorMessage = "Diện tích tối thiểu 15 m^2 và tối đa 50 m^2, vui lòng kiểm tra lại")]
        public decimal? area { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số người ở tối đa")]
        [Range(1, 5, ErrorMessage = "Số người ở tối thiểu là 1 và tối đa là 5, vui lòng kiểm tra lại")]
        public int? max_qty_man { get; set; }
    }
    #endregion
}
