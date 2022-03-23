namespace Smart_Temperature_Monitoring
{
    partial class sfrmEvent1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sfrmEvent1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbbSelectedTool = new System.Windows.Forms.ComboBox();
            this.txtSetting = new System.Windows.Forms.Label();
            this.dtDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gvEvent = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEvent)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel2.Controls.Add(this.btnExport);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Controls.Add(this.cbbSelectedTool);
            this.panel2.Controls.Add(this.txtSetting);
            this.panel2.Controls.Add(this.dtDateTo);
            this.panel2.Controls.Add(this.dtDateFrom);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1326, 100);
            this.panel2.TabIndex = 56;
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
            this.btnExport.Location = new System.Drawing.Point(1115, 56);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(122, 34);
            this.btnExport.TabIndex = 65;
            this.btnExport.Text = "EXPORT";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(987, 56);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(122, 34);
            this.btnOk.TabIndex = 64;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cbbSelectedTool
            // 
            this.cbbSelectedTool.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSelectedTool.FormattingEnabled = true;
            this.cbbSelectedTool.ItemHeight = 26;
            this.cbbSelectedTool.Location = new System.Drawing.Point(164, 56);
            this.cbbSelectedTool.Name = "cbbSelectedTool";
            this.cbbSelectedTool.Size = new System.Drawing.Size(174, 34);
            this.cbbSelectedTool.TabIndex = 63;
            // 
            // txtSetting
            // 
            this.txtSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSetting.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSetting.ForeColor = System.Drawing.Color.White;
            this.txtSetting.Location = new System.Drawing.Point(0, 0);
            this.txtSetting.Name = "txtSetting";
            this.txtSetting.Size = new System.Drawing.Size(1326, 54);
            this.txtSetting.TabIndex = 47;
            this.txtSetting.Text = "EVENT";
            this.txtSetting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtDateTo
            // 
            this.dtDateTo.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(153)))));
            this.dtDateTo.CustomFormat = "yyyy-MM-dd";
            this.dtDateTo.Font = new System.Drawing.Font("Calibri", 16F);
            this.dtDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateTo.Location = new System.Drawing.Point(773, 56);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.Size = new System.Drawing.Size(174, 34);
            this.dtDateTo.TabIndex = 62;
            // 
            // dtDateFrom
            // 
            this.dtDateFrom.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(153)))));
            this.dtDateFrom.CustomFormat = "yyyy-MM-dd";
            this.dtDateFrom.Font = new System.Drawing.Font("Calibri", 16F);
            this.dtDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateFrom.Location = new System.Drawing.Point(486, 56);
            this.dtDateFrom.Name = "dtDateFrom";
            this.dtDateFrom.Size = new System.Drawing.Size(174, 34);
            this.dtDateFrom.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(97, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 34);
            this.label2.TabIndex = 58;
            this.label2.Text = "TOOL :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(690, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 34);
            this.label4.TabIndex = 60;
            this.label4.Text = "DATE TO :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(375, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 34);
            this.label1.TabIndex = 59;
            this.label1.Text = "DATE FROM :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gvEvent
            // 
            this.gvEvent.AllowUserToAddRows = false;
            this.gvEvent.AllowUserToDeleteRows = false;
            this.gvEvent.AllowUserToResizeColumns = false;
            this.gvEvent.AllowUserToResizeRows = false;
            this.gvEvent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEvent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvEvent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEvent.Location = new System.Drawing.Point(12, 106);
            this.gvEvent.MultiSelect = false;
            this.gvEvent.Name = "gvEvent";
            this.gvEvent.ReadOnly = true;
            this.gvEvent.RowHeadersVisible = false;
            this.gvEvent.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvEvent.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gvEvent.RowTemplate.ReadOnly = true;
            this.gvEvent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvEvent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvEvent.ShowCellErrors = false;
            this.gvEvent.ShowCellToolTips = false;
            this.gvEvent.ShowEditingIcon = false;
            this.gvEvent.ShowRowErrors = false;
            this.gvEvent.Size = new System.Drawing.Size(1302, 593);
            this.gvEvent.TabIndex = 57;
            // 
            // sfrmEvent1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(1326, 711);
            this.Controls.Add(this.gvEvent);
            this.Controls.Add(this.panel2);
            this.Name = "sfrmEvent1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Temperature Monitoring  - Event";
            this.Load += new System.EventHandler(this.sfrmEvent1_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvEvent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label txtSetting;
        private System.Windows.Forms.DataGridView gvEvent;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cbbSelectedTool;
        private System.Windows.Forms.DateTimePicker dtDateTo;
        private System.Windows.Forms.DateTimePicker dtDateFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExport;
    }
}