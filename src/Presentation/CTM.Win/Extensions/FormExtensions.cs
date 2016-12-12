using System.Windows.Forms;
using CTM.Core;
using CTM.Core.Infrastructure;

namespace CTM.Win.Extensions
{
    public static class FormExtensions
    {
        /// <summary>
        /// 激活已打开的窗体
        /// </summary>
        /// <param name="childFormName"></param>
        /// <param name="parentForm"></param>
        /// <param name="isChildMdiForm"></param>
        /// <returns></returns>
        public static bool ActiveOpenedForm(this Form parentForm, string childFormName, bool isChildMdiForm)
        {
            if (isChildMdiForm)
            {
                foreach (var child in parentForm.MdiChildren)
                {
                    if (child.Name == childFormName)
                    {
                        child.Activate();
                        return true;
                    }
                }
            }
            else
            {
                foreach (var child in parentForm.OwnedForms)
                {
                    if (child.Name == childFormName)
                    {
                        child.Activate();
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Display tabbed form
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentForm"></param>
        /// <param name="title"></param>
        /// <param name="pageMode"></param>
        public static void DisplayTabbedForm<T>(this Form parentForm, string title = "") where T : BaseForm
        {
            var type = typeof(T);

            if (parentForm.ActiveOpenedForm(type.Name, true)) return;

            var form = EngineContext.Current.Resolve<T>();
            form.Text = title;      
            form.MdiParent = parentForm;
            form.StartPosition = FormStartPosition.CenterParent;
            form.Show();
        }

        /// <summary>
        ///  Display normal form
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentForm"></param>
        /// <param name="title"></param>
        public static void DisplayForm<T>(this Form parentForm, string title = null) where T : BaseForm
        {
            var type = typeof(T);

            if (parentForm.ActiveOpenedForm(type.Name, false)) return;

            var form = EngineContext.Current.Resolve<T>();
            form.Owner = parentForm;
            form.Text = title;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }

        /// <summary>
        /// Create dialog
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentForm"></param>
        /// <param name="borderStyle"></param>
        /// <returns></returns>
        public static T CreateDialog<T>(this Form parentForm, FormBorderStyle borderStyle = FormBorderStyle.FixedToolWindow) where T : BaseForm
        {
            var dialog = EngineContext.Current.Resolve<T>();
            dialog.Owner = parentForm;
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.FormBorderStyle = borderStyle;

            return dialog;
        }
    }
}