namespace CTM.Win
{
    public class BaseForm : DevExpress.XtraEditors.XtraForm
    {
        public BaseForm()
        {
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
        }
    }
}