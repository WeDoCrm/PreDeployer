﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeDoCommon;
using System.Configuration.Install;
using System.Windows.Forms;

namespace CustomAction
{
    public class FlowController
    {
        InstallController ctr = new InstallController();
        WindowWrapper wrapper;
        InstallContext context;

        public FlowController(WindowWrapper wrapper, InstallContext context)
        {
            this.wrapper = wrapper;
            this.context = context;
        }

        /// <summary>
        /// installer1에서 호출하여 
        /// </summary>
        public void DoMain()
        {
            FlowInfo flowInfo = new FlowInfo();
            while (true) {
                switch (flowInfo.NextStep)
                {
                    case Step.CHECK_WINPCAP_INSTALLED:
                        Logger.info("Step.CHECK_WINPCAP_INSTALLED");
                        flowInfo = doCheckWinpcapInstalled(flowInfo);
                        break;
                    case Step.INSTALL_WINPCAP:
                        Logger.info("Step.INSTALL_WINPCAP");
                        flowInfo = doInstallWinpcap(flowInfo);
                        break;
                    case Step.CHECK_DB_INSTALLED:
                        Logger.info("Step.CHECK_DB_INSTALLED");
                        flowInfo = doCheckDBInstalled(flowInfo);
                        break;
                    case Step.INSTALL_NEW_DB:
                        Logger.info("Step.INSTALL_NEW_DB");
                        flowInfo = doInstallNewDB(flowInfo);
                        break;
                    case Step.DELETE_INSTALL_DB:
                        Logger.info("Step.DELETE_INSTALL_DB");
                        flowInfo = doDeleteInstallDB(flowInfo);
                        break;
                    case Step.CHECK_WEDO_INSTALLED:
                        Logger.info("Step.CHECK_WEDO_INSTALLED");
                        flowInfo = doCheckWeDoInstalled(flowInfo);
                        break;
                    case Step.SET_CONFIG:
                        Logger.info("Step.SET_CONFIG");
                        flowInfo = doSetConfig(flowInfo);
                        break;
                    case Step.UPDATE_CONFIG:
                        Logger.info("Step.UPDATE_CONFIG");
                        flowInfo = doUpdateConfig(flowInfo);
                        break;
                    case Step.INSTALL_WEDO:
                        Logger.info("Step.INSTALL_WEDO");
                        flowInfo = doInstallWeDo(flowInfo);
                        break;
                    case Step.GENERATE_DATA:
                        Logger.info("Step.GENERATE_DATA");
                        flowInfo = doGenerateData(flowInfo);
                        break;
                    case Step.REGISTER_FIREWALL:
                        Logger.info("Step.REGISTER_FIREWALL");
                        flowInfo = doRegisterFirewall(flowInfo);
                        break;
                    case Step.NO_MORE_STEP:
                        Logger.info("Step.NO_MORE_STEP");
                        break;
                    case Step.NONE:
                        Logger.info("Step.NONE");
                        break;
                }
                if (flowInfo != null && flowInfo.NextStep == Step.NO_MORE_STEP)
                {
                    Logger.info("DoMain Exit.");
                    break;
                }
            }
        }

        /* Winpcap설치 확인 */
        private FlowInfo doCheckWinpcapInstalled(FlowInfo flowInfo) {

            if (!ctr.CheckWinpcapInstalled())
            {
                Logger.error("WinpCap설치중 설치취소");
                flowInfo.NextStep = Step.INSTALL_WINPCAP;
                flowInfo.PrevStep = Step.CHECK_WINPCAP_INSTALLED;
            }
            else
            {
                Logger.error("WinpCap설치중 설치취소");
                flowInfo.NextStep = Step.CHECK_DB_INSTALLED;
                flowInfo.PrevStep = Step.CHECK_WINPCAP_INSTALLED;
            }

            return flowInfo;
        }
        
        /* Winpcap설치 */
        private FlowInfo doInstallWinpcap(FlowInfo flowInfo) {
            
            FrmWinpcapInstall frmWinpcapInstall = new FrmWinpcapInstall(this.context);

            if (frmWinpcapInstall.ShowDialog(wrapper) == DialogResult.OK)
            {
                if (frmWinpcapInstall.NeedInstall())
                {
                    ctr.InstallWinpcap();
                }
            }
            else
            {
                Logger.error("WinpCap설치중 설치취소");
                throw new Exception("WinpCap설치중 설치취소");
            }
            frmWinpcapInstall.Dispose();
            
            flowInfo.NextStep = Step.CHECK_DB_INSTALLED;
            flowInfo.PrevStep = Step.INSTALL_WINPCAP;

            return flowInfo;
        }        
        
