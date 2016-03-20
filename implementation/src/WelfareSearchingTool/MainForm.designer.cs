namespace WelfareSearchingTool
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbRandCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pbRandCode = new System.Windows.Forms.PictureBox();
            this.cbIsSave = new System.Windows.Forms.CheckBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbLastRankingNum = new System.Windows.Forms.Label();
            this.tbResult_recvNum = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbResult_Area = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbResult_Id = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbResult_RankingNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbResult_Name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.软件说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRandCode)).BeginInit();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1136, 598);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1130, 144);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbRandCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pbRandCode);
            this.groupBox1.Controls.Add(this.cbIsSave);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbUserName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1130, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户登陆";
            // 
            // tbRandCode
            // 
            this.tbRandCode.Location = new System.Drawing.Point(655, 40);
            this.tbRandCode.Name = "tbRandCode";
            this.tbRandCode.Size = new System.Drawing.Size(100, 28);
            this.tbRandCode.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(571, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "验证码：";
            // 
            // pbRandCode
            // 
            this.pbRandCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbRandCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbRandCode.Location = new System.Drawing.Point(876, 40);
            this.pbRandCode.Name = "pbRandCode";
            this.pbRandCode.Size = new System.Drawing.Size(161, 50);
            this.pbRandCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbRandCode.TabIndex = 5;
            this.pbRandCode.TabStop = false;
            this.pbRandCode.Click += new System.EventHandler(this.pbRandCode_Click);
            // 
            // cbIsSave
            // 
            this.cbIsSave.AutoSize = true;
            this.cbIsSave.Checked = true;
            this.cbIsSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsSave.Location = new System.Drawing.Point(439, 48);
            this.cbIsSave.Name = "cbIsSave";
            this.cbIsSave.Size = new System.Drawing.Size(106, 22);
            this.cbIsSave.TabIndex = 4;
            this.cbIsSave.Text = "是否保存";
            this.cbIsSave.UseVisualStyleBackColor = true;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(134, 88);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(260, 28);
            this.tbPassword.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码：";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(134, 42);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(260, 28);
            this.tbUserName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbLastRankingNum);
            this.panel2.Controls.Add(this.tbResult_recvNum);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.tbResult_Area);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.tbResult_Id);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.tbResult_RankingNum);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.tbResult_Name);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnCheck);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 153);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1130, 474);
            this.panel2.TabIndex = 1;
            // 
            // lbLastRankingNum
            // 
            this.lbLastRankingNum.AutoSize = true;
            this.lbLastRankingNum.Location = new System.Drawing.Point(400, 81);
            this.lbLastRankingNum.Name = "lbLastRankingNum";
            this.lbLastRankingNum.Size = new System.Drawing.Size(17, 18);
            this.lbLastRankingNum.TabIndex = 17;
            this.lbLastRankingNum.Text = ".";
            // 
            // tbResult_recvNum
            // 
            this.tbResult_recvNum.Location = new System.Drawing.Point(153, 163);
            this.tbResult_recvNum.Name = "tbResult_recvNum";
            this.tbResult_recvNum.Size = new System.Drawing.Size(466, 28);
            this.tbResult_recvNum.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(61, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 18);
            this.label8.TabIndex = 15;
            this.label8.Text = "回执编号：";
            // 
            // tbResult_Area
            // 
            this.tbResult_Area.Location = new System.Drawing.Point(153, 225);
            this.tbResult_Area.Name = "tbResult_Area";
            this.tbResult_Area.Size = new System.Drawing.Size(466, 28);
            this.tbResult_Area.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(61, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 18);
            this.label7.TabIndex = 13;
            this.label7.Text = "可选面积：";
            // 
            // tbResult_Id
            // 
            this.tbResult_Id.Location = new System.Drawing.Point(153, 118);
            this.tbResult_Id.Name = "tbResult_Id";
            this.tbResult_Id.Size = new System.Drawing.Size(466, 28);
            this.tbResult_Id.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "身份证：";
            // 
            // tbResult_RankingNum
            // 
            this.tbResult_RankingNum.Location = new System.Drawing.Point(153, 75);
            this.tbResult_RankingNum.Name = "tbResult_RankingNum";
            this.tbResult_RankingNum.Size = new System.Drawing.Size(212, 28);
            this.tbResult_RankingNum.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "当前排名：";
            // 
            // tbResult_Name
            // 
            this.tbResult_Name.Location = new System.Drawing.Point(153, 29);
            this.tbResult_Name.Name = "tbResult_Name";
            this.tbResult_Name.Size = new System.Drawing.Size(212, 28);
            this.tbResult_Name.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(85, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "姓名：";
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(943, 29);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(112, 92);
            this.btnCheck.TabIndex = 0;
            this.btnCheck.Text = "查询排名";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1136, 32);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.软件说明ToolStripMenuItem});
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(58, 28);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // 软件说明ToolStripMenuItem
            // 
            this.软件说明ToolStripMenuItem.Name = "软件说明ToolStripMenuItem";
            this.软件说明ToolStripMenuItem.Size = new System.Drawing.Size(165, 30);
            this.软件说明ToolStripMenuItem.Text = "软件说明";
            this.软件说明ToolStripMenuItem.Click += new System.EventHandler(this.软件说明ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Location = new System.Drawing.Point(0, 608);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1136, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 630);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "查询公租房排名信息";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRandCode)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.CheckBox cbIsSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbRandCode;
        private System.Windows.Forms.TextBox tbRandCode;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbResult_RankingNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbResult_Name;
        private System.Windows.Forms.TextBox tbResult_Id;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbResult_Area;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbResult_recvNum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 软件说明ToolStripMenuItem;
        private System.Windows.Forms.Label lbLastRankingNum;
    }
}

