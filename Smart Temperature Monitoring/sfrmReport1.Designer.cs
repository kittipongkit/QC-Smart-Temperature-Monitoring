namespace Smart_Temperature_Monitoring
{
    partial class sfrmReport1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.calenReport = new System.Windows.Forms.MonthCalendar();
            this.txtDateSelected = new System.Windows.Forms.Label();
            this.cbbSelectedTool = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbSelectedLocation = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbbSelectedFoor = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbbSelectedName = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbbReportType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(690, 90);
            this.panel1.TabIndex = 54;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1233, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 53;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1233, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "label5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1233, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "label3";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(291, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 34);
            this.label1.TabIndex = 47;
            this.label1.Text = "REPORT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(261, 418);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(204, 34);
            this.btnOk.TabIndex = 54;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // calenReport
            // 
            this.calenReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calenReport.Location = new System.Drawing.Point(399, 226);
            this.calenReport.MaxSelectionCount = 1;
            this.calenReport.Name = "calenReport";
            this.calenReport.TabIndex = 55;
            this.calenReport.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calenReport_DateSelected);
            // 
            // txtDateSelected
            // 
            this.txtDateSelected.BackColor = System.Drawing.SystemColors.Window;
            this.txtDateSelected.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtDateSelected.ForeColor = System.Drawing.Color.Black;
            this.txtDateSelected.Location = new System.Drawing.Point(462, 196);
            this.txtDateSelected.Name = "txtDateSelected";
            this.txtDateSelected.Size = new System.Drawing.Size(164, 27);
            this.txtDateSelected.TabIndex = 56;
            this.txtDateSelected.Text = "Date";
            this.txtDateSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbSelectedTool
            // 
            this.cbbSelectedTool.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSelectedTool.FormattingEnabled = true;
            this.cbbSelectedTool.ItemHeight = 19;
            this.cbbSelectedTool.Location = new System.Drawing.Point(81, 147);
            this.cbbSelectedTool.Name = "cbbSelectedTool";
            this.cbbSelectedTool.Size = new System.Drawing.Size(227, 27);
            this.cbbSelectedTool.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(81, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 25);
            this.label2.TabIndex = 57;
            this.label2.Text = "Tool :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(80, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(229, 25);
            this.label4.TabIndex = 57;
            this.label4.Text = "Location :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbSelectedLocation
            // 
            this.cbbSelectedLocation.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSelectedLocation.FormattingEnabled = true;
            this.cbbSelectedLocation.ItemHeight = 19;
            this.cbbSelectedLocation.Location = new System.Drawing.Point(81, 218);
            this.cbbSelectedLocation.Name = "cbbSelectedLocation";
            this.cbbSelectedLocation.Size = new System.Drawing.Size(227, 27);
            this.cbbSelectedLocation.TabIndex = 58;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(80, 261);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(229, 25);
            this.label7.TabIndex = 57;
            this.label7.Text = "Floor:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbSelectedFoor
            // 
            this.cbbSelectedFoor.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSelectedFoor.FormattingEnabled = true;
            this.cbbSelectedFoor.ItemHeight = 19;
            this.cbbSelectedFoor.Location = new System.Drawing.Point(80, 289);
            this.cbbSelectedFoor.Name = "cbbSelectedFoor";
            this.cbbSelectedFoor.Size = new System.Drawing.Size(227, 27);
            this.cbbSelectedFoor.TabIndex = 58;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(81, 332);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(229, 25);
            this.label8.TabIndex = 57;
            this.label8.Text = "Temp Name :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbSelectedName
            // 
            this.cbbSelectedName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSelectedName.FormattingEnabled = true;
            this.cbbSelectedName.ItemHeight = 19;
            this.cbbSelectedName.Location = new System.Drawing.Point(81, 360);
            this.cbbSelectedName.Name = "cbbSelectedName";
            this.cbbSelectedName.Size = new System.Drawing.Size(227, 27);
            this.cbbSelectedName.TabIndex = 58;
            this.cbbSelectedName.Click += new System.EventHandler(this.cbbSelectedName_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(399, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(229, 25);
            this.label9.TabIndex = 57;
            this.label9.Text = "Type :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbReportType
            // 
            this.cbbReportType.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbReportType.FormattingEnabled = true;
            this.cbbReportType.ItemHeight = 19;
            this.cbbReportType.Location = new System.Drawing.Point(399, 147);
            this.cbbReportType.Name = "cbbReportType";
            this.cbbReportType.Size = new System.Drawing.Size(227, 27);
            this.cbbReportType.TabIndex = 58;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(399, 198);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 25);
            this.label10.TabIndex = 57;
            this.label10.Text = "Date :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sfrmReport1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 476);
            this.Controls.Add(this.cbbSelectedName);
            this.Controls.Add(this.cbbSelectedFoor);
            this.Controls.Add(this.cbbSelectedLocation);
            this.Controls.Add(this.cbbReportType);
            this.Controls.Add(this.cbbSelectedTool);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDateSelected);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.calenReport);
            this.Controls.Add(this.panel1);
            this.Name = "sfrmReport1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Temperature Monitoring  - Report";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.MonthCalendar calenReport;
        private System.Windows.Forms.Label txtDateSelected;
        private System.Windows.Forms.ComboBox cbbSelectedTool;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbSelectedLocation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbbSelectedFoor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbbSelectedName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbbReportType;
        private System.Windows.Forms.Label label10;
    }
}