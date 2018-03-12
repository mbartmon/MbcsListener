using System.Data;
using System;
using System.Collections;

namespace MbcsListener
{
    public class Issues : clsDB
    {

        // $ID: Exp $


        public const string TABLE = "Issues";
        public const string SQL_COL_ISSUE_ID = "issue_id";
        public const string SQL_COL_SEQUENCE_NUMBER = "sequence_number";
        public const string SQL_COL_ACCOUNT_ID = "account_id";
        public const string SQL_COL_TIMESTAMP = "timestamp";
        public const string SQL_COL_CATEGORY = "note";
        public const string SQL_COL_RECIPIENT_IDS = "recipient_ids";
        public const string SQL_COL_DUE_DATE = "due_date";
        public const string SQL_COL_NOTIFY_RESPONSIBLE = "notify_responsible";
        public const string SQL_COL_PRIORITY = "priority";
        public const string SQL_COL_STATUS = "status";
        public const string SQL_COL_SUBJECT = "subject";
        public const string SQL_COL_ALERT_MODE = "alert_mode";
        public const string SQL_COL_PERSON_RESPONSIBLE = "person_responsible";
        public const string SQL_COL_LAST_UPDATE = "last_update";
        public const string SQL_COL_REPORTED = "reported";
        public const string SQL_COL_ASSIGNED = "assigned";
        public const string SQL_COL_VENDOR_ID = "vendor_id";

        public const string SQL_COL_LOCATION_ID = "location_id";

        public const string SQL_LOAD_DASHBOARD = "SELECT " + TABLE + "." + SQL_COL_ISSUE_ID + " AS ID, " + TABLE + "." + SQL_COL_TIMESTAMP + " AS Created, " + SQL_COL_CATEGORY + ", " + TABLE + "." + SQL_COL_DUE_DATE + " AS [Due Date], " + TABLE + "." + SQL_COL_ACCOUNT_ID + ", " + SQL_COL_PERSON_RESPONSIBLE + " AS Responsible, " + SQL_COL_STATUS + " AS [Issue Status], " + SQL_COL_PRIORITY + " AS [Issue Priority]";

        public const string SQL_LOAD_DASHBOARD_BUILDING = "SELECT " + TABLE + "." + SQL_COL_ISSUE_ID + " AS ID, " + TABLE + "." + SQL_COL_TIMESTAMP + " AS Created, " + SQL_COL_CATEGORY + ", " + SQL_COL_LOCATION_ID + ", " + SQL_COL_VENDOR_ID + ", " + TABLE + "." + SQL_COL_REPORTED + ", " + TABLE + "." + SQL_COL_ASSIGNED + ", " + TABLE + "." + SQL_COL_ACCOUNT_ID + ", " + SQL_COL_PERSON_RESPONSIBLE + " AS Responsible, " + SQL_COL_STATUS + " AS [Issue Status], " + SQL_COL_PRIORITY + " AS [Issue Priority]";

        const string SQL_LOAD_FOR_PRINT = "SELECT " + SQL_COL_ISSUE_ID + ", " + SQL_COL_SEQUENCE_NUMBER + ", " + SQL_COL_ACCOUNT_ID + ", " + SQL_COL_TIMESTAMP + ", " + SQL_COL_CATEGORY + ", " + SQL_COL_RECIPIENT_IDS + ", " + SQL_COL_DUE_DATE + ", " + SQL_COL_NOTIFY_RESPONSIBLE + ", " + SQL_COL_PRIORITY + ", " + SQL_COL_STATUS + ", " + SQL_COL_SUBJECT + ", " + SQL_COL_ALERT_MODE + ", " + SQL_COL_PERSON_RESPONSIBLE + ", " + SQL_COL_LAST_UPDATE + ", " + SQL_COL_REPORTED + ", " + SQL_COL_ASSIGNED + ", " + SQL_COL_VENDOR_ID + ", " + SQL_COL_LOCATION_ID + " FROM " + TABLE + " WHERE " + SQL_COL_ISSUE_ID + "=?;";


