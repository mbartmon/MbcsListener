using System.Data;
using System.Windows.Forms;
using MbcsCentral;
using System;

namespace MbcsListener
{
    public class clsDB
    {
        LogClass Log = LogClass.instance;

        protected const string DS_DATA_SET = "wintrm";
        protected string loadStmt;
        public string LoadStmt;
        public string loadAllStmt;
        public string insertStmt;
        public string updateStmt;
        public string localTable;
        public string priKeyName;
        public string connectionString;
        protected System.Data.SqlClient.SqlConnection localCN;
        /*
        public string LoadStmt { set { loadStmt = value; } }
        protected string loadAllStmt;
        public string LoadAllStmt { set { loadAllStmt = value; } }
        protected string insertStmt;
        public string InsertStmt { set { insertStmt = value; } }
        protected string updateStmt;
        public string UpdateStmt { set { updateStmt = value; } }
        protected string localTable;
        public string LocalTable { set { localTable = value; } }
        protected string priKeyName;
        public string PriKeyName { set { priKeyName = value; } }
        protected System.Data.SqlClient.SqlConnection localCN;
        // Deprecated?
        protected string connectionString;
        public string ConnectionString { set { connectionString = value; } }
        */
        protected string SQL_DELETE;

        public clsDB() { }

        public clsDB(string table, string priKeyName, string loadStmt, string loadAllStmt, string insertStmt, string updateStmt, System.Data.SqlClient.SqlConnection cn)
        {
            SQL_DELETE = "DELETE FROM " + table + " WHERE " + priKeyName + " = ?;";


            try
            {
                this.localTable = table;
                this.priKeyName = priKeyName;
                this.loadStmt = loadStmt;
                this.loadAllStmt = loadAllStmt;
                this.insertStmt = insertStmt;
                this.updateStmt = updateStmt;
                //this.localCN = cn;
                this.connectionString = cn.ConnectionString;
                SQL_DELETE = "DELETE FROM " + localTable + " WHERE [" + priKeyName + "] = ?;";
            }
            catch (System.Exception)
            {
                return;
            }
        }

