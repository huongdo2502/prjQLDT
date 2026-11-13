namespace QLDT.UI
{
    partial class frmHome
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
            this.btn_dsphong = new System.Windows.Forms.Button();
            this.btn_dshopdong = new System.Windows.Forms.Button();
            this.btn_dsphieuthu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_dsphong
            // 
            this.btn_dsphong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btn_dsphong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_dsphong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_dsphong.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_dsphong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_dsphong.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dsphong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btn_dsphong.Location = new System.Drawing.Point(0, 0);
            this.btn_dsphong.Margin = new System.Windows.Forms.Padding(2);
            this.btn_dsphong.Name = "btn_dsphong";
            this.btn_dsphong.Size = new System.Drawing.Size(1314, 80);
            this.btn_dsphong.TabIndex = 0;
            this.btn_dsphong.Text = "DANH SÁCH PHÒNG";
            this.btn_dsphong.UseVisualStyleBackColor = false;
            this.btn_dsphong.Click += new System.EventHandler(this.btn_dsphong_Click);
            // 
            // btn_dshopdong
            // 
            this.btn_dshopdong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btn_dshopdong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_dshopdong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_dshopdong.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_dshopdong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_dshopdong.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dshopdong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btn_dshopdong.Location = new System.Drawing.Point(0, 80);
            this.btn_dshopdong.Margin = new System.Windows.Forms.Padding(2);
            this.btn_dshopdong.Name = "btn_dshopdong";
            this.btn_dshopdong.Size = new System.Drawing.Size(1314, 80);
            this.btn_dshopdong.TabIndex = 1;
            this.btn_dshopdong.Text = "DANH SÁCH HỢP ĐỒNG";
            this.btn_dshopdong.UseVisualStyleBackColor = false;
            this.btn_dshopdong.Click += new System.EventHandler(this.btn_dshopdong_Click);
            // 
            // btn_dsphieuthu
            // 
            this.btn_dsphieuthu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btn_dsphieuthu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_dsphieuthu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_dsphieuthu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_dsphieuthu.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold);
            this.btn_dsphieuthu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btn_dsphieuthu.Location = new System.Drawing.Point(0, 160);
            this.btn_dsphieuthu.Margin = new System.Windows.Forms.Padding(2);
            this.btn_dsphieuthu.Name = "btn_dsphieuthu";
            this.btn_dsphieuthu.Size = new System.Drawing.Size(1314, 80);
            this.btn_dsphieuthu.TabIndex = 2;
            this.btn_dsphieuthu.Text = "DANH SÁCH PHIẾU THU";
            this.btn_dsphieuthu.UseVisualStyleBackColor = false;
            this.btn_dsphieuthu.Click += new System.EventHandler(this.btn_dsphieuthu_Click);
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1314, 682);
            this.Controls.Add(this.btn_dsphieuthu);
            this.Controls.Add(this.btn_dshopdong);
            this.Controls.Add(this.btn_dsphong);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmHome";
            this.Text = "S";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmHome_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_dsphong;
        private System.Windows.Forms.Button btn_dshopdong;
        private System.Windows.Forms.Button btn_dsphieuthu;
    }
}

