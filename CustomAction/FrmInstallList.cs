using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration.Install;

namespace CustomAction
{
    public partial class FrmInstallList : Form
    {
        System.Configuration.Install.InstallContext formContext;

        public FrmInstallList()
        {
            InitializeComponent();
        }

        public FrmInstallList(System.Configuration.Install.InstallContext context)
        {
            formContext = context;
            InitializeComponent();
            this.CenterToScreen();
        }

        public bool NeedWinpcapInstall()
        {
            return this.CkBoxInstallWinpcap.Checked;
        }

        public bool NeedWeDoServerInstall()
        {
            return this.CkBoxInstallWeDoServer.Checked;
        }
        
        public bool NeedMySqlInstall()
        {
            return this.CkBoxInstallMySql.Checked;
        }

        private void CkBoxInstallAll_CheckedChanged(object sender, EventArgs e)
        {
            if (CkBoxInstallAll.Checked)
            {
                CkBoxInstallWinpcap.Checked = true;
                CkBoxInstallMySql.Checked = true;
                CkBoxInstallWeDoServer.Checked = true;

                CkBoxInstallWinpcap.Enabled = false;
                CkBoxInstallMySql.Enabled = false;
                CkBoxInstallWeDoServer.Enabled = false;
            }
            else
            {
                CkBoxInstallWinpcap.Enabled = true;
                CkBoxInstallMySql.Enabled = true;
                CkBoxInstallWeDoServer.Enabled = true;
            }
        }
    }
}