        protected static string prepareStatement(string sql, string[] options)
        {
            string s = "";
            int i;
            int n = 0;

            if (sql == null || sql.Length == 0)
                return s;

            if (options == null || (options.Length - 1) < 0)
            {
                s = sql;
            }
            else
            {
                for (i = 1; i <= sql.Length; i++)
                {
                    if (sql.Substring(i - 1, 1) == "?")
                    {
                        if (n <= (options.Length - 1))
                        {
                            if (!(options[n] == null))
                            {
                                if (options[n].ToLower().Equals("true"))
                                {
                                    options[n] = "1";
                                }
                                else if (options[n].ToLower().Equals("false"))
                                {
                                    options[n] = "0";
                                }
                            }
                            s += options[n];
                            n++;
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        s += sql.Substring(i - 1, 1);
                    }
                }
            }
            return s;

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        protected DataSet loadByPrimaryKey(string key)
        {

            string[] options = new string[2];
            options[0] = key;
            try
            {
                //localCN.Open();
                //localCN.ConnectionString = connectionString;
                string stmt = prepareStatement(loadStmt, options);
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(stmt, GlobalShared.CN.ConnectionString);
                DataSet ds = new DataSet(DS_DATA_SET);
                da.Fill(ds, DS_DATA_SET);
                ColumnCount = ds.Tables[0].Columns.Count;
                return ds;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("Error while retrieving record from " + localTable + ": " + ex.Message);
                return null;
            }
            catch (System.Exception ex)
            {
                ErrorMessage = ex.Message;
                Log.Log((int)LogClass.logType.ErrorCondition, "Error while retrieveing record from " + localTable + ": " + ex.Message);
                return null;
            }
            finally
            {
                try
                {
                    if (localCN.State == ConnectionState.Open)
                        localCN.Close();
                }
                catch
                {
                }
            }

        }

        protected DataSet loadAll(bool debug = false)
        {

            /* if (localCN.State != ConnectionState.Open)
            {
                try
                {
                    localCN.Open();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    Log.Log((int)LogClass.logType.ErrorCondition, "Opening database: <" + localCN.ConnectionString + ">" + "\r\n" + "   " + loadAllStmt + "\r\n" + "   " + ex.Message, false, MessageBoxButtons.OK);
                    return null;
                }
                catch (System.Exception ex)
                {
                    Log.Log((int)LogClass.logType.ErrorCondition, "Opening database: <" + localCN.ConnectionString + ">" + "\r\n" + "   " + loadAllStmt + "\r\n" + "   " + ex.Message);
                    return null;
                }
            }   */
            try
            {
                if (debug)
                {
                    GlobalShared.Log.debug = true;
                    GlobalShared.Log.Log((int)LogClass.logType.Debug, "clsDB.loadAll [ConnectionString= " + GlobalShared.CN.ConnectionString + "]", false);
                    GlobalShared.Log.debug = false;
                }
                if (debug)
                {
                    GlobalShared.Log.debug = true;
                    GlobalShared.Log.Log((int)LogClass.logType.Debug, "clsDB.loadAll [" + loadAllStmt + "]", false);
                    GlobalShared.Log.debug = false;
                }
                //localCN.ConnectionString = connectionString;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(loadAllStmt, GlobalShared.CN.ConnectionString);
                DataSet ds = new DataSet(DS_DATA_SET);
                da.Fill(ds, DS_DATA_SET);
                ColumnCount = ds.Tables[0].Columns.Count;
                if (debug)
                {
                    GlobalShared.Log.debug = true;
                    GlobalShared.Log.Log((int)LogClass.logType.Debug, "clsDB.loadAll [ColumnCount= " + ColumnCount.ToString() + "]", false);
                    GlobalShared.Log.debug = false;
                }
                return ds;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ErrorMessage = ex.Message;
                Log.Log((int)LogClass.logType.ErrorCondition, "Error while retrieving records from " + localTable + ": " + ex.Message, false, MessageBoxButtons.OK);
                return null;
            }
            catch (System.Exception ex)
            {
                ErrorMessage = ex.Message;
                Log.Log((int)LogClass.logType.ErrorCondition, "Error while retrieving records from " + localTable + ": " + ex.Message, false, MessageBoxButtons.OK);
            }
            return null;

        }

        protected DataSet load(string stmt, string[] options, bool LogZero = false)
        {

            if (options != null)
            {
                if (options.Length > 0 && options[0] != null && options[0].Trim() != "")
                {
                    stmt = prepareStatement(stmt, options);
                }
            }
            //MessageBoxCustom.Show("Query = " + stmt);

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(stmt, GlobalShared.CN.ConnectionString);
            DataSet ds = new DataSet(DS_DATA_SET);
            try
            {
                da.Fill(ds, (localTable == null || localTable.Trim().Equals("") ? "TABLE" : localTable));
                ColumnCount = ds.Tables[0].Columns.Count;
                if (LogZero && (ds == null || ds.Tables[0].Rows.Count == 0))
                {
                    GlobalShared.Log.Log((int)LogClass.logType.Warning, "No records returned [clsDb.load(stmt, options)]: columns=" + ColumnCount.ToString() + ", " + stmt, false);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ErrorMessage = ex.Message;
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "[clsDb.load(stmt, options)]: " + ex.Message + System.Environment.NewLine + "   Stmt: " + stmt, true);
                return null;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "[clsDb.load(stmt, options)]: " + ex.Message + System.Environment.NewLine + "   Stmt: " + stmt, true);
                return null;
            }

            return ds;

        }

        protected bool updateRecord(string key, string[] values, string tableName = "", bool debug = false)
        {
            try
            {
                try
                {
                    localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.CN.ConnectionString);
                    localCN.Open();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show("Error while openning DB connection: " + ex.Message);
                    return false;
                }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(this.updateStmt, values);
                if (!tableName.Trim().Equals("") && debug)
                    GlobalShared.Log.Log((int)LogClass.logType.Info, tableName + ": update <" + cmd.CommandText + ">", false);
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ErrorMessage = ex.Message;
                //MessageBox.Show("Error while updating record: " + ex.Message);
                return false;
            }
            finally
            {
                try
                {
                    localCN.Close();
                }
                catch
                {
                }
            }
            return true;
        }

