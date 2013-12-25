namespace CustomAction
{
    partial class FrmConfig
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LabelLicenseFile = new System.Windows.Forms.Label();
            this.ButtonLicenseFile = new System.Windows.Forms.Button();
            this.LabelCompanyCd = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CkBoxKeepConfig = new System.Windows.Forms.CheckBox();
            this.LabelMsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonNext = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.LabelWarning = new System.Windows.Forms.Label();
            this.ButtonDBReinstall = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LabelWarning);
            this.groupBox1.Controls.Add(this.LabelLicenseFile);
            this.groupBox1.Controls.Add(this.ButtonLicenseFile);
            this.groupBox1.Controls.Add(this.LabelCompanyCd);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(32, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(540, 336);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            // 
            // LabelLicenseFile
            // 
            this.LabelLicenseFile.AutoSize = true;
            this.LabelLicenseFile.Location = new System.Drawing.Point(148, 71);
            this.LabelLicenseFile.Name = "LabelLicenseFile";
            this.LabelLicenseFile.Size = new System.Drawing.Size(0, 12);
            this.LabelLicenseFile.TabIndex = 9;
            // 
            // ButtonLicenseFile
            // 
            this.ButtonLicenseFile.Location = new System.Drawing.Point(23, 65);
            this.ButtonLicenseFile.Name = "ButtonLicenseFile";
            this.ButtonLicenseFile.Size = new System.Drawing.Size(122, 24);
            this.ButtonLicenseFile.TabIndex = 8;
            this.ButtonLicenseFile.Text = "라이센스 파일열기";
            this.ButtonLicenseFile.UseVisualStyleBackColor = true;
            this.ButtonLicenseFile.Click += new System.EventHandler(this.ButtonLicenseFile_Click);
            // 
            // LabelCompanyCd
            // 
            this.LabelCompanyCd.AutoSize = true;
            this.LabelCompanyCd.Location = new System.Drawing.Point(90, 100);
            this.LabelCompanyCd.Name = "LabelCompanyCd";
            this.LabelCompanyCd.Size = new System.Drawing.Size(99, 12);
            this.LabelCompanyCd.TabIndex = 7;
            this.LabelCompanyCd.Text = "회사명(회사코드)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CkBoxKeepConfig);
            this.panel1.Controls.Add(this.LabelMsg);
            this.panel1.Location = new System.Drawing.Point(13, 165);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 108);
            this.panel1.TabIndex = 6;
            // 
            // CkBoxKeepConfig
            // 
            this.CkBoxKeepConfig.AutoSize = true;
            this.CkBoxKeepConfig.Location = new System.Drawing.Point(16, 35);
            this.CkBoxKeepConfig.Name = "CkBoxKeepConfig";
            this.CkBoxKeepConfig.Size = new System.Drawing.Size(96, 16);
            this.CkBoxKeepConfig.TabIndex = 5;
            this.CkBoxKeepConfig.Text = "기존설정유지";
            this.CkBoxKeepConfig.UseVisualStyleBackColor = true;
            // 
            // LabelMsg
            // 
            this.LabelMsg.AutoSize = true;
            this.LabelMsg.Location = new System.Drawing.Point(8, 11);
            this.LabelMsg.Name = "LabelMsg";
            this.LabelMsg.Size = new System.Drawing.Size(251, 12);
            this.LabelMsg.TabIndex = 4;
            this.LabelMsg.Text = "이전에 설치된 서버설정을 유지하시겠습니까?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "회사코드:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(293, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "WeDo Server 실행에 필요한 라이센스를 인증합니다. \r\n배포받은 라이센스파일을 찾아 선택하세요.";
            // 
            // ButtonNext
            // 
            this.ButtonNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonNext.Location = new System.Drawing.Point(392, 374);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(87, 23);
            this.ButtonNext.TabIndex = 17;
            this.ButtonNext.Text = "다음 >";
            this.ButtonNext.UseVisualStyleBackColor = true;
            this.ButtonNext.Click += new System.EventHandler(this.ButtonNext_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(484, 374);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(87, 23);
            this.ButtonCancel.TabIndex = 16;
            this.ButtonCancel.Text = "취소";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "ini";
            this.openFileDialog1.Filter = "WeDo License 파일|*.ini";
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            // 
            // LabelWarning
            // 
            this.LabelWarning.AutoSize = true;
            this.LabelWarning.ForeColor = System.Drawing.Color.OrangeRed;
            this.LabelWarning.Location = new System.Drawing.Point(27, 124);
            this.LabelWarning.Name = "LabelWarning";
            this.LabelWarning.Size = new System.Drawing.Size(349, 24);
            this.LabelWarning.TabIndex = 10;
            this.LabelWarning.Text = "WeDo Server가 사용하는 DB는 다음과 같은 포트를 사용합니다.\r\n원하는 포트를 직접 지정하실 수 있습니다.";
            // 
            // ButtonDBReinstall
            // 
            this.ButtonDBReinstall.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.ButtonDBReinstall.Location = new System.Drawing.Point(299, 374);
            this.ButtonDBReinstall.Name = "ButtonDBReinstall";
            this.ButtonDBReinstall.Size = new System.Drawing.Size(87, 23);
            this.ButtonDBReinstall.TabIndex = 19;
            this.ButtonDBReinstall.Text = "DB재설치 >";
            this.ButtonDBReinstall.UseVisualStyleBackColor = true;
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 422);
            this.Controls.Add(this.ButtonDBReinstall);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButtonNext);
            this.Controls.Add(this.ButtonCancel);
            this.Name = "FrmConfig";
            this.Text = "WeDo Server 설치 - 라이센스인증";
            this.Shown += new System.EventHandler(this.FrmConfig_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonNext;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.CheckBox CkBoxKeepConfig;
        private System.Windows.Forms.Label LabelMsg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ButtonLicenseFile;
        private System.Windows.Forms.Label LabelCompanyCd;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label LabelLicenseFile;
        private System.Windows.Forms.Label LabelWarning;
        private System.Windows.Forms.Button ButtonDBReinstall;

    }
}