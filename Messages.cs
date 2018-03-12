using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;

namespace MbcsListener
{
    public class Messages : clsDB
    {

        public const string SQL_COL_MESSAGE_ID = "message_id";
        public const string SQL_COL_ISSUE_ID = "issue_id";
        public const string SQL_COL_ACCOUNT_ID = "account_id";
        public const string SQL_COL_DATE_CREATED = "date_created";
        public const string SQL_COL_DIRECTION = "direction";
        public const string SQL_COL_DATE_PROCESSED = "date_processed";
        public const string SQL_COL_REQUEST_CONFIRM = "request_confirm";
        public const string SQL_COL_CONFIRM_RECEIVED = "confirm_received";
        public const string SQL_COL_DATE_CONFIRMED = "date_confirmed";
        public const string SQL_COL_READ = "read";
        public const string SQL_COL_FILENAME = "filename";

        public const string SQL_COL_ID = "SQL_COL_MESSAGE_ID";

        public const string TABLE = "messages";

        static string SQL_LOAD = " SELECT " + "[" + SQL_COL_MESSAGE_ID + "]," + "[" + SQL_COL_ISSUE_ID + "]," + "[" + SQL_COL_ACCOUNT_ID + "]," + "[" + SQL_COL_DATE_CREATED + "]," + "[" + SQL_COL_DIRECTION + "]," + "[" + SQL_COL_DATE_PROCESSED + "]," + "[" + SQL_COL_REQUEST_CONFIRM + "]," + "[" + SQL_COL_CONFIRM_RECEIVED + "]," + "[" + SQL_COL_DATE_CONFIRMED + "]," + "[" + SQL_COL_READ + "]," + "[" + SQL_COL_FILENAME + "]" + " FROM " + TABLE + " WHERE " + SQL_COL_MESSAGE_ID + " = ?;";

        static string SQL_LOAD_ALL = " SELECT " + "[" + SQL_COL_MESSAGE_ID + "]," + "[" + SQL_COL_ISSUE_ID + "]," + "[" + SQL_COL_ACCOUNT_ID + "]," + "[" + SQL_COL_DATE_CREATED + "]," + "[" + SQL_COL_DIRECTION + "]," + "[" + SQL_COL_DATE_PROCESSED + "]," + "[" + SQL_COL_REQUEST_CONFIRM + "]," + "[" + SQL_COL_CONFIRM_RECEIVED + "]," + "[" + SQL_COL_DATE_CONFIRMED + "]," + "[" + SQL_COL_READ + "]," + "[" + SQL_COL_FILENAME + "]" + " FROM " + TABLE + ";";

        static string SQL_INSERT = " INSERT INTO " + TABLE + " (" + "[" + SQL_COL_ISSUE_ID + "]," + "[" + SQL_COL_ACCOUNT_ID + "]," + "[" + SQL_COL_DATE_CREATED + "]," + "[" + SQL_COL_DIRECTION + "]," + "[" + SQL_COL_DATE_PROCESSED + "]," + "[" + SQL_COL_REQUEST_CONFIRM + "]," + "[" + SQL_COL_CONFIRM_RECEIVED + "]," + "[" + SQL_COL_DATE_CONFIRMED + "]," + "[" + SQL_COL_READ + "]," + "[" + SQL_COL_FILENAME + "]" + ") VALUES (?, ?, #?#, ?, #?#, ?, ?, #?#, ?, '?');";

        static string SQL_UPDATE = " UPDATE " + TABLE + " SET " + "[" + SQL_COL_ISSUE_ID + "]=?," + "[" + SQL_COL_ACCOUNT_ID + "]=?," + "[" + SQL_COL_DATE_CREATED + "]=#?#," + "[" + SQL_COL_DIRECTION + "]=?," + "[" + SQL_COL_DATE_PROCESSED + "]=#?#," + "[" + SQL_COL_REQUEST_CONFIRM + "]=?," + "[" + SQL_COL_CONFIRM_RECEIVED + "]=?," + "[" + SQL_COL_DATE_CONFIRMED + "]=#?#," + "[" + SQL_COL_READ + "]=?," + "[" + SQL_COL_FILENAME + "]='?'" + " FROM " + TABLE + " WHERE " + SQL_COL_MESSAGE_ID + " = ?;";
        protected int ID = 0;
        protected int message_id;
        protected int issue_id;
        protected int account_id;
        protected DateTime date_created;
        protected int direction;
        protected DateTime date_processed;
        protected bool request_confirm;
        protected bool confirm_received;
        protected DateTime date_confirmed;
        protected bool _read;

        protected string filename;
        public Messages() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
            
        }

