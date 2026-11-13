
using System;
using System.Windows.Forms;
using QLDT.Service;
using System.Data;
using QLDT.UI.ViewModel;

namespace QLDT.UI
{
    public partial class frmRoom : Form
    {
        private IRoomService _roomService;
        private IValidationService _validationService;

        private string msg;

        public frmRoom()
        {
            InitializeComponent();
            _roomService = new RoomService();
            _validationService = new ValidationService();

        }

        private void frmRoom_Load(object sender, EventArgs e)
        {
            Load_dsphong();

            // Hiển thị Diện Tích
            nud_area.Minimum = 0;
            nud_area.Maximum = 99;
            nud_area.DecimalPlaces = 2;

            //Hiển thị Số người ở         
            nud_max_qty_man.Minimum = 0;
            nud_max_qty_man.Maximum = 100;
            nud_max_qty_man.DecimalPlaces = 0; //Chũ số sau dấu phấy
            nud_max_qty_man.Increment = 1; // bước nhảy tăng, giảm 1

            //ResetForm
            txt_room_nbr.Clear();
            txt_name.Clear();

            nud_area.Value = nud_area.Minimum;
            nud_max_qty_man.Value = nud_max_qty_man.Minimum;

            txt_room_nbr.Enabled = false;
            txt_name.Enabled = false;
            cmb_status.Enabled = false;
            nud_area.Enabled = false;
            nud_max_qty_man.Enabled = false;

            dgv_tblqldt_room.Enabled = true;

            btn_insert.Enabled = true;
            btn_insert.Text = "Thêm Mới";

            btn_edit.Enabled = false;
            btn_edit.Text = "Cập Nhật";

            btn_delete.Enabled = false;
            btn_cancel.Enabled = false;
        }

        private void Load_dsphong()
        {
            DataTable dt = _roomService.tblqldt_room_get_all();
            dgv_tblqldt_room.DataSource = dt;
        }

        private void dgv_dsphong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txt_room_nbr.Text = dgv_tblqldt_room.Rows[e.RowIndex].Cells["room_nbr"].Value.ToString();
                txt_name.Text = dgv_tblqldt_room.Rows[e.RowIndex].Cells["name"].Value.ToString();
                cmb_status.Text = dgv_tblqldt_room.Rows[e.RowIndex].Cells["status"].Value.ToString();
                nud_area.Value = Convert.ToDecimal(dgv_tblqldt_room.Rows[e.RowIndex].Cells["area"].Value);
                nud_max_qty_man.Value = Convert.ToDecimal(dgv_tblqldt_room.Rows[e.RowIndex].Cells["max_qty_man"].Value);
            }

            //Bật, tắt button
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

                    btn_insert.Text = "Lưu";
                    btn_edit.Enabled = false;
                    btn_cancel.Enabled = true;
                    btn_delete.Enabled = false;

                    dgv_tblqldt_room.Enabled = false;

                    txt_room_nbr.Clear();
                    txt_name.Clear();
                    cmb_status.SelectedIndex = -1;
                    nud_area.Value = 0;
                    nud_max_qty_man.Value = 0;

                    txt_room_nbr.Enabled = true;
                    txt_name.Enabled = true;
                    cmb_status.Enabled = true;
                    nud_area.Enabled = true;
                    nud_max_qty_man.Enabled = true;

                    txt_room_nbr.Focus();

