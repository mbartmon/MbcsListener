using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Email.Net.Common;
using System.IO;
namespace MbcsListener
{
	static class UtilFunctions
	{
		public static string findIssueTag(Rfc822Message msg)
		{

			string tag = "";
			bool found = false;

			if (GlobalShared.regx.IsMatch(msg.Subject)) {
				found = true;
				return parseIssueTag(msg.Subject);
			}
			if (msg.Header.ExtraHeaders.Count > 0) {
				foreach (System.Collections.Generic.KeyValuePair<string, string> hdr in msg.Header.ExtraHeaders) {
					if (hdr.Key.Equals(GlobalShared.ISSUE_HEADER)) {
						found = true;
						tag = hdr.Value;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
			}
			return tag;

		}

		public static string parseIssueTag(string s)
		{

			System.Text.RegularExpressions.Match part = GlobalShared.regx.Match(s);
            return part.ToString();

		}


		public static void parseMailToken(string token, ref string[] values)
		{
			string s = null;
			try {
				s = token.Substring(token.LastIndexOf("<") + 1);
				s = s.Substring(0, s.IndexOf(">") - 1);
				values = s.Split('.');
			} catch (System.Exception ex) {
				return;
			}

		}
		public static string saveEmail(Rfc822Message msg, long issueId, long acctId, string uid)
		{

			string filename = "";
			if ((GlobalShared.mailInDir != null) & !string.IsNullOrEmpty(GlobalShared.mailInDir.Trim())) {
				filename = string.Format(GlobalShared.popFilename, GlobalShared.mailInDir, acctId, issueId, DateTime.Now, uid, "txt");
				System.IO.StreamWriter sw = new System.IO.StreamWriter(filename, false);
				sw.Write(messageFormat(msg));
				sw.Flush();
				sw.Close();
			}
			return filename;

		}

		public static string messageFormat(Rfc822Message msg)
		{

			string outString = "";
			outString = "From: " + msg.From.Address + Microsoft.VisualBasic.Constants.vbCrLf;
			outString += "To:      ";
			foreach (Email.Net.Common.EmailAddress addr in msg.To) {
				outString += addr.Address + ", \"";
			}
			outString += Microsoft.VisualBasic.Constants.vbCrLf;
			bool cc = false;
			foreach (Email.Net.Common.EmailAddress addr in msg.CarbonCopies) {
				if (!cc) {
					outString += "Bcc:   ";
					cc = true;
				}
				outString += addr.Address + ", ";
			}
			foreach (Email.Net.Common.EmailAddress addr in msg.BlindedCarbonCopies) {
				if (!cc) {
					outString += "Bcc:   ";
					cc = true;
				}
				outString += addr.Address + ", ";
			}
			outString += "Date:    " + msg.Date + Microsoft.VisualBasic.Constants.vbCrLf;
			outString += "Subject: " + msg.Subject + Microsoft.VisualBasic.Constants.vbCrLf;
			outString += "Message: " + Microsoft.VisualBasic.Constants.vbCrLf + msg.Text + Microsoft.VisualBasic.Constants.vbCrLf;
			return outString;

		}

		public static bool searchUID(string uid)
		{

			bool found = false;
			if (!string.IsNullOrEmpty(GlobalShared.mailInDir.Trim())) {
				string[] fnames = null;
				try {
					fnames = Directory.GetFiles(GlobalShared.mailInDir, "*_*_*_" + uid + ".*");
				} catch (IOException ioEx) {
				} catch (System.Exception ex) {
				}
				if ((fnames != null)) {
					foreach (string fname in fnames) {
						if ((fname != null)) {
							if (!string.IsNullOrEmpty(fname.Trim())) {
								found = true;
							}
						}
					}
				}
			}
			return found;

		}
		public static string wrapRecipIds(int[] ids)
		{
			if (ids.Length <= 0) {
				return null;
			}

			string s = "";
			for (int i = 0; i <= ids.Length - 1; i++) {
				if (i > 0) {
					s += ",";
				}

                s += ids[i].ToString();
			}

			return s;
		}

		public static int[] unwrapRecipIds(string s)
		{
			if (s == null || s.Length == 0) {
				return null;
			}

			char[] d = null;
			d = new char[1];
			d[0] = ',';
			string[] idsStr = s.Split(d);

			int[] ids = new int[idsStr.Length];
			try {
				for (int i = 0; i <= idsStr.Length - 1; i++) {
					ids[i] = int.Parse(idsStr[i]);
				}
			} catch {
			}

			return ids;
		}

		public static string getConfig(Dictionary<string, string> d, string key, bool yesno)
		{

			string st = null;
			d.TryGetValue(key, out st);
			if ((st != null)) {
				if (!string.IsNullOrEmpty(st.Trim())) {
					if (yesno) {
						return (st.ToLower().Equals("true") ? "true" : "false");
					} else {
						return st;
					}
				}
			}
			return null;

		}
	}
}
