using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Outlook;
using MbcsUtils;
using Email.Net.Common;
using Email.Net.Common.Configurations;
using Email.Net.Pop3;
using Email.Net.Common.Collections;
using Email.Net.Smtp;
using Redemption;
namespace MbcsListener
{

    public partial class MbcsListener
	{
        private const string LOCAL_USER_DIRECTORY = "UserConfig";
        private const string MBCSCENTRAL_CONFIG = "MbcsCentral.config";
        static string SqlBaseConnectionString = "Data Source=<source>;Initial Catalog=<catalog>;User Id=<user>;Password=<password>;Encrypt=True;Trusted_Connection=False;TrustServerCertificate=True;Integrated Security=False;Connection Timeout=30;";
        private const string DB_USER_NAME = "mbartmon";
        private const string DB_PASSWORD = "Vietnam65";


        // default wait time = 10 minutes in milliseconds (10 minutes * 60 seconds * 1000 milliseconds)
        public long RETRIEVE_MAIL_INTERVAL = 600000;
			// 30 seconds for debugging 1000
		public long DELAY = 30000;
		private static string appPath;
		private string pipeName = "";
		public bool isEnabled = true;
		public bool loadAuto = true;
		public bool readSmtp = true;
		public bool readOutlook = false;
		private string Username;
		private string Password;
		private string Host;
		private int Port;
		private string Email;
		private bool SSL;
		private bool TSL;
		private long authenticationType;
		private string companyName;
		private string emailSignature;
		public bool runFlag = true;
		public Thread runThread;
		public string MbcsListenerPath;

		internal System.Threading.Timer myTimer;
		protected override void OnStart(string[] args)
		{
			// Add code here to start your service. This method should set things
			// in motion so your service can do its work.

			Thread.Sleep(30000);
            appPath = System.AppDomain.CurrentDomain.BaseDirectory;
            if (!appPath.EndsWith("\\"))
            {
                appPath += "\\";
            }

            GlobalShared.user = new MbcsCentral.Accounts();
			//GlobalShared.user.setName("Service");
			GlobalShared.logFileName = "MbcsListener.log";
            GlobalShared.Log = MbcsUtils.LogClass.instance;
            GlobalShared.Log.logFileName = appPath + GlobalShared.logFileName;

            GlobalShared.Log.Log((int)LogClass.logType.Config, "***********************************", false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "Starting up. appPath=" + appPath, false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "***********************************", false);

			// Setup connectionstrings and paths
			GlobalShared.Log.Log((int)LogClass.logType.Config, "Setting up connectionstrings and paths", false);
            MbcsUtils.Parameters configs = new MbcsUtils.Parameters(appPath + "MbcsListener.config");
            try
            {
                configs.getConfigs();
            }
            catch (System.Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.Config, "Error getting configs. There may be a duplicate name. Cannot continue. " + ex.Message, false);
                return;
            }
            object obj = null;
			configs.getItem(GlobalShared.smtpConfigs[(int)GlobalShared.smtpItem.MbcsListenerPath], ref obj);
            MbcsListenerPath = (string)obj;
            string connectionString = GetDatabaseParms();
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   MbcsListenerPath=" + MbcsListenerPath, false);
			string oldString = "dbSource";
			string newString = "WinTRM.accdb";
			//pmPath = GetSetting(AppName:="Winpm", Section:="Options", Key:="DefaultDataBase", Default:="c:\\pmsys\\winpm.mdb")
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   pmPath=" + GlobalShared.pmPath, false);
			connectionString = connectionString.Replace(oldString, MbcsListenerPath + newString);
			GlobalShared.CN = new SqlConnection(connectionString);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   CN=" + connectionString, false);
			// Setup MAIL directories
			GlobalShared.mailDir = MbcsListenerPath + GlobalShared.mailDir;
			GlobalShared.mailInDir = GlobalShared.mailDir + GlobalShared.mailInDir;
			GlobalShared.mailOutDir = GlobalShared.mailDir + GlobalShared.mailOutDir;
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   mailDir=" + GlobalShared.mailDir, false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   mailInDir=" + GlobalShared.mailInDir, false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   mailOutdir=" + GlobalShared.mailOutDir, false);

