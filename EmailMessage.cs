using Chilkat;
using System;
using System.Windows.Forms;
using MbcsUtils;

namespace MbcsListener
{
    [Serializable()]
    public class EmailMessage
    {

        protected string _UID;
        protected string _toAddr;
        protected string _fromAddr;
        protected string _bcc;
        protected string _subject;
        protected DateTime _receiveDate;
        protected string _filename;
        protected string _message;

        protected Chilkat.Email _emailMessage;

        public EmailMessage()
        {
        }


        public EmailMessage(string toAddr, string fromAddr, string bcc, string subject, DateTime receiveDate, string filename, string uid = "")
        {
            _toAddr = toAddr;
            _fromAddr = fromAddr;
            _bcc = bcc;
            _subject = subject;
            _receiveDate = receiveDate;
            _filename = filename;
            _UID = uid;

        }


        public EmailMessage(string uid, Chilkat.Email msg)
        {
            _toAddr = msg.GetTo(0);
            _fromAddr = msg.From.ToString();
            _bcc = "";

            for (int i = 0; i <= msg.NumCC - 1; i++)
            {
                _bcc += msg.GetCC(i) + " ";
            }
            _subject = msg.Subject;
            _receiveDate = msg.EmailDate;
            _filename = null;
            _message = msg.GetPlainTextBody();
            _UID = msg.Uidl;
            _emailMessage = msg;

        }


        public void fetchMessage(SmtpConfig smtpAcct)
        {
            string Username;
            string Password;
            string Host;
            int Port;
            string Email;
            bool SSL;
            bool TSL;
            long authenticationType;


            Chilkat.MailMan mailman = new Chilkat.MailMan();
            bool success = mailman.UnlockComponent("BARTMNMAILQ_3K24X6hk7C2J");
            if (!success)
            {
                MessageBox.Show("Could not initialize email component");
                return;
            }

            if (smtpAcct != null)
            {
                Username = smtpAcct.in_userName;
                Password = smtpAcct.in_password;
                Host = smtpAcct.in_server;
                Port = smtpAcct.in_port;
                Email = smtpAcct.emailAddress;
                SSL = smtpAcct.in_SSL;
                TSL = smtpAcct.in_TSL;
                authenticationType = smtpAcct.in_authentication;
            }
            else
            {
                GlobalShared.Log.Log((int)LogClass.logType.Warning, "No POP configuration found", false);
                return;
            }

            mailman.MailHost = Host;
            //TCP port for connection
            mailman.MailPort = (ushort)Port;
            //Username to login to the POP3 server
            mailman.PopUsername = Username;
            //Password to login to the POP3 server
            mailman.PopPassword = Password;
            // SSL Interaction type
            if (TSL)
            {
                mailman.StartTLS = true;
            }
            else if (SSL)
            {
                mailman.PopSsl = true;
            }
            try
            {
                _emailMessage = mailman.FetchEmail(_UID);
                _message = _emailMessage.GetPlainTextBody();
            }
            catch (System.Exception ex)
            {
            }
            finally
            {
                mailman = null;
            }

        }

        public static int sort(EmailMessage x, EmailMessage y)
        {
            return y.receiveDate.CompareTo(x.receiveDate);
        }

        public string toAddr
        {
            get { return _toAddr; }
            set { _toAddr = value; }
        }
        public string fromAddr
        {
            get { return _fromAddr; }
            set { _fromAddr = value; }
        }
        public string bcc
        {
            get { return _bcc; }
            set { _bcc = value; }
        }
        public string subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
        public DateTime receiveDate
        {
            get { return _receiveDate; }
            set { _receiveDate = value; }
        }
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        public string filename
        {
            get { return _filename; }
            set { _filename = value; }
        }
        public string UID
        {
            get { return _UID; }
            set { _UID = value; }
        }

        public Chilkat.Email emailMessage
        {
            get { return _emailMessage; }
            set { _emailMessage = value; }
        }
    }
}