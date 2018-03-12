using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using MbcsUtils;
using MbcsCentral;
using System.Data.SqlClient;

namespace MbcsListener
{
    public static class GlobalShared
    {

        static System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
        public static string VERSION = assembly.GetName().Version.ToString();
        public const string APPNAME = "WinTrm";
        public static string ConnectionType = "AccessConnectionString";
        //public static IntPtr mainHWND;
        public const string UPDATE_URL = "http://www.bartmon.com/Files/Wintrm/Wintrm-Setup.txt";
        public static DateTime NULL_DATE = new DateTime(1900, 1, 1, 0, 0, 0); // "1/1/1900 12:00:00 AM";
        public static SqlConnection CN;
        public static string appPath;
        public static string pmPath;
        public static string dbPath;
        public const string formKeyLoc = "SOFTWARE\\MBCS\\WinTRM\\Forms\\";
        public const string smtpKeyLoc = "SOFTWARE\\MBCS\\WinTRM\\SMTP\\";
        public const string popKeyLoc = "SOFTWARE\\MBCS\\WinTRM\\POP\\";
        public const string optionsKeyLoc = "SOFTWARE\\MBCS\\WinTRM\\Options\\";
        public const string listenerService = "MbcsListener";
        public const string ISSUE_HEADER = "X-Ref";
        // 5 minutes
        public const long EMAIL_RETRIEVAL_INTERVAL = 300000;
        public const string FILE_VALUE_SEPARATOR = "|";
        public static Regex regx = new Regex("<[0-9]+\\.[0-9]+\\.[0-9]+>");
        public static MbcsUtils.LogClass Log;
        public static string logFileName = "Logs\\MbcsListener.log";
        public static bool autoEmail;
        public const string LISTENER_PIPE_NAME = "MbcsPipeListener";
        public static string pipeName = "";
        public static List<EmailMessage> inbox;
        // ctxNotes enum
        public enum ShowBy : int
        {
            ctxNotesShowEntered,
            ctxNotesShowDue,
            ctxNotesShowCreated,
            ctxNotesShowResponsible,
            ctxNotesShowCategory
        }
        public enum MailBy : int
        {
            Issue,
            Account,
            Tenant
        }
        // email type
        public enum eType : int
        {
            general,
            note
        }

        public static string[] issuesBy = {
        "Date Entered",
        "Date Due",
        "Entered By",
        "Person Responsible",
        "Category"

    };
        // Application Configuration Items
        public const string MAIN_CONFIGURATION_FILE = "MbcsListener.config";
        public const string CONFIG_JOB_SERVER = "config.job.server";
        public const string CONFIG_MAIN_DATABASE = "config.main.database";
        public const string CONFIG_PM_DATABASE = "config.pm.database";

        public static string jobServer;
        public enum smtpItem : int
        {
            UserName,
            Password,
            IncomingHost,
            OutgoingHost,
            IncomingPort,
            OutgoingPort,
            EmailAddress,
            UseInSSL,
            UseInTSL,
            UseOutSSL,
            UseOutTSL,
            AuthenticationReceive,
            Authentication,
            ListenSleepTime,
            ReadSmtp,
            ReadOutlook,
            MbcsListenerPath
        }
        public static string[] smtpConfigs = {
        "UserName",
        "Password",
        "IncomingHost",
        "OutgoingHost",
        "IncomingPort",
        "OutgoingPort",
        "EmailAddress",
        "UseInSSL",
        "UseInTSL",
        "UseOutSSL",
        "UseOutTSL",
        "AuthenticationReceive",
        "Authentication",
        "ListenSleepTime",
        "ReadSmtp",
        "ReadOutlook",
        "WinTrmPath"

    };
        // Reference information appended to email subjects for later pairing with original issue record
        // issue id, sending acct #, tenant #
        public static string subjectRef = " <{0:00000}.{1:00000}.{2:00000}>";
        // Filename string for saved email messages
        // mail directory, account id, issue id, date/time, extension
        public static string mailFilename = "{0}{1:00000}_{2:00000}_{3:yyyyMMddhhmmss}.{4}";
        // mail directory, account id, issue id, date/time, uid, extension
        public static string popFilename = "{0}{1:00000}_{2:00000}_{3:yyyyMMddhhmmss}_{4}.{5}";
        // WinPM database variables
        public static string pmConnectionType = "WinpmConnectionString";
        public static System.Data.OleDb.OleDbConnection pmCN;
        public static string pmDbSource = "winpm.mdb";

        public static bool pmFlag = false;
        // The master logged on user object *MUST BE INSTANTIATED BEFORE ANY OTHER OPERATIONS CAN BE PERFORMED*

        public static Accounts user;
        // Mail directory names
        public static string mailDir = "MAIL\\";
        public static string mailInDir = "IN\\";
        public static string mailOutDir = "OUT\\";

        public static string listPath = "Lists\\";
        // Report Configurations
        public enum report : int
        {
            BuildingIssue,
            TenantIssue
        }
        public static string[] reports = {
        "Building Issue",
        "Tenant Issue"
    };
        public static string[] reportClass = {
        "BuildingIssue",
        "TenantIssue"
    };

        public static object[] args;
        // Job Creation 
        public const string JOB_ACCOUNTID = "account_id";
        public const string JOB_RECIPIENTLIST = "recipient_list";
        public const string JOB_COMPANYID = "company_ids";
        public const string JOB_BUILDINGID = "building_ids";
        public const string JOB_TENANTTYPE = "tenant_types";
        public const string JOB_FROM = "from";
        public const string JOB_SUBJECT = "subject";
        public const string JOB_BODY = "body";
        public const string JOB_DELIVERYTIME = "delivery_time";
        public const string JOB_ID = "job_id";
        public const string JOB_SCHEDULE = "schedule";
        public const string JOB_HOWOFTEN = "howOften";

        public const string JOB_DAYTOPROCESS_2 = "dayToProcess2";

        public const string JOB_MESSAGE_END_MARKER = "***";
        //public static Quartz.Impl.RemoteScheduler jobRemoteSched;

        //public static Quartz.Impl.StdSchedulerFactory jobSchedFactory;
        // TextDynamic License Information
        public const string LICENSE_NAME = "Michael Bartmon";

        public const string LICENSE_KEY = "JHA9-C4H1-PB6T-%1WC";
        public class mailObj
        {

            private TimerCallback _tcb;
            private List<EmailMessage> _inbox;

            private System.Windows.Controls.DataGrid _grd;

            public mailObj()
            {
            }
            public mailObj(TimerCallback tcb, List<EmailMessage> inbox, System.Windows.Controls.DataGrid grd)
            {
                _tcb = tcb;
                _inbox = inbox;
                _grd = grd;
            }

            public TimerCallback tcb
            {
                get { return _tcb; }
                set { _tcb = value; }
            }
            public List<EmailMessage> inbox
            {
                get { return _inbox; }
                set { _inbox = value; }
            }
            public System.Windows.Controls.DataGrid grd
            {
                get { return _grd; }
                set { _grd = value; }
            }
        }

    }
}