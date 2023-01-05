namespace OnePointSupportTool
{
    partial class frmMaintenanceServer
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
            this.btnAppPool = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAppPool
            // 
            this.btnAppPool.Location = new System.Drawing.Point(322, 61);
            this.btnAppPool.Name = "btnAppPool";
            this.btnAppPool.Size = new System.Drawing.Size(141, 51);
            this.btnAppPool.TabIndex = 0;
            this.btnAppPool.Text = "IIS הגדרות";
            this.btnAppPool.UseVisualStyleBackColor = true;
            // 
            // frmMaintenanceServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 320);
            this.Controls.Add(this.btnAppPool);
            this.Name = "frmMaintenanceServer";
            this.Text = "Support Tool Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAppPool;
    }
}