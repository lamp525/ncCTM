using System.Collections.Generic;
using CTM.Core.Domain.InvestmentDecision;
using CTM.Win.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;

namespace CTM.Win.CustomControl
{
    public class TreeListComboBox
    {
        public static PopupContainerControl CreatePopupIDReasonCategoryTree(IList<DecisionReasonCategory> categories, int width = 300, int height = 300)
        {
            TreeList tl = new TreeList();
            TreeListColumn tcId = new TreeListColumn();
            tcId.FieldName = nameof(DecisionReasonCategory.Id);
            tcId.Visible = false;
            tl.Columns.Add(tcId);

            TreeListColumn tcParentId = new TreeListColumn();
            tcParentId.FieldName = nameof(DecisionReasonCategory.ParentId);
            tcId.Visible = false;
            tl.Columns.Add(tcParentId);

            TreeListColumn tcName = new TreeListColumn();
            tcName.FieldName = nameof(DecisionReasonCategory.Name);
            tcName.Visible = true;
            tl.Columns.Add(tcName);

            tl.Initialize(categories, tcId.FieldName, tcParentId.FieldName, expandAll: false, autoWidth: true, showColumns: false, showVertLines: false, showHorzLines: false);

            PopupContainerControl pcc = new PopupContainerControl();
            pcc.Controls.Add(tl);
            pcc.Size = new System.Drawing.Size(width, height);

            tl.Dock = System.Windows.Forms.DockStyle.Fill;

            return pcc;
        }
    }
}