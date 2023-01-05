using Microsoft.Win32.TaskScheduler;
using OnePointSupportTool.Classes;
using OnePointSupportToolAppLogic.Classes;
using OnePointSupportToolBL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnePointSupportTool
{
    public partial class frmMaintenance : Form
    {

        private StringBuilder m_output;
        private string[] linesBranchNumbers;

        public frmMaintenance()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
            btnConnect.Click += BtnConnect_Click;
            btnDisconnect.Click += BtnDisconnect_Click;

            radioButtonNetwork.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            radioButtonLocal.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);

            btnShow1.Click += BtnShow1_Click;
            btnShow2.Click += BtnShow2_Click;
            btnShow3.Click += BtnShow3_Click;
            btnShow4.Click += BtnShow4_Click; //tns11.2
            btnShow5.Click += BtnShow5_Click;
            btnShow6.Click += BtnShow6_Click;

            btnLogFiles.Click += BtnLogFiles_Click;
            btnDeclaration.Click += BtnDeclaration_Click;
            btnEmergency.Click += BtnEmergency_Click;
            btnSnapshootFiles.Click += BtnSnapshootFiles_Click;

            //cmd
            btnPing.Click += BtnPingAsync_Click;
            btnTracert.Click += BtnTracertAsync_Click;

            //powershell
            btnIPConfig.Click += BtnIPConfigAsync_Click;

            //vnc
            btnConnectVNC.Click += BtnConnectVNC_Click;

            //rdp
            btnConnectRdp.Click += BtnConnectRdp_Click;

            cboBranches.SelectedValueChanged += CboBranches_SelectedValueChanged;
            cboBranchNo.SelectedValueChanged += CboBranchNo_SelectedValueChanged;


            btnPathPing.Click += BtnPathPing_Click;
            btnClear.Click += BtnClear_Click;
        }

        private async void BtnClear_Click(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                RunCommand("cmd.exe",@"\cls", true);
                //Output("Done :)");

            });

        }

        private async void BtnPathPing_Click(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                RunCommand("Pathping", txtAddress.Text, true);
                //Output("Done :)");
            });
        }

        public void ShowHideWaitLabel(bool val)
        {
            lblWait.Visible = val;
            //Thread.Sleep(1000);
            lblWait.Refresh();
        }
        
        private void BtnConnectRdp_Click(object sender, EventArgs e)
        {
            frmRDP rdp = new frmRDP(txtAddress.Text, txtUsername.Text, txtPassword.Text);
            rdp.ShowDialog();
        }

        private void BtnConnectVNC_Click(object sender, EventArgs e)
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo(FileNames.UltraVNVPath, txtAddress.Text + " -Password 1 -noauto -256colors");

            using (Process process = new Process())
            {
                process.StartInfo = procStartInfo;
                process.Start();
            }
        }        

        private async void BtnIPConfigAsync_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;

            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                RunCommand(FileNames.PSToolsPath + FileNames.PSTools.PsExec.ToString() + ".exe", "\\172.30.24.105 ipconfig /all", false);
                //Output("Done :)");
            });
        }

        private async void BtnTracertAsync_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            tabControl1.SelectedIndex = 0;

            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                RunCommand("tracert.exe", "www.google.com", true);
                //Output("Done :)");
            });
        }

        private async void BtnPingAsync_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            tabControl1.SelectedIndex = 0;

            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                RunCommand("ping.exe", txtAddress.Text, true);
                //Output("Done :)");
            });
        }

        public void Output(string text, bool isCmd)
        {
            if (isCmd)
            {
                BeginInvoke(new System.Action(delegate ()
                {
                    richTextBox1.AppendText(text + Environment.NewLine);
                    richTextBox1.ScrollToCaret();
                }));
            }
            else
            {
                BeginInvoke(new System.Action(delegate ()
                {
                    richTextBox2.AppendText(text + Environment.NewLine);
                    richTextBox2.ScrollToCaret();
                }));
            }
        }

        private void RunCommand(string filename, string commandLine, bool isCmd)
        {
            var fileName = filename;
            var arguments = commandLine;

            var info = new ProcessStartInfo();
            info.FileName = fileName;
            info.Arguments = arguments;

            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.CreateNoWindow = true;

            using (var p = new Process())
            {
                p.StartInfo = info;
                p.EnableRaisingEvents = true;

                p.OutputDataReceived += (s, o) =>
                {
                    Output(o.Data, isCmd);
                };
                p.ErrorDataReceived += (s, o) =>
                {
                    Output(o.Data, isCmd);
                };
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                p.WaitForExit();
            }
        }

        private void BtnSnapshootFiles_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            
            if (txtAddress.Text == string.Empty && lblLocalPath.Text == "LocalPath")
                return;

            if (getMyMessage("Do you really want to delete all Snapshoot files?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ShowHideWaitLabel(true);
                
                string path = string.Empty;

                if (radioButtonNetwork.Checked)
                    path = "\\\\" + txtAddress.Text + "\\C$";
                else
                    path = lblLocalPath.Text;

                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                string[] files = Directory.GetFiles(path, "ScreenShot*.jpg*");

                int count = files.Length;
                int i = 0;

                progressBar1.Maximum = count;

                foreach (FileInfo file in di.GetFiles("ScreenShot*.jpg*"))
                {
                    file.Delete();
                    progressBar1.Value = i;
                    i++;
                }

                progressBar1.Value = 0;
                ShowHideWaitLabel(false);
                MessageBox.Show("All ScreenShot files was deleted!");
            }
        }

        private void BtnEmergency_Click(object sender, EventArgs e)
        {
            string path = string.Empty;

            if (radioButtonNetwork.Checked)
            {
                path = "\\\\" + txtAddress.Text + "\\C$" + FileNames.EmergencyFolder;
                OpenFolderByPath(path);
            }
            else
            {
                path = "\\\\" + SystemInformation.ComputerName + "\\C$" + FileNames.EmergencyFolder;
                OpenFolderByPath(path);
            }
        }

        // מחיקת קבצי declar
        private void BtnDeclaration_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            
            if (txtAddress.Text == string.Empty && lblLocalPath.Text == "LocalPath")
                return;

            if (getMyMessage("Do you really want to delete all declaration files?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ShowHideWaitLabel(true);

                string path = string.Empty;

                if (radioButtonNetwork.Checked)
                    path = "\\\\" + txtAddress.Text + "\\C$" + FileNames.DeclarPath;
                else
                    path = lblLocalPath.Text + FileNames.DeclarPath;

                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                //IEnumerable<string> files = Directory.EnumerateFiles(path).Any(f => f.IndexOf("Declar", StringComparison.OrdinalIgnoreCase) > 0);

                string[] files = Directory.GetFiles(path, "Declar*.nbt*");

                int count = files.Length;
                int i = 0;

                progressBar1.Maximum = count;

                foreach (FileInfo file in di.GetFiles("Declar*.nbt*"))
                {
                    file.Delete();
                    progressBar1.Value = i;
                    i++;
                }

                progressBar1.Value = 0;
                MessageBox.Show("All Declaration files was deleted!");
                ShowHideWaitLabel(false);
            }
        }

        // מחיקת קבצי לוג
        private void BtnLogFiles_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            

            if (txtAddress.Text == string.Empty && lblLocalPath.Text == "LocalPath")
                return;

            if (getMyMessage("Do you really want to delete all logs files?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ShowHideWaitLabel(true);

                string path = string.Empty;

                if (radioButtonNetwork.Checked)
                    path = "\\\\" + txtAddress.Text + "\\C$" + FileNames.LogFolder;
                else
                    path = lblLocalPath.Text + FileNames.LogFolder;


                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                int count = di.GetFiles().Count();
                progressBar1.Maximum = count;

                int i = 0;

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                    progressBar1.Value = i;
                    i++;
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                progressBar1.Value = 0;
                ShowHideWaitLabel(false);
                MessageBox.Show("All Logs files was deleted!");              
            }
        }


        private void BtnShow6_Click(object sender, EventArgs e)
        {
            OpenFileByPath(txtConfigEMV.Text);
        }

        private void BtnShow5_Click(object sender, EventArgs e)
        {
            OpenFolderByPath(txtOracleFolderPath.Text);
        }

        // tns11
        private void BtnShow4_Click(object sender, EventArgs e)
        {
            OpenFileByPath(txtTNS11Path.Text);
        }

        private void BtnShow3_Click(object sender, EventArgs e)
        {
            OpenFileByPath(txtNibitiniPath.Text);
        }

        private void BtnShow2_Click(object sender, EventArgs e)
        {
            OpenFileByPath(txtTNSPath.Text);
        }

        private void BtnShow1_Click(object sender, EventArgs e)
        {
            OpenFileByPath(txtNibit2001iniPath.Text);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (radioButtonNetwork.Checked)
                BtnDisconnect_Click(sender, e);
        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButtonNetwork.Checked)
            {
                DisableControlsInForm(true);
                ClearFieldText();
                lblLocalPath.Text = "Local Path";
                txtAddress.Text = "";
            }
            else if (radioButtonLocal.Checked)
            {
                DisableControlsInForm(false);
                lblLocalPath.Text = "\\\\" + SystemInformation.ComputerName + "\\C$";
                txtAddress.Text = "";
                LoadPathsOfFiles();
            }
        }

        private void DisableControlsInForm(bool status)
        {
            txtAddress.Enabled = status;
            txtDrive.Enabled = status;
            txtUsername.Enabled = status;
            txtPassword.Enabled = status;
            btnConnect.Enabled = status;
            btnDisconnect.Enabled = status;
        }

        // ניקוי שדות
        private void ClearFieldText()
        {
            txtNibit2001iniPath.Text = string.Empty;
            txtTNSPath.Text = string.Empty;
            txtTNS11Path.Text = string.Empty;
            txtNibitiniPath.Text = string.Empty;
            txtOracleFolderPath.Text = string.Empty;
            txtConfigEMV.Text = string.Empty;
        }

        private void OpenFileByPath(string path)
        {
            if (File.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                MessageBox.Show("File doesn't exist\n" + path);
            }
        }

        // פתיחת נתיב לפי תיקייה
        private void OpenFolderByPath(string path)
        {
            if (Directory.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                MessageBox.Show("Folder doesn't exist\n" + path);
            }
        }

        // טעינת טופס
        private void Form1_Load(object sender, EventArgs e)
        {
            //set the address field to a share name for the current computer
            //txtAddress.Text = "\\\\" + SystemInformation.ComputerName + "\\C$";

            txtUsername.Text = FileNames.UserCredential; // from db
            txtPassword.Text = FileNames.PasswordCredential; // from db

            txtAddress.Text = FileNames.IpStartVictory;
            
            gBoxMode.Text = "Mode (Victory)";

            // set first net
            cboBranches.SelectedIndex = 0;

            LoadBranchesOfClient();
        }

        private void CboBranchNo_SelectedValueChanged(object sender, EventArgs e)
        {
            string[] lineOfContents = { string.Empty };

            switch (cboBranches.SelectedItem)
            {
                case "Victory":
                    lineOfContents = File.ReadAllLines(Environment.CurrentDirectory + @"\" + FileNames.ClientsBranch +  @"\" + FileNames.Victory);

                    foreach (var line in lineOfContents)
                    {
                        string[] linesBranchNumbers = line.Split(' ');

                        if (linesBranchNumbers[0] == cboBranchNo.Text)
                        {
                            txtAddress.Text = FileNames.IpStartVictory + linesBranchNumbers[3] + ".101";
                            break;
                        }                      
                    }
                         
                    break;
                
                case "Mshuk":
                    lineOfContents = File.ReadAllLines(Environment.CurrentDirectory + @"\" + FileNames.ClientsBranch + @"\" + FileNames.Mshuk);

                    foreach (var line in lineOfContents)
                    {
                        string[] linesBranchNumbers = line.Split(' ');

                        if (linesBranchNumbers[0] == cboBranchNo.Text)
                        {
                            txtAddress.Text = FileNames.IpStartMShuk + linesBranchNumbers[1] + ".101";
                            break;
                        }
                    }
                    break;

                case "Bareket":
                    lineOfContents = File.ReadAllLines(Environment.CurrentDirectory + @"\" + FileNames.ClientsBranch + @"\" + FileNames.Bareket);

                    foreach (var line in lineOfContents)
                    {
                        string[] linesBranchNumbers = line.Split(' ');

                        if (linesBranchNumbers[0] == cboBranchNo.Text)
                        {
                            txtAddress.Text = FileNames.IpStartBareket + linesBranchNumbers[1] + ".101";
                            break;
                        }
                    }
                    break;

                case "HCohen":
                    lineOfContents = File.ReadAllLines(Environment.CurrentDirectory + @"\" + FileNames.ClientsBranch + @"\" + FileNames.HCohen);

                    foreach (var line in lineOfContents)
                    {
                        string[] linesBranchNumbers = line.Split(' ');

                        if (linesBranchNumbers[0] == cboBranchNo.Text)
                        {
                            txtAddress.Text = FileNames.IpStartHCohen + linesBranchNumbers[1] + ".101";
                            break;
                        }
                    }
                    break;               
            }
        }

        private void CboBranches_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadBranchesOfClient();
        }

        private void LoadBranchesOfClient()
        {
            string[] lineOfContents = { string.Empty };
            cboBranchNo.Items.Clear();

            switch (cboBranches.SelectedItem)
            {
                case "Victory":
                    txtAddress.Text = FileNames.IpStartVictory;
                    

                    lineOfContents = File.ReadAllLines(FileNames.ClientsBranch + @"\" + FileNames.Victory);

                    foreach (var line in lineOfContents)
                    {
                        string[] linesBranchNumbers = line.Split(' ');

                        cboBranchNo.Items.Add(linesBranchNumbers[0]); //  0 - num of branch, 3 - extension of ip               
                    }
                   
                    break;

                case "Mshuk":
                    txtAddress.Text = FileNames.IpStartMShuk;

                    lineOfContents = File.ReadAllLines(FileNames.ClientsBranch + @"\" + FileNames.Mshuk);

                    foreach (var line in lineOfContents)
                    {
                        string[] linesBranchNumbers = line.Split(' ');

                        cboBranchNo.Items.Add(linesBranchNumbers[0]); //  0 - num of branch, 3 - extension of ip               
                    }

                    break;

                case "Bareket":
                    txtAddress.Text = FileNames.IpStartBareket;

                    lineOfContents = File.ReadAllLines(FileNames.ClientsBranch + @"\" + FileNames.Bareket);

                    foreach (var line in lineOfContents)
                    {
                        string[] linesBranchNumbers = line.Split(' ');

                        cboBranchNo.Items.Add(linesBranchNumbers[0]); //  0 - num of branch, 3 - extension of ip               
                    }

                    break;
                
                case "HCohen":
                    txtAddress.Text = FileNames.IpStartBareket;

                    lineOfContents = File.ReadAllLines(FileNames.ClientsBranch + @"\" + FileNames.HCohen);

                    foreach (var line in lineOfContents)
                    {
                        string[] linesBranchNumbers = line.Split(' ');

                        cboBranchNo.Items.Add(linesBranchNumbers[0]); //  0 - num of branch, 3 - extension of ip               
                    }

                    break;
            }

            cboBranchNo.SelectedIndex = 0;
        }


        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            NetworkDrive oNetDrive = new NetworkDrive();

            try
            {
                //unmap the drive
                oNetDrive.Force = true; //conForce.Checked;
                oNetDrive.LocalDrive = "U:"; //txtDrive.Text;
                oNetDrive.UnMapDrive();

                MessageBox.Show("Drive Unmap Successful!");

                ClearFieldText();

                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Disconnected";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot unmap drive! \n" + ex.Message);
            }

            oNetDrive = null;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            NetworkDrive oNetDrive = new NetworkDrive();

            try
            {
                ShowHideWaitLabel(true);

                //set propertys
                oNetDrive.Force = true;
                oNetDrive.Persistent = true;
                oNetDrive.LocalDrive = txtDrive.Text;
                oNetDrive.PromptForCredentials = false;
                oNetDrive.ShareName = @"\\" + txtAddress.Text + FileNames.LocalDrive;
                oNetDrive.SaveCredentials = true;

                oNetDrive.MapDrive(txtUsername.Text, txtPassword.Text);
                MessageBox.Show("Drive Map Successful!");

                LoadPathsOfFiles();

                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "Connected";         
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot map drive! \n" + ex.Message);

                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Disconnected";
            }

            oNetDrive = null;
            ShowHideWaitLabel(false);
        }

        private void LoadPathsOfFiles()
        {
            if (radioButtonNetwork.Checked)
            {
                txtNibit2001iniPath.Text = "\\\\" + txtAddress.Text + "\\C$" + FileNames.Nibit2001INI;
                txtTNSPath.Text = "\\\\" + txtAddress.Text + "\\C$" + FileNames.TNSName;
                txtTNS11Path.Text = "\\\\" + txtAddress.Text + "\\C$" + FileNames.TNSName11;
                txtNibitiniPath.Text = "\\\\" + txtAddress.Text + "\\C$" + FileNames.NibitINI;
                txtOracleFolderPath.Text = "\\\\" + txtAddress.Text + "\\C$" + FileNames.OracleFolder;
                txtConfigEMV.Text = "\\\\" + txtAddress.Text + "\\C$" + FileNames.ConfigEMV;
            }
            else
            {
                txtNibit2001iniPath.Text = lblLocalPath.Text + FileNames.Nibit2001INI;
                txtTNSPath.Text = lblLocalPath.Text + FileNames.TNSName;
                txtTNS11Path.Text = lblLocalPath.Text + FileNames.TNSName11;
                txtNibitiniPath.Text = lblLocalPath.Text + FileNames.NibitINI;
                txtOracleFolderPath.Text = lblLocalPath.Text + FileNames.OracleFolder;
                txtConfigEMV.Text = lblLocalPath.Text + FileNames.ConfigEMV;
            }
        }

        // ולידטציה כתובת IP
        private void ValidateIpAddress(string strIP)
        {
            IPAddress ip;

            string ipaddress = strIP;

            bool ValidateIP = IPAddress.TryParse(ipaddress, out ip);
            if (ValidateIP)
                MessageBox.Show("This is a valide ip address");
            else
                MessageBox.Show("This is not a valide ip address");
        }

        private DialogResult getMyMessage(string text, string title, MessageBoxButtons Msgbtn, MessageBoxIcon MsgIcn)
        {
            DialogResult dlg = MessageBox.Show(text, title, Msgbtn, MsgIcn, MessageBoxDefaultButton.Button1);
            return dlg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    using (TaskService tasksrvc = new TaskService(@"\\" + txtAddress.Text, txtUsername.Text, txtDomain.Text, txtPassword.Text))
            //    {
            //        Microsoft.Win32.TaskScheduler.Task task = tasksrvc.FindTask("klita");

            //        task.Enabled = false;

            //        MessageBox.Show("The task was disabled successful!");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Couldn't connect to this machine!","",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //}


            string some = "\"klita\"";
            try
            {
                //Process.Start("cmd.exe", "/cL:\\2isayas\\isayas\\psexec\\psexec.exe \\\\" + txtAddress + " -nobanner -i cmd.exe /k schtasks /delete /TN " + some + " /F").WaitForExit(7000);
                //Process.Start("cmd.exe", "/cL:\\2isayas\\isayas\\psexec\\pskill.exe \\\\" + txtAddress + " cmd.exe").WaitForExit(5000);

                Process.Start("cmd.exe", "/cL:\\" + FileNames.PSToolsPath + FileNames.PSTools.PsExec + ".exe"  + @"\" + txtAddress + " -nobanner -i cmd.exe /k schtasks /delete /TN " + some + " /F").WaitForExit(7000);
                Process.Start("cmd.exe", "/cL:\\" + FileNames.PSToolsPath + FileNames.PSTools.pskill + ".exe"  + @"\" + txtAddress + " cmd.exe").WaitForExit(5000);

                MessageBox.Show("The task was deleted succesfully!");
            }
            catch
            {
                MessageBox.Show("Delete manually", "ERROR!");
            }
        }
    }
}