            GlobalShared.Log.Log((int)LogClass.logType.Config, "Retrieving SMTP configuration.", false);
			string fldr = string.Format("{0}\\{1}\\{2}\\{3}", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "MBCS", "Wintrm", "MbcsListener.config");

			//If Not IsNothing(configs) Then
			//DictUtils.getItem(configs, smtpConfigs(smtpItem.ListenSleepTime), RETRIEVE_MAIL_INTERVAL)
			//DictUtils.getItem(configs, smtpConfigs(smtpItem.ReadOutlook), readOutlook)
			//DictUtils.getItem(configs, smtpConfigs(smtpItem.ReadSmtp), readSmtp)
			//DictUtils.getItem(configs, smtpConfigs(smtpItem.AuthenticationReceive), authenticationType)
			//DictUtils.getItem(configs, smtpConfigs(smtpItem.UserName), Username)
			//DictUtils.getItem(configs, smtpConfigs(smtpItem.Password), Password)
			//DictUtils.getItem(configs, smtpConfigs(smtpItem.IncomingHost), Host)
			//DictUtils.getItem(configs, smtpConfigs(smtpItem.IncomingPort), Port)
			//DictUtils.getItem(configs, smtpConfigs(smtpItem.EmailAddress), Email)
			//DictUtils.getItem(configs, smtpConfigs(smtpItem.UseInSSL), SSL)
			//DictUtils.getItem(configs, smtpConfigs(smtpItem.UseInTSL), TSL)
			//End If

			GlobalShared.Log.Log((int)LogClass.logType.Config, "   Username=" + Username, false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   Password=" + Password, false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   Host=" + Host, false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   Port=" + Port, false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   Email=" + Email, false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   SSL=" + SSL, false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   TSL=" + TSL, false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   Authentication=" + authenticationType, false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   RETRIEVE_MAIL_INTERVAL=" + RETRIEVE_MAIL_INTERVAL, false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   ReadSmtp=" + readSmtp, false);
			GlobalShared.Log.Log((int)LogClass.logType.Config, "   ReadOutlook=" + readOutlook, false);

			//If Username.Trim = "" Or Host.Trim = "" Or Port <= 0 Or Email.Trim = "" Then
			//Log.Log((int)LogClass.logType.ErrorCondition, "No Username found. Email configuration needs to be setup. Exiting")
			//End If

			obj = "HELLO";
			TimerCallback tcb = this.listen;
			GlobalShared.Log.Log((int)LogClass.logType.Config, "Setting up Timer", false);
			try {
				myTimer = new System.Threading.Timer(tcb, obj, DELAY, RETRIEVE_MAIL_INTERVAL);
			} catch (System.IO.FileNotFoundException ex) {
				GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
			} catch (System.Exception ex) {
				GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
			}
            GlobalShared.Log.Log((int)LogClass.logType.Config, "Ready to listen for messages", false);

			//End If
		}

		protected override void OnStop()
		{
			// Add code here to perform any tear-down necessary to stop your service.
			GlobalShared.Log.Log((int)LogClass.logType.Info, "Stopping Service", false);
			myTimer.Change(Timeout.Infinite, Timeout.Infinite);
			runFlag = false;

		}


		protected override void OnPause()
		{
            GlobalShared.Log.Log((int)LogClass.logType.Info, "Pausing Service", false);
			isEnabled = false;
			myTimer.Change(Timeout.Infinite, Timeout.Infinite);

		}


		protected override void OnContinue()
		{
            GlobalShared.Log.Log((int)LogClass.logType.Info, "Resuming Service", false);
			isEnabled = true;
			myTimer.Change(DELAY, RETRIEVE_MAIL_INTERVAL);

		}

