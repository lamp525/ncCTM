using System;
using System.Diagnostics;
using System.Windows.Forms;
using CTM.Core.Infrastructure;
using CTM.Core.Util;
using CTM.Win.Forms;
using CTM.Win.Util;
using DevExpress.Skins;
using DevExpress.UserSkins;

namespace CTM.Win
{
    internal static class Startup
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                //Prevent Repetition Start
                var currentProcess = Process.GetCurrentProcess();
                ProcessHelper.RepetitionStartManage(currentProcess);

                BonusSkins.Register();
                SkinManager.EnableFormSkins();
                SkinManager.EnableMdiFormSkins();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //Initialize engine context
                EngineContext.Initialize(false);

                var loginForm = EngineContext.Current.Resolve<_dialogLogin>();

                loginForm.StartPosition = FormStartPosition.CenterScreen;
                loginForm.FormBorderStyle = FormBorderStyle.FixedSingle;
                loginForm.MinimizeBox = false;
                loginForm.MaximizeBox = false;
                loginForm.ShowDialog();

                if (loginForm.DialogResult == DialogResult.OK)
                {
                    var mainForm = EngineContext.Current.Resolve<FrmMain>();
                    Application.Run(mainForm);
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }
    }
}