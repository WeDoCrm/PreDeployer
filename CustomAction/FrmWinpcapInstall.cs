﻿using System;
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
    public partial class FrmWinpcapInstall : Form
    {
        System.Configuration.Install.InstallContext formContext;

        public FrmWinpcapInstall()
        {
            InitializeComponent();
        }

        public FrmWinpcapInstall(System.Configuration.Install.InstallContext context)
        {
            formContext = context;
            InitializeComponent();
            this.CenterToScreen();
        }

        //폼 우측상단 버튼 안보임
        private const int WS_SYSMENU = 0x80000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~WS_SYSMENU;
                return cp;
            }
        }

        public bool NeedInstall()
        {
            return this.CkBoxInstallWinpcap.Checked;
        }
    }
}
