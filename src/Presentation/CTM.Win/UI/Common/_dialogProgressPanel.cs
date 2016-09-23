using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CTM.Core.Util;

namespace CTM.Win.UI.Common
{
    public partial class _dialogProgressPanel : Form
    {
        private delegate bool CloseFormCallBack(bool stopFlag);

        private string _progressPanelSkinName = AppConfigHelper.DefaultSkinName;

        private bool _closeFlag = false;

        public int DialogPositionX { get; set; }

        public int DialogPositionY { get; set; }

        public string ProgressCaptionText { get; set; }

        public string ProgressDescriptionText { get; set; }

        public _dialogProgressPanel()
        {
            InitializeComponent();
        }

        public bool Process(bool stopFlag)
        {
            if (this.InvokeRequired)
            {
                CloseFormCallBack d = new CloseFormCallBack(Process);
                this.Invoke(d, new object[] { stopFlag });
            }
            else
            {
                if (stopFlag)
                {
                    this._closeFlag = true;
                    this.Close();
                }
            }

            return stopFlag;
        }

        private void _dialogProgressPanel_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            // this.Location = new System.Drawing.Point(DialogPositionX, DialogPositionY);

            this.progressPanel1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.progressPanel1.LookAndFeel.SkinName = _progressPanelSkinName;
            this.progressPanel1.Caption = this.ProgressCaptionText;
            this.progressPanel1.Description = this.ProgressDescriptionText;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (_closeFlag)
                this.Close();
        }
    }
}