        /* DB설치 확인 */
        private FlowInfo doCheckDBInstalled(FlowInfo flowInfo) {
            bool prevDbExists = ctr.prevDbExists();

            FrmDbInstall frmDbInstall = new FrmDbInstall(this.context, ctr);
            //포트 확인 및 선택
            if (frmDbInstall.ShowDialog(wrapper) == DialogResult.OK)
            {
                Logger.info(string.Format("ctr.DbPort[{0}]ctr.NeedPrevDbRemove[{1}]", ctr.DbPort, ctr.NeedPrevDbRemove));
            }
            else
            {
                Logger.error("DB설치중 설치취소");
                throw new Exception("DB설치중 설치취소");
            }
            frmDbInstall.Dispose();

            if (prevDbExists)
            {
                if (ctr.NeedPrevDbRemove)
                {
                    flowInfo.NextStep = Step.DELETE_INSTALL_DB;
                    flowInfo.PrevStep = Step.CHECK_DB_INSTALLED;
                }
                else
                {
                    flowInfo.NextStep = Step.CHECK_WEDO_INSTALLED;
                    flowInfo.PrevStep = Step.CHECK_DB_INSTALLED;
                }
            }
            else
            {
                flowInfo.NextStep = Step.INSTALL_NEW_DB;
                flowInfo.PrevStep = Step.CHECK_DB_INSTALLED;
            }

            return flowInfo;
        }

        /* DB 신규설치 */
        private FlowInfo doInstallNewDB(FlowInfo flowInfo) {
            //FrmStatus를 통해 처리진행현황 보여줌.
            string header = "MySql DB를 설치합니다.";
            string title = ConstDef.mainTitle;
            FrmStatus frmStatus = null;

            frmStatus = new FrmStatus(this.context, title, header, ctr.InstallDb);


            ctr.LogWriteHandler += frmStatus.OnStatusWrite;


            if (frmStatus.ShowDialog(wrapper) == DialogResult.OK)
            {
                Logger.info("DB설치 완료");
            }
            else
            {
                Logger.error("DB설치중 설치취소");
                throw new Exception("DB설치중 설치취소");
            }
            ctr.LogWriteHandler -= frmStatus.OnStatusWrite;
            frmStatus.Dispose();

            flowInfo.NextStep = Step.SET_CONFIG;
            flowInfo.PrevStep = Step.INSTALL_NEW_DB;

            return flowInfo;
        }

        /* DB 삭제후 설치 */
        private FlowInfo doDeleteInstallDB(FlowInfo flowInfo)
        {
            //FrmStatus를 통해 처리진행현황 보여줌.
            string header = "MySql DB를 설치합니다.";
            string title = ConstDef.mainTitle;
            FrmStatus frmStatus = null;

            frmStatus = new FrmStatus(this.context, title, header, ctr.RemoveAndInstallDb);

            ctr.LogWriteHandler += frmStatus.OnStatusWrite;


            if (frmStatus.ShowDialog(wrapper) == DialogResult.OK)
            {
                Logger.info("DB설치 완료");
            }
            else
            {
                Logger.error("DB설치중 설치취소");
                throw new Exception("DB설치중 설치취소");
            }
            ctr.LogWriteHandler -= frmStatus.OnStatusWrite;
            frmStatus.Dispose();

            flowInfo.NextStep = Step.SET_CONFIG;
            flowInfo.PrevStep = Step.DELETE_INSTALL_DB;

            return flowInfo;

        }
        
        /* WeDo설치 확인 */
        private FlowInfo doCheckWeDoInstalled(FlowInfo flowInfo) {
            flowInfo.NextStep = Step.SET_CONFIG;
            flowInfo.PrevStep = Step.CHECK_WEDO_INSTALLED;

            return flowInfo;
        }        
        
        /* 설정값 지정 */
        private FlowInfo doSetConfig(FlowInfo flowInfo) {
            //3.회사코드등록 및 config 파일 백업
            FrmConfig frmConfig = new FrmConfig(this.context, ctr);
            //회사코드 등록 및 기존 config유지 선택
            if (frmConfig.ShowDialog(wrapper) == DialogResult.OK)
            {
                ctr.UpdateCompanyCode();

                if (ctr.KeepPrevConfig)
                {
                    Logger.info("Config file정보 복원.");
                    flowInfo.NextStep = Step.UPDATE_CONFIG;
                }
                else
                {
                    Logger.info("Config file정보 복원하지 않음.");
                    flowInfo.NextStep = Step.GENERATE_DATA;
                }
                flowInfo.PrevStep = Step.SET_CONFIG;
            }
            else if (frmConfig.ShowDialog(wrapper) == DialogResult.Retry) /*재설치를 선택한 경우*/
            {
                flowInfo.NextStep = Step.CHECK_DB_INSTALLED;
                flowInfo.PrevStep = Step.SET_CONFIG;
            } 
            else
            {
                Logger.error("중 설치취소");
                throw new Exception("DB설치중 설치취소");
            }

            frmConfig.Dispose();

            return flowInfo;
        }        
        