        public Messages(int message_id, int issue_id, Int32 account_id, DateTime date_created, Int32 direction, DateTime date_processed, bool request_confirm, bool confirm_received, DateTime date_confirmed, bool read, string filename) : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {
            this.message_id = message_id;
            this.issue_id = issue_id;
            this.account_id = account_id;
            this.date_created = date_created;
            this.direction = direction;
            this.date_processed = date_processed;
            this.request_confirm = request_confirm;
            this.confirm_received = confirm_received;
            this.date_confirmed = date_confirmed;
            this._read = read;
            this.filename = filename;

        }
        public Messages(string table, string priKeyName, string loadStmt, string loadAllStmt, string insertStmt, string updateStmt) : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.CN)
        {

        }


        protected Messages fill(DataRow dr)
        {

            try {
                ID = 1;
                this.message_id = (int)dr[SQL_COL_MESSAGE_ID];
                this.issue_id = (int)dr[SQL_COL_ISSUE_ID];
                this.account_id = (int)dr[SQL_COL_ACCOUNT_ID];
                this.date_created = (DateTime)dr[SQL_COL_DATE_CREATED];
                this.direction = (int)dr[SQL_COL_DIRECTION];
                this.date_processed = (DateTime)dr[SQL_COL_DATE_PROCESSED];
                this.request_confirm = (bool)dr[SQL_COL_REQUEST_CONFIRM];
                this.confirm_received = (bool)dr[SQL_COL_CONFIRM_RECEIVED];
                this.date_confirmed = (DateTime)dr[SQL_COL_DATE_CONFIRMED];
                this._read = (bool)dr[SQL_COL_READ];
                this.filename = (string)dr[SQL_COL_FILENAME];
            } catch (OleDbException ex) {
            }
            return this;

        }

        public static Messages load(int message_id)
        {

            Messages obj = new Messages();
            string[] opt = { message_id.ToString() };
            DataSet ds = new DataSet();
            ds = obj.load(SQL_LOAD, opt);
            DataRow dr = ds.Tables[0].Rows[0];

            return obj.fill(dr);

        }

        public static Messages[] load()
        {

            ArrayList list = new ArrayList();

            DataSet ds = new DataSet();
            Messages obj = new Messages();

            ds = obj.loadAll();
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows) {
                obj = new Messages();
                obj.fill(dr);
                list.Add(obj);
                obj = null;
            }
            try {
                return (Messages[])list.ToArray(typeof(Messages));
            } catch (OleDbException ex) {
                return null;
            }

        }

        public bool save()
        {

            if (ID > 0) {
                string[] values = {
                issue_id.ToString(),
                account_id.ToString(),
                date_created.ToString(),
                direction.ToString(),
                date_processed.ToString(),
                request_confirm.ToString(),
                confirm_received.ToString(),
                date_confirmed.ToString(),
                _read.ToString(),
                filename,
                message_id.ToString()
            };
                if (this.updateRecord(ID.ToString(), values)) {
                    return true;
                } else {
                    return false;
                }
            } else {
                string[] values = {
                issue_id.ToString(),
                account_id.ToString(),
                date_created.ToString(),
                direction.ToString(),
                date_processed.ToString(),
                request_confirm.ToString(),
                confirm_received.ToString(),
                date_confirmed.ToString(),
                _read.ToString(),
                filename
            };
                ID = this.insertRecord(values);
                if (ID >= 0) {
                    return true;
                }
            }
            return false;

        }

        // Get/Set Routines
        public Int32 getmessage_id()
        {
            return this.message_id;
        }

        public void setissue_id(Int32 value)
        {
            this.issue_id = value;
        }
        public Int32 getissue_id()
        {
            return this.issue_id;
        }

        public void setaccount_id(Int32 value)
        {
            this.account_id = value;
        }
        public Int32 getaccount_id()
        {
            return this.account_id;
        }

        public void setdate_created(DateTime value)
        {
            this.date_created = value;
        }
        public DateTime getdate_created()
        {
            return this.date_created;
        }

        public void setdirection(Int32 value)
        {
            this.direction = value;
        }
        public Int32 getdirection()
        {
            return this.direction;
        }

        public void setdate_processed(DateTime value)
        {
            this.date_processed = value;
        }
        public DateTime getdate_processed()
        {
            return this.date_processed;
        }

        public void setrequest_confirm(bool value)
        {
            this.request_confirm = value;
        }
        public bool getrequest_confirm()
        {
            return this.request_confirm;
        }

        public void setconfirm_received(bool value)
        {
            this.confirm_received = value;
        }
        public bool getconfirm_received()
        {
            return this.confirm_received;
        }

        public void setdate_confirmed(DateTime value)
        {
            this.date_confirmed = value;
        }
        public DateTime getdate_confirmed()
        {
            return this.date_confirmed;
        }

        public void setfilename(string value)
        {
            this.filename = value;
        }
        public string getfilename()
        {
            return this.filename;
        }

    }
}