		public void listen(object stateInfo)
		{
			myTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
			try {
				if (!isEnabled | !runFlag)
					return;
				if (readSmtp) {
					GlobalShared.Log.Log((int)LogClass.logType.Info, "Checking for SMTP messages", false);
					System.Collections.Generic.SortedList<string, Rfc822Message> messages = getSmtpMessages();
					if ((messages != null)) {
						if (messages.Count > 0) {
							GlobalShared.Log.Log((int)LogClass.logType.Info, "About to process messages", false);
							foreach (KeyValuePair<string, Rfc822Message> msg in messages) {
								if (!string.IsNullOrEmpty(UtilFunctions.findIssueTag(msg.Value))) {
									processMessage(msg);
								}
								if (!runFlag | !isEnabled)
									break; // TODO: might not be correct. Was : Exit For
							}
						}
					} else {
                        GlobalShared.Log.Log((int)LogClass.logType.Info, "No SMTP messages found", false);
					}
				} else {
                    GlobalShared.Log.Log((int)LogClass.logType.Info, "readSmtp=" + readSmtp, false);
				}

				if (readOutlook) {
					GlobalShared.Log.Log((int)LogClass.logType.Info, "Checking for Outlook messages", false);
					getOutlookMessages();
				}
			} catch (System.IO.FileNotFoundException ex) {
				GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
			} catch (System.IO.IOException ex) {
				GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
			} catch (System.Exception ex) {
				GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
			}
			myTimer.Change(RETRIEVE_MAIL_INTERVAL, RETRIEVE_MAIL_INTERVAL);

		}

		private SortedList<string, Rfc822Message> getSmtpMessages()
		{

			Pop3Client target = new Pop3Client();
			target.Host = Host;
			//TCP port for connection
			target.Port = Convert.ToUInt16(Port);
			//Username to login to the POP3 server
			target.Username = Username;
			//Password to login to the POP3 server
			target.Password = Password;
			// SSL Interaction type
			Email.Net.Common.Configurations.EInteractionType interaction = default(Email.Net.Common.Configurations.EInteractionType);
			if (TSL) {
				interaction = EInteractionType.StartTLS;
			} else if (SSL) {
				interaction = EInteractionType.SSLPort;
			} else {
				interaction = EInteractionType.Plain;
			}
			target.SSLInteractionType = EInteractionType.SSLPort;
			target.AuthenticationType = EAuthenticationType.Login;
			if (authenticationType == (int)EAuthenticationType.None) {
				Password = "";
				Username = "";
			}

			System.Collections.Generic.SortedList<string, Rfc822Message> messages = new System.Collections.Generic.SortedList<string, Rfc822Message>();
			Rfc822Message msg = null;
			// Login to POP server
			try {
				Pop3Response response = target.Login();
				if (response.Type == EPop3ResponseType.OK) {
					// Retrieve Unique IDs for all messages
					Pop3MessageUIDInfoCollection messageUIDs = target.GetAllUIDMessages();
					//Check if messages already received
					foreach (Pop3MessageUIDInfo uidInfo in messageUIDs) {
						try {
							msg = target.GetMessage(uidInfo.SerialNumber);
						} catch (Email.Net.Common.Exceptions.ConnectionException ex) {
						} catch (Email.Net.Common.Exceptions.AuthenticationMethodNotSupportedException ex) {
						} catch (System.Exception ex) {
						}
						if ((msg != null)) {
							if (!string.IsNullOrEmpty(UtilFunctions.findIssueTag(msg).Trim())) {
								if (!UtilFunctions.searchUID(uidInfo.UniqueNumber)) {
									// No.  Add to list
									messages.Add(uidInfo.UniqueNumber, target.GetMessage(uidInfo.SerialNumber));
								}
							}
						}
						if (!runFlag | !isEnabled)
							break; // TODO: might not be correct. Was : Exit For
					}
					string cnt = (messages.Count.ToString());
					cnt += " SMTP messages were found";
					GlobalShared.Log.Log((int)LogClass.logType.Info, cnt, false);
				}
				//Logout from the server
				target.Logout();
			} catch (IOException ex) {
				GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
			} catch (Email.Net.Common.Exceptions.ConnectionException ex) {
				GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
			} catch (System.Exception ex) {
				GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, ex.Message, false);
			}
			return messages;

		}