        const string SQL_LOAD = "SELECT " + SQL_COL_ISSUE_ID + ", " + SQL_COL_SEQUENCE_NUMBER + ", " + SQL_COL_ACCOUNT_ID + ", " + SQL_COL_TIMESTAMP + ", " + SQL_COL_CATEGORY + ", " + SQL_COL_RECIPIENT_IDS + ", " + SQL_COL_DUE_DATE + ", " + SQL_COL_NOTIFY_RESPONSIBLE + ", " + SQL_COL_PRIORITY + ", " + SQL_COL_STATUS + ", " + SQL_COL_SUBJECT + ", " + SQL_COL_ALERT_MODE + ", " + SQL_COL_PERSON_RESPONSIBLE + ", " + SQL_COL_LAST_UPDATE + ", " + SQL_COL_REPORTED + ", " + SQL_COL_ASSIGNED + ", " + SQL_COL_VENDOR_ID + ", " + SQL_COL_LOCATION_ID + " FROM " + TABLE + " WHERE " + SQL_COL_ISSUE_ID + "=?;";

        const string SQL_LOAD_ALL = "SELECT " + SQL_COL_ISSUE_ID + ", " + SQL_COL_SEQUENCE_NUMBER + ", " + SQL_COL_ACCOUNT_ID + ", " + SQL_COL_TIMESTAMP + ", " + SQL_COL_CATEGORY + ", " + SQL_COL_RECIPIENT_IDS + ", " + SQL_COL_DUE_DATE + ", " + SQL_COL_NOTIFY_RESPONSIBLE + ", " + SQL_COL_PRIORITY + ", " + SQL_COL_STATUS + ", " + SQL_COL_SUBJECT + ", " + SQL_COL_ALERT_MODE + ", " + SQL_COL_PERSON_RESPONSIBLE + ", " + SQL_COL_LAST_UPDATE + ", " + SQL_COL_REPORTED + ", " + SQL_COL_ASSIGNED + ", " + SQL_COL_VENDOR_ID + ", " + SQL_COL_LOCATION_ID + " FROM " + TABLE + " ORDER BY " + SQL_COL_TIMESTAMP + ", " + SQL_COL_ACCOUNT_ID + ";";

        const string SQL_INSERT = "INSERT INTO [" + TABLE + "] (" + SQL_COL_SEQUENCE_NUMBER + ", " + SQL_COL_ACCOUNT_ID + ", [" + SQL_COL_TIMESTAMP + "], [" + SQL_COL_CATEGORY + "], " + SQL_COL_RECIPIENT_IDS + ", " + SQL_COL_DUE_DATE + ", " + SQL_COL_NOTIFY_RESPONSIBLE + ", " + SQL_COL_PRIORITY + ", " + SQL_COL_STATUS + ", " + SQL_COL_SUBJECT + ", " + SQL_COL_ALERT_MODE + ", " + SQL_COL_PERSON_RESPONSIBLE + ", " + SQL_COL_LAST_UPDATE + ", " + SQL_COL_REPORTED + ", " + SQL_COL_ASSIGNED + ", " + SQL_COL_VENDOR_ID + ", " + SQL_COL_LOCATION_ID + ") VALUES (?, ?, #?#, '?', '?', #?#, ?, ?, ?, '?', ?, ?, #?#, #?#, #?#, ?, ?);";

        const string SQL_UPDATE = "UPDATE [" + TABLE + "] SET " + SQL_COL_SEQUENCE_NUMBER + "=?, " + SQL_COL_ACCOUNT_ID + "=?, [" + SQL_COL_TIMESTAMP + "]=#?#, [" + SQL_COL_CATEGORY + "]='?', " + SQL_COL_RECIPIENT_IDS + "='?', " + SQL_COL_DUE_DATE + "=#?#, " + SQL_COL_NOTIFY_RESPONSIBLE + "=?, " + SQL_COL_PRIORITY + "=?, " + SQL_COL_STATUS + "=?, " + SQL_COL_SUBJECT + "='?', " + SQL_COL_ALERT_MODE + "=?, " + SQL_COL_PERSON_RESPONSIBLE + "=?, " + SQL_COL_LAST_UPDATE + "=#?#" + ", " + SQL_COL_REPORTED + "=#?#, " + SQL_COL_ASSIGNED + "=#?#, " + SQL_COL_VENDOR_ID + "=?, " + SQL_COL_LOCATION_ID + "=?" + " WHERE " + SQL_COL_ISSUE_ID + "=?;";

