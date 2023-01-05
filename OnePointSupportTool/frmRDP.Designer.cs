
namespace OnePointSupportTool
{
    partial class frmRDP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRDP));
            this.axMsRdpClient6NotSafeForScripting1 = new AxMSTSCLib.AxMsRdpClient6NotSafeForScripting();
            ((System.ComponentModel.ISupportInitialize)(this.axMsRdpClient6NotSafeForScripting1)).BeginInit();
            this.SuspendLayout();
            // 
            // axMsRdpClient6NotSafeForScripting1
            // 
            this.axMsRdpClient6NotSafeForScripting1.Enabled = true;
            this.axMsRdpClient6NotSafeForScripting1.Location = new System.Drawing.Point(12, 12);
            this.axMsRdpClient6NotSafeForScripting1.Name = "axMsRdpClient6NotSafeForScripting1";
            this.axMsRdpClient6NotSafeForScripting1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMsRdpClient6NotSafeForScripting1.OcxState")));
            this.axMsRdpClient6NotSafeForScripting1.Size = new System.Drawing.Size(1190, 689);
            this.axMsRdpClient6NotSafeForScripting1.TabIndex = 0;
            // 
            // frmRDP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 708);
            this.Controls.Add(this.axMsRdpClient6NotSafeForScripting1);
            this.Name = "frmRDP";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "חיבור מרחוק ";
            ((System.ComponentModel.ISupportInitialize)(this.axMsRdpClient6NotSafeForScripting1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMSTSCLib.AxMsRdpClient6NotSafeForScripting axMsRdpClient6NotSafeForScripting1;
    }
}