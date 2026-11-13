
using System;
using System.Windows.Forms;
using QLDT.Service;
using System.Data;

namespace QLDT.UI
{
    public partial class frmInvoice : Form
    {
        private IInvoiceService _invoiceService;
        private IValidationService _validationService;

        private string msg;


        public frmInvoice()
        {
            InitializeComponent();
            _invoiceService = new InvoiceService();
            _validationService = new ValidationService();
        }

        private void frmInvoice_Load(object sender, EventArgs e)
        {
            Load_tblqldt_invoice();

            //Clear Form
            cmb_contract_nbr.Text = "";

            nud_room_price.Value = 0;
            nud_old_electric_reading.Value = 0;
            nud_new_electric_reading.Value = 0;
            nud_electric_price.Value = 0;
            nud_electric_amount.Value = 0;
            nud_old_water_reading.Value = 0;
            nud_new_water_reading.Value = 0;
            nud_water_price.Value = 0;
            nud_water_price.Value = 0;
            nud_trash_fee.Value = 0;
            nud_security_fee.Value = 0;

            nud_total_amount.Value = 0;
            cmb_payment_status.SelectedIndex = -1;

            dtp_collection_date.Format = DateTimePickerFormat.Custom;
            dtp_collection_date.CustomFormat = " ";
            dtp_collection_date.Checked = false;

            chk_is_checkout.Checked = false;

            // Disable control

            cmb_contract_nbr.Enabled = false;
            nud_room_price.Enabled = false;

            nud_old_electric_reading.Enabled = false;
            nud_new_electric_reading.Enabled = false;
            nud_electric_price.Enabled = false;
            nud_electric_amount.Enabled = false;

            nud_old_water_reading.Enabled = false;
            nud_new_water_reading.Enabled = false;
            nud_water_price.Enabled = false;
            nud_water_amount.Enabled = false;

            nud_trash_fee.Enabled = false;
            nud_security_fee.Enabled = false;
            nud_total_amount.Enabled = false;
            cmb_payment_status.Enabled = false;
            dtp_collection_date.Enabled = false;
            chk_is_checkout.Enabled = false;

            nud_room_price.Minimum = 0;
            nud_room_price.Maximum = 99999999;

            nud_electric_price.Minimum = 0;
            nud_electric_price.Maximum = 9999;
            nud_electric_amount.Minimum = 0;
            nud_electric_amount.Maximum = 9999999;

            nud_water_price.Minimum = 0;
            nud_water_price.Maximum = 99999;
            nud_water_amount.Minimum = 0;
            nud_water_amount.Maximum = 999999;

            nud_trash_fee.Minimum = 0;
            nud_trash_fee.Maximum = 99999;

            nud_security_fee.Minimum = 0;
            nud_security_fee.Maximum = 99999;

            nud_total_amount.Minimum = 0;
            nud_total_amount.Maximum = 9999999;

            nud_old_electric_reading.Minimum = 0;
            nud_old_electric_reading.Maximum = 9999;
            nud_new_electric_reading.Minimum = 0;
            nud_new_electric_reading.Maximum = 9999;

            nud_old_water_reading.Minimum = 0;
            nud_old_water_reading.Maximum = 999;
            nud_new_water_reading.Minimum = 0;
            nud_new_water_reading.Maximum = 999;
            nud_new_water_reading.Maximum = 999;

            //Chỉ cho nhập số nguyên
            nud_total_amount.DecimalPlaces = 0;
            nud_room_price.DecimalPlaces = 0;
            nud_old_electric_reading.DecimalPlaces = 0;
            nud_new_electric_reading.DecimalPlaces = 0;
            nud_electric_price.DecimalPlaces = 0;
            nud_electric_amount.DecimalPlaces = 0;
            nud_old_water_reading.DecimalPlaces = 0;
            nud_new_water_reading.DecimalPlaces = 0;
            nud_water_price.DecimalPlaces = 0;
            nud_water_amount.DecimalPlaces = 0;
            nud_trash_fee.DecimalPlaces = 0;
            nud_security_fee.DecimalPlaces = 0;

            // Format dấu phân cách hàng nghìn
            nud_total_amount.ThousandsSeparator = true;
            nud_room_price.ThousandsSeparator = true;
            nud_electric_price.ThousandsSeparator = true;
            nud_electric_amount.ThousandsSeparator = true;
            nud_water_amount.ThousandsSeparator = true;
            nud_water_price.ThousandsSeparator = true;
            nud_trash_fee.ThousandsSeparator = true;
            nud_security_fee.ThousandsSeparator = true;

            //dtp_collection_date.ShowCheckBox = true;
            //dtp_collection_date.Format = DateTimePickerFormat.Custom;
            //dtp_collection_date.CustomFormat = "dd/MM/yyyy";
        }