        const string SQL_LOAD_BY_ACCOUNT_ID = "SELECT " + SQL_COL_ISSUE_ID + ", " + SQL_COL_SEQUENCE_NUMBER + ", " + SQL_COL_ACCOUNT_ID + ", " + SQL_COL_TIMESTAMP + ", " + SQL_COL_CATEGORY + ", " + SQL_COL_RECIPIENT_IDS + ", " + SQL_COL_DUE_DATE + ", " + SQL_COL_NOTIFY_RESPONSIBLE + ", " + SQL_COL_PRIORITY + ", " + SQL_COL_STATUS + ", " + SQL_COL_SUBJECT + ", " + SQL_COL_ALERT_MODE + ", " + SQL_COL_PERSON_RESPONSIBLE + ", " + SQL_COL_LAST_UPDATE + ", " + SQL_COL_REPORTED + ", " + SQL_COL_ASSIGNED + ", " + SQL_COL_VENDOR_ID + ", " + SQL_COL_LOCATION_ID + " FROM " + TABLE + " WHERE " + SQL_COL_ACCOUNT_ID + "=?;";

        const string SQL_LOAD_BY_ACCOUNT_ID_ORDER_BY = "SELECT " + SQL_COL_ISSUE_ID + ", " + SQL_COL_SEQUENCE_NUMBER + ", " + SQL_COL_ACCOUNT_ID + ", " + SQL_COL_TIMESTAMP + ", " + SQL_COL_CATEGORY + ", " + SQL_COL_RECIPIENT_IDS + ", " + SQL_COL_DUE_DATE + ", " + SQL_COL_NOTIFY_RESPONSIBLE + ", " + SQL_COL_PRIORITY + ", " + SQL_COL_STATUS + ", " + SQL_COL_SUBJECT + ", " + SQL_COL_ALERT_MODE + ", " + SQL_COL_PERSON_RESPONSIBLE + ", " + SQL_COL_LAST_UPDATE + ", " + SQL_COL_REPORTED + ", " + SQL_COL_ASSIGNED + ", " + SQL_COL_VENDOR_ID + ", " + SQL_COL_LOCATION_ID + " FROM " + TABLE + " WHERE " + SQL_COL_ACCOUNT_ID + "=?" + " ORDER BY ";


        const string SQL_LOAD_BY_SEQUENCE_NUMBER = "SELECT " + SQL_COL_ISSUE_ID + ", " + SQL_COL_SEQUENCE_NUMBER + ", " + SQL_COL_ACCOUNT_ID + ", " + SQL_COL_TIMESTAMP + ", " + SQL_COL_CATEGORY + ", " + SQL_COL_RECIPIENT_IDS + ", " + SQL_COL_DUE_DATE + ", " + SQL_COL_NOTIFY_RESPONSIBLE + ", " + SQL_COL_PRIORITY + ", " + SQL_COL_STATUS + ", " + SQL_COL_SUBJECT + ", " + SQL_COL_ALERT_MODE + ", " + SQL_COL_PERSON_RESPONSIBLE + ", " + SQL_COL_LAST_UPDATE + ", " + SQL_COL_REPORTED + ", " + SQL_COL_ASSIGNED + ", " + SQL_COL_VENDOR_ID + ", " + SQL_COL_LOCATION_ID + " FROM " + TABLE + " WHERE " + SQL_COL_SEQUENCE_NUMBER + "=?;";

        const string SQL_LOAD_ORDER_BY = "SELECT " + SQL_COL_ISSUE_ID + ", " + SQL_COL_SEQUENCE_NUMBER + ", " + SQL_COL_ACCOUNT_ID + ", " + SQL_COL_TIMESTAMP + ", " + SQL_COL_CATEGORY + ", " + SQL_COL_RECIPIENT_IDS + ", " + SQL_COL_DUE_DATE + ", " + SQL_COL_NOTIFY_RESPONSIBLE + ", " + SQL_COL_PRIORITY + ", " + SQL_COL_STATUS + ", " + SQL_COL_SUBJECT + ", " + SQL_COL_ALERT_MODE + ", " + SQL_COL_PERSON_RESPONSIBLE + ", " + SQL_COL_LAST_UPDATE + ", " + SQL_COL_REPORTED + ", " + SQL_COL_ASSIGNED + ", " + SQL_COL_VENDOR_ID + ", " + SQL_COL_LOCATION_ID + " FROM " + TABLE + " ORDER BY ";
        // Fields
        public long issueId;
        public long sequenceNumber;
        public long accountId;
        public System.DateTime timestamp;
        public string category;
        public int[] recipientids;
        public System.DateTime duedate;
        public bool notifyResponsible;
        public int priority;
        public int status;
        public int alertMode;
        public string subject;
        public long personResponsible;
        protected DateTime lastUpdate;
        protected DateTime reported;
        protected DateTime assigned;
        protected long vendorId;

        protected long locationId;
        //Other variables

