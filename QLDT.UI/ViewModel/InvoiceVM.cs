using System;
using System.ComponentModel.DataAnnotations;

namespace QLDT.UI.ViewModel
{
    #region tblqldt_invoice
    public class tblqldt_invoice_vm
    {
        public int billing_month { get; set; }
        public int billing_year { get; set; }

    }
    #endregion
    #region tblqldt_invoice_reading_vn
    public class tblqldt_invoice_reading_vm
    {
        [Required]
        public string receipt_nbr { get; set; }

        [Range(0, 999999)]
        public int new_electric_reading { get; set; }

        [Range(0, 999999)]
        public int new_water_reading { get; set; }
    }
    #endregion
    #region tblqldt_invoice_calculate_vn
    public class tblqldt_invoice_calculate_vm
    {
        [Required]
        public string contract_nbr { get; set; }

        [Range(0, 999999)]
        public int old_electric_reading { get; set; }

        [Range(0, 999999)]
        public int new_electric_reading { get; set; }

        [Range(0, 999999)]
        public int old_water_reading { get; set; }

        [Range(0, 999999)]
        public int new_water_reading { get; set; }

        [Range(0, 999999999)]
        public decimal room_price { get; set; }

        [Range(0, 999999999)]
        public decimal electric_price { get; set; }

        [Range(0, 999999999)]
        public decimal water_price { get; set; }

        [Range(0, 999999999)]
        public decimal trash_fee { get; set; }

        [Range(0, 999999999)]
        public decimal security_fee { get; set; }
    }
    #endregion

}
