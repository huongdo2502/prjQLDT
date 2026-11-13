namespace QLDT.UI
{
    partial class frmRoom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_room_nbr = new System.Windows.Forms.TextBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.cmb_status = new System.Windows.Forms.ComboBox();
            this.nud_area = new System.Windows.Forms.NumericUpDown();
            this.nud_max_qty_man = new System.Windows.Forms.NumericUpDown();
            this.btn_insert = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgv_tblqldt_room = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_area)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_max_qty_man)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tblqldt_room)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1268, 50);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10);
            this.label1.Size = new System.Drawing.Size(1268, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "THÔNG TIN PHÒNG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txt_room_nbr, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_name, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmb_status, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.nud_area, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.nud_max_qty_man, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 50);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1268, 200);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(103, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã Phòng :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(12, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tên Phòng :";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(12, 90);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label4.Size = new System.Drawing.Size(108, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Trạng Thái :";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(12, 126);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label5.Size = new System.Drawing.Size(101, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Diện Tích :";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(12, 162);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label6.Size = new System.Drawing.Size(171, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "Số Người Ở Tối Đa :";
            // 
            // txt_room_nbr
            // 
            this.txt_room_nbr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_room_nbr.BackColor = System.Drawing.Color.White;
            this.txt_room_nbr.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.txt_room_nbr.ForeColor = System.Drawing.Color.Black;
            this.txt_room_nbr.Location = new System.Drawing.Point(511, 14);
            this.txt_room_nbr.Margin = new System.Windows.Forms.Padding(2);
            this.txt_room_nbr.Name = "txt_room_nbr";
            this.txt_room_nbr.Size = new System.Drawing.Size(745, 27);
            this.txt_room_nbr.TabIndex = 1;
            // 
            // txt_name
            // 
            this.txt_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_name.BackColor = System.Drawing.Color.White;
            this.txt_name.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.txt_name.ForeColor = System.Drawing.Color.Black;
            this.txt_name.Location = new System.Drawing.Point(511, 50);
            this.txt_name.Margin = new System.Windows.Forms.Padding(2);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(745, 27);
            this.txt_name.TabIndex = 3;
            // 
            // cmb_status
            // 
            this.cmb_status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_status.BackColor = System.Drawing.Color.White;
            this.cmb_status.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.cmb_status.ForeColor = System.Drawing.Color.Black;
            this.cmb_status.FormattingEnabled = true;
            this.cmb_status.Items.AddRange(new object[] {
            "TRONG",
            "KHONG_CHO_THUE",
            "DANG_THUE"});
            this.cmb_status.Location = new System.Drawing.Point(511, 86);
            this.cmb_status.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_status.Name = "cmb_status";
            this.cmb_status.Size = new System.Drawing.Size(745, 27);
            this.cmb_status.TabIndex = 5;
            // 
            // nud_area
            // 
            this.nud_area.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_area.BackColor = System.Drawing.Color.White;
            this.nud_area.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.nud_area.ForeColor = System.Drawing.Color.Black;
            this.nud_area.Location = new System.Drawing.Point(511, 122);
            this.nud_area.Margin = new System.Windows.Forms.Padding(2);
            this.nud_area.Name = "nud_area";
            this.nud_area.Size = new System.Drawing.Size(745, 27);
            this.nud_area.TabIndex = 7;
            // 
            // nud_max_qty_man
            // 
            this.nud_max_qty_man.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_max_qty_man.BackColor = System.Drawing.Color.White;
            this.nud_max_qty_man.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.nud_max_qty_man.ForeColor = System.Drawing.Color.Black;
            this.nud_max_qty_man.Location = new System.Drawing.Point(511, 158);
            this.nud_max_qty_man.Margin = new System.Windows.Forms.Padding(2);
            this.nud_max_qty_man.Name = "nud_max_qty_man";
            this.nud_max_qty_man.Size = new System.Drawing.Size(745, 27);
            this.nud_max_qty_man.TabIndex = 9;
            // 
            // btn_insert
            // 
            this.btn_insert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(187)))), ((int)(((byte)(120)))));
            this.btn_insert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_insert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_insert.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.btn_insert.ForeColor = System.Drawing.Color.Black;
            this.btn_insert.Location = new System.Drawing.Point(10, 5);
            this.btn_insert.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.btn_insert.Name = "btn_insert";
            this.btn_insert.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btn_insert.Size = new System.Drawing.Size(120, 38);
            this.btn_insert.TabIndex = 0;
            this.btn_insert.Text = "Thêm Mới";
            this.btn_insert.UseVisualStyleBackColor = false;
            this.btn_insert.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(153)))), ((int)(((byte)(225)))));
            this.btn_edit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_edit.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.btn_edit.ForeColor = System.Drawing.Color.Black;
            this.btn_edit.Location = new System.Drawing.Point(140, 5);
            this.btn_edit.Margin = new System.Windows.Forms.Padding(5);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(120, 38);
            this.btn_edit.TabIndex = 1;
            this.btn_edit.Text = "Cập Nhật";
            this.btn_edit.UseVisualStyleBackColor = false;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(101)))), ((int)(((byte)(101)))));
            this.btn_delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.btn_delete.ForeColor = System.Drawing.Color.Black;
            this.btn_delete.Location = new System.Drawing.Point(270, 5);
            this.btn_delete.Margin = new System.Windows.Forms.Padding(5);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(120, 38);
            this.btn_delete.TabIndex = 2;
            this.btn_delete.Text = "Xóa";
            this.btn_delete.UseVisualStyleBackColor = false;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.flowLayoutPanel1.Controls.Add(this.btn_insert);
            this.flowLayoutPanel1.Controls.Add(this.btn_edit);
            this.flowLayoutPanel1.Controls.Add(this.btn_delete);
            this.flowLayoutPanel1.Controls.Add(this.btn_cancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 250);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1268, 50);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(174)))), ((int)(((byte)(192)))));
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.btn_cancel.Location = new System.Drawing.Point(398, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(120, 38);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "Hủy";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 300);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1268, 50);
            this.panel2.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Navy;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(10);
            this.label7.Size = new System.Drawing.Size(268, 46);
            this.label7.TabIndex = 0;
            this.label7.Text = "DANH SÁCH PHÒNG";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgv_tblqldt_room);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 350);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1268, 332);
            this.panel3.TabIndex = 6;
            // 
            // dgv_tblqldt_room
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.dgv_tblqldt_room.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_tblqldt_room.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_tblqldt_room.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_tblqldt_room.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_tblqldt_room.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_tblqldt_room.EnableHeadersVisualStyles = false;
            this.dgv_tblqldt_room.Location = new System.Drawing.Point(0, 0);
            this.dgv_tblqldt_room.Margin = new System.Windows.Forms.Padding(0);
            this.dgv_tblqldt_room.Name = "dgv_tblqldt_room";
            this.dgv_tblqldt_room.RowTemplate.Height = 28;
            this.dgv_tblqldt_room.Size = new System.Drawing.Size(1268, 332);
            this.dgv_tblqldt_room.TabIndex = 0;
            this.dgv_tblqldt_room.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_dsphong_CellClick);
            // 
            // frmRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1268, 682);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(550, 700);
            this.Name = "frmRoom";
            this.Text = "frmPhong";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRoom_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_area)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_max_qty_man)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tblqldt_room)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.DataGridView dgv_tblqldt_room;
        public System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_room_nbr;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.ComboBox cmb_status;
        private System.Windows.Forms.NumericUpDown nud_area;
        private System.Windows.Forms.NumericUpDown nud_max_qty_man;
        private System.Windows.Forms.Button btn_cancel;
    }
}