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
    public partial class _dialogIPRResult : BaseForm
    {
        #region Fields

        private readonly ICommonService _commonService;
        private readonly IInvestmentDecisionService _IDService;

        private RepositoryItemImageComboBox riImageComboBoxTradeType;
        private RepositoryItemImageComboBox riImageComboBoxTrendType;
        private RepositoryItemImageComboBox riImageComboBoxOperateScheme;
        private RepositoryItemImageComboBox riImageComboBoxOperateMode;



        private bool _isExpanded = true;

        #region Properties

        public string SerialNo { get; set; }

        public DateTime AnalysisDate { get; set; }

        #endregion Properties

        #endregion Fields

        #region Constructors

        public _dialogIPRResult(ICommonService commonService, IInvestmentDecisionService IDService)
        {
            InitializeComponent();

            this._commonService = commonService;
            this._IDService = IDService;
        }

        #endregion Constructors

        #region Utilities

        private void FormInit()
        {
            this.esiTitle.Text = $@"个股投资计划记录 -{SerialNo} -分析日期：{AnalysisDate.ToShortDateString()}";

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
                    Description ="高开高走",
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
                    Description ="短差",
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

            this.bandedGridView1.SetLayout(showAutoFilterRow: false, showGroupPanel: true, columnPanelRowHeight: -1,setAlternateRowColor:false);
            this.bandedGridView1.SetColumnHeaderAppearance();

            foreach (GridColumn column in this.bandedGridView1.Columns)
            {
                if (column.Name == this.colStockCode.Name || column.Name == this.colStockName.Name)
                    column.OptionsColumn.AllowEdit = false;
                else
                    column.OptionsColumn.AllowEdit = true;
            }
        }

        private void BindIPR()
        {
            this.gridControl1.DataSource = null;

            string commandText = $@"SELECT * FROM [dbo].[v_IPRDetail] WHERE  SerialNo ='{SerialNo}' ORDER BY StockName, InvestorName,  Probability DESC, CreateTime";
            DataSet ds = SqlHelper.ExecuteDataset(AppConfig._ConnString, CommandType.Text, commandText);
            if (ds == null || ds.Tables.Count == 0) return;
            this.gridControl1.DataSource = ds.Tables[0];

            this.btnExpandOrCollapse.Text = _isExpanded ? " 全部收起 " : " 全部展开 ";

            if (_isExpanded)
                this.bandedGridView1.ExpandAllGroups();
            else
                this.bandedGridView1.CollapseAllGroups();
        }

        #endregion Utilities

        #region Events

        private void _dialogIPRResult_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
                BindIPR();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
        }

        private void btnExpandOrCollapse_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnExpandOrCollapse.Enabled = false;
                this._isExpanded = !_isExpanded;

                if (_isExpanded)
                    this.bandedGridView1.ExpandAllGroups();
                else
                    this.bandedGridView1.CollapseAllGroups();

                this.btnExpandOrCollapse.Text = _isExpanded ? " 全部收起 " : " 全部展开 ";
            }
            finally
            {
                this.btnExpandOrCollapse.Enabled = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnRefresh.Enabled = false;
                BindIPR();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.btnRefresh.Enabled = true;
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
            if (e.Column.Name == this.colTrend.Name)
            {
                ImageComboBoxEdit icb = new ImageComboBoxEdit();
                icb.Properties.Items.AddRange(this.riImageComboBoxTrendType.Items);
                e.RepositoryItem = icb.Properties;

                foreach (ImageComboBoxItem item in icb.Properties.Items)
                {
                    if (e.CellValue != null && e.CellValue.ToString() == item.Value.ToString())
                    {
                        icb.SelectedItem = item;
                        return;
                    }
                }
            }
            //操作方案
            else if (e.Column.Name == this.colScheme.Name)
            {
                ImageComboBoxEdit icb = new ImageComboBoxEdit();
                icb.Properties.Items.AddRange(this.riImageComboBoxOperateScheme.Items);
                e.RepositoryItem = icb.Properties;

                foreach (ImageComboBoxItem item in icb.Properties.Items)
                {
                    if (e.CellValue != null && e.CellValue.ToString() == item.Value.ToString())
                    {
                        icb.SelectedItem = item;
                        return;
                    }
                }
            }
            //交易类别
            else if (e.Column.Name == this.colTradeType.Name)
            {
                ImageComboBoxEdit icb = new ImageComboBoxEdit();
                icb.Properties.Items.AddRange(this.riImageComboBoxTradeType.Items);
                e.RepositoryItem = icb.Properties;

                foreach (ImageComboBoxItem item in icb.Properties.Items)
                {
                    if (e.CellValue != null && e.CellValue.ToString() == item.Value.ToString())
                    {
                        icb.SelectedItem = item;
                        return;
                    }
                }
            }
            //操作方式
            else if (e.Column.Name == this.colOperateMode.Name)
            {
                ImageComboBoxEdit icb = new ImageComboBoxEdit();
                icb.Properties.Items.AddRange(this.riImageComboBoxOperateMode.Items);
                e.RepositoryItem = icb.Properties;

                foreach (ImageComboBoxItem item in icb.Properties.Items)
                {
                    if (e.CellValue != null && e.CellValue.ToString() == item.Value.ToString())
                    {
                        icb.SelectedItem = item;
                        return;
                    }
                }
            }
        }

        #endregion Events
    }
}