#region using
using System;
using System.Diagnostics;
using System.Windows.Forms;
#endregion using

namespace Wagner.InsertStatementGenerator
{
    static class ErrorHandler
    {
        private const string msgSuffix = "\n\nSee the windows event log for details.";

        public static void ShowLoggedErrorMessage( string msg )
        {
            MessageBox.Show(
                msg + msgSuffix,
                Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error );
        }

        public static void LogException( string msg, Exception ex )
        {
            string logMessage = String.Empty;

            if( msg == null )
                msg = String.Empty;

            logMessage = ex.ToString();

            if( msg != String.Empty )
                logMessage = msg + "\n\n" + logMessage;

            EventLog.WriteEntry( Program.AppName, logMessage, EventLogEntryType.Error );
        }
    }
}
