using ArdaniGuide.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaitingDelete;

namespace OnePointSupportTool
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            btnMaintenceKupa.Click += BtnMaintenceKupa_Click;
            btnGuide.Click += BtnGuide_Click;
            btnContacts.Click += BtnContacts_Click;
            btnDifferentSupport.Click += BtnDifferentSupport_Click;
            btnMaintenceServer.Click += BtnMaintenceServer_Click;
        }

        private void BtnMaintenceServer_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnDifferentSupport_Click(object sender, EventArgs e)
        {
            frmDifferentSupport frmDifferentSupport = new frmDifferentSupport();
            frmDifferentSupport.ShowDialog();
        }

        private void BtnContacts_Click(object sender, EventArgs e)
        {
            frmContactsAndServers fContactsAndServers = new frmContactsAndServers();
            fContactsAndServers.ShowDialog();
        }

        private void BtnGuide_Click(object sender, EventArgs e)
        {
            FrmSearch fGuide = new FrmSearch();
            fGuide.ShowDialog();
        }

        private void BtnMaintenceKupa_Click(object sender, EventArgs e)
        {
            frmMaintenance fMaintenance = new frmMaintenance();
            fMaintenance.ShowDialog();
        }
    }
}
