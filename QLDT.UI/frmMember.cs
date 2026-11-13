using System;
using System.Windows.Forms;
using QLDT.Service;
using System.Data;
using QLDT.Service;
using QLDT.UI.ViewModel;

namespace QLDT.UI
{
    public partial class frmMember : Form
    {
        private IContractService _memberService;
        private IValidationService _validationService;

        private string msg;
        public frmMember()
        {
            InitializeComponent();
            _memberService = new ContractService();
            _validationService = new ValidationService();
        }

        private void frmMember_Load(object sender, EventArgs e)
        {

            Load_tblqldt_member();
            Load_contract_get_all(); //Load tất cả hợp đồng

            //ResetForm
            dgv_tblqldt_member.Enabled = true;

            txt_member_nbr.Clear();
            cmb_contract_nbr.SelectedIndex = -1;
            txt_full_name.Clear();
            chk_is_primary_tenant.Checked = false;
            txt_national_id.Clear();

            dtp_birth_date.ShowCheckBox = true;
            dtp_birth_date.Format = DateTimePickerFormat.Custom;
            dtp_birth_date.CustomFormat = " ";
            dtp_birth_date.Checked = false;

            rdo_male.Checked = false;
            rdo_female.Checked = false;
            txt_phone.Clear();
            txt_address.Clear();
            cmb_status.SelectedIndex = -1;
            
            txt_member_nbr.Enabled = false;
            cmb_contract_nbr.Enabled = false;
            txt_full_name.Enabled = false;
            chk_is_primary_tenant.Enabled = false;
            txt_national_id.Enabled = false;
            txt_phone.Enabled = false;
            dtp_birth_date.Enabled = false;
            rdo_male.Enabled = false;
            rdo_female.Enabled = false;
            txt_address.Enabled = false;
            cmb_status.Enabled = false;

            btn_insert.Enabled = true;
            btn_insert.Text = "Thêm Mới";

            btn_edit.Enabled = false;
            btn_edit.Text = "Cập Nhật";

            btn_delete.Enabled = false;
            btn_cancel.Enabled = false;
        }

