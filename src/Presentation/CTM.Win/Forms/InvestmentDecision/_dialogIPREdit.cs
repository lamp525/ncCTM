using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CTM.Data;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;

namespace CTM.Win.Forms.InvestmentDecision
{
    public partial class _dialogIPREdit : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;

        private RepositoryItemImageComboBox riImageComboBoxTradeType;
        private RepositoryItemImageComboBox riImageComboBoxTrendType;
        private RepositoryItemImageComboBox riImageComboBoxOperateScheme;
        private RepositoryItemImageComboBox riImageComboBoxOperateMode;

        private string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["CTMContext"].ToString();

        #region Properties

        public string SerialNo { get; set; }

        public DateTime AnalysisDate { get; set; }

        #endregion Properties

        #endregion Fields

        #region Constructors

        public _dialogIPREdit(ICommonService commonService, IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._commonService = commonService;
            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.esiTitle.Text = $@"股票池个股分析记录 - 分析日期：{AnalysisDate.ToShortDateString()}  分析人员：{LoginInfo.CurrentUser.UserName}";

            //走势预判
            var trendTypes = new List<ImageComboBoxItem>
            {
                 new ImageComboBoxItem
                {
                    Description ="低开低走",
                    Value ="1",
                },
                new ImageComboBoxItem
                {
                    Description ="低开低走回升",
                    Value ="2",
                },
                       new ImageComboBoxItem
                {
                    Description ="低开高走",
                    Value ="3",
                },
                        new ImageComboBoxItem
                {
                    Description ="低开高走回落",
                    Value ="4",
                },
                new ImageComboBoxItem
                {
                    Description ="冲高回落",
                    Value ="5",
                },
                new ImageComboBoxItem
                {
                    Description ="冲高回落回升",
                    Value ="6",
                },
                new ImageComboBoxItem
                {
                    Description =" 高开高走",
                    Value ="7",
                },
                new ImageComboBoxItem
                {
                    Description ="高开高走回落",
                    Value ="8",
                },
                new ImageComboBoxItem
                {
                    Description ="跌停",
                    Value ="9",
                },
                new ImageComboBoxItem
                {
                    Description ="涨停",
                    Value ="10",
                },
            };

            var imageComboBoxTrendType = new ImageComboBoxEdit();
            imageComboBoxTrendType.Initialize(trendTypes, displayAdditionalItem: false);
            this.riImageComboBoxTrendType = imageComboBoxTrendType.Properties;

            //操作方案
            var operateSchemes = new List<ImageComboBoxItem>
            {
                new ImageComboBoxItem
                {
                    Description ="低止损",
                     Value ="1",
                },
                new ImageComboBoxItem
                {
                    Description ="塔仓买入",
                     Value ="2",
                },
                new ImageComboBoxItem
                {
                    Description ="日内短差",
                     Value ="3",
                },
            };
            var imageComboBoxOperateScheme = new ImageComboBoxEdit();
            imageComboBoxOperateScheme.Initialize(operateSchemes, displayAdditionalItem: false);
            this.riImageComboBoxOperateScheme = imageComboBoxOperateScheme.Properties;

            //交易类别
            var tradeTypes = new List<ImageComboBoxItem>
            {
                new ImageComboBoxItem
                {
                    Description ="目标",
                    Value ="1",
                },
                new ImageComboBoxItem
                {
                    Description ="波段",
                    Value ="2",
                },
                       new ImageComboBoxItem
                {
                    Description ="隔日短差",
                    Value ="3",
                },
            };

            var imageComboBoxTradeType = new ImageComboBoxEdit();
            imageComboBoxTradeType.Initialize(tradeTypes, displayAdditionalItem: false);
            this.riImageComboBoxTradeType = imageComboBoxTradeType.Properties;

            //操作方式
            var operateModes = new List<ImageComboBoxItem>
            {
                new ImageComboBoxItem
                {
                    Description ="保留",
                    Value ="1",
                },
                 new ImageComboBoxItem
                {
                    Description ="买入",
                    Value ="2",
                },
                  new ImageComboBoxItem
                {
                    Description ="卖出",
                    Value ="3",
                },
            };
            var imageComboBoxOperateMode = new ImageComboBoxEdit();
            imageComboBoxOperateMode.Initialize(operateModes, displayAdditionalItem: false);
            this.riImageComboBoxOperateMode = imageComboBoxOperateMode.Properties;

            //取得股票池股票信息
            var stockPool = _IDService.GetIDStockPool().OrderBy(x => x.StockCode).ToList();
            var stocks = stockPool.Select(x => new StockInfoModel
            {
                FullCode = x.StockCode,
                Name = x.StockName,
                DisplayMember = x.StockCode + " - " + x.StockName,
            }
            ).ToList();
            this.luStock.Initialize(stocks, "FullCode", "DisplayMember", enableSearch: true, searchColumnIndex: 0);

            this.btnDelete.Enabled = false;
            this.btnAdd.Enabled = false;
            this.bandedGridView1.SetLayout(editable: true, readOnly: false, showAutoFilterRow: false, multiSelect: true, showCheckBoxRowSelect: true);
            this.bandedGridView1.SetColumnHeaderAppearance();

            foreach (GridColumn column in this.bandedGridView1.VisibleColumns)
            {
                if (column.Name == this.colStockCode.Name || column.Name == this.colStockName.Name)
                    column.OptionsColumn.AllowEdit = false;
                else
                    column.OptionsColumn.AllowEdit = true;
            }
        }

