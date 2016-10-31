using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CTM.Core.Domain.InvestmentDecision;
using CTM.Core.Domain.User;
using CTM.Services.InvestmentDecision;
using CTM.Services.User;
using CTM.Win.Extensions;
using CTM.Win.Util;

namespace CTM.Win.UI.InvestmentDecision
{
    public partial class FrmIDCommittee : BaseForm
    {
        #region Fields

        private readonly IInvestmentDecisionService _IDService;
        private readonly IUserService _userService;

        #endregion Fields

        #region Constructors

        public FrmIDCommittee(IInvestmentDecisionService IDService, IUserService userService)
        {
            InitializeComponent();

            this._IDService = IDService;
            this._userService = userService;
        }

        #endregion Constructors

        #region Utilities

        private void BindCommittee()
        {
            var committees = _IDService.GetIDCommittees();

            this.gridControl1.DataSource = committees;
        }

        private void FormInit()
        {
            this.btnRemove.Enabled = false;
            this.btnAdd.Enabled = false;

            this.gridView1.SetLayout();
        }

        private void BindInvestor()
        {
            var users = _userService.GetAllUsers();

            var committees = this.gridView1.DataSource as List<InvestmentDecisionCommittee>;
            if (committees.Any())
            {
                var committeeCodes = committees.Select(x => x.Code).Distinct().ToArray();

                users = users.Where(x => !committeeCodes.Contains(x.Code)).ToList();
            }
            this.luInvestor.Initialize(users, "Code", "Name", enableSearch: true);
        }

        private void RefreshForm()
        {
            this.luInvestor.EditValue = null;

            BindCommittee();

            BindInvestor();
        }

        #endregion Utilities

        #region Events

        private void FrmIDCommittee_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();

                BindCommittee();

                BindInvestor();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void luInvestor_EditValueChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = !string.IsNullOrEmpty(this.luInvestor.SelectedValue());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;

                var investor = this.luInvestor.GetSelectedDataRow() as UserInfo;

                if (investor == null) return;

                _IDService.AddIDCommittee(investor.Code, investor.Name);

                RefreshForm();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
                this.btnAdd.Enabled = true;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnRemove.Enabled = false;

                var myView = this.gridView1;

                var selectedHandles = myView.GetSelectedRows();

                if (selectedHandles.Length == 0) return;

                selectedHandles = myView.GetSelectedRows().Where(x => x > -1).ToArray();

                if (DXMessage.ShowYesNoAndWarning("确定删除选择的成员吗？") == DialogResult.Yes)
                {
                    var ids = new List<int>();

                    for (var rowhandle = 0; rowhandle < selectedHandles.Length; rowhandle++)
                    {
                        ids.Add(int.Parse(myView.GetRowCellValue(selectedHandles[rowhandle], colId).ToString()));
                    }

                    this._IDService.DeleteIDCommittee(ids.ToArray());

                    RefreshForm();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
                this.btnRemove.Enabled = true;
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var myView = this.gridView1;
            var selectedHandles = myView.GetSelectedRows();
            if (selectedHandles.Any())
                selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

            if (selectedHandles.Length > 0)
                this.btnRemove.Enabled = true;
            else
                this.btnRemove.Enabled = false;
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion Events
    }
}