        private void Load_tblqldt_member()
        {
            DataTable dt = _memberService.tblqldt_member_get_all();
            dgv_tblqldt_member.DataSource = dt;

            //Định dạng cột ngày sinh dd/MM/yyyyy
            dgv_tblqldt_member.Columns["birth_date"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void Load_contract_get_all()
        {
            DataTable dt = _memberService.tblqldt_contract_get_all();

            cmb_contract_nbr.DataSource = dt;
            cmb_contract_nbr.DisplayMember = "contract_nbr";
            cmb_contract_nbr.ValueMember = "contract_nbr";
        }

        private void Load_contrac_get_for_member()
        {
            DataTable dt = _memberService.tblqldt_contract_get_for_member();
            cmb_contract_nbr.DataSource = dt;
            cmb_contract_nbr.DisplayMember = "contract_nbr";
            cmb_contract_nbr.ValueMember = "contract_nbr";

        }

        private void dgv_tblqldt_member_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txt_member_nbr.Text = dgv_tblqldt_member.Rows[e.RowIndex].Cells["member_nbr"].Value.ToString();
                cmb_contract_nbr.SelectedValue = dgv_tblqldt_member.Rows[e.RowIndex].Cells["contract_nbr"].Value.ToString();
                txt_full_name.Text = dgv_tblqldt_member.Rows[e.RowIndex].Cells["full_name"].Value.ToString();

                chk_is_primary_tenant.Checked = Convert.ToBoolean(dgv_tblqldt_member.Rows[e.RowIndex].Cells["is_primary_tenant"].Value);
                txt_national_id.Text = dgv_tblqldt_member.Rows[e.RowIndex].Cells["national_id"].Value.ToString();
                txt_phone.Text = dgv_tblqldt_member.Rows[e.RowIndex].Cells["phone"].Value.ToString();

                dtp_birth_date.Format = DateTimePickerFormat.Custom;
                dtp_birth_date.CustomFormat = "dd/MM/yyyy";
                dtp_birth_date.Checked = true;
                dtp_birth_date.Value = (DateTime)dgv_tblqldt_member.Rows[e.RowIndex].Cells["birth_date"].Value;

                string gender = dgv_tblqldt_member.Rows[e.RowIndex].Cells["gender"].Value.ToString();
                if (gender == "Nam")
                {
                    rdo_male.Checked = true;
                    rdo_female.Checked = false;
                }
                else
                {
                    rdo_male.Checked = false;
                    rdo_female.Checked = true;
                }

                txt_address.Text = dgv_tblqldt_member.Rows[e.RowIndex].Cells["address"].Value.ToString();
                cmb_status.Text = dgv_tblqldt_member.Rows[e.RowIndex].Cells["status"].Value.ToString();

                //bật, tắt button
                btn_insert.Enabled = true;
                btn_edit.Enabled = true;
                btn_delete.Enabled = true;
                btn_cancel.Enabled = false;
            }
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_insert.Text == "Thêm Mới")
                {
                    Load_contrac_get_for_member();
                    btn_insert.Text = "Lưu";
                    btn_insert.Enabled = true;
                    btn_delete.Enabled = false;
                    btn_cancel.Enabled = true;

                    dgv_tblqldt_member.Enabled = false;

                    txt_member_nbr.Clear();                   
                    cmb_contract_nbr.SelectedIndex = -1;
                    txt_full_name.Clear();
                    txt_national_id.Clear();
                    rdo_male.Checked = false;
                    rdo_female.Checked = false;
                    txt_phone.Clear();
                    txt_address.Clear();
                                                   
                    cmb_status.Items.Clear();
                    cmb_status.Items.Add("DANG_O");
                    cmb_status.Items.Add("CHO_NHAN_PHONG");
                    cmb_status.SelectedIndex = -1;

                    dtp_birth_date.Format = DateTimePickerFormat.Custom;
                    dtp_birth_date.CustomFormat = "dd/MM/yyyy";
                    dtp_birth_date.Checked = false;

                    txt_member_nbr.Enabled = true;
                    cmb_contract_nbr.Enabled = true;
                    txt_full_name.Enabled = true;
                    chk_is_primary_tenant.Enabled = true;
                    txt_national_id.Enabled = true;
                    txt_phone.Enabled = true;
                    dtp_birth_date.Enabled = true;
                    rdo_male.Enabled = true;
                    rdo_female.Enabled = true;
                    txt_address.Enabled = true;
                    cmb_status.Enabled = true;

                    txt_member_nbr.Focus();
                }
                else
                {
                    tblqldt_member_insert_vm vm = new tblqldt_member_insert_vm();
                    vm.member_nbr = txt_member_nbr.Text;
                    vm.contract_nbr = cmb_contract_nbr.Text;
                    vm.full_name = txt_full_name.Text;
                    vm.is_primary_tenant = chk_is_primary_tenant.Checked;
                    vm.national_id = txt_national_id.Text;
                    vm.birth_date = dtp_birth_date.Value;
                    vm.status = cmb_status.Text;

                    string gender;
                    if (rdo_male.Checked)
                    {
                        gender = "Nam";
                    }
                    else
                    {
                        gender = "Nữ";
                    }

                    vm.gender = gender;

                    vm.phone = txt_phone.Text;
                    vm.address = txt_address.Text;

                    _validationService.Validate(vm);

                    DataTable dt = _memberService.tblqldt_member_insert
                    (
                        txt_member_nbr.Text,
                        cmb_contract_nbr.Text,
                        txt_full_name.Text,
                        chk_is_primary_tenant.Checked,
                        txt_national_id.Text,
                        dtp_birth_date.Value,
                        gender,
                        txt_phone.Text,
                        txt_address.Text,
                        cmb_status.Text
                    );

                    msg = dt.Rows[0]["msg"].ToString();
                    MessageBox.Show(msg, "Thêm Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    Load_tblqldt_member();

                    //ResetForm
                    dgv_tblqldt_member.Enabled = true;

                    txt_member_nbr.Clear();
                    cmb_contract_nbr.SelectedIndex = -1;
                    chk_is_primary_tenant.Checked = false;
                    txt_national_id.Clear();

                    dtp_birth_date.ShowCheckBox = true;
                    dtp_birth_date.Format = DateTimePickerFormat.Custom;
                    dtp_birth_date.CustomFormat = " ";
                    dtp_birth_date.Checked = false;

                    rdo_male.Checked = false;
                    rdo_female.Checked = false;
                    txt_phone.Clear();
                    txt_address.Clear();
                    cmb_status.SelectedIndex = -1;

                    txt_member_nbr.Enabled = false;
                    cmb_contract_nbr.Enabled = false;
                    txt_full_name.Enabled = false;
                    chk_is_primary_tenant.Enabled = false;
                    txt_national_id.Enabled = false;
                    txt_phone.Enabled = false;
                    dtp_birth_date.Enabled = false;
                    rdo_male.Enabled = false;
                    rdo_female.Enabled = false;
                    txt_address.Enabled = false;
                    cmb_status.Enabled = false;

                    btn_insert.Enabled = true;
                    btn_insert.Text = "Thêm Mới";

                    btn_edit.Enabled = false;
                    btn_edit.Text = "Cập Nhật";

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
                    
                    Load_contrac_get_for_member();

                    btn_edit.Text = "Lưu";
                    btn_insert.Enabled = false;
                    btn_delete.Enabled = false;
                    btn_cancel.Enabled = true;

                    dgv_tblqldt_member.Enabled = false;

                    txt_member_nbr.Enabled = false; // khóa mã khi update
                    cmb_contract_nbr.Enabled = true;
                    txt_full_name.Enabled = true;
                    chk_is_primary_tenant.Enabled = true;
                    txt_national_id.Enabled = true;
                    txt_phone.Enabled = true;
                    dtp_birth_date.Enabled = true;
                    rdo_male.Enabled = true;
                    rdo_female.Enabled = true;
                    txt_address.Enabled = true;
                    cmb_status.Enabled = true;

                    txt_full_name.Focus();
                }
                else
                {
                    tblqldt_member_update_vm vm = new tblqldt_member_update_vm();
                    vm.member_nbr = txt_member_nbr.Text;        
                    vm.contract_nbr = cmb_contract_nbr.Text;
                    vm.full_name = txt_full_name.Text;
                    vm.is_primary_tenant = chk_is_primary_tenant.Checked;
                    vm.national_id = txt_national_id.Text;
                    vm.birth_date = dtp_birth_date.Value;
                    vm.status = cmb_status.Text;

                    string gender;
                    if (rdo_male.Checked)
                    {
                        gender = "Nam";
                    }
                    else
                    {
                        gender = "Nữ";
                    }

                    vm.gender = gender;

                    vm.phone = txt_phone.Text;
                    vm.address = txt_address.Text;

                    _validationService.Validate(vm);

                    DataTable dt = _memberService.tblqldt_member_update(
                        txt_member_nbr.Text,
                        cmb_contract_nbr.Text,
                        txt_full_name.Text,
                        chk_is_primary_tenant.Checked,
                        txt_national_id.Text,
                        dtp_birth_date.Value,
                        gender,
                        txt_phone.Text,
                        txt_address.Text,
                        cmb_status.Text
                    );

                    msg = dt.Rows[0]["msg"].ToString();
                    MessageBox.Show(msg, "Cập Nhật Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Load_tblqldt_member();

                    // RESET FORM
                    dgv_tblqldt_member.Enabled = true;

                    txt_member_nbr.Clear();
                    cmb_contract_nbr.SelectedIndex = -1;
                    txt_full_name.Clear();
                    chk_is_primary_tenant.Checked = false;
                    txt_national_id.Clear();

                    dtp_birth_date.ShowCheckBox = true;
                    dtp_birth_date.Format = DateTimePickerFormat.Custom;
                    dtp_birth_date.CustomFormat = " ";
                    dtp_birth_date.Checked = false;

                    rdo_male.Checked = false;
                    rdo_female.Checked = false;
                    txt_phone.Clear();
                    txt_address.Clear();
                    cmb_status.SelectedIndex = -1;

                    txt_member_nbr.Enabled = false;
                    cmb_contract_nbr.Enabled = false;
                    txt_full_name.Enabled = false;
                    chk_is_primary_tenant.Enabled = false;
                    txt_national_id.Enabled = false;
                    txt_phone.Enabled = false;
                    dtp_birth_date.Enabled = false;
                    rdo_male.Enabled = false;
                    rdo_female.Enabled = false;
                    txt_address.Enabled = false;
                    cmb_status.Enabled = false;

                    btn_edit.Text = "Cập Nhật";
                    btn_insert.Enabled = true;
                    btn_delete.Enabled = false;
                    btn_cancel.Enabled = false;
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
                if (string.IsNullOrEmpty(txt_member_nbr.Text))
                {
                    MessageBox.Show("Chưa chọn thành viên!");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa thành viên này không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.No)
                    return;

                DataTable dt = _memberService.tblqldt_member_delete(txt_member_nbr.Text);

                msg = dt.Rows[0]["msg"].ToString();
                MessageBox.Show(msg, "Cập Nhập Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Load_tblqldt_member();

                //Reset Form

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            dgv_tblqldt_member.Enabled = true;

            txt_member_nbr.Clear();
            cmb_contract_nbr.SelectedIndex = -1;
            chk_is_primary_tenant.Checked = false;
            txt_national_id.Clear();

            dtp_birth_date.ShowCheckBox = true;
            dtp_birth_date.Format = DateTimePickerFormat.Custom;
            dtp_birth_date.CustomFormat = " ";
            dtp_birth_date.Checked = false;

            rdo_male.Checked = false;
            rdo_female.Checked = false;
            txt_phone.Clear();
            txt_address.Clear();
            cmb_status.SelectedIndex = -1;

            txt_member_nbr.Enabled = false;
            cmb_contract_nbr.Enabled = false;
            txt_full_name.Enabled = false;
            chk_is_primary_tenant.Enabled = false;
            txt_national_id.Enabled = false;
            txt_phone.Enabled = false;
            dtp_birth_date.Enabled = false;
            rdo_male.Enabled = false;
            rdo_female.Enabled = false;
            txt_address.Enabled = false;
            cmb_status.Enabled = false;

            btn_insert.Enabled = true;
            btn_insert.Text = "Thêm Mới";

            btn_edit.Enabled = false;
            btn_edit.Text = "Cập Nhật";

            btn_delete.Enabled = false;
            btn_cancel.Enabled = false;
        }
    }
}
