using System.Threading;
using System.Windows.Forms;
using CTM.Win.UI.Common;

namespace CTM.Win.Util
{
    public class ProgressPanelHelper
    {
        private _dialogProgressPanel _progressDialog = null;

        private delegate bool DisplayHandle(bool flag);

        private bool _stopFlag = false;

        public bool StopFlag
        {
            get { return _stopFlag; }
            set { _stopFlag = value; }
        }

        public Thread CreateProgressPanelThread(string progressCaptionText = "请稍后", string progressDescriptionText = "数据加载中...")
        {
            Thread myThread = new Thread(() =>
            {
                DisplayHandle _displayProgressPanel = null;

                MethodInvoker mi = new MethodInvoker(() =>
                {
                    _progressDialog = new _dialogProgressPanel();
                    _displayProgressPanel = new DisplayHandle(_progressDialog.Process);

                    _progressDialog.ProgressCaptionText = progressCaptionText;
                    _progressDialog.ProgressDescriptionText = progressDescriptionText;
                    _progressDialog.ShowDialog();

                    _progressDialog = null;
                }
                );

                mi.BeginInvoke(null, null);

                while (_displayProgressPanel == null)
                {
                    Thread.Sleep(50);
                }

                do
                {
                    Thread.Sleep(50);
                    _displayProgressPanel.Invoke(_stopFlag);
                }
                while (!_stopFlag);
            }
            );

            myThread.IsBackground = true;

            return myThread;
        }
    }
}