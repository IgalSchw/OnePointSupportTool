using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnePointSupportTool
{
    public partial class frmRDP : Form
    {
        private readonly string ipDest;
        private readonly string userName;
        private readonly string password;

        public frmRDP(string ipDest, string userName, string Password)
        {
            InitializeComponent();
            this.ipDest = ipDest;
            this.userName = userName;
            password = Password;
            this.Text = ipDest;
            ConnectViaRDP();
            this.FormClosed += FrmRDP_FormClosed;
        }


        private void FrmRDP_FormClosed(object sender, FormClosedEventArgs e)
        {
            axMsRdpClient6NotSafeForScripting1 = null;
            this.Dispose();
        }

        private void ConnectViaRDP()
        {
            axMsRdpClient6NotSafeForScripting1.Server = ipDest;
            axMsRdpClient6NotSafeForScripting1.UserName = userName;
            axMsRdpClient6NotSafeForScripting1.AdvancedSettings2.ClearTextPassword = password;


            axMsRdpClient6NotSafeForScripting1.Connect();
        }
    }
}
