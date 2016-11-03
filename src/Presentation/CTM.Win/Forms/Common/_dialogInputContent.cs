using System;
using CTM.Win.Util;

namespace CTM.Win.Forms.Common
{
    public partial class _dialogInputContent : BaseForm
    {
        #region Properties

        public string ContentTitle { get; set; }

        #endregion Properties

        #region Delegates

        public delegate void ReturnContentToParentForm(string content);

        public event ReturnContentToParentForm ReturnEvent;

        #endregion Delegates

        public _dialogInputContent()
        {
            InitializeComponent();
        }

        private void _dialogTextInput_Load(object sender, EventArgs e)
        {
            this.lciContent.Text = ContentTitle;
            this.memoContent.Text = string.Empty;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnOK.Enabled = false;

                var content = this.memoContent.Text.Trim();

                if (string.IsNullOrEmpty(content))
                {
                    DXMessage.ShowTips($"{ContentTitle}不能为空！");
                    this.memoContent.Focus();
                    return;
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                ReturnEvent?.Invoke(content);

                this.Close();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnOK.Enabled = true;
            }
        }
    }
}