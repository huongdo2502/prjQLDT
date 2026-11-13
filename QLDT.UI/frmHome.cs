using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDT.UI
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();

            this.Margin = new Padding(0);
            this.Padding = new Padding(0);
            this.BackColor = Color.FromArgb(244, 246, 249); //Màu nền form xám nhạt
            this.Size = new Size(1024, 768); //Kích thước màn hình

        }

        private void btn_dsphong_Click(object sender, EventArgs e)
        {
            frmRoom fr = new frmRoom();
            fr.ShowDialog();

        }

        private void btn_dshopdong_Click(object sender, EventArgs e)
        {
            frmContract fc = new frmContract();
            fc.ShowDialog();

        }

        private void btn_dsphieuthu_Click(object sender, EventArgs e)
        {
            frmInvoice fi = new frmInvoice();
            fi.ShowDialog();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            // Đặt màu sắc và kích thước cho nút Danh sách phòng
            btn_dsphong.FlatStyle = FlatStyle.Flat; //Hiệu ứng
            btn_dsphong.BackColor = Color.FromArgb(52, 73, 94); //màu nền
            btn_dsphong.ForeColor = Color.FromArgb(248, 250, 252); //màu chữ
            btn_dsphong.Font = new Font("Times New Roman", 24F, FontStyle.Bold); //kiểu chữ, size chữ, đạm-nhạt
            btn_dsphong.Size = new Size(1000, 110); //kích thược button
            btn_dsphong.Cursor = Cursors.Hand; //hiệu ứng đổi thành hình bàn tay khi dê chuột vào

            // Đặt màu sắc và kích thước cho nút Danh sách hợp đồng
            btn_dshopdong.FlatStyle = FlatStyle.Flat;
            btn_dshopdong.BackColor = Color.FromArgb(52, 73, 94);
            btn_dshopdong.ForeColor = Color.FromArgb(248, 250, 252);
            btn_dshopdong.Font = new Font("Times New Roman", 24F, FontStyle.Bold);
            btn_dshopdong.Size = new Size(1000, 110);
            btn_dshopdong.Cursor = Cursors.Hand;

            // Đặt màu sắc và kích thước cho nút Danh sách thu tiền
            btn_dsphieuthu.FlatStyle = FlatStyle.Flat;
            btn_dsphieuthu.BackColor = Color.FromArgb(52, 73, 94);
            btn_dsphieuthu.ForeColor = Color.FromArgb(248, 250, 252);
            btn_dsphieuthu.Font = new Font("Times New Roman", 24F, FontStyle.Bold);
            btn_dsphieuthu.Size = new Size(1000, 110);
            btn_dsphieuthu.Cursor = Cursors.Hand;
        }  
    }
}
