namespace project1
{
    partial class XuatHD
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
            this.reportViewerHD = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerHD
            // 
            this.reportViewerHD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerHD.Location = new System.Drawing.Point(0, 0);
            this.reportViewerHD.Name = "reportViewerHD";
            this.reportViewerHD.ServerReport.BearerToken = null;
            this.reportViewerHD.Size = new System.Drawing.Size(717, 923);
            this.reportViewerHD.TabIndex = 0;
            //this.reportViewerHD.Load += new System.EventHandler(this.reportViewerHD_Load);
            // 
            // XuatHD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 923);
            this.Controls.Add(this.reportViewerHD);
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "XuatHD";
            this.Text = "XuatHD";
            this.Load += new System.EventHandler(this.XuatHD_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerHD;
    }
}