		private SortedList<string, Rfc822Message> getOutlookMessages()
		{

			System.Collections.Generic.SortedList<string, Rfc822Message> messages = new System.Collections.Generic.SortedList<string, Rfc822Message>();

			//Dim tempApp As Outlook.Application
			//Dim tempInbox As Outlook.MAPIFolder
			//Dim InboxItems As Outlook.Items
			//Dim tempMail As Object = Nothing
			//Dim objattachments, objAttach 

			RDOSession session = null;
			//object inbox = null;
			try {
                //tempApp = New Outlook.Application

                session = (RDOSession)Interaction.CreateObject("Redemption.RDOSession");
				session.Logon();
			} catch (System.Exception ex) {
				GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to create Outlook object: " + ex.Message, false);
				return null;
			}
			//tempInbox = tempApp.GetNamespace("Mapi").GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox)

			RDOFolder inbox = session.GetDefaultFolder(rdoDefaultFolders.olFolderInbox);
			//InboxItems = tempInbox.Items
			//Dim msg As Outlook.MailItem
			foreach (MailItem msg in inbox.Items) {
				//For Each msg In InboxItems
				if (!string.IsNullOrEmpty(UtilFunctions.findIssueTag((Rfc822Message)msg).Trim())) {
					if (!UtilFunctions.searchUID(msg.EntryID)) {
						// No.  Add to list
						messages.Add(msg.EntryID, (Rfc822Message)msg);
					}
				}
				if (!runFlag | !isEnabled)
					break; // TODO: might not be correct. Was : Exit For
			}
			session.Logoff();
			string cnt = messages.Count.ToString();
			cnt += " Outlook messages were found";
			GlobalShared.Log.Log((int)LogClass.logType.Info, cnt,false);

			return messages;

		}

		private void processMessage(System.Collections.Generic.KeyValuePair<string, Rfc822Message> msg)
		{
			string token = UtilFunctions.findIssueTag(msg.Value);
			string[] parts = null;
			// issueId, accountId, tenantId
			UtilFunctions.parseMailToken(token, ref parts);

			if ((parts != null)) {
				if (parts.Length > 2) {
                    // Verify issueId
                    int key = 0;
                    int.TryParse(parts[0], out key);
					Issues issue = Issues.load((long)key);
					if ((issue == null)) {
						// Invalid issueId
						GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "PopListener: Invalid IssueId found <" + parts[0] + "> in message <" + msg.Key + "<", false);
						return;
					}
					// Save the message
					string filename = UtilFunctions.saveEmail(msg.Value, Convert.ToInt64(parts[0]), issue.accountId, msg.Key);
					Messages message = new Messages(0, key, (int)issue.accountId, DateTime.Now, 1, DateTime.Now, false, false, GlobalShared.NULL_DATE, false, filename);
					message.save();
				}
			}

		}

        static string GetDatabaseParms()
        {
            string DatabaseSource = "";
            string InitialCatalog = "";
            string MachineName = "";
            MbcsUtils.Parameters parms = null;
            parms = new MbcsUtils.Parameters(appPath + LOCAL_USER_DIRECTORY + "\\" + MBCSCENTRAL_CONFIG);
            parms.getConfigs();
            object obj = null;
            parms.getItem("databaseSource", ref obj);
            DatabaseSource = (string)obj; //  + ",1433";
            parms.getItem("initialCatalog", ref obj);
            InitialCatalog = (string)obj;
            parms.getItem("MachineName", ref obj);
            MachineName = (string)(obj == null || ((string)obj).Contains("*") ? "" : (string)obj + "\\");
            if (DatabaseSource == null || InitialCatalog == null || DatabaseSource.Trim() == "" || InitialCatalog.Trim() == "")
            {
                return null;
            }
            if (MachineName != null && !MachineName.Trim().Equals(""))
                DatabaseSource = MachineName + DatabaseSource;
            return SqlBaseConnectionString.Replace("<source>", DatabaseSource).
                                                    Replace("<catalog>", InitialCatalog).
                                                    Replace("<user>", DB_USER_NAME).
                                                    Replace("<password>", DB_PASSWORD);
        }


        public MbcsListener()
		{
			InitializeComponent();
		}

	}
}