        private void BindIPR()
        {
            string commandText = $@"SELECT * FROM [dbo].[v_IPRDetail] WHERE InvestorCode = '{LoginInfo.CurrentUser.UserCode }' AND AnalysisDate = '{AnalysisDate}'";
            DataSet ds = SqlHelper.ExecuteDataset(_connString, CommandType.Text, commandText);
            if (ds == null || ds.Tables.Count == 0) return;
            this.gridControl1.DataSource = ds.Tables[0];
        }

        #endregion Utilities

        #region Events

        private void _dialogIPREdit_Load(object sender, EventArgs e)
        {
            try
            {
                SerialNo = "IPR000001";
                AnalysisDate = DateTime.Now.Date;

                FormInit();
                BindIPR();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void luStock_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(luStock.SelectedValue()))
                this.btnAdd.Enabled = false;
            else
                this.btnAdd.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnDelete.Enabled = false;

                var selectedHandles = this.bandedGridView1.GetSelectedRows();

                if (selectedHandles.Any())
                    selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

                if (DXMessage.ShowYesNoAndWarning("确定删除选择的分析记录吗？") == System.Windows.Forms.DialogResult.Yes)
                {
                    var ids = new List<int>();

                    for (var rowhandle = 0; rowhandle < selectedHandles.Length; rowhandle++)
                    {
                        ids.Add(int.Parse(this.bandedGridView1.GetRowCellValue(selectedHandles[rowhandle], colId).ToString()));
                    }

                    _IDService.DeleteInvestmentPlanRecord(ids);

                    BindIPR();
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnDelete.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAdd.Enabled = false;

                var stockInfo = this.luStock.GetSelectedDataRow() as StockInfoModel;
                if (stockInfo == null) return;

                _IDService.AddInvestmentPlanRecord(SerialNo, stockInfo.FullCode, stockInfo.Name, LoginInfo.CurrentUser.UserCode, AnalysisDate);
                BindIPR();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnAdd.Enabled = true;
            }
        }

        private void bandedGridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void bandedGridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle < 0) return;

            //走势预判
            if (e.Column == this.colTrend)
            {
                ImageComboBoxEdit icb = new ImageComboBoxEdit();
                icb.Properties.Items.AddRange(this.riImageComboBoxTrendType.Items);
                e.RepositoryItem = icb.Properties;

                foreach (ImageComboBoxItem item in icb.Properties.Items)
                {
                    if (e.CellValue == item.Value)
                    {
                        icb.SelectedItem = item;
                        return;
                    }
                }
            }
            //操作方案
            else if (e.Column == this.colScheme)
            {
                ImageComboBoxEdit icb = new ImageComboBoxEdit();
                icb.Properties.Items.AddRange(this.riImageComboBoxOperateScheme.Items);
                e.RepositoryItem = icb.Properties;

                foreach (ImageComboBoxItem item in icb.Properties.Items)
                {
                    if (e.CellValue == item.Value)
                    {
                        icb.SelectedItem = item;
                        return;
                    }
                }
            }
            //交易类别
            else if (e.Column == this.colTradeType)
            {
                ImageComboBoxEdit icb = new ImageComboBoxEdit();
                icb.Properties.Items.AddRange(this.riImageComboBoxTradeType.Items);
                e.RepositoryItem = icb.Properties;

                foreach (ImageComboBoxItem item in icb.Properties.Items)
                {
                    if (e.CellValue == item.Value)
                    {
                        icb.SelectedItem = item;
                        return;
                    }
                }
            }
            //操作方式
            else if (e.Column == this.colOperateMode)
            {
                ImageComboBoxEdit icb = new ImageComboBoxEdit();
                icb.Properties.Items.AddRange(this.riImageComboBoxOperateMode.Items);
                e.RepositoryItem = icb.Properties;

                foreach (ImageComboBoxItem item in icb.Properties.Items)
                {
                    if (e.CellValue == item.Value)
                    {
                        icb.SelectedItem = item;
                        return;
                    }
                }
            }
        }

        private void bandedGridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                var selectedHandles = this.bandedGridView1.GetSelectedRows();
                if (selectedHandles.Any())
                    selectedHandles = selectedHandles.Where(x => x > -1).ToArray();

                if (selectedHandles.Length == 0)
                {
                    this.btnDelete.Enabled = false;
                }
                else
                {
                    this.btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void bandedGridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
        }

        #endregion Events

   
    }
}