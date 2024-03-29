﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace WeDoCommon
{

    public class ConstDef
    {

        public const string MYSQL_SERVICE_NAME = "WedoSqlTest";
        public const string WEDO_MAIN_DIR = "eclues";
        
        public const string WORK_DIR = "\\" + WEDO_MAIN_DIR +"\\";
        public const string APP_DATA_DIR = WORK_DIR + "AppData\\";
        public const string APP_DATA_CONFIG_DIR = APP_DATA_DIR + "config\\";
        public const string DB_BACKUP_DIR = APP_DATA_DIR + "dbBackup\\";
        public const string WEDO_SERVER_DIR = WORK_DIR + "WeDo Server\\";         //WDMsgServer
        public const string mainTitle = "WeDo Server 설치";
        public const string WINPCAP = "\\WinPcap_4_1_2.exe";
        public const string WINPCAP_INSTALLNAME = "WinPcap 4.1.2";
        public const string LOG_FILE = "Installer_";
        public const string LOG_DIR = WEDO_SERVER_DIR + "log";
        public const string LOG_FILE_FMT = "yyyyMMdd";
        public const string LOG_DATE_TIME_FMT = "yyyy-MM-dd HH:mm:sss";

        public const string WEDO_SERVER_NAME = "WeDo Server";
        public const string WEDO_CLIENT_NAME = "WeDo Messenger";
        public const string WEDO_MYSQL_NAME = "WeDo MySql Server";

        public const string WEDO_SERVER_EXE = "WD_Server.exe";         //WDMsgServer
        public const string WEDO_SERVER_CONFIG = "WD_Server.exe.config";         //WDMsgServer
        public const string APP_DATA_CONFIG_FILE = APP_DATA_CONFIG_DIR + WEDO_SERVER_CONFIG;
        public const string WEDO_SERVER_CMD = WEDO_SERVER_DIR + WEDO_SERVER_EXE;         //WDMsgServer
        public const string WEDO_CLIENT_EXE = WORK_DIR + "WeDo\\WDMsg_Client.exe";             //WDMsgClient
        public const string MYINI_BASE_DIR = "C:/" + WEDO_MAIN_DIR + "/db/mysql-5.5.19-win32/";//WeDoMySQL
        public const string MYINI_DATA_DIR = MYINI_BASE_DIR + "Data/";//WeDoMySQL
        public const string MYSQL_DIR = WORK_DIR + "db\\mysql-5.5.19-win32\\";//WeDoMySQL
        public const string MYSQL_INI = MYSQL_DIR + "my.ini";//WeDoMySQL
        public const string MYSQL_SERVICE_CMD = MYSQL_DIR + "bin\\mysqld.exe";//WeDoMySQL
        public const string MYSQL_INSTALL_OPT = "--install " + MYSQL_SERVICE_NAME
            + " --defaults-file=" + MYSQL_INI;
        public const string MYSQL_UNINSTALL_OPT = "--remove " + MYSQL_SERVICE_NAME;
        //\MiniCTI\mysql-5.5.19-win32\bin\mysqld --defaults-file=\MiniCTI\mysql-5.5.19-win32\my.ini WedoSql

        //db install
        public const string APP_NAMESPACE = "CustomAction";
        public const string MYSQL_ZIP_FILE = APP_NAMESPACE + ".mysql.mysql-5.5.19-win32.zip";
        public const string MYSQL_CREATE_USER_FILE = APP_NAMESPACE + ".mysql.create_user.sql";
        public const string MYSQL_CREATE_DB_FILE = APP_NAMESPACE + ".mysql.create_database.sql";
        public const string MYSQL_CREATE_TABLE_FILE = APP_NAMESPACE + ".mysql.create_table.sql";
        public const string MYSQL_INSERT_DATA_FILE = APP_NAMESPACE + ".mysql.insert_data.sql";

        //db backup
        public const string MYSQL_BACKUP_CMD = MYSQL_DIR + "bin\\mysqldump.exe";
        public const string WEDO_DB_BACKUP_OPT = " --default-character-set=euckr --user root --password=Genesys!@# wedo_db ";
        public const string DEFAULT_DB = "mysql";
        public const string WEDO_DB = "wedo_db";
        public const string WEDO_DB_URL = "127.0.0.1";
        public const string WEDO_DB_USER = "root";
        public const string WEDO_DB_PASSWORD = "Genesys!@#";
        public const string WEDO_DB_BACKUP_FILE = DB_BACKUP_DIR + "\\wedo_db_{0}.dmp";

        public const int MYSQL_PORT = 3306;

        public const string LICENSE_FILE = "*license.ini";
    }
    
}
