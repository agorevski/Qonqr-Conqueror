using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Web.Script.Serialization;
using System.Windows.Forms;


namespace Qonqr
{  
    
    public class EntryPoint
    {
       
        public static int Main(string[] args)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += currentDomain_UnhandledException;

            ConquererForm conquererForm = new ConquererForm();

            Application.Run(conquererForm);

            return 0;
        }

        #region Event Handlers

        static void currentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        #endregion
    }
}