        private void Load_tblqldt_get_all_invoce()
        {
            DataTable dt = _invoiceService.tblqldt_contract_get_all_invoice();

            cmb_contract_nbr.DataSource = dt;
            cmb_contract_nbr.DisplayMember = "contract_nbr";
            cmb_contract_nbr.ValueMember = "contract_nbr";
        }

        private void Load_tblqldt_invoice()
        {
            int month = 0;
            int year = 0;

            if (!string.IsNullOrWhiteSpace(cmb_billing_month.Text))
                month = Convert.ToInt32(cmb_billing_month.Text);

            if (!string.IsNullOrWhiteSpace(cmb_billing_year.Text))
                year = Convert.ToInt32(cmb_billing_year.Text);

            dgv_tblqldt_invoice.DataSource =
                _invoiceService.tblqldt_invoice_get_all(month, year);
        }

        private void dgv_tblqldt_invoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    cmb_billing_month.Text = dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["billing_month"].Value.ToString();
                    cmb_billing_year.Text = dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["billing_year"].Value.ToString();
                    cmb_contract_nbr.Text = dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["contract_nbr"].Value.ToString();
                    nud_room_price.Value = Convert.ToDecimal(dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["room_price"].Value);
                    nud_old_electric_reading.Value = Convert.ToDecimal(dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["old_electric_reading"].Value);
                    nud_electric_price.Value = Convert.ToDecimal(dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["electric_price"].Value);
                    //nud_electric_amount.Value = Convert.ToDecimal(dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["electric_amount"].Value);
                    object val = dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["electric_amount"].Value;

                    if (val == DBNull.Value || val == null)
                    {
                        nud_electric_amount.Value = 0;
                    }
                    else
                    {
                        nud_electric_amount.Value = Convert.ToDecimal(val);
                    }
                    nud_old_water_reading.Value = Convert.ToDecimal(dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["old_water_reading"].Value);
                    nud_water_price.Value = Convert.ToDecimal(dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["water_price"].Value);
                    //nud_water_amount.Value = Convert.ToDecimal(dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["water_amount"].Value);
                    object water = dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["water_amount"].Value;

                    if (water == DBNull.Value || water == null)
                    {
                        nud_water_amount.Value = 0;
                    }
                    else
                    {
                        nud_water_amount.Value = Convert.ToDecimal(water);
                    }
                    nud_trash_fee.Value = Convert.ToDecimal(dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["trash_fee"].Value);
                    nud_security_fee.Value = Convert.ToDecimal(dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["security_fee"].Value);
                    //nud_total_amount.Value = Convert.ToDecimal(dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["total_amount"].Value);
                    object total = dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["total_amount"].Value;

                    if (total == DBNull.Value || total == null)
                    {
                        nud_total_amount.Value = 0;
                    }
                    else
                    {
                        nud_total_amount.Value = Convert.ToDecimal(total);
                    }

                    cmb_payment_status.Text = dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["payment_status"].Value.ToString();

                    // Chỉ số điện mới
                    object newElectric = dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["new_electric_reading"].Value;
                    if (newElectric == DBNull.Value || newElectric == null)
                    {
                        nud_new_electric_reading.Value = 0;
                    }
                    else
                    {
                        nud_new_electric_reading.Value = Convert.ToDecimal(newElectric);
                    }
                    // Chỉ số nước mới 
                    object newWater = dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["new_water_reading"].Value;
                    if (newWater == DBNull.Value || newWater == null)
                    {
                        nud_new_water_reading.Value = 0;
                    }
                    else
                    {
                        nud_new_water_reading.Value = Convert.ToDecimal(newWater);
                    }

