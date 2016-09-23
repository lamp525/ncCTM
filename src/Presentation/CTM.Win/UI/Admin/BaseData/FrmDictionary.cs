using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Domain.Dictionary;
using CTM.Services.Dictionary;
using CTM.Win.Extensions;
using CTM.Win.Models;
using CTM.Win.Util;

namespace CTM.Win.UI.Admin.BaseData
{
    public partial class FrmDictionary : BaseForm
    {
        #region Fields

        private readonly IDictionaryService _dictionaryService;

        #endregion Fields

        #region Constructors

        public FrmDictionary(IDictionaryService dictionaryService)
        {
            InitializeComponent();

            this._dictionaryService = dictionaryService;
        }

        #endregion Constructors

        #region Utilities

        /// <summary>
        /// 绑定字典信息
        /// </summary>
        private void BindDictionaryInfo(int? typeId = null)
        {
            IList<DictionaryInfo> info = new List<DictionaryInfo>();

            if (typeId.HasValue)
                info = _dictionaryService.GetDictionaryInfoByTypeId(typeId.Value);
            else
                info = _dictionaryService.GetAllDictinaryInfo();

            List<DictionaryInfoModel> result = info.Select(x => new DictionaryInfoModel
            {
                Id = x.Id,
                TypeId = x.TypeId,
                TypeName = x.DictionaryType.Name,
                Code = x.Code,
                Name = x.Name,
                Remarks = x.Remarks
            }).ToList();

            this.gridControlInfos.DataSource = result;
        }

        private void ResetTypeInput()
        {
            this.txtTypeName.Text = string.Empty;
            this.txtTypeRemarks.Text = string.Empty;
        }

        private void ResetInfoInput()
        {
            this.txtInfoName.Text = string.Empty;
            this.txtInfoRemarks.Text = string.Empty;
        }

        #endregion Utilities

        #region Events

        private void FrmDictionary_Load(object sender, EventArgs e)
        {
            this.gridViewTypes.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false);
            this.gridViewInfos.SetLayout(showAutoFilterRow: false, showCheckBoxRowSelect: false);

            BindDictionary();

            BindDictionaryInfo();
        }

        private void BindDictionary()
        {
            var types = _dictionaryService.GetAllDictionaryTypes();

            this.gridControlTypes.DataSource = types;

            var source = types.Select((x) => new ComboBoxItemModel
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            this.cbType.Initialize(source);
        }

        /// <summary>
        /// 重置字典类型输入信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ResetTypeInput();
        }

        /// <summary>
        /// 添加字典类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.dxValidationProvider1.Validate()) return;

                this.simpleButton2.Enabled = false;

                var typeEntity = new DictionaryType
                {
                    Name = this.txtTypeName.Text.Trim(),
                    Remarks = this.txtTypeRemarks.Text.Trim(),
                };

                if (_dictionaryService.GetDictionaryTypeIdByName(typeEntity.Name) > 0)
                {
                    DXMessage.ShowTips("该类型已经存在！");
                    this.simpleButton2.Enabled = true;
                    return;
                }

                _dictionaryService.AddDictionaryType(typeEntity);

                ResetTypeInput();

                this.BindDictionary();
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.simpleButton2.Enabled = true;
            }
        }

        /// <summary>
        /// 重置字典信息输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ResetInfoInput();
        }

        /// <summary>
        /// 添加字典信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.dxValidationProvider2.Validate()) return;

                this.simpleButton4.Enabled = false;

                var infoEntity = new DictionaryInfo
                {
                    Name = this.txtInfoName.Text.Trim(),
                    TypeId = int.Parse((this.cbType.SelectedItem as ComboBoxItemModel).Value),
                    Remarks = txtInfoRemarks.Text.Trim()
                };

                if (_dictionaryService.IsExistedDictionaryInfo(infoEntity.TypeId, infoEntity.Name))
                {
                    DXMessage.ShowTips("该字典名称已经存在！");
                    this.simpleButton2.Enabled = true;
                    return;
                }

                _dictionaryService.AddDictionaryInfo(infoEntity);

                ResetInfoInput();

                BindDictionaryInfo(infoEntity.TypeId);
            }
            catch (Exception ex)
            {
                DXMessage.ShowError(ex.Message);
            }
            finally
            {
                this.simpleButton4.Enabled = true;
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var typeId = int.Parse(this.cbType.SelectedValue());

            BindDictionaryInfo(typeId);
        }

        private void gridViewTypes_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (gridViewTypes.SelectedRowsCount == 0) return;

            var typeId = gridViewTypes.GetRowCellValue(gridViewTypes.GetSelectedRows()[0], "Id").ToString();

            this.cbType.DefaultSelected(typeId);

            ComboBoxItemModel typeData = new ComboBoxItemModel
            {
                Text = gridViewTypes.GetRowCellValue(gridViewTypes.GetSelectedRows()[0], "Name").ToString(),
                Value = gridViewTypes.GetRowCellValue(gridViewTypes.GetSelectedRows()[0], "Id").ToString()
            };

            this.cbType.SelectedItem = typeData;

            BindDictionaryInfo(int.Parse(typeData.Value));
        }

        #endregion Events
    }
}