        public static string[] issueStatus = {
        "Open",
        "Viewed",
        "Closed"
    };
        public static string[] issuePriority = {
        "Low",
        "Medium",
        "High",
        "Critical"

    };
        public enum issueOrder
        {
            byCreateDate,
            byDueDate,
            byCreator,
            byResponsible
        }

        public static string[] orderBy = {
        string.Format("{0} {1}, {2}", TABLE + "." + SQL_COL_TIMESTAMP, "DESC", SQL_COL_ACCOUNT_ID),
        string.Format("{0}, {1}", TABLE + "." + SQL_COL_DUE_DATE, SQL_COL_ACCOUNT_ID),
        SQL_COL_ACCOUNT_ID,
        string.Format("{0}, {1}", SQL_COL_PERSON_RESPONSIBLE, SQL_COL_ACCOUNT_ID),
        string.Format("{0}, {1}", SQL_COL_CATEGORY, SQL_COL_TIMESTAMP)

    };
        enum alerts : int
        {
            none,
            initialLogon = 1,
            everyLogon = 2,
            initialOpenApp = 4,
            everyOpenApp = 8,
            intialOpenRecord = 16,
            everyOpenRecord = 32
        }


        public Issues() : base(TABLE, SQL_COL_ISSUE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
            

        }

        public Issues(long sequenceNumber, long accountId, System.DateTime timestamp, string category, int[] recipientids, System.DateTime duedate, bool notifyResponsible, int priority, int status, string subject, int alertMode, long personResponsible, System.DateTime lastupdate) : 
            base(TABLE, SQL_COL_ISSUE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {            
            this.sequenceNumber = sequenceNumber;
            this.accountId = accountId;
            this.timestamp = timestamp;
            this.category = category;
            this.recipientids = recipientids;
            this.duedate = duedate;
            this.notifyResponsible = notifyResponsible;
            this.priority = priority;
            this.status = status;
            this.subject = subject;
            this.alertMode = alertMode;
            this.personResponsible = personResponsible;
            this.lastUpdate = lastupdate;

        }

        public Issues(string table, string priKeyName, string loadStmt, string loadAllStmt, string insertStmt, string updateStmt) : base(TABLE, SQL_COL_ISSUE_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)

        {

        }


        Issues fill(DataRow dr)
        {

            try {
                this.issueId = (int)dr[SQL_COL_ISSUE_ID];
                this.accountId = (int)dr[SQL_COL_ACCOUNT_ID];
                this.sequenceNumber = (long)dr[SQL_COL_SEQUENCE_NUMBER];
                this.timestamp = (DateTime)dr[SQL_COL_TIMESTAMP];
                this.category = (string)dr[SQL_COL_CATEGORY];
                if (!Convert.IsDBNull(dr[SQL_COL_RECIPIENT_IDS])) {
                    this.recipientids = UtilFunctions.unwrapRecipIds((string)dr[SQL_COL_RECIPIENT_IDS]);
                }
                if (!Convert.IsDBNull(dr[SQL_COL_DUE_DATE])) {
                    this.duedate = (DateTime)dr[SQL_COL_DUE_DATE];
                }
                this.notifyResponsible = (bool)dr[SQL_COL_NOTIFY_RESPONSIBLE];
                this.priority = (int)dr[SQL_COL_PRIORITY];
                this.status = (int)dr[SQL_COL_STATUS];
                if (Convert.IsDBNull(dr[SQL_COL_SUBJECT])) {
                    this.subject = "";
                } else {
                    this.subject = (string)dr[SQL_COL_SUBJECT];
                }
                this.alertMode = (int)dr[SQL_COL_ALERT_MODE];
                this.personResponsible = (long)dr[SQL_COL_PERSON_RESPONSIBLE];
                if (!Convert.IsDBNull(dr[SQL_COL_LAST_UPDATE])) {
                    this.lastUpdate = (DateTime)dr[SQL_COL_LAST_UPDATE];
                }
                if (!Convert.IsDBNull(dr[SQL_COL_REPORTED])) {
                    this.reported = (DateTime)dr[SQL_COL_REPORTED];
                }
                if (!Convert.IsDBNull(dr[SQL_COL_ASSIGNED])) {
                    this.assigned = (DateTime)dr[SQL_COL_ASSIGNED];
                }
                this.vendorId = (long)dr[SQL_COL_VENDOR_ID];
                this.locationId =(long) dr[SQL_COL_LOCATION_ID];
            } catch (System.Exception ex) {
            }
            return this;

        }

        public static Issues load(long id)
        {

            Issues note = new Issues();
            DataSet ds = new DataSet();
            ds = note.loadByPrimaryKey(id.ToString());
            try {
                DataRow dr = ds.Tables[0].Rows[0];
                return note.fill(dr);
            } catch {
                return null;
            }

        }

        public static Issues[] load()
        {

            ArrayList list = new ArrayList();
            DataSet ds = new DataSet();
            Issues note = new Issues();

            ds = note.loadAll();
            note = null;
            foreach (DataRow dr in ds.Tables[0].Rows) {
                note = new Issues();
                note.fill(dr);
                list.Add(note);
                note = null;
            }
            try {
                return (Issues[])list.ToArray(typeof(Issues));
            } catch {
                return null;
            }

        }

        public static Issues[] load(int field)
        {

            ArrayList list = new ArrayList();
            DataSet ds = new DataSet();
            Issues note = new Issues();
            string xsql = string.Format("{0}{1};", SQL_LOAD_ORDER_BY, orderBy[field]);
            ds = note.load(xsql, null);
            note = null;
            foreach (DataRow dr in ds.Tables[0].Rows) {
                note = new Issues();
                note.fill(dr);
                list.Add(note);
                note = null;
            }
            try {
                return (Issues[])list.ToArray(typeof(Issues));
            } catch {
                return null;
            }

        }

        public static Issues[] load(long acctId, int field)
        {

            ArrayList list = new ArrayList();
            DataSet ds = new DataSet();
            Issues note = new Issues();
            string xsql = string.Format("{0}{1};", SQL_LOAD_BY_ACCOUNT_ID_ORDER_BY, orderBy[field]);
            string[] s = new string[1];
            s[0] = acctId.ToString();
            ds = note.load(xsql, s);
            note = null;
            foreach (DataRow dr in ds.Tables[0].Rows) {
                note = new Issues();
                note.fill(dr);
                list.Add(note);
                note = null;
            }
            try {
                return (Issues[])list.ToArray(typeof(Issues));
            } catch (Exception ex)
            {
                return null;
            }

        }
        public static Issues[] loadByAccountId(long accountId)
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Issues note = new Issues();
            string[] s = new string[1];
            s[0] = accountId.ToString();

            ds = note.load(SQL_LOAD_BY_ACCOUNT_ID, s);
            note = null;
            foreach (DataRow dr in ds.Tables[0].Rows) {
                note = new Issues();
                note.fill(dr);
                list.Add(note);
                note = null;
            }
            try {
                return (Issues[])list.ToArray(typeof(Issues));
            } catch {
                return null;
            }

        }
        public static Issues[] loadBySequenceNumber(long sequenceNumber)
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Issues note = new Issues();
            string[] s = new string[1];
            s[0] = sequenceNumber.ToString();

