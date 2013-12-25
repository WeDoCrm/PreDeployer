using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration.Install;
using System.IO;
using WeDoCommon;
using System.Diagnostics;

namespace CustomAction
{
    public partial class FrmConfig : Form
    {
        System.Configuration.Install.InstallContext formContext = null;
        InstallController ctrl = null;
        string companyCd = "";
        string companyName = "";

        public delegate void DelAppendStatusMsg(string msg);
        DelAppendStatusMsg delAppendStatusMsg = null;

        public FrmConfig()
        {
            InitializeComponent();
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

        public FrmConfig(System.Configuration.Install.InstallContext context, InstallController ctrl)
        {
            formContext = context;
            this.ctrl = ctrl;
            InitializeComponent();
            this.CenterToScreen();
        }

        private void FrmConfig_Shown(object sender, EventArgs e)
        {
            //wedo가 설치된게 있는지 확인한다.
            //있는경우 
            //1.회사코드를 보여주고
            //2.하단 질문을 보여준다.
            SetPrevWeDoInfo(ctrl.prevWeDoExists());
            Initialize();
        }

        public void SetPrevWeDoInfo(bool exists)
        {
            if (exists)
            {
                panel1.Visible = true;
                CkBoxKeepConfig.Checked = true;
            }
            else
            {
                panel1.Visible = false;
            }
        }

        public void Initialize()
        {
            ButtonNext.Enabled = false;
            ButtonDBReinstall.Enabled = false;
            LabelWarning.Text = "";
            LabelCompanyCd.Text = "";
        }

        public bool KeepConfig()
        {
            return CkBoxKeepConfig.Checked;
        }

        /// <summary>
        /// 회사코드가 변경된 경우는 db재설치를 한경우
        /// 신규이거나 변경사항 없으면 패스
        /// 등록한 라이센스파일을 C:\eclues\AppData\config로 복사. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNext_Click(object sender, EventArgs e)
        {
            ctrl.KeepPrevConfig = CkBoxKeepConfig.Checked;
            if (ctrl.CompanyCd != "" && !ctrl.CompanyCd.Equals(companyCd))
            {
                if (MessageBox.Show(string.Format("이전에 사용하던 회사코드({0})와 다른 회사코드({1})가 적용되었습니다. "
                                                + "\n회사코드를 변경하시겠습니까?"
                                                 , ctrl.CompanyCd
                                                 , companyCd)
                                    , "회사코드 변경"
                                    , MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.None;
                    ButtonLicenseFile.Focus();
                    return;
                }
            }
            ctrl.CompanyCd = companyCd;
            ctrl.CompanyName = companyName;
            FileInfo info = new FileInfo(LabelLicenseFile.Text);
            LicenseHandler handler = new LicenseHandler(info);
            handler.BackupLicenseFile(ConstDef.APP_DATA_CONFIG_DIR);
            
        }

        private void ButtonLicenseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            if (!Directory.Exists(ConstDef.APP_DATA_CONFIG_DIR))
            {
                fileDialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                fileDialog.InitialDirectory = ConstDef.APP_DATA_CONFIG_DIR;
            }
            fileDialog.DefaultExt = "ini";
            fileDialog.RestoreDirectory = true;
            fileDialog.Filter = "WeDo License 파일|*.ini";
            fileDialog.SupportMultiDottedExtensions = true;

            if (CommonUtil.STAShowDialog(fileDialog) == DialogResult.OK)
            {
                Initialize();
                LabelLicenseFile.Text = fileDialog.FileName;
                FileInfo info = new FileInfo(LabelLicenseFile.Text);
                LicenseHandler handler = new LicenseHandler(info);
                handler.LogWriteHandler += OnStatusWrite;

                handler.ReadLicense();

                companyCd = handler.CompanyCode;
                companyName = handler.CompanyName;
                LabelCompanyCd.Text = string.Format("{0}({1})", companyName, companyCd);

                if (ctrl.NeedDbReinstall(handler.CompanyCode))
                {
                    ButtonDBReinstall.Enabled = true;
                    ButtonNext.Enabled = false;
                    LabelWarning.Text = string.Format("라이센스파일의 회사코드[{0}]가 기존 회사코드[{1}]와 다릅니다.\n"
                        +" 새로운 회사코드를 쓰려면 DB를 재설치해야 합니다."
                        , handler.CompanyCode, ctrl.CompanyCd);

                } else {
                    ButtonDBReinstall.Enabled = false;


                    if (handler.ValidLicense())
                    {
                        ButtonNext.Enabled = true;
                        LabelWarning.Text = "라이센스파일이 유효합니다.";
                    }
                    else
                    {
                        ButtonNext.Enabled = false;

                        LabelWarning.Text = "라이센스파일 검증에 실패했습니다. 파일유효성을 확인하세요.\n";

                        int resultCode = Convert.ToInt16(handler.ResultMessage.Split('&')[0]);
                        if (resultCode == (int)LicenseResult.ERR_EXPIRED)
                        {
                            LabelWarning.Text += "License 만료일자 지남.";
                        }
                        else if (resultCode == (int)LicenseResult.ERR_INVALID_FILE)
                        {
                            LabelWarning.Text += "잘못된 등록일자, 회사코드/명 또는 라이센스키.";
                        }
                        else if (resultCode == (int)LicenseResult.ERR_MAC_ADDR)
                        {
                            LabelWarning.Text += string.Format("License file 잘못된 mac address값.실제 mac address[{0}]", handler.MacAddress);
                        }
                    }
                }

            }
            fileDialog.Dispose();
        }

        public void OnStatusWrite(object sender, StringEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                Logger.info(e.EventString);
            });
        }
    }
}
