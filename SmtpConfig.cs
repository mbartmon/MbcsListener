using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MbcsListener
{
	public class SmtpConfig : clsDB, INotifyPropertyChanged
	{
	    public event PropertyChangedEventHandler PropertyChanged;

	    public static string TABLE = "SmtpConfig";

	    public const string SQL_COL_I_D = "ID";
	    public const string SQL_COL_PORT = "port";
	    public const string SQL_COL_AUTHENTICATION = "authentication";
	    public const string SQL_COL_IN_PORT = "in_port";
	    public const string SQL_COL_IN_AUTHENTICATION = "in_authentication";
	    public const string SQL_COL_S_S_L = "SSL";
	    public const string SQL_COL_T_S_L = "TSL";
	    public const string SQL_COL_IN_S_S_L = "in_SSL";
	    public const string SQL_COL_IN_T_S_L = "in_TSL";
	    public const string SQL_COL_DEFAULT_ACCOUNT = "defaultAccount";
	    public const string SQL_COL_MAIL_TAG = "MailTag";
	    public const string SQL_COL_DESCRIPTION = "description";
	    public const string SQL_COL_EMAIL_ADDRESS = "emailAddress";
	    public const string SQL_COL_USER_NAME = "userName";
	    public const string SQL_COL_PASSWORD = "password";
	    public const string SQL_COL_SERVER = "server";
	    public const string SQL_COL_IN_USER_NAME = "in_userName";
	    public const string SQL_COL_IN_PASSWORD = "in_password";
	    public const string SQL_COL_IN_SERVER = "in_server";
        public const string SQL_COL_ACCOUNTID = "accountId";
        public const string SQL_COL_DISPLAY_NAME = "displayName"; 

		static string SQL_LOAD = "SELECT " +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_PORT + "]," +
				"[" + SQL_COL_AUTHENTICATION + "]," +
				"[" + SQL_COL_IN_PORT + "]," +
				"[" + SQL_COL_IN_AUTHENTICATION + "]," +
				"[" + SQL_COL_S_S_L + "]," +
				"[" + SQL_COL_T_S_L + "]," +
				"[" + SQL_COL_IN_S_S_L + "]," +
				"[" + SQL_COL_IN_T_S_L + "]," +
				"[" + SQL_COL_DEFAULT_ACCOUNT + "]," +
				"[" + SQL_COL_MAIL_TAG + "]," +
				"[" + SQL_COL_DESCRIPTION + "]," +
				"[" + SQL_COL_EMAIL_ADDRESS + "]," +
				"[" + SQL_COL_USER_NAME + "]," +
				"[" + SQL_COL_PASSWORD + "]," +
				"[" + SQL_COL_SERVER + "]," +
				"[" + SQL_COL_IN_USER_NAME + "]," +
				"[" + SQL_COL_IN_PASSWORD + "]," +
				"[" + SQL_COL_IN_SERVER + "]," +
                "[" + SQL_COL_ACCOUNTID + "]," +
                "[" + SQL_COL_DISPLAY_NAME + "]" +
				" FROM " + TABLE +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

		static string SQL_LOAD_ALL = "SELECT " +
				"[" + SQL_COL_I_D + "]," +
				"[" + SQL_COL_PORT + "]," +
				"[" + SQL_COL_AUTHENTICATION + "]," +
				"[" + SQL_COL_IN_PORT + "]," +
				"[" + SQL_COL_IN_AUTHENTICATION + "]," +
				"[" + SQL_COL_S_S_L + "]," +
				"[" + SQL_COL_T_S_L + "]," +
				"[" + SQL_COL_IN_S_S_L + "]," +
				"[" + SQL_COL_IN_T_S_L + "]," +
				"[" + SQL_COL_DEFAULT_ACCOUNT + "]," +
				"[" + SQL_COL_MAIL_TAG + "]," +
				"[" + SQL_COL_DESCRIPTION + "]," +
				"[" + SQL_COL_EMAIL_ADDRESS + "]," +
				"[" + SQL_COL_USER_NAME + "]," +
				"[" + SQL_COL_PASSWORD + "]," +
				"[" + SQL_COL_SERVER + "]," +
				"[" + SQL_COL_IN_USER_NAME + "]," +
				"[" + SQL_COL_IN_PASSWORD + "]," +
				"[" + SQL_COL_IN_SERVER + "]," +
                "[" + SQL_COL_ACCOUNTID + "]," +
                "[" + SQL_COL_DISPLAY_NAME + "]" +
				" FROM " + TABLE + ";";

		static string SQL_INSERT = "INSERT INTO " + TABLE + " (" +
				"[" + SQL_COL_PORT + "]," +
				"[" + SQL_COL_AUTHENTICATION + "]," +
				"[" + SQL_COL_IN_PORT + "]," +
				"[" + SQL_COL_IN_AUTHENTICATION + "]," +
				"[" + SQL_COL_S_S_L + "]," +
				"[" + SQL_COL_T_S_L + "]," +
				"[" + SQL_COL_IN_S_S_L + "]," +
				"[" + SQL_COL_IN_T_S_L + "]," +
				"[" + SQL_COL_DEFAULT_ACCOUNT + "]," +
				"[" + SQL_COL_MAIL_TAG + "]," +
				"[" + SQL_COL_DESCRIPTION + "]," +
				"[" + SQL_COL_EMAIL_ADDRESS + "]," +
				"[" + SQL_COL_USER_NAME + "]," +
				"[" + SQL_COL_PASSWORD + "]," +
				"[" + SQL_COL_SERVER + "]," +
				"[" + SQL_COL_IN_USER_NAME + "]," +
				"[" + SQL_COL_IN_PASSWORD + "]," +
				"[" + SQL_COL_IN_SERVER + "]," +
                "[" + SQL_COL_ACCOUNTID + "]," +
                "[" + SQL_COL_DISPLAY_NAME + "]" +
				") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, '?', '?', '?', '?', '?', '?', '?', '?', '?', ?, '?');";

		static string SQL_UPDATE = "UPDATE " + TABLE + " SET " +
				"[" + SQL_COL_PORT + "]=?," +
				"[" + SQL_COL_AUTHENTICATION + "]=?," +
				"[" + SQL_COL_IN_PORT + "]=?," +
				"[" + SQL_COL_IN_AUTHENTICATION + "]=?," +
				"[" + SQL_COL_S_S_L + "]=?," +
				"[" + SQL_COL_T_S_L + "]=?," +
				"[" + SQL_COL_IN_S_S_L + "]=?," +
				"[" + SQL_COL_IN_T_S_L + "]=?," +
				"[" + SQL_COL_DEFAULT_ACCOUNT + "]=?," +
				"[" + SQL_COL_MAIL_TAG + "]='?'," +
				"[" + SQL_COL_DESCRIPTION + "]='?'," +
				"[" + SQL_COL_EMAIL_ADDRESS + "]='?'," +
				"[" + SQL_COL_USER_NAME + "]='?'," +
				"[" + SQL_COL_PASSWORD + "]='?'," +
				"[" + SQL_COL_SERVER + "]='?'," +
				"[" + SQL_COL_IN_USER_NAME + "]='?'," +
				"[" + SQL_COL_IN_PASSWORD + "]='?'," +
				"[" + SQL_COL_IN_SERVER + "]='?'," +
                "[" + SQL_COL_ACCOUNTID + "]=?," +
                "[" + SQL_COL_DISPLAY_NAME + "]='?'" +
				" WHERE " + "[" + SQL_COL_I_D + "] = ?;";

        static string SQL_LOAD_BY_TAG = "SELECT " +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_PORT + "]," +
                "[" + SQL_COL_AUTHENTICATION + "]," +
                "[" + SQL_COL_IN_PORT + "]," +
                "[" + SQL_COL_IN_AUTHENTICATION + "]," +
                "[" + SQL_COL_S_S_L + "]," +
                "[" + SQL_COL_T_S_L + "]," +
                "[" + SQL_COL_IN_S_S_L + "]," +
                "[" + SQL_COL_IN_T_S_L + "]," +
                "[" + SQL_COL_DEFAULT_ACCOUNT + "]," +
                "[" + SQL_COL_MAIL_TAG + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_EMAIL_ADDRESS + "]," +
                "[" + SQL_COL_USER_NAME + "]," +
                "[" + SQL_COL_PASSWORD + "]," +
                "[" + SQL_COL_SERVER + "]," +
                "[" + SQL_COL_IN_USER_NAME + "]," +
                "[" + SQL_COL_IN_PASSWORD + "]," +
                "[" + SQL_COL_IN_SERVER + "]," +
                "[" + SQL_COL_ACCOUNTID + "]," +
                "[" + SQL_COL_DISPLAY_NAME + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_MAIL_TAG + "] = '?';";

        static string SQL_LOAD_BY_DESCRIPTION = "SELECT " +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_PORT + "]," +
                "[" + SQL_COL_AUTHENTICATION + "]," +
                "[" + SQL_COL_IN_PORT + "]," +
                "[" + SQL_COL_IN_AUTHENTICATION + "]," +
                "[" + SQL_COL_S_S_L + "]," +
                "[" + SQL_COL_T_S_L + "]," +
                "[" + SQL_COL_IN_S_S_L + "]," +
                "[" + SQL_COL_IN_T_S_L + "]," +
                "[" + SQL_COL_DEFAULT_ACCOUNT + "]," +
                "[" + SQL_COL_MAIL_TAG + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_EMAIL_ADDRESS + "]," +
                "[" + SQL_COL_USER_NAME + "]," +
                "[" + SQL_COL_PASSWORD + "]," +
                "[" + SQL_COL_SERVER + "]," +
                "[" + SQL_COL_IN_USER_NAME + "]," +
                "[" + SQL_COL_IN_PASSWORD + "]," +
                "[" + SQL_COL_IN_SERVER + "]," +
                "[" + SQL_COL_ACCOUNTID + "]," +
                "[" + SQL_COL_DISPLAY_NAME + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_DESCRIPTION + "] = '?';";

        static string SQL_LOAD_BY_ACCOUNTID = "SELECT " +
                "[" + SQL_COL_I_D + "]," +
                "[" + SQL_COL_PORT + "]," +
                "[" + SQL_COL_AUTHENTICATION + "]," +
                "[" + SQL_COL_IN_PORT + "]," +
                "[" + SQL_COL_IN_AUTHENTICATION + "]," +
                "[" + SQL_COL_S_S_L + "]," +
                "[" + SQL_COL_T_S_L + "]," +
                "[" + SQL_COL_IN_S_S_L + "]," +
                "[" + SQL_COL_IN_T_S_L + "]," +
                "[" + SQL_COL_DEFAULT_ACCOUNT + "]," +
                "[" + SQL_COL_MAIL_TAG + "]," +
                "[" + SQL_COL_DESCRIPTION + "]," +
                "[" + SQL_COL_EMAIL_ADDRESS + "]," +
                "[" + SQL_COL_USER_NAME + "]," +
                "[" + SQL_COL_PASSWORD + "]," +
                "[" + SQL_COL_SERVER + "]," +
                "[" + SQL_COL_IN_USER_NAME + "]," +
                "[" + SQL_COL_IN_PASSWORD + "]," +
                "[" + SQL_COL_IN_SERVER + "]," +
                "[" + SQL_COL_ACCOUNTID + "]," +
                "[" + SQL_COL_DISPLAY_NAME + "]" +
                " FROM " + TABLE +
                " WHERE " + "[" + SQL_COL_ACCOUNTID + "] = ?;";

        protected int m_ID;
		protected int m_port;
		protected int m_authentication;
		protected int m_in_port;
		protected int m_in_authentication;
		protected bool m_SSL;
		protected bool m_TSL;
		protected bool m_in_SSL;
		protected bool m_in_TSL;
		protected bool m_defaultAccount;
		protected string m_MailTag;
		protected string m_description;
		protected string m_emailAddress;
		protected string m_userName;
		protected string m_password;
		protected string m_server;
		protected string m_in_userName;
		protected string m_in_password;
		protected string m_in_server;
        protected int m_accountId = -1;
        protected string m_displayName;
		protected bool m_isChanged;

		public SmtpConfig() : base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
		}

		public SmtpConfig(int ID,int port,int authentication,int in_port,int in_authentication,bool SSL,bool TSL,bool in_SSL,bool in_TSL,bool defaultAccount,string MailTag,string description,string emailAddress,string userName,string password,string server,string in_userName,string in_password,string in_server, int accountId = -1, string displayName = "") : 
				base(TABLE, SQL_COL_I_D, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, MbcsCentral.GlobalShared.CN)
		{
			m_ID = ID;
			m_port = port;
			m_authentication = authentication;
			m_in_port = in_port;
			m_in_authentication = in_authentication;
			m_SSL = SSL;
			m_TSL = TSL;
			m_in_SSL = in_SSL;
			m_in_TSL = in_TSL;
			m_defaultAccount = defaultAccount;
			m_MailTag = MailTag;
			m_description = description;
			m_emailAddress = emailAddress;
			m_userName = userName;
			m_password = password;
			m_server = server;
			m_in_userName = in_userName;
			m_in_password = in_password;
			m_in_server = in_server;
            m_accountId = accountId;
            m_displayName = displayName;
		}

		protected SmtpConfig fill(DataRow dr)
		{
			try {
				m_ID = (int) dr[SQL_COL_I_D];
				m_port = (int) dr[SQL_COL_PORT];
				m_authentication = (int) dr[SQL_COL_AUTHENTICATION];
				m_in_port = (int) dr[SQL_COL_IN_PORT];
				m_in_authentication = (int) dr[SQL_COL_IN_AUTHENTICATION];
				m_SSL = (bool) dr[SQL_COL_S_S_L];
				m_TSL = (bool) dr[SQL_COL_T_S_L];
				m_in_SSL = (bool) dr[SQL_COL_IN_S_S_L];
				m_in_TSL = (bool) dr[SQL_COL_IN_T_S_L];
				m_defaultAccount = (bool) dr[SQL_COL_DEFAULT_ACCOUNT];
				m_MailTag = (string) (Convert.IsDBNull(dr[SQL_COL_MAIL_TAG]) ? "" : dr[SQL_COL_MAIL_TAG]);
				m_description = (string) (Convert.IsDBNull(dr[SQL_COL_DESCRIPTION]) ? "" : dr[SQL_COL_DESCRIPTION]);
				m_emailAddress = (string) (Convert.IsDBNull(dr[SQL_COL_EMAIL_ADDRESS]) ? "" : dr[SQL_COL_EMAIL_ADDRESS]);
				m_userName = (string) (Convert.IsDBNull(dr[SQL_COL_USER_NAME]) ? "" : dr[SQL_COL_USER_NAME]);
				m_password = (string) (Convert.IsDBNull(dr[SQL_COL_PASSWORD]) ? "" : dr[SQL_COL_PASSWORD]);
				m_server = (string) (Convert.IsDBNull(dr[SQL_COL_SERVER]) ? "" : dr[SQL_COL_SERVER]);
				m_in_userName = (string) (Convert.IsDBNull(dr[SQL_COL_IN_USER_NAME]) ? "" : dr[SQL_COL_IN_USER_NAME]);
				m_in_password = (string) (Convert.IsDBNull(dr[SQL_COL_IN_PASSWORD]) ? "" : dr[SQL_COL_IN_PASSWORD]);
				m_in_server = (string) (Convert.IsDBNull(dr[SQL_COL_IN_SERVER]) ? "" : dr[SQL_COL_IN_SERVER]);
                m_accountId = (int)dr[SQL_COL_ACCOUNTID];
                m_displayName = (string)(Convert.IsDBNull(dr[SQL_COL_DISPLAY_NAME]) ? "" : dr[SQL_COL_DISPLAY_NAME]);

                if (m_accountId > 0)
                {
                    MbcsCentral.Accounts account = MbcsCentral.Accounts.load(m_accountId);
                    if (account != null)
                        AccountName = account.Name;
                    else
                        AccountName = "[Not Found]";
                }
                else if (m_accountId == 0)
                    AccountName = "[Organization]";
                else
                    AccountName = "[None]";
			} catch (SqlException ex) {
			}
			return this;

		}

		public static SmtpConfig load( int m_ID)
		{
			SmtpConfig obj  = new SmtpConfig();
			string[] opt = {m_ID.ToString()};
			DataSet ds = new DataSet();
			ds = obj.load(SQL_LOAD, opt);
			if (ds != null) {
				DataRow dr = ds.Tables[0].Rows[0];
				return obj.fill(dr);
			} else
				return null;

		}

		public static SmtpConfig[] load()
		{
			ArrayList list = new ArrayList();

			DataSet ds = new DataSet();
			SmtpConfig obj = new SmtpConfig();

			ds = obj.loadAll();
		if (ds == null)
				return null;
			obj = null;
			foreach (DataRow dr in ds.Tables[0].Rows) {
				obj = new SmtpConfig();
				obj.fill(dr);
				list.Add(obj);
				obj = null;
			}
			try {
				return (SmtpConfig[])list.ToArray(typeof(SmtpConfig));
			} catch (SqlException ex) {
				return null;
			}

		}


        public static SmtpConfig load(string mailTag)
        {
            SmtpConfig obj = new SmtpConfig();
            string[] opt = { mailTag };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_TAG, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return obj.fill(dr);
            }
            else
                return null;

        }

        public static SmtpConfig[] loadByAccount(int AccountId)
        {
            ArrayList list = new ArrayList();
            SmtpConfig obj = new SmtpConfig();
            string[] opt = { AccountId.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_ACCOUNTID, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj = null;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new SmtpConfig();
                    obj.fill(dr);
                    list.Add(obj);
                    obj = null;
                }
                try
                {
                    return (SmtpConfig[])list.ToArray(typeof(SmtpConfig));
                }
                catch (SqlException ex)
                {
                    return null;
                }
            }
            else
                return null;

        }

        public static SmtpConfig loadByDescription(string desc)
        {
            SmtpConfig obj = new SmtpConfig();
            string[] opt = { desc };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD_BY_DESCRIPTION, opt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return obj.fill(dr);
            }
            else
                return null;

        }

        public bool save()
		{
			if (m_ID > 0 ) {
				string[] values = {m_port.ToString(), m_authentication.ToString(), m_in_port.ToString(), m_in_authentication.ToString(), m_SSL.ToString(), m_TSL.ToString(), m_in_SSL.ToString(), m_in_TSL.ToString(), m_defaultAccount.ToString(), m_MailTag, m_description, m_emailAddress, m_userName, m_password, m_server, m_in_userName, m_in_password, m_in_server, m_accountId.ToString(), m_displayName, m_ID.ToString()};
				if (this.updateRecord(m_ID.ToString(), values)) {
					return true;
				} else {
					return false;
				}
			} else {
				string[] values = {m_port.ToString(), m_authentication.ToString(), m_in_port.ToString(), m_in_authentication.ToString(), m_SSL.ToString(), m_TSL.ToString(), m_in_SSL.ToString(), m_in_TSL.ToString(), m_defaultAccount.ToString(), m_MailTag, m_description, m_emailAddress, m_userName, m_password, m_server, m_in_userName, m_in_password, m_in_server, m_accountId.ToString(), m_displayName};
				m_ID = this.insertRecord(values);
				if (m_ID >= 0) {
					return true;
				}
			}
			return false;

		}

		private void OnPropertyChanged(string info) {
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(info));
				m_isChanged = true;
			}
		}

		// Properties
        public bool IsChanged
        {
            get { return m_isChanged; }
            set { m_isChanged = value; }
        }
		public int ID {
			get { return m_ID; }
		}

		public int port {
			set{ m_port = value;
			OnPropertyChanged("port"); }
			get { return m_port; }
		}

		public int authentication {
			set{ m_authentication = value;
			OnPropertyChanged("authentication"); }
			get { return m_authentication; }
		}

		public int in_port {
			set{ m_in_port = value;
			OnPropertyChanged("in_port"); }
			get { return m_in_port; }
		}

		public int in_authentication {
			set{ m_in_authentication = value;
			OnPropertyChanged("in_authentication"); }
			get { return m_in_authentication; }
		}

		public bool SSL {
			set{ m_SSL = value;
			OnPropertyChanged("SSL"); }
			get { return m_SSL; }
		}

		public bool TSL {
			set{ m_TSL = value;
			OnPropertyChanged("TSL"); }
			get { return m_TSL; }
		}

		public bool in_SSL {
			set{ m_in_SSL = value;
			OnPropertyChanged("in_SSL"); }
			get { return m_in_SSL; }
		}

		public bool in_TSL {
			set{ m_in_TSL = value;
			OnPropertyChanged("in_TSL"); }
			get { return m_in_TSL; }
		}

		public bool defaultAccount {
			set{ m_defaultAccount = value;
			OnPropertyChanged("defaultAccount"); }
			get { return m_defaultAccount; }
		}

		public string MailTag {
			set{ m_MailTag = value;
			OnPropertyChanged("MailTag"); }
			get { return m_MailTag; }
		}

		public string description {
			set{ m_description = value;
			OnPropertyChanged("description"); }
			get { return m_description; }
		}

		public string emailAddress {
			set{ m_emailAddress = value;
			OnPropertyChanged("emailAddress"); }
			get { return m_emailAddress; }
		}

		public string userName {
			set{ m_userName = value;
			OnPropertyChanged("userName"); }
			get { return m_userName; }
		}

		public string password {
			set{ m_password = value;
			OnPropertyChanged("password"); }
			get { return m_password; }
		}

		public string server {
			set{ m_server = value;
			OnPropertyChanged("server"); }
			get { return m_server; }
		}

		public string in_userName {
			set{ m_in_userName = value;
			OnPropertyChanged("in_userName"); }
			get { return m_in_userName; }
		}

		public string in_password {
			set{ m_in_password = value;
			OnPropertyChanged("in_password"); }
			get { return m_in_password; }
		}

		public string in_server {
			set{ m_in_server = value;
			OnPropertyChanged("in_server"); }
			get { return m_in_server; }
		}

        public int AccountId
        {
            set { m_accountId = value;
                OnPropertyChanged("AccountId");
            }
            get { return m_accountId; }
        }

        public string AccountName
        { get; set; }

        public string DisplayName
        {
            set { m_displayName = value;
                OnPropertyChanged("DisplayName");
            }
            get { return m_displayName; }
        }

}
}
