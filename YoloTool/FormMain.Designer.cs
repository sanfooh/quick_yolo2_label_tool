namespace YoloTool
{
    partial class FormMain
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonClear = new System.Windows.Forms.Button();
            this.textBoxTrain = new System.Windows.Forms.TextBox();
            this.radioButtonWindows = new System.Windows.Forms.RadioButton();
            this.radioButtonLinux = new System.Windows.Forms.RadioButton();
            this.textBoxRoot = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPercent = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.listBoxLableIndex = new System.Windows.Forms.ListBox();
            this.listBoxLable = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxImport = new System.Windows.Forms.TextBox();
            this.buttonImport = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAddObj = new System.Windows.Forms.TextBox();
            this.buttonAddObj = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1192, 595);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonAddObj);
            this.tabPage1.Controls.Add(this.textBoxAddObj);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.buttonImport);
            this.tabPage1.Controls.Add(this.textBoxImport);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.buttonClear);
            this.tabPage1.Controls.Add(this.textBoxTrain);
            this.tabPage1.Controls.Add(this.radioButtonWindows);
            this.tabPage1.Controls.Add(this.radioButtonLinux);
            this.tabPage1.Controls.Add(this.textBoxRoot);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBoxPercent);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.buttonCreate);
            this.tabPage1.Controls.Add(this.listBoxLableIndex);
            this.tabPage1.Controls.Add(this.listBoxLable);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.listBoxFiles);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1184, 569);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "create";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(4, 24);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(198, 23);
            this.buttonClear.TabIndex = 18;
            this.buttonClear.Text = "delete jpg donot have txt";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // textBoxTrain
            // 
            this.textBoxTrain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxTrain.Location = new System.Drawing.Point(10, 541);
            this.textBoxTrain.Name = "textBoxTrain";
            this.textBoxTrain.Size = new System.Drawing.Size(214, 20);
            this.textBoxTrain.TabIndex = 17;
            // 
            // radioButtonWindows
            // 
            this.radioButtonWindows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonWindows.AutoSize = true;
            this.radioButtonWindows.Location = new System.Drawing.Point(56, 500);
            this.radioButtonWindows.Name = "radioButtonWindows";
            this.radioButtonWindows.Size = new System.Drawing.Size(66, 17);
            this.radioButtonWindows.TabIndex = 16;
            this.radioButtonWindows.Text = "windows";
            this.radioButtonWindows.UseVisualStyleBackColor = true;
            // 
            // radioButtonLinux
            // 
            this.radioButtonLinux.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonLinux.AutoSize = true;
            this.radioButtonLinux.Checked = true;
            this.radioButtonLinux.Location = new System.Drawing.Point(4, 500);
            this.radioButtonLinux.Name = "radioButtonLinux";
            this.radioButtonLinux.Size = new System.Drawing.Size(46, 17);
            this.radioButtonLinux.TabIndex = 15;
            this.radioButtonLinux.TabStop = true;
            this.radioButtonLinux.Text = "linux";
            this.radioButtonLinux.UseVisualStyleBackColor = true;
            // 
            // textBoxRoot
            // 
            this.textBoxRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxRoot.Location = new System.Drawing.Point(75, 449);
            this.textBoxRoot.Name = "textBoxRoot";
            this.textBoxRoot.Size = new System.Drawing.Size(149, 20);
            this.textBoxRoot.TabIndex = 14;
            this.textBoxRoot.Text = "/root/yolo/darknet/";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 452);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "darknet folder";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 472);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "%";
            // 
            // textBoxPercent
            // 
            this.textBoxPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxPercent.Location = new System.Drawing.Point(95, 470);
            this.textBoxPercent.Name = "textBoxPercent";
            this.textBoxPercent.Size = new System.Drawing.Size(100, 20);
            this.textBoxPercent.TabIndex = 11;
            this.textBoxPercent.Text = "20";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 473);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Test percent";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCreate.Location = new System.Drawing.Point(136, 500);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(75, 23);
            this.buttonCreate.TabIndex = 9;
            this.buttonCreate.Text = "create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // listBoxLableIndex
            // 
            this.listBoxLableIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLableIndex.FormattingEnabled = true;
            this.listBoxLableIndex.Location = new System.Drawing.Point(1020, 53);
            this.listBoxLableIndex.Name = "listBoxLableIndex";
            this.listBoxLableIndex.Size = new System.Drawing.Size(156, 225);
            this.listBoxLableIndex.TabIndex = 8;
            this.listBoxLableIndex.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBoxLableIndex_KeyUp);
            // 
            // listBoxLable
            // 
            this.listBoxLable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLable.FormattingEnabled = true;
            this.listBoxLable.Location = new System.Drawing.Point(1022, 302);
            this.listBoxLable.Name = "listBoxLable";
            this.listBoxLable.Size = new System.Drawing.Size(156, 238);
            this.listBoxLable.TabIndex = 5;
            this.listBoxLable.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBoxLable_KeyUp);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(230, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 497);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(15, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(10, 10);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.HorizontalScrollbar = true;
            this.listBoxFiles.Location = new System.Drawing.Point(8, 53);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxFiles.Size = new System.Drawing.Size(216, 381);
            this.listBoxFiles.TabIndex = 2;
            this.listBoxFiles.SelectedIndexChanged += new System.EventHandler(this.listBoxFiles_SelectedIndexChanged);
            this.listBoxFiles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listBoxFiles_KeyUp);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1184, 569);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "test";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(209, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "以后再写，嘿嘿";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "path:";
            // 
            // textBoxImport
            // 
            this.textBoxImport.Location = new System.Drawing.Point(268, 24);
            this.textBoxImport.Name = "textBoxImport";
            this.textBoxImport.Size = new System.Drawing.Size(483, 20);
            this.textBoxImport.TabIndex = 20;
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(766, 22);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 23);
            this.buttonImport.TabIndex = 21;
            this.buttonImport.Text = "import jpg";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1022, 283);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Labels";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1020, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "objs";
            // 
            // textBoxAddObj
            // 
            this.textBoxAddObj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddObj.Location = new System.Drawing.Point(1053, 33);
            this.textBoxAddObj.Name = "textBoxAddObj";
            this.textBoxAddObj.Size = new System.Drawing.Size(67, 20);
            this.textBoxAddObj.TabIndex = 24;
            // 
            // buttonAddObj
            // 
            this.buttonAddObj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddObj.Location = new System.Drawing.Point(1127, 31);
            this.buttonAddObj.Name = "buttonAddObj";
            this.buttonAddObj.Size = new System.Drawing.Size(49, 23);
            this.buttonAddObj.TabIndex = 25;
            this.buttonAddObj.Text = "Add";
            this.buttonAddObj.UseVisualStyleBackColor = true;
            this.buttonAddObj.Click += new System.EventHandler(this.buttonAddObj_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(164, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(502, 177);
            this.label7.TabIndex = 1;
            this.label7.Text = resources.GetString("label7.Text");
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 595);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormMain";
            this.Text = "YoloTool";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.TabPage tabPage2;
        private comEnPicBox.comEnPiBox comEnPiBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBoxLableIndex;
        private System.Windows.Forms.ListBox listBoxLable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPercent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.TextBox textBoxRoot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButtonWindows;
        private System.Windows.Forms.RadioButton radioButtonLinux;
        private System.Windows.Forms.TextBox textBoxTrain;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonAddObj;
        private System.Windows.Forms.TextBox textBoxAddObj;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.TextBox textBoxImport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;

    }
}

