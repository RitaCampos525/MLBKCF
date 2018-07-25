using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace MultilinhasDataLayer
{
    public static class WriteLog
    {

        public static void Log(TraceLevel loglevel, string type, Exception ex, string user, string host)
        {
            Log(loglevel, type, ex.Message , user, host);
            if (ex.StackTrace != null)
            {
                Log(loglevel, type, ex.StackTrace, user, host);
            }
            if (ex.InnerException != null) {
                Log(loglevel, type, ex.InnerException, user, host);
            }

        }

        public static void Log(TraceLevel loglevel, string type, string message, string user, string host)
        {
            OdbcConnection con = new OdbcConnection(ConfigurationManager.ConnectionStrings["MASTERDB2LOCAL"].ConnectionString);

            try
            {
                string query = string.Format("INSERT INTO AB_WEB_LOGS (LOGLEVEL, APPLICATIONNAME, LOGTYPENAME, THREAD, USERNAME, MACHINE, MESSAGE, AUDITTIMESTAMP) "

                   + " VALUES('{0}','{1}','{2}','{3}','{4}','{5}', '{6}' , CURRENT TIMESTAMP)",
                  loglevel.ToString(),
                  "Multilinhas",
                  type,
                  HttpContext.Current.Session.SessionID,
                  user, host, message);

                OdbcDataAdapter ad = new OdbcDataAdapter();
                con.Open();
                ad.InsertCommand = new OdbcCommand(query, con);
                ad.InsertCommand.ExecuteNonQuery();
                con.Dispose();
            }
            catch
            {
                //throw new Exception(ex.Message);
            }
        }

        internal static string SerializeToString<T>(this T value)
        {
            var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(value.GetType());
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, value, emptyNamepsaces);
                return stream.ToString().Replace("\r\n", "").Trim(); ;
            }
        }
    }
}
