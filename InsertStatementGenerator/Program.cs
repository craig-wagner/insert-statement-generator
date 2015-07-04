#region using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
#endregion using

namespace Wagner.InsertStatementGenerator
{
    static class Program
    {
        public const string AppName = "Insert Statement Generator";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create the source, if it does not already exist.
            if( !EventLog.SourceExists( AppName ) )
            {
                EventLog.CreateEventSource( AppName, "Application" );
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new MainForm() );
        }
    }
}