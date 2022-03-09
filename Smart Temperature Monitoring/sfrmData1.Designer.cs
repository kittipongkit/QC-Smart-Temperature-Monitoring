namespace Smart_Temperature_Monitoring
{
    partial class sfrmData1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sfrmData1));
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.dtDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbSelectedTool = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.cbbSampling = new System.Windows.Forms.ComboBox();
            this.cbbSelectedName = new System.Windows.Forms.ComboBox();
            this.cbbSelectedFoor = new System.Windows.Forms.ComboBox();
            this.cbbSelectedLocation = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(12, 20);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(1781, 646);
            this.cartesianChart1.TabIndex = 4;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // dtDateTo
            // 
            this.dtDateTo.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(153)))));
            this.dtDateTo.CustomFormat = "yyyy-MM-dd";
            this.dtDateTo.Font = new System.Drawing.Font("Calibri", 16F);
            this.dtDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateTo.Location = new System.Drawing.Point(970, 44);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.Size = new System.Drawing.Size(174, 34);
            this.dtDateTo.TabIndex = 50;
            this.dtDateTo.ValueChanged += new System.EventHandler(this.dtDateTo_ValueChanged);
            // 
            // dtDateFrom
            // 
            this.dtDateFrom.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(153)))));
            this.dtDateFrom.CustomFormat = "yyyy-MM-dd";
            this.dtDateFrom.Font = new System.Drawing.Font("Calibri", 16F);
            this.dtDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateFrom.Location = new System.Drawing.Point(778, 44);
            this.dtDateFrom.Name = "dtDateFrom";
            this.dtDateFrom.Size = new System.Drawing.Size(174, 34);
            this.dtDateFrom.TabIndex = 49;
            this.dtDateFrom.ValueChanged += new System.EventHandler(this.dtDateFrom_ValueChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(963, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 34);
            this.label4.TabIndex = 48;
            this.label4.Text = "DATE TO :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(774, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 34);
            this.label1.TabIndex = 47;
            this.label1.Text = "DATE FROM :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 34);
            this.label2.TabIndex = 47;
            this.label2.Text = "TOOL :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbSelectedTool
            // 
            this.cbbSelectedTool.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSelectedTool.FormattingEnabled = true;
            this.cbbSelectedTool.ItemHeight = 26;
            this.cbbSelectedTool.Location = new System.Drawing.Point(16, 44);
            this.cbbSelectedTool.Name = "cbbSelectedTool";
            this.cbbSelectedTool.Size = new System.Drawing.Size(174, 34);
            this.cbbSelectedTool.TabIndex = 51;
            this.cbbSelectedTool.Click += new System.EventHandler(this.cbbSelectedTool_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(1351, 39);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(122, 45);
            this.btnOk.TabIndex = 52;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.cbbSampling);
            this.panel1.Controls.Add(this.cbbSelectedName);
            this.panel1.Controls.Add(this.cbbSelectedFoor);
            this.panel1.Controls.Add(this.cbbSelectedLocation);
            this.panel1.Controls.Add(this.cbbSelectedTool);
            this.panel1.Controls.Add(this.dtDateTo);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dtDateFrom);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1819, 90);
            this.panel1.TabIndex = 53;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(1489, 39);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(122, 45);
            this.btnExport.TabIndex = 53;
            this.btnExport.Text = "EXPORT";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cbbSampling
            // 
            this.cbbSampling.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSampling.FormattingEnabled = true;
            this.cbbSampling.ItemHeight = 26;
            this.cbbSampling.Location = new System.Drawing.Point(1160, 44);
            this.cbbSampling.Name = "cbbSampling";
            this.cbbSampling.Size = new System.Drawing.Size(174, 34);
            this.cbbSampling.TabIndex = 51;
            // 
            // cbbSelectedName
            // 
            this.cbbSelectedName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSelectedName.FormattingEnabled = true;
            this.cbbSelectedName.ItemHeight = 26;
            this.cbbSelectedName.Location = new System.Drawing.Point(588, 44);
            this.cbbSelectedName.Name = "cbbSelectedName";
            this.cbbSelectedName.Size = new System.Drawing.Size(174, 34);
            this.cbbSelectedName.TabIndex = 51;
            this.cbbSelectedName.Click += new System.EventHandler(this.cbbSelectedName_Click);
            // 
            // cbbSelectedFoor
            // 
            this.cbbSelectedFoor.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSelectedFoor.FormattingEnabled = true;
            this.cbbSelectedFoor.ItemHeight = 26;
            this.cbbSelectedFoor.Location = new System.Drawing.Point(399, 44);
            this.cbbSelectedFoor.Name = "cbbSelectedFoor";
            this.cbbSelectedFoor.Size = new System.Drawing.Size(174, 34);
            this.cbbSelectedFoor.TabIndex = 51;
            this.cbbSelectedFoor.Click += new System.EventHandler(this.cbbSelectedFoor_Click);
            // 
            // cbbSelectedLocation
            // 
            this.cbbSelectedLocation.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSelectedLocation.FormattingEnabled = true;
            this.cbbSelectedLocation.ItemHeight = 26;
            this.cbbSelectedLocation.Location = new System.Drawing.Point(209, 44);
            this.cbbSelectedLocation.Name = "cbbSelectedLocation";
            this.cbbSelectedLocation.Size = new System.Drawing.Size(174, 34);
            this.cbbSelectedLocation.TabIndex = 51;
            this.cbbSelectedLocation.Click += new System.EventHandler(this.cbbSelectedLocation_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(586, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 34);
            this.label6.TabIndex = 47;
            this.label6.Text = "TEMP NAME :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(397, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 34);
            this.label5.TabIndex = 47;
            this.label5.Text = "FOOR :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(1156, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(174, 34);
            this.label7.TabIndex = 47;
            this.label7.Text = "SAMPLING TIME :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(207, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 34);
            this.label3.TabIndex = 47;
            this.label3.Text = "LOCATION :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.cartesianChart1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 90);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1819, 678);
            this.panel2.TabIndex = 54;
            // 
            // sfrmData1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1819, 768);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "sfrmData1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Temperature Monitoring  - Data";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.DateTimePicker dtDateTo;
        private System.Windows.Forms.DateTimePicker dtDateFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbSelectedTool;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbbSampling;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ComboBox cbbSelectedLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbSelectedName;
        private System.Windows.Forms.ComboBox cbbSelectedFoor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}