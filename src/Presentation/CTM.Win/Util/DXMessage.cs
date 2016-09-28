using System.Windows.Forms;

namespace CTM.Win.Util
{
    /// <summary>
    /// DevExpress Message Box
    /// </summary>
    public class DXMessage
    {
        public static DialogResult ShowTips(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowYesNoAndTips(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        public static DialogResult ShowYesNoCancelAndTips(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "提示信息", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
        }

        public static DialogResult ShowWarning(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowYesNoAndWarning(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "警告信息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowError(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowYesNoAndError(string message)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(message, "错误信息", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }
    }
}