        /* 설정값 변경 */
        private FlowInfo doUpdateConfig(FlowInfo flowInfo) {
            ctr.RecoverConfigFile();

            flowInfo.NextStep = Step.GENERATE_DATA;
            flowInfo.PrevStep = Step.UPDATE_CONFIG;

            return flowInfo;
        }        
        
        /* WeDo 설치 */
        private FlowInfo doInstallWeDo(FlowInfo flowInfo) {
            return flowInfo;
        }        
        
        /* 데이터 생성 */
        private FlowInfo doGenerateData(FlowInfo flowInfo) {
            //기존DB있는경우, 회사코드만 변경. 
            //                변경안된 경우는 스킵
            //신규DB인 경우, 신규데이터 설정
            //**FrmStatus를 통해 처리진행현황 보여줌.
            if (ctr.NeedDataGenerate())
            {
                FrmStatus frmStatusDataGen = new FrmStatus(this.context, ConstDef.mainTitle
                    , "회사코드 및 기본정보를 생성합니다.", ctr.GenerateData);

                if (frmStatusDataGen != null)
                {
                    ctr.LogWriteHandler += frmStatusDataGen.OnStatusWrite;


                    if (frmStatusDataGen.ShowDialog(wrapper) == DialogResult.OK)
                    {
                        Logger.info("데이터생성 완료");
                    }
                    else
                    {
                        Logger.error("데이터생성중 설치취소");
                        throw new Exception("데이터생성중 설치취소");
                    }
                    ctr.LogWriteHandler -= frmStatusDataGen.OnStatusWrite;
                    frmStatusDataGen.Dispose();
                }
            }
            flowInfo.NextStep = Step.REGISTER_FIREWALL;
            flowInfo.PrevStep = Step.GENERATE_DATA;

            return flowInfo;
        }        
        
         /* 방화벽 등록 */
        private FlowInfo doRegisterFirewall(FlowInfo flowInfo) {
            FrmStatus frmStatusFirewall = new FrmStatus(this.context, ConstDef.mainTitle
   , "방화벽설정을 등록합니다.", ctr.RegisterFirewall);

            if (frmStatusFirewall != null)
            {
                ctr.LogWriteHandler += frmStatusFirewall.OnStatusWrite;


                if (frmStatusFirewall.ShowDialog(wrapper) == DialogResult.OK)
                {
                    Logger.info("방화벽등록 완료");
                }
                else
                {
                    Logger.error("방화벽등록중 설치취소");
                    throw new Exception("방화벽등록중 설치취소");
                }
                ctr.LogWriteHandler -= frmStatusFirewall.OnStatusWrite;
                frmStatusFirewall.Dispose();
            }
            flowInfo.NextStep = Step.NO_MORE_STEP;
            flowInfo.PrevStep = Step.REGISTER_FIREWALL;

            return flowInfo;
        }        
              
    }

    public class FlowInfo
    {
        Step prevStep = Step.NONE;
        public Step PrevStep
        {
            get { return prevStep; }
            set { prevStep = value; }
        }
        Step nextStep = Step.CHECK_WINPCAP_INSTALLED;
        public Step NextStep
        {
            get { return nextStep; }
            set { nextStep = value; }
        }
    }

    public enum Step
    {
        CHECK_WINPCAP_INSTALLED, /* Winpcap설치 확인 */
        INSTALL_WINPCAP,         /* Winpcap설치 */
        CHECK_DB_INSTALLED,      /* DB설치 확인 */
        INSTALL_NEW_DB,          /* DB 신규설치 */
        DELETE_INSTALL_DB,       /* DB 삭제후 설치 */
        CHECK_WEDO_INSTALLED,    /* WeDo설치 확인 */
        SET_CONFIG,              /* 설정값 지정 */
        UPDATE_CONFIG,           /* 설정값 변경 */
        INSTALL_WEDO,            /* WeDo 설치 */
        GENERATE_DATA,           /* 데이터 생성 */
        REGISTER_FIREWALL,       /* 방화벽 등록 */
        NO_MORE_STEP,            /* 추가 진행없음 */
        NONE                     /* 미설정 */
    }

    public class WindowWrapper : System.Windows.Forms.IWin32Window
    {
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }

        public IntPtr Handle
        {
            get { return _hwnd; }
        }

        private IntPtr _hwnd;
    }

}
