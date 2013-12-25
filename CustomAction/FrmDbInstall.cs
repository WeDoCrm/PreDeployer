using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration.Install;
using WeDoCommon;

namespace CustomAction
{
    public partial class FrmDbInstall : Form
    {
        System.Configuration.Install.InstallContext formContext = null;
        InstallController ctrl = null;

        public FrmDbInstall()
        {
            InitializeComponent();
        }

        public FrmDbInstall(System.Configuration.Install.InstallContext context, InstallController ctrl)
        {
            Logger.info("FrmDbInstall");

            formContext = context;
            this.ctrl = ctrl;
            InitializeComponent();
            this.CenterToScreen();
        }

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

        private void Initialize()
        {
            this.ButtonNext.Enabled = false;
            LabelWarning.Text = "";
            ButtonCheckPort.Text = "사용여부확인";
        }

        public void SetPrevDbInfo(bool exists)
        {
            Logger.info("SetPrevDbInfo:"+exists);
            if (!exists)
            {
                panel1.Visible = false;
                panel2.Location = panel1.Location;
            }
            else
            {
                //기존 DB가 존재한다면 원래쓰던 포트를 그대로 쓴다.
                TextBoxMySqlPort.Text = Convert.ToString(ctrl.prevDbPort());
                LabelDbDir.Text = string.Format("설치된 DB경로:{0}", ctrl.PrevMySqlDir);
                SetPortAlreadyUsed(true);
            }
        }

        //이미 사용중인 디비면 포트를 그대로 쓰고, 신규이면 포트확인을 한다.
        //이미 사용중 디비포트면 
        public void SetPortAlreadyUsed(bool used)
        {
            Logger.info("SetPortAlreadyUsed:"+used);
            if (used && !ctrl.prevDbExists())
            {
                int mySqlPort = Convert.ToInt16(TextBoxMySqlPort.Text);
                LabelWarning.Text = String.Format("{0} 포트는 이미 다른 프로그램이 사용중입니다." + Environment.NewLine + "{1}번 포트를 확인해보세요."
                    , mySqlPort, (mySqlPort+1));
                TextBoxMySqlPort.Text = Convert.ToString(mySqlPort + 1);
                ButtonCheckPort.Text = "사용여부확인";
                ButtonNext.Enabled = false;

                Logger.info(LabelWarning.Text);
            }
            else
            {
                LabelWarning.Text = String.Format("{0} 포트는 사용 가능합니다.", TextBoxMySqlPort.Text);
                ButtonCheckPort.Text = "사용가능";
                ButtonNext.Enabled = true;
                Logger.info(LabelWarning.Text);
            }
        }

        private void TextBoxMySqlPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void FrmDbInstall_Shown(object sender, EventArgs e)
        {
            Logger.info("FrmDbInstall_Shown");
            Initialize();
            if (ctrl != null)
            {
                SetPrevDbInfo(ctrl.prevDbExists());
            }
        }

        private void ButtonCheckPort_Click(object sender, EventArgs e)
        {
            if (!TextBoxMySqlPort.Text.Equals(""))
            {
                int mySqlPort = Convert.ToInt16(TextBoxMySqlPort.Text);
                SetPortAlreadyUsed(ctrl.PortAlreadyUsed(mySqlPort));
            }
        }

        private void TextBoxMySqlPort_TextChanged(object sender, EventArgs e)
        {
            Initialize();
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            ctrl.DbPort = Convert.ToInt16(TextBoxMySqlPort.Text);
            ctrl.NeedPrevDbRemove = CkBoxNeedDbDelete.Checked;
        }

    }
}
