namespace QLDT.UI
{
    partial class frmContract
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_contract_nbr = new System.Windows.Forms.TextBox();
            this.cmb_room_nbr = new System.Windows.Forms.ComboBox();
            this.dtp_start_date = new System.Windows.Forms.DateTimePicker();
            this.dtp_end_date = new System.Windows.Forms.DateTimePicker();
            this.nud_room_price = new System.Windows.Forms.NumericUpDown();
            this.nud_electric_price = new System.Windows.Forms.NumericUpDown();
            this.nud_water_price = new System.Windows.Forms.NumericUpDown();
            this.nud_trash_fee = new System.Windows.Forms.NumericUpDown();
            this.nud_security_fee = new System.Windows.Forms.NumericUpDown();
            this.nud_deposit = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_insert = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_dsthanhvien = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgv_tblqldt_contract = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_room_price)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_electric_price)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_water_price)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_trash_fee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_security_fee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_deposit)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tblqldt_contract)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1254, 50);
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
            this.label1.Size = new System.Drawing.Size(1254, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "THÔNG TIN HỢP ĐỒNG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label9, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label11, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.txt_contract_nbr, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmb_room_nbr, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtp_start_date, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.dtp_end_date, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.nud_room_price, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.nud_electric_price, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.nud_water_price, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.nud_trash_fee, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.nud_security_fee, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.nud_deposit, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Times New Roman", 13F);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1254, 198);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(130, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã Hợp Đồng :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label3.Location = new System.Drawing.Point(12, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mã Phòng :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Ngày Bắt Đầu :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label5.Location = new System.Drawing.Point(12, 122);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.label5.Size = new System.Drawing.Size(137, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Ngày Kết Thúc :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label6.Location = new System.Drawing.Point(12, 159);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.label6.Size = new System.Drawing.Size(91, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "Tiền Cọc :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label7.Location = new System.Drawing.Point(656, 17);
            this.label7.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Giá Phòng :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label8.Location = new System.Drawing.Point(656, 52);
            this.label8.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 20);
            this.label8.TabIndex = 12;
            this.label8.Text = "Giá Điện :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label9.Location = new System.Drawing.Point(656, 87);
            this.label9.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 20);
            this.label9.TabIndex = 14;
            this.label9.Text = "Giá Nước";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label10.Location = new System.Drawing.Point(656, 122);
            this.label10.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 20);
            this.label10.TabIndex = 16;
            this.label10.Text = "Tiền Rác :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.label11.Location = new System.Drawing.Point(656, 159);
            this.label11.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 20);
            this.label11.TabIndex = 18;
            this.label11.Text = "Tiền An Ninh :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_contract_nbr
            // 
            this.txt_contract_nbr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_contract_nbr.Location = new System.Drawing.Point(184, 14);
            this.txt_contract_nbr.Margin = new System.Windows.Forms.Padding(2);
            this.txt_contract_nbr.Name = "txt_contract_nbr";
            this.txt_contract_nbr.Size = new System.Drawing.Size(440, 27);
            this.txt_contract_nbr.TabIndex = 1;
            // 
            // cmb_room_nbr
            // 
            this.cmb_room_nbr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_room_nbr.FormattingEnabled = true;
            this.cmb_room_nbr.Location = new System.Drawing.Point(184, 49);
            this.cmb_room_nbr.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_room_nbr.Name = "cmb_room_nbr";
            this.cmb_room_nbr.Size = new System.Drawing.Size(440, 27);
            this.cmb_room_nbr.TabIndex = 3;
            this.cmb_room_nbr.SelectedIndexChanged += new System.EventHandler(this.cmb_room_nbr_SelectedIndexChanged);
            // 
            // dtp_start_date
            // 
            this.dtp_start_date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp_start_date.Location = new System.Drawing.Point(184, 84);
            this.dtp_start_date.Margin = new System.Windows.Forms.Padding(2);
            this.dtp_start_date.Name = "dtp_start_date";
            this.dtp_start_date.Size = new System.Drawing.Size(440, 27);
            this.dtp_start_date.TabIndex = 5;
            // 
            // dtp_end_date
            // 
            this.dtp_end_date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp_end_date.Location = new System.Drawing.Point(184, 119);
            this.dtp_end_date.Margin = new System.Windows.Forms.Padding(2);
            this.dtp_end_date.Name = "dtp_end_date";
            this.dtp_end_date.Size = new System.Drawing.Size(440, 27);
            this.dtp_end_date.TabIndex = 7;
            this.dtp_end_date.ValueChanged += new System.EventHandler(this.dtp_end_date_ValueChanged);
            // 
            // nud_room_price
            // 
            this.nud_room_price.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_room_price.Location = new System.Drawing.Point(800, 14);
            this.nud_room_price.Margin = new System.Windows.Forms.Padding(2);
            this.nud_room_price.Name = "nud_room_price";
            this.nud_room_price.Size = new System.Drawing.Size(442, 27);
            this.nud_room_price.TabIndex = 11;
            // 
            // nud_electric_price
            // 
            this.nud_electric_price.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_electric_price.Location = new System.Drawing.Point(800, 49);
            this.nud_electric_price.Margin = new System.Windows.Forms.Padding(2);
            this.nud_electric_price.Name = "nud_electric_price";
            this.nud_electric_price.Size = new System.Drawing.Size(442, 27);
            this.nud_electric_price.TabIndex = 13;
            // 
            // nud_water_price
            // 
            this.nud_water_price.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_water_price.Location = new System.Drawing.Point(800, 84);
            this.nud_water_price.Margin = new System.Windows.Forms.Padding(2);
            this.nud_water_price.Name = "nud_water_price";
            this.nud_water_price.Size = new System.Drawing.Size(442, 27);
            this.nud_water_price.TabIndex = 15;
            // 
            // nud_trash_fee
            // 
            this.nud_trash_fee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_trash_fee.Location = new System.Drawing.Point(800, 119);
            this.nud_trash_fee.Margin = new System.Windows.Forms.Padding(2);
            this.nud_trash_fee.Name = "nud_trash_fee";
            this.nud_trash_fee.Size = new System.Drawing.Size(442, 27);
            this.nud_trash_fee.TabIndex = 17;
            // 
            // nud_security_fee
            // 
            this.nud_security_fee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_security_fee.Location = new System.Drawing.Point(800, 155);
            this.nud_security_fee.Margin = new System.Windows.Forms.Padding(2);
            this.nud_security_fee.Name = "nud_security_fee";
            this.nud_security_fee.Size = new System.Drawing.Size(442, 27);
            this.nud_security_fee.TabIndex = 19;
            // 
            // nud_deposit
            // 
            this.nud_deposit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_deposit.Location = new System.Drawing.Point(184, 155);
            this.nud_deposit.Margin = new System.Windows.Forms.Padding(2);
            this.nud_deposit.Name = "nud_deposit";
            this.nud_deposit.Size = new System.Drawing.Size(440, 27);
            this.nud_deposit.TabIndex = 9;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.flowLayoutPanel1.Controls.Add(this.btn_insert);
            this.flowLayoutPanel1.Controls.Add(this.btn_edit);
            this.flowLayoutPanel1.Controls.Add(this.btn_delete);
            this.flowLayoutPanel1.Controls.Add(this.btn_cancel);
            this.flowLayoutPanel1.Controls.Add(this.btn_dsthanhvien);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 248);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1254, 48);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btn_insert
            // 
            this.btn_insert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(187)))), ((int)(((byte)(120)))));
            this.btn_insert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_insert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_insert.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
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
            this.btn_edit.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_edit.ForeColor = System.Drawing.Color.Black;
            this.btn_edit.Location = new System.Drawing.Point(140, 5);
            this.btn_edit.Margin = new System.Windows.Forms.Padding(5);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
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
            this.btn_delete.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_delete.ForeColor = System.Drawing.Color.Black;
            this.btn_delete.Location = new System.Drawing.Point(270, 5);
            this.btn_delete.Margin = new System.Windows.Forms.Padding(5);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btn_delete.Size = new System.Drawing.Size(120, 38);
            this.btn_delete.TabIndex = 2;
            this.btn_delete.Text = "Xóa";
            this.btn_delete.UseVisualStyleBackColor = false;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_dsthanhvien
            // 
            this.btn_dsthanhvien.BackColor = System.Drawing.Color.Navy;
            this.btn_dsthanhvien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_dsthanhvien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_dsthanhvien.ForeColor = System.Drawing.Color.White;
            this.btn_dsthanhvien.Location = new System.Drawing.Point(530, 5);
            this.btn_dsthanhvien.Margin = new System.Windows.Forms.Padding(5);
            this.btn_dsthanhvien.Name = "btn_dsthanhvien";
            this.btn_dsthanhvien.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btn_dsthanhvien.Size = new System.Drawing.Size(300, 38);
            this.btn_dsthanhvien.TabIndex = 4;
            this.btn_dsthanhvien.Text = "Nhập Danh Sách Thành Viên";
            this.btn_dsthanhvien.UseVisualStyleBackColor = false;
            this.btn_dsthanhvien.Click += new System.EventHandler(this.btn_dsthanhvien_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(174)))), ((int)(((byte)(192)))));
            this.btn_cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_cancel.ForeColor = System.Drawing.Color.Black;
            this.btn_cancel.Location = new System.Drawing.Point(400, 5);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(5);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.btn_cancel.Size = new System.Drawing.Size(120, 38);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Hủy";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 296);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1254, 50);
            this.panel2.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Navy;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(10);
            this.label12.Size = new System.Drawing.Size(1254, 50);
            this.label12.TabIndex = 0;
            this.label12.Text = "DANH SÁCH HỢP ĐỒNG";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgv_tblqldt_contract);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 346);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1254, 384);
            this.panel3.TabIndex = 4;
            // 
            // dgv_tblqldt_contract
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.dgv_tblqldt_contract.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_tblqldt_contract.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_tblqldt_contract.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_tblqldt_contract.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_tblqldt_contract.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_tblqldt_contract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_tblqldt_contract.EnableHeadersVisualStyles = false;
            this.dgv_tblqldt_contract.Location = new System.Drawing.Point(0, 0);
            this.dgv_tblqldt_contract.Margin = new System.Windows.Forms.Padding(0);
            this.dgv_tblqldt_contract.Name = "dgv_tblqldt_contract";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_tblqldt_contract.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_tblqldt_contract.RowTemplate.Height = 28;
            this.dgv_tblqldt_contract.Size = new System.Drawing.Size(1254, 384);
            this.dgv_tblqldt_contract.TabIndex = 0;
            this.dgv_tblqldt_contract.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_dshopdong_CellClick);
            // 
            // frmContract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 730);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(550, 700);
            this.Name = "frmContract";
            this.Text = "frmHopDong";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmContract_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_room_price)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_electric_price)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_water_price)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_trash_fee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_security_fee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_deposit)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tblqldt_contract)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_contract_nbr;
        private System.Windows.Forms.ComboBox cmb_room_nbr;
        private System.Windows.Forms.DateTimePicker dtp_start_date;
        private System.Windows.Forms.DateTimePicker dtp_end_date;
        private System.Windows.Forms.NumericUpDown nud_room_price;
        private System.Windows.Forms.NumericUpDown nud_electric_price;
        private System.Windows.Forms.NumericUpDown nud_water_price;
        private System.Windows.Forms.NumericUpDown nud_trash_fee;
        private System.Windows.Forms.NumericUpDown nud_security_fee;
        private System.Windows.Forms.NumericUpDown nud_deposit;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgv_tblqldt_contract;
        private System.Windows.Forms.Button btn_dsthanhvien;
        private System.Windows.Forms.Button btn_cancel;
    }
}