using System;
using System.Windows.Forms;
using System.Data;
using QLDT.Service;
using QLDT.UI.ViewModel;

namespace QLDT.UI
{
    public partial class frmContract : Form
    {
        private IContractService _contractService;
        private IValidationService _validationService;

        private string msg;

        public frmContract()
        {
            InitializeComponent();
            _contractService = new ContractService();
            _validationService = new ValidationService();
        }

        private void frmContract_Load(object sender, EventArgs e)
        {

            Load_all_rooms(); //Load mã phòng

            Load_tblqldt_contract(); //Load mã hợp đồng

            nud_deposit.Minimum = 0;
            nud_deposit.Maximum = 99999999;
            nud_deposit.DecimalPlaces = 0;

            nud_room_price.Minimum = 0;
            nud_room_price.Maximum = 99999999;
            nud_room_price.DecimalPlaces = 0;

            nud_electric_price.Minimum = 0;
            nud_electric_price.Maximum = 9999;
            nud_electric_price.DecimalPlaces = 0;

            nud_water_price.Minimum = 0;
            nud_water_price.Maximum = 99999;
            nud_water_price.DecimalPlaces = 0;

            nud_trash_fee.Minimum = 0;
            nud_trash_fee.Maximum = 99999;
            nud_trash_fee.DecimalPlaces = 0;

            nud_security_fee.Minimum = 0;
            nud_security_fee.Maximum = 99999;
            nud_security_fee.DecimalPlaces = 0;

            //ResetForm
            dgv_tblqldt_contract.Enabled = true;

            txt_contract_nbr.Clear();
            cmb_room_nbr.SelectedIndex = -1;

            dtp_start_date.ShowCheckBox = true;
            dtp_start_date.Format = DateTimePickerFormat.Custom;
            dtp_start_date.CustomFormat = " ";
            dtp_start_date.Checked = false;

            dtp_end_date.ShowCheckBox = true;
            dtp_end_date.Format = DateTimePickerFormat.Custom;
            dtp_end_date.CustomFormat = " ";
            dtp_end_date.Checked = false;

            nud_deposit.Value = nud_deposit.Minimum;
            nud_room_price.Value = nud_room_price.Minimum;
            nud_electric_price.Value = nud_electric_price.Minimum;
            nud_water_price.Value = nud_water_price.Minimum;
            nud_trash_fee.Value = nud_trash_fee.Minimum;
            nud_security_fee.Value = nud_security_fee.Minimum;

            txt_contract_nbr.Enabled = false;
            cmb_room_nbr.Enabled = false;
            dtp_start_date.Enabled = false;
            dtp_end_date.Enabled = false;
            nud_deposit.Enabled = false;
            nud_room_price.Enabled = false;
            nud_electric_price.Enabled = false;
            nud_water_price.Enabled = false;
            nud_trash_fee.Enabled = false;
            nud_security_fee.Enabled = false;

            btn_insert.Enabled = true;
            btn_insert.Text = "Thêm Mới";
            btn_edit.Enabled = false;
            btn_edit.Text = "Cập Nhật";
            btn_delete.Enabled = false;
            btn_cancel.Enabled = false;

            //dtp_start_date.ShowCheckBox = true;
            //dtp_start_date.Format = DateTimePickerFormat.Custom;
            //dtp_start_date.CustomFormat = "dd/MM/yyyy";

            //Định dạng hiển thị cột ngày bắt đầu và kết thúc
            dgv_tblqldt_contract.Columns["start_date"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv_tblqldt_contract.Columns["end_date"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void Load_tblqldt_contract()
        {
            DataTable dt = _contractService.tblqldt_contract_get_all();

            dgv_tblqldt_contract.AutoGenerateColumns = true;  // 👈 THÊM DÒNG NÀY
            dgv_tblqldt_contract.DataSource = dt;
        }

        private void Load_all_rooms()
        {
            DataTable dt = _contractService.tblqldt_room_get_all_contract();

            cmb_room_nbr.DataSource = dt;
            cmb_room_nbr.DisplayMember = "room_nbr";
            cmb_room_nbr.ValueMember = "room_nbr";
            cmb_room_nbr.SelectedIndex = -1;
        }

        private void Load_available_rooms()
        {
            DataTable dt = _contractService.tblqldt_room_get_available(); //Chỉ load phòng trống lên cmb

            cmb_room_nbr.DataSource = dt;
            cmb_room_nbr.DisplayMember = "room_nbr";
            cmb_room_nbr.ValueMember = "room_nbr";
            cmb_room_nbr.SelectedIndex = -1;
        }

        private void dgv_dshopdong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {                
                txt_contract_nbr.Text = dgv_tblqldt_contract.Rows[e.RowIndex].Cells["contract_nbr"].Value.ToString();
                cmb_room_nbr.SelectedValue = dgv_tblqldt_contract.Rows[e.RowIndex].Cells["room_nbr"].Value.ToString();
                var value1 = dgv_tblqldt_contract.Rows[e.RowIndex].Cells["start_date"].Value;
                if (value1 == null || value1 == DBNull.Value)
                {
                    dtp_start_date.Checked = false;
                    dtp_start_date.Format = DateTimePickerFormat.Custom;
                    dtp_start_date.CustomFormat = " ";
                }
                else
                {
                    dtp_start_date.Checked = true;
                    dtp_start_date.Format = DateTimePickerFormat.Custom;
                    dtp_start_date.CustomFormat = "dd/MM/yyyy";
                    dtp_start_date.Value = Convert.ToDateTime(value1);
                }

                var value2 = dgv_tblqldt_contract.Rows[e.RowIndex]
                    .Cells["end_date"].Value;

                if (value2 == null || value2 == DBNull.Value)
                {
                    dtp_end_date.Checked = false;
                    dtp_end_date.Format = DateTimePickerFormat.Custom;
                    dtp_end_date.CustomFormat = " ";
                }
                else
                {
                    dtp_end_date.Checked = true;
                    dtp_end_date.Format = DateTimePickerFormat.Custom;
                    dtp_end_date.CustomFormat = "dd/MM/yyyy";
                    dtp_end_date.Value = Convert.ToDateTime(value2);
                }

                object value = dgv_tblqldt_contract.Rows[e.RowIndex].Cells["deposit"].Value;

                if (value == null || value == DBNull.Value)
                {
                    nud_deposit.Value = 0;
                }
                else
                {
                    nud_deposit.Value = Convert.ToDecimal(value);
                }

                nud_room_price.Value = Convert.ToDecimal(dgv_tblqldt_contract.Rows[e.RowIndex].Cells["room_price"].Value);
                nud_electric_price.Value = Convert.ToDecimal(dgv_tblqldt_contract.Rows[e.RowIndex].Cells["electric_price"].Value);
                nud_water_price.Value = Convert.ToDecimal(dgv_tblqldt_contract.Rows[e.RowIndex].Cells["water_price"].Value);
                nud_trash_fee.Value = Convert.ToDecimal(dgv_tblqldt_contract.Rows[e.RowIndex].Cells["trash_fee"].Value);
                nud_security_fee.Value = Convert.ToDecimal(dgv_tblqldt_contract.Rows[e.RowIndex].Cells["security_fee"].Value);

            }

            //Bật form thông tin
            txt_contract_nbr.Enabled = false;
            cmb_room_nbr.Enabled = false;
            dtp_start_date.Enabled = false;
            dtp_end_date.Enabled = false;
            nud_deposit.Enabled = false;
            nud_room_price.Enabled = false;
            nud_electric_price.Enabled = false;
            nud_water_price.Enabled = false;
            nud_trash_fee.Enabled = false;
            nud_security_fee.Enabled = false;

            //Bật button
            btn_insert.Enabled = true;
            btn_edit.Enabled = true;
            btn_delete.Enabled = true;
            btn_cancel.Enabled = false;
        }
    

        private void btn_insert_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_insert.Text == "Thêm Mới")
                {

                    Load_available_rooms(); //Load phòng TRONG

                    btn_insert.Text = "Lưu";
                    btn_edit.Enabled = false;
                    btn_delete.Enabled = false;
                    btn_cancel.Enabled = true;

                    dgv_tblqldt_contract.Enabled = false;

                    //Resetform
                    txt_contract_nbr.Clear();
                    cmb_room_nbr.SelectedIndex = -1;

                    dtp_start_date.Format = DateTimePickerFormat.Custom;
                    dtp_start_date.CustomFormat = " ";
                    dtp_start_date.Checked = false;

                    dtp_end_date.Format = DateTimePickerFormat.Custom;
                    dtp_end_date.CustomFormat = " ";
                    dtp_end_date.Checked = false;

                    nud_deposit.Value = nud_deposit.Minimum;
                    nud_room_price.Value = nud_room_price.Minimum;
                    nud_electric_price.Value = nud_electric_price.Minimum;
                    nud_water_price.Value = nud_water_price.Minimum;
                    nud_trash_fee.Value = nud_trash_fee.Minimum;
                    nud_security_fee.Value = nud_security_fee.Minimum;


                    txt_contract_nbr.Enabled = true;
                    cmb_room_nbr.Enabled = true;
                    dtp_start_date.Enabled = true;
                    dtp_end_date.Enabled = true;
                    nud_deposit.Enabled = true;
                    nud_room_price.Enabled = true;
                    nud_electric_price.Enabled = true;
                    nud_water_price.Enabled = true;
                    nud_trash_fee.Enabled = true;
                    nud_security_fee.Enabled = true;

                    txt_contract_nbr.Focus();
                    return;
                }
                else
                {
                    tblqldt_contract_insert_vm vm = new tblqldt_contract_insert_vm();
                    vm.contract_nbr = txt_contract_nbr.Text;

                    vm.room_nbr = cmb_room_nbr.SelectedValue?.ToString();
                    //vm.start_date = dtp_start_date.Checked ? dtp_start_date.Value : (DateTime?)null;
                    //vm.end_date = dtp_end_date.Checked ? dtp_end_date.Value : (DateTime?)null;
                    //vm.deposit = nud_deposit.Value == 0 ? (decimal?)null : nud_deposit.Value;
                    //vm.room_price = (decimal)nud_room_price.Value;
                    //vm.electric_price = (decimal)nud_electric_price.Value;
                    //vm.water_price = (decimal)nud_water_price.Value;
                    //vm.trash_fee = (decimal)nud_trash_fee.Value;
                    //vm.security_fee = (decimal)nud_security_fee.Value;
                    vm.deposit = string.IsNullOrWhiteSpace(nud_deposit.Text) ? (decimal?)null : nud_deposit.Value;
                    vm.room_price = string.IsNullOrWhiteSpace(nud_room_price.Text) ? (decimal?)null : nud_room_price.Value;
                    vm.electric_price = string.IsNullOrWhiteSpace(nud_electric_price.Text) ? (decimal?)null : nud_electric_price.Value;
                    vm.water_price = string.IsNullOrWhiteSpace(nud_water_price.Text) ? (decimal?)null : nud_water_price.Value;
                    vm.trash_fee = string.IsNullOrWhiteSpace(nud_trash_fee.Text) ? (decimal?)null : nud_trash_fee.Value;
                    vm.security_fee = string.IsNullOrWhiteSpace(nud_security_fee.Text) ? (decimal?)null : nud_security_fee.Value;

                    _validationService.Validate(vm);

                    DataTable dt = _contractService.tblqldt_contract_insert(
                        txt_contract_nbr.Text,
                        cmb_room_nbr.SelectedValue?.ToString(),
                        //nud_deposit.Value == 0 ? (decimal?)null : nud_deposit.Value,
                        //nud_room_price.Value,
                        //nud_electric_price.Value,
                        //nud_water_price.Value,
                        //nud_trash_fee.Value,
                        //nud_security_fee.Value                
                        vm.deposit,
                        vm.room_price.Value,
                        vm.electric_price.Value,
                        vm.water_price.Value,
                        vm.trash_fee.Value,
                        vm.security_fee.Value
                        );

                    msg = dt.Rows[0]["msg"].ToString();
                    MessageBox.Show(msg, "Thêm Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    Load_tblqldt_contract(); //Load ds hợp đồng
                    Load_available_rooms(); //Load lại mã phòng

                    //Resetform
                    dgv_tblqldt_contract.Enabled = true;

                    txt_contract_nbr.Clear();
                    cmb_room_nbr.SelectedIndex = -1;

                    dtp_start_date.Format = DateTimePickerFormat.Custom;
                    dtp_start_date.CustomFormat = " ";
                    dtp_start_date.Checked = false;

                    dtp_end_date.Format = DateTimePickerFormat.Custom;
                    dtp_end_date.CustomFormat = " ";
                    dtp_end_date.Checked = false;

                    nud_deposit.Value = nud_deposit.Minimum;
                    nud_room_price.Value = nud_room_price.Minimum;
                    nud_electric_price.Value = nud_electric_price.Minimum;
                    nud_water_price.Value = nud_water_price.Minimum;
                    nud_trash_fee.Value = nud_trash_fee.Minimum;
                    nud_security_fee.Value = nud_security_fee.Minimum;

                    txt_contract_nbr.Enabled = false;
                    cmb_room_nbr.Enabled = false;
                    dtp_start_date.Enabled = false;
                    dtp_end_date.Enabled = false;
                    nud_deposit.Enabled = false;
                    nud_room_price.Enabled = false;
                    nud_electric_price.Enabled = false;
                    nud_water_price.Enabled = false;
                    nud_trash_fee.Enabled = false;
                    nud_security_fee.Enabled = false;

                    btn_insert.Text = "Thêm Mới";
                    btn_edit.Text = "Cập Nhật";
                    btn_edit.Enabled = false;
                    btn_delete.Enabled = false;
                    btn_cancel.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_edit.Text == "Cập Nhật")
                {
                    if (dtp_end_date.Checked)
                    {
                        MessageBox.Show(
                            "Hợp đồng kết thúc không thể chỉnh sửa!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    btn_edit.Text = "Lưu";
                    btn_edit.Enabled = true;

                    btn_insert.Enabled = false;
                    btn_delete.Enabled = false;
                    btn_cancel.Enabled = true;

                    dgv_tblqldt_contract.Enabled = false;
                    txt_contract_nbr.Enabled = false;
                    cmb_room_nbr.Enabled = false;

                    dtp_start_date.Enabled = true;
                    dtp_end_date.Enabled = true;
                    nud_deposit.Enabled = true;
                    nud_room_price.Enabled = true;
                    nud_electric_price.Enabled = true;
                    nud_water_price.Enabled = true;
                    nud_trash_fee.Enabled = true;
                    nud_security_fee.Enabled = true;

                    txt_contract_nbr.Focus();
                    return;
                }
                else
                {
                    //lưu edit
                    tblqldt_contract_update_vm vm = new tblqldt_contract_update_vm();
                    vm.contract_nbr = txt_contract_nbr.Text;
                    vm.room_nbr = cmb_room_nbr.Text;
                    //vm.start_date = dtp_start_date.Checked ? dtp_start_date.Value : (DateTime?)null;
                    //vm.end_date = dtp_end_date.Checked ? dtp_end_date.Value : (DateTime?)null;
                    //vm.deposit = nud_deposit.Value == 0 ? (decimal?)null : nud_deposit.Value;
                    //vm.room_price = (decimal)nud_room_price.Value;
                    //vm.electric_price = (decimal)nud_electric_price.Value;
                    //vm.water_price = (decimal)nud_water_price.Value;
                    //vm.trash_fee = (decimal)nud_trash_fee.Value;
                    //vm.security_fee = (decimal)nud_security_fee.Value;
                    vm.deposit = string.IsNullOrWhiteSpace(nud_deposit.Text) ? (decimal?)null : nud_deposit.Value;
                    vm.room_price = string.IsNullOrWhiteSpace(nud_room_price.Text) ? (decimal?)null : nud_room_price.Value;
                    vm.electric_price = string.IsNullOrWhiteSpace(nud_electric_price.Text) ? (decimal?)null : nud_electric_price.Value;
                    vm.water_price = string.IsNullOrWhiteSpace(nud_water_price.Text) ? (decimal?)null : nud_water_price.Value;
                    vm.trash_fee = string.IsNullOrWhiteSpace(nud_trash_fee.Text) ? (decimal?)null : nud_trash_fee.Value;
                    vm.security_fee = string.IsNullOrWhiteSpace(nud_security_fee.Text) ? (decimal?)null : nud_security_fee.Value;

                    _validationService.Validate(vm);

                    DataTable dt = _contractService.tblqldt_contract_update(
                        //txt_contract_nbr.Text,
                        //cmb_room_nbr.Text,
                        //nud_deposit.Value == 0 ? (decimal?)null : nud_deposit.Value,
                        //nud_room_price.Value,
                        //nud_electric_price.Value,
                        //nud_water_price.Value,
                        //nud_trash_fee.Value,
                        //nud_security_fee.Value
                        txt_contract_nbr.Text,
                        cmb_room_nbr.Text,
                        vm.deposit,
                        vm.room_price.Value,
                        vm.electric_price.Value,
                        vm.water_price.Value,
                        vm.trash_fee.Value,
                        vm.security_fee.Value
                        );

                    msg = dt.Rows[0]["msg"].ToString();
                    MessageBox.Show(msg, "Cập Nhật Hợp Đồng", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Load_tblqldt_contract();

                    //Resetform
                    txt_contract_nbr.Clear();
                    cmb_room_nbr.SelectedIndex = -1;


                    dtp_start_date.Checked = false;
                    dtp_end_date.Checked = false;

                    nud_deposit.Value = nud_deposit.Minimum;
                    nud_room_price.Value = nud_room_price.Minimum;
                    nud_electric_price.Value = nud_electric_price.Minimum;
                    nud_water_price.Value = nud_water_price.Minimum;
                    nud_trash_fee.Value = nud_trash_fee.Minimum;
                    nud_security_fee.Value = nud_security_fee.Minimum;

                    btn_edit.Text = "Cập Nhật";
                    btn_edit.Enabled = true;

                    btn_insert.Enabled = true;
                    btn_delete.Enabled = false;
                    btn_cancel.Enabled = false;

                    dgv_tblqldt_contract.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc muốn xóa hợp đồng này?", "Xác nhận xóa",
                                MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

                if (rs != DialogResult.Yes)
                    return;

                string contract_nbr = txt_contract_nbr.Text;
                DataTable dt = _contractService.tblqldt_contract_delete(contract_nbr);

                msg = dt.Rows[0]["msg"].ToString();
                MessageBox.Show(msg, "Cập Nhập Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Load_tblqldt_contract();

                //Reset Form
                txt_contract_nbr.Clear();
                cmb_room_nbr.SelectedIndex = -1;

                dtp_start_date.Format = DateTimePickerFormat.Custom;
                dtp_start_date.CustomFormat = " ";
                dtp_start_date.Checked = false;

                dtp_end_date.Format = DateTimePickerFormat.Custom;
                dtp_end_date.CustomFormat = " ";
                dtp_end_date.Checked = false;

                nud_deposit.Value = nud_deposit.Minimum;
                nud_room_price.Value = nud_room_price.Minimum;
                nud_electric_price.Value = nud_electric_price.Minimum;
                nud_water_price.Value = nud_water_price.Minimum;
                nud_trash_fee.Value = nud_trash_fee.Minimum;
                nud_security_fee.Value = nud_security_fee.Minimum;

                txt_contract_nbr.Enabled = false;
                cmb_room_nbr.Enabled = false;
                dtp_start_date.Enabled = false;
                dtp_end_date.Enabled = false;
                nud_deposit.Enabled = false;
                nud_room_price.Enabled = false;
                nud_electric_price.Enabled = false;
                nud_water_price.Enabled = false;
                nud_trash_fee.Enabled = false;
                nud_security_fee.Enabled = false;

                btn_insert.Enabled = true;
                btn_insert.Text = "Thêm Mới";
                btn_edit.Enabled = false;
                btn_edit.Text = "Cập Nhật";
                btn_delete.Enabled = false;
                btn_cancel.Enabled = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            dgv_tblqldt_contract.Enabled = true;

            txt_contract_nbr.Clear();
            cmb_room_nbr.SelectedIndex = -1;

            //dtp_NgayBatDau.ShowCheckBox = true;
            dtp_start_date.Format = DateTimePickerFormat.Custom;
            dtp_start_date.CustomFormat = " ";
            dtp_start_date.Checked = false;

            //dtp_NgayKetThuc.ShowCheckBox = true;
            dtp_end_date.Format = DateTimePickerFormat.Custom;
            dtp_end_date.CustomFormat = " ";
            dtp_end_date.Checked = false;

            nud_deposit.Value = nud_deposit.Minimum;
            nud_room_price.Value = nud_room_price.Minimum;
            nud_electric_price.Value = nud_electric_price.Minimum;
            nud_water_price.Value = nud_water_price.Minimum;
            nud_trash_fee.Value = nud_trash_fee.Minimum;
            nud_security_fee.Value = nud_security_fee.Minimum;

            txt_contract_nbr.Enabled = false;
            cmb_room_nbr.Enabled = false;
            dtp_start_date.Enabled = false;
            dtp_end_date.Enabled = false;
            nud_deposit.Enabled = false;
            nud_room_price.Enabled = false;
            nud_electric_price.Enabled = false;
            nud_water_price.Enabled = false;
            nud_trash_fee.Enabled = false;
            nud_security_fee.Enabled = false;

            btn_insert.Enabled = true;
            btn_insert.Text = "Thêm Mới";
            btn_edit.Enabled = false;
            btn_edit.Text = "Cập Nhật";
            btn_delete.Enabled = false;
            btn_cancel.Enabled = false;
        }

        private void btn_dsthanhvien_Click(object sender, EventArgs e)
        {
            frmMember tv = new frmMember();
            tv.ShowDialog();
        }

        private void cmb_room_nbr_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_room_nbr.ValueMember = "room_nbr";
            cmb_room_nbr.DisplayMember = "room_nbr";
        }

        private void dtp_end_date_ValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show(
                   "Ngày kết thúc được hệ thống tự động cập nhật khi thực hiện Trả phòng trong Phiếu thu.",
                   "Thông báo",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
        }
    }
}