        protected bool updateRecordSpecial(string key, string[] values, string updateStatement, string tableName = "", bool debug = false)
        {
            try
            {
                try
                {
                    localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.CN.ConnectionString);
                    localCN.Open();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show("Error while openning DB connection: " + ex.Message);
                    return false;
                }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(updateStatement, values);
                int rc = cmd.ExecuteNonQuery();
                if (!tableName.Trim().Equals("") && debug)
                {
                    GlobalShared.Log.Log((int)LogClass.logType.Info, tableName + ": update <" + cmd.CommandText + ">", false);
                    GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, rc.ToString() + " rows updated.", false);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("Error while updating record: " + ex.Message);
                return false;
            }
            finally
            {
                try
                {
                    localCN.Close();
                }
                catch
                {
                }
            }
            return true;
        }
        protected int insertRecord(string[] values)
        {
            int id = -1;
            try
            {
                try
                {
                    localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.CN.ConnectionString);
                    localCN.Open();
                }
                catch (System.Exception ex)
                {

                }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(this.insertStmt, values);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@Identity";
                try
                {
                    id = System.Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (InvalidCastException ex)
                { }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.ToLower().IndexOf("duplicate") + 1 > 0)
                {
                    IsDupe = true;
                    ErrorMessage = ex.Message;
                    //MessageBox.Show("Attempt to add a duplicate record.");
                }
                else
                {
                    MessageBox.Show("Error while inserting record into " + localTable + ": " + ex.Message);
                }
                return id;
            }
            finally
            {
                try
                {
                    localCN.Close();
                }
                catch
                {
                }
            }
            return id;
        }

        public bool delete(string key)
        {

            try
            {
                localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.CN.ConnectionString);
                localCN.Open();
                string[] values = new string[] { key };
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(SQL_DELETE, values);
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("Error while deleting record: " + ex.Message);
                return false;
            }
            finally
            {
                try
                {
                    localCN.Close();
                }
                catch
                {
                }
            }
            return true;
        }

        protected bool query(string stmt, string[] options)
        {

            try
            {
                localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.CN.ConnectionString);
                localCN.Open();
            }
            catch (System.Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to open DB connection <" + localCN.ConnectionString + "> for query <" + stmt + ">: " + ex.Message, true);
                return false;
            }
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(stmt, options);
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ErrorMessage = ex.Message;
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Executing Query <" + stmt + ">: " + ex.Message, true);
                return false;
            }
            finally
            {
                try
                {
                    localCN.Close();
                }
                catch { }
            }

            return true;
        }

        public long queryWithCount(string stmt, string[] options)
        {
            long count = -1;

            try
            {
                localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.CN.ConnectionString);
                localCN.Open();
            }
            catch (System.Exception ex)
            {
                ErrorMessage = ex.Message;
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to open DB connection <" + localCN.ConnectionString + "> for query <" + stmt + ">: " + ex.Message, true);
                return -1;
            }
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = localCN;
                cmd.CommandText = prepareStatement(stmt, options);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@ROWCOUNT";
                count = System.Convert.ToInt64(cmd.ExecuteScalar());

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ErrorMessage = ex.Message;
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Executing Query <" + stmt + ">: " + ex.Message, true);
                return -1;
            }
            finally
            {
                try
                {
                    localCN.Close();
                }
                catch { }
            }

            return count;

        }

        public static DataSet Query(string stmt, System.Data.SqlClient.SqlConnection localCN)
        {

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(stmt, GlobalShared.CN.ConnectionString);
            DataSet ds = new DataSet(DS_DATA_SET);
            try
            {
                da.Fill(ds, DS_DATA_SET);
                try
                {
                    localCN.Close();
                }
                catch
                {
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return null;
            }
            return ds;

        }

        public System.Data.SqlClient.SqlConnection LocalCN
        {
            get { return localCN; }
            set
            {
                localCN = value;
                try
                {
                    this.connectionString = LocalCN.ConnectionString;
                }
                catch { }
            }
        }

        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        public System.Data.DataRow GetColumn(string name)
        {
            DataTable table = GetTableSchema();
            foreach (DataRow dr in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    if (dr[col].ToString().ToLower().Equals(name.ToLower()))
                    {
                        return dr;
                    }
                }
            }
            return null;
        }
        public DataTable GetTableSchema()
        {
            System.Data.SqlClient.SqlCommand cmd;
            System.Data.SqlClient.SqlDataReader reader;
            DataTable schema;
            string query;

            try
            {
                localCN = new System.Data.SqlClient.SqlConnection(GlobalShared.CN.ConnectionString);
                localCN.Open();
            }
            catch (System.Exception ex)
            {
                GlobalShared.Log.Log((int)LogClass.logType.ErrorCondition, "Unable to open DB connection <" + localCN.ConnectionString + "> for <GetTableSchema>: " + ex.Message, true);
                return null;
            }
            query = String.Format(System.Globalization.CultureInfo.InvariantCulture, "SELECT * FROM {0}", this.localTable);
            cmd = new System.Data.SqlClient.SqlCommand(GlobalShared.CN.ConnectionString);
            cmd.CommandText = query;
            cmd.Connection = localCN;
            reader = cmd.ExecuteReader();

            schema = reader.GetSchemaTable();

            foreach (System.Data.DataRow row in schema.Rows)
            {
                foreach (System.Data.DataColumn col in schema.Columns)
                {
                    Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                }
                Console.WriteLine("============================");
            }

            try { localCN.Close(); } catch { }
            return schema;
        }
        public bool IsDupe { get; set; } = false;
        public int ColumnCount { get; set; }
        public string ErrorMessage { get; set; } = "";

    }
}