            ds = note.load(SQL_LOAD_BY_SEQUENCE_NUMBER, s);
            note = null;
            foreach (DataRow dr in ds.Tables[0].Rows) {
                note = new Issues();
                note.fill(dr);
                list.Add(note);
                note = null;
            }
            return (Issues[])list.ToArray(typeof(Issues));

        }
        public bool Save()
        {

            if (issueId > 0) {
                string[] values = {
                sequenceNumber.ToString(),
                accountId.ToString(),
                timestamp.ToString("yyyy-MM-dd HH:MM:ss"),
                category,
                UtilFunctions.wrapRecipIds(recipientids),
                duedate.ToString("yyyy-MM-dd HH:MM:ss"),
                notifyResponsible.ToString(),
                priority.ToString(),
                status.ToString(),
                subject,
                alertMode.ToString(),
                personResponsible.ToString(),
                lastUpdate.ToString("yyyy-MM-dd HH:MM:ss"),
                reported.ToString("yyyy-MM-dd HH:MM:ss"),
                assigned.ToString("yyyy-MM-dd HH:MM:ss"),
                vendorId.ToString(),
                locationId.ToString(),
                issueId.ToString()
            };
                if (this.updateRecord(issueId.ToString(), values)) {
                    return true;
                } else {
                    return false;
                }
            } else {
                string[] values = {
                sequenceNumber.ToString(),
                accountId.ToString(),
                timestamp.ToString("yyyy-MM-dd HH:MM:ss"),
                category,
                UtilFunctions.wrapRecipIds(recipientids),
                duedate.ToString("yyyy-MM-dd HH:MM:ss"),
                notifyResponsible.ToString(),
                priority.ToString(),
                status.ToString(),
                subject,
                alertMode.ToString(),
                personResponsible.ToString(),
                timestamp.ToString("yyyy-MM-dd HH:MM:ss"),
                reported.ToString("yyyy-MM-dd HH:MM:ss"),
                assigned.ToString("yyyy-MM-dd HH:MM:ss"),
                vendorId.ToString(),
                locationId.ToString()
            };
                this.issueId = this.insertRecord(values);
                if (issueId >= 0) {
                    return true;
                }
            }
            return false;

        }