                    return;
                }
                else
                {
                    tblqldt_room_insert_vm vm = new tblqldt_room_insert_vm();
                    vm.room_nbr = txt_room_nbr.Text;
                    vm.name = txt_name.Text;
                    vm.status = cmb_status.Text;
                    vm.area = nud_area.Value;
                    vm.max_qty_man = (int)nud_max_qty_man.Value;

                    _validationService.Validate(vm);

                    DataTable dt = _roomService.tblqldt_room_insert
                    (
                        txt_room_nbr.Text,
                        txt_name.Text,
                        cmb_status.Text,
                        nud_area.Value,
                        (int)nud_max_qty_man.Value
                    );

                    msg = dt.Rows[0]["msg"].ToString();
                    MessageBox.Show(msg, "Thêm Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Thêm thành công thì load lại
                    Load_dsphong();

                    //Resetform

                    //txt_room_nbr.Clear();
                    txt_name.Clear();

                    cmb_status.SelectedIndex = -1;
                    cmb_status.Items.Clear();

                    nud_area.Value = nud_area.Minimum;
                    nud_max_qty_man.Value = nud_max_qty_man.Minimum;

                    txt_room_nbr.Enabled = false;
                    txt_name.Enabled = false;
                    cmb_status.Enabled = false;
                    nud_area.Enabled = false;
                    nud_max_qty_man.Enabled = false;

                    dgv_tblqldt_room.Enabled = true;

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
                    if (cmb_status.Text == "DANG_THUE")
                    {
                        MessageBox.Show(
                            "Phòng đang thuê không thể chỉnh sửa!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    btn_edit.Text = "Lưu";
                    btn_insert.Enabled = false;
                    btn_cancel.Enabled = true;
                    btn_delete.Enabled = false;
                    dgv_tblqldt_room.Enabled = false;

                    txt_room_nbr.Enabled = false;
                    txt_name.Enabled = true;
                    nud_area.Enabled = true;
                    nud_max_qty_man.Enabled = true;

                    cmb_status.Enabled = true;

                    txt_name.Focus();

                    return;
                }

                else // Save
                {
                    tblqldt_room_update_vm vm = new tblqldt_room_update_vm();
                    vm.name = txt_name.Text;
                    vm.status = cmb_status.Text;
                    vm.area = nud_area.Value;
                    vm.max_qty_man = (int)nud_max_qty_man.Value;

                    _validationService.Validate(vm);

                    DataTable dt = _roomService.tblqldt_room_update(
                        txt_room_nbr.Text,
                        txt_name.Text,
                        cmb_status.Text,
                        nud_area.Value,
                        (int)nud_max_qty_man.Value
                    );

                    msg = dt.Rows[0]["msg"].ToString();
                    MessageBox.Show(msg, "Cập Nhập Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Load_dsphong();

                    //Reset form
                    txt_room_nbr.Clear();
                    txt_name.Clear();

                    nud_area.Value = nud_area.Minimum;
                    nud_max_qty_man.Value = nud_max_qty_man.Minimum;

                    txt_room_nbr.Enabled = false;
                    txt_name.Enabled = false;
                    cmb_status.Enabled = false;
                    nud_area.Enabled = false;
                    nud_max_qty_man.Enabled = false;

                    dgv_tblqldt_room.Enabled = true;

                    btn_insert.Enabled = true;
                    btn_insert.Text = "Thêm Mới";

                    btn_edit.Enabled = false;
                    btn_edit.Text = "Cập Nhật";

                    btn_delete.Enabled = false;
                    btn_cancel.Enabled = false;

                    txt_name.Focus();

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
                DialogResult rs = MessageBox.Show("Bạn có chắc muốn xóa phòng này?", "Xác nhận xóa",
                                MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

                if (rs != DialogResult.Yes)
                    return;

                string room_nbr = txt_room_nbr.Text;
                DataTable dt = _roomService.tblqldt_room_delete(room_nbr);

                msg = dt.Rows[0]["msg"].ToString();
                MessageBox.Show(msg, "Cập Nhập Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Load_dsphong();

                //Reset form
                txt_room_nbr.Clear();
                txt_name.Clear();

                nud_area.Value = nud_area.Minimum;
                nud_max_qty_man.Value = nud_max_qty_man.Minimum;

                txt_room_nbr.Enabled = false;
                txt_name.Enabled = false;
                cmb_status.Enabled = false;
                nud_area.Enabled = false;
                nud_max_qty_man.Enabled = false;

                dgv_tblqldt_room.Enabled = true;

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
            txt_room_nbr.Clear();
            txt_name.Clear();

            nud_area.Value = nud_area.Minimum;
            nud_max_qty_man.Value = nud_max_qty_man.Minimum;

            txt_room_nbr.Enabled = false;
            txt_name.Enabled = false;
            cmb_status.Enabled = false;
            nud_area.Enabled = false;
            nud_max_qty_man.Enabled = false;

            dgv_tblqldt_room.Enabled = true;

            btn_insert.Enabled = true;
            btn_insert.Text = "Thêm Mới";

            btn_edit.Enabled = false;
            btn_edit.Text = "Cập Nhật";

            btn_delete.Enabled = false;
            btn_cancel.Enabled = false;
        }
    } 
}
