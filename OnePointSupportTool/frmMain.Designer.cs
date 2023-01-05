
namespace OnePointSupportTool
{
    partial class frmMain
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
            this.btnMaintenceKupa = new System.Windows.Forms.Button();
            this.btnGuide = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnContacts = new System.Windows.Forms.Button();
            this.btnDifferentSupport = new System.Windows.Forms.Button();
            this.btnMaintenceServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMaintenceKupa
            // 
            this.btnMaintenceKupa.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnMaintenceKupa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnMaintenceKupa.Location = new System.Drawing.Point(297, 135);
            this.btnMaintenceKupa.Name = "btnMaintenceKupa";
            this.btnMaintenceKupa.Size = new System.Drawing.Size(220, 39);
            this.btnMaintenceKupa.TabIndex = 0;
            this.btnMaintenceKupa.Text = "תחזוקת עמדה";
            this.btnMaintenceKupa.UseVisualStyleBackColor = false;
            // 
            // btnGuide
            // 
            this.btnGuide.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGuide.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnGuide.Location = new System.Drawing.Point(297, 222);
            this.btnGuide.Name = "btnGuide";
            this.btnGuide.Size = new System.Drawing.Size(220, 36);
            this.btnGuide.TabIndex = 1;
            this.btnGuide.Text = "מדריכים (שאלות ותשובות)";
            this.btnGuide.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button1.Location = new System.Drawing.Point(41, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(214, 39);
            this.button1.TabIndex = 2;
            this.button1.Text = "WisePay";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // btnContacts
            // 
            this.btnContacts.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnContacts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnContacts.Location = new System.Drawing.Point(35, 135);
            this.btnContacts.Name = "btnContacts";
            this.btnContacts.Size = new System.Drawing.Size(220, 39);
            this.btnContacts.TabIndex = 5;
            this.btnContacts.Text = "אנשי קשר ושרתים";
            this.btnContacts.UseVisualStyleBackColor = false;
            // 
            // btnDifferentSupport
            // 
            this.btnDifferentSupport.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDifferentSupport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnDifferentSupport.Location = new System.Drawing.Point(35, 176);
            this.btnDifferentSupport.Name = "btnDifferentSupport";
            this.btnDifferentSupport.Size = new System.Drawing.Size(220, 39);
            this.btnDifferentSupport.TabIndex = 6;
            this.btnDifferentSupport.Text = "תמיכה נוספת";
            this.btnDifferentSupport.UseVisualStyleBackColor = false;
            // 
            // btnMaintenceServer
            // 
            this.btnMaintenceServer.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnMaintenceServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnMaintenceServer.Location = new System.Drawing.Point(297, 180);
            this.btnMaintenceServer.Name = "btnMaintenceServer";
            this.btnMaintenceServer.Size = new System.Drawing.Size(220, 39);
            this.btnMaintenceServer.TabIndex = 7;
            this.btnMaintenceServer.Text = "תחזוקת שרת";
            this.btnMaintenceServer.UseVisualStyleBackColor = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OnePointSupportTool.Properties.Resources.One_point_background_image;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(743, 326);
            this.Controls.Add(this.btnMaintenceServer);
            this.Controls.Add(this.btnDifferentSupport);
            this.Controls.Add(this.btnContacts);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnGuide);
            this.Controls.Add(this.btnMaintenceKupa);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "One-Point Support Tool";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMaintenceKupa;
        private System.Windows.Forms.Button btnGuide;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnContacts;
        private System.Windows.Forms.Button btnDifferentSupport;
        private System.Windows.Forms.Button btnMaintenceServer;
    }
}