        //Utility functions

        //retrieve alert modes as booleans
        public bool alertInitialLogon()
        {
            return ((alertMode == (int)alerts.initialLogon) || (alertMode == (int)alerts.everyLogon) ? true : false);
        }
        public bool alertEveryLogon()
        {
            return alertMode == (int)alerts.everyLogon ? true : false;
        }
        public bool alertIntialOpenApp()
        {
            return ((alertMode == (int)alerts.initialOpenApp) || (alertMode == (int)alerts.everyOpenApp) ? true : false);
        }
        public bool alertEveryOpenApp()
        {
            return alertMode == (int)alerts.everyOpenApp ? true : false;
        }
        public bool alertInitialOpenRecord()
        {
            return ((alertMode ==  (int)alerts.intialOpenRecord) || (alertMode == (int)alerts.everyOpenRecord) ? true : false);
        }
        public bool alertEveryOpenRecord()
        {
            return alertMode == (int)alerts.everyOpenRecord ? true : false;
        }

        //Accessors
        public void setAccountId(long accountId)
        {
            this.accountId = accountId;
        }
        public void setsequenceNumber(int sequenceNumber)
        {
            this.sequenceNumber = sequenceNumber;
        }
        public void setTimestamp(System.DateTime timestamp)
        {
            this.timestamp = timestamp;
        }
        public void setCategory(string category)
        {
            this.category = category;
        }
        public void setRecipientIds(int[] recipientids)
        {
            this.recipientids = recipientids;
        }
        public void setDuedate(System.DateTime duedate)
        {
            this.duedate = duedate;
        }
        public void setNotifyResponsible(bool notifyResponsible)
        {
            this.notifyResponsible = notifyResponsible;
        }
        public void setPriority(int priority)
        {
            this.priority = priority;
        }
        public void setStatus(int status)
        {
            this.status = status;
        }
        public void setSubject(string subject)
        {
            this.subject = subject;
        }
        public void setAlertMod(int alertMode)
        {
            this.alertMode = alertMode;
        }
        public void setAlertModeAdditive(int alertMode)
        {
            this.alertMode = this.alertMode + alertMode;
        }
        public void setPersonResponsible(long personResponsible)
        {
            this.personResponsible = personResponsible;
        }
        public long getNoteId()
        {
            return issueId;
        }
        public long getAccountId()
        {
            return accountId;
        }
        public long getSequenceNumber()
        {
            return sequenceNumber;
        }
        public System.DateTime getTimestamp()
        {
            return timestamp;
        }
        public string getCategory()
        {
            return category;
        }
        public int[] getRecipientIds()
        {
            return recipientids;
        }
        public System.DateTime getDuedate()
        {
            return duedate;
        }
        public bool getNotifyResponsible()
        {
            return notifyResponsible;
        }
        public int getPriority()
        {
            return priority;
        }
        public int getStatus()
        {
            return status;
        }
        public string getSubject()
        {
            return subject;
        }
        public int getAlertMode()
        {
            return alertMode;
        }
        public long getPersonResponsible()
        {
            return personResponsible;
        }
        public void setLastUpdate(System.DateTime last)
        {
            this.lastUpdate = last;
        }
        public System.DateTime getLastUpdate()
        {
            return this.lastUpdate;
        }
        public void setReported(System.DateTime reported)
        {
            this.reported = reported;
        }
        public System.DateTime getReported()
        {
            return this.reported;
        }
        public void setAssigned(System.DateTime assigned)
        {
            this.assigned = assigned;
        }
        public System.DateTime getAssigned()
        {
            return this.assigned;
        }
        public void setVendorId(long vendorId)
        {
            this.vendorId = vendorId;
        }
        public long getVendorId()
        {
            return this.vendorId;
        }
        public void setLocationId(long locationId)
        {
            this.locationId = locationId;
        }
        public long getLocationId()
        {
            return this.locationId;
        }

    }
}