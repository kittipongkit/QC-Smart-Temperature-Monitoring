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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSetting = new System.Windows.Forms.Label();
            this.gvEvent = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEvent)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel2.Controls.Add(this.txtSetting);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 90);
            this.panel2.TabIndex = 56;
            // 
            // txtSetting
            // 
            this.txtSetting.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSetting.ForeColor = System.Drawing.Color.White;
            this.txtSetting.Location = new System.Drawing.Point(12, 27);
            this.txtSetting.Name = "txtSetting";
            this.txtSetting.Size = new System.Drawing.Size(960, 34);
            this.txtSetting.TabIndex = 47;
            this.txtSetting.Text = "EVENT";
            this.txtSetting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gvEvent
            // 
            this.gvEvent.AllowUserToAddRows = false;
            this.gvEvent.AllowUserToDeleteRows = false;
            this.gvEvent.AllowUserToResizeColumns = false;
            this.gvEvent.AllowUserToResizeRows = false;
            this.gvEvent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvEvent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEvent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvEvent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEvent.Enabled = false;
            this.gvEvent.Location = new System.Drawing.Point(12, 96);
            this.gvEvent.MultiSelect = false;
            this.gvEvent.Name = "gvEvent";
            this.gvEvent.ReadOnly = true;
            this.gvEvent.RowHeadersVisible = false;
            this.gvEvent.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvEvent.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gvEvent.RowTemplate.ReadOnly = true;
            this.gvEvent.ShowCellErrors = false;
            this.gvEvent.ShowCellToolTips = false;
            this.gvEvent.ShowEditingIcon = false;
            this.gvEvent.ShowRowErrors = false;
            this.gvEvent.Size = new System.Drawing.Size(960, 510);
            this.gvEvent.TabIndex = 57;
            // 
            // sfrmEvent1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(984, 621);
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
    }
}