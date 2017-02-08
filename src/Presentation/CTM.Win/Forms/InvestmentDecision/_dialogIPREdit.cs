using System;
using System.Collections.Generic;
using CTM.Services.Common;
using CTM.Services.InvestmentDecision;
using CTM.Win.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

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
            this.bandedGridView1.SetLayout();
            this.bandedGridView1.SetColumnHeaderAppearance();

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
            this.riImageComboBoxOperateScheme= imageComboBoxOperateScheme.Properties;

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
        }

        #endregion Utilities

        #region Events

        private void _dialogIPREdit_Load(object sender, EventArgs e)
        {
            try
            {
                FormInit();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
        #endregion Events


    }
}