                    // Ngày thu
                    object value = dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["collection_date"].Value;
                    if (value != DBNull.Value && value != null)
                    {
                        dtp_collection_date.Format = DateTimePickerFormat.Custom;
                        dtp_collection_date.CustomFormat = "dd/MM/yyyy";
                        dtp_collection_date.Value = Convert.ToDateTime(value);
                        dtp_collection_date.Checked = true;
                    }
                    else
                    {
                        dtp_collection_date.Format = DateTimePickerFormat.Custom;
                        dtp_collection_date.CustomFormat = " ";
                        dtp_collection_date.Checked = false;
                    }

                    chk_is_checkout.Checked = Convert.ToBoolean(dgv_tblqldt_invoice.Rows[e.RowIndex].Cells["is_checkout"].Value);

                    //Tắt form thông tin

                    cmb_billing_month.Enabled = true;
                    cmb_billing_year.Enabled = true;

                    cmb_contract_nbr.Enabled = false;
                    nud_room_price.Enabled = false;

                    nud_old_electric_reading.Enabled = false;
                    nud_new_electric_reading.Enabled = true;
                    nud_electric_price.Enabled = false;
                    nud_electric_amount.Enabled = false;

                    nud_old_water_reading.Enabled = false;
                    nud_new_water_reading.Enabled = true;
                    nud_water_price.Enabled = false;
                    nud_water_amount.Enabled = false;

                    nud_trash_fee.Enabled = false;
                    nud_security_fee.Enabled = false;
                    nud_total_amount.Enabled = false;
                    cmb_payment_status.Enabled = true;
                    dtp_collection_date.Enabled = true;
                    chk_is_checkout.Enabled = true;

                    btn_insert.Enabled = true;
                    btn_edit.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmb_billing_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_tblqldt_invoice();
        }

        private void cmb_billing_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_tblqldt_invoice();
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_billing_month.SelectedIndex == -1 || cmb_billing_year.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn tháng và năm trước.",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    cmb_billing_month.Focus();
                    return;
                }

                string contract = cmb_contract_nbr.Text;
                int month = Convert.ToInt32(cmb_billing_month.Text);
                int year = Convert.ToInt32(cmb_billing_year.Text);

                DataTable dt = _invoiceService.tblqldt_invoice_create(month, year);

                MessageBox.Show(dt.Rows[0]["msg"].ToString());

                Load_tblqldt_invoice();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cmb_contract_nbr.Text))
                {
                    MessageBox.Show("Vui lòng chọn hợp đồng trước");
                    return;
                }

                string contract_nbr = cmb_contract_nbr.Text;

                int month = Convert.ToInt32(cmb_billing_month.Text);
                int year = Convert.ToInt32(cmb_billing_year.Text);

                int oldElectric = (int)nud_old_electric_reading.Value;
                int newElectric = (int)nud_new_electric_reading.Value;

                int oldWater = (int)nud_old_water_reading.Value;
                int newWater = (int)nud_new_water_reading.Value;
                string paymentStatus = cmb_payment_status.Text;

                // Call service
                DataTable dt = _invoiceService.tblqldt_invoice_update(
                    contract_nbr,
                    month,
                    year,
                    oldElectric,
                    newElectric,
                    oldWater,
                    newWater,
                    paymentStatus,
                    chk_is_checkout.Checked
                );

                MessageBox.Show(dt.Rows[0]["msg"].ToString(), "Thông báo");

                Load_tblqldt_invoice();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void dtp_collection_date_ValueChanged(object sender, EventArgs e)
        {
            dtp_collection_date.Format = DateTimePickerFormat.Custom;
            dtp_collection_date.CustomFormat = "dd/MM/yyyy";
        }

        private void btn_dsphieuthu_Click(object sender, EventArgs e)
        {

        }
    }
}
