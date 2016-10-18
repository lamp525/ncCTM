using System.Collections.Generic;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;

namespace CTM.Win.Extensions
{
    public static class ControlExtensions
    {
        #region CheckedListBoxControl

        /// <summary>
        /// Initialize CheckedListBoxControl
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="checkedListBox"></param>
        /// <param name="source"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        public static void Initialize<T>(this CheckedListBoxControl checkedListBox, IList<T> source, string valueMember, string displayMember)
        {
            checkedListBox.DataSource = source;
            checkedListBox.DisplayMember = displayMember;
            checkedListBox.ValueMember = valueMember;
        }

        #endregion CheckedListBoxControl

        #region ComboBoxEdit

        /// <summary>
        ///   Initialize ComboBoxEdit
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="source"></param>
        /// <param name="displayAdditionalItem"></param>
        /// <param name="additionalItemText"></param>
        /// <param name="additionalItemValue"></param>
        public static void Initialize(this ComboBoxEdit comboBox, List<ComboBoxItemModel> source, bool displayAdditionalItem = false, string additionalItemText = " 全部 ", string additionalItemValue = "0")
        {
            var count = source.Count;

            if (displayAdditionalItem)
            {
                var allSelect = new ComboBoxItemModel
                {
                    Text = additionalItemText,
                    Value = additionalItemValue,
                };
                comboBox.Properties.Items.Add(allSelect);

                count += 1;
            }
            comboBox.Properties.Items.AddRange(source);
            comboBox.Properties.NullText = "请选择...";
            comboBox.Properties.DropDownRows = count;
            //comboBox.SelectedIndex = -1;
            comboBox.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
        }

        /// <summary>
        /// Set ComboBoxEdit default selected
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="source"></param>
        /// <param name="value"></param>
        public static void DefaultSelected(this ComboBoxEdit comboBox, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                comboBox.SelectedIndex = -1;
                return;
            }

            foreach (ComboBoxItemModel item in comboBox.Properties.Items)
            {
                if (item.Value == value)
                {
                    comboBox.SelectedItem = item;
                    return;
                }
            }

            comboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Get selected item's value
        /// </summary>
        /// <param name="comboBox"></param>
        /// <returns></returns>
        public static string SelectedValue(this ComboBoxEdit comboBox)
        {
            return comboBox.SelectedItem == null ? string.Empty : (comboBox.SelectedItem as ComboBoxItemModel).Value;
        }

        #endregion ComboBoxEdit

        #region GridControl/GridView

        /// <summary>
        /// Set GridView Layout
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="showGroupPanel"></param>
        /// <param name="showFilterPanel"></param>
        /// <param name="showFooter"></param>
        /// <param name="showAutoFilterRow"></param>
        /// <param name="columnAutoWidth"></param>
        /// <param name="editable"></param>
        /// <param name="readOnly"></param>
        /// <param name="setAlternateRowColor"></param>
        /// <param name="showCheckBoxRowSelect"></param>
        /// <param name="checkBoxSelectorColumnWidth"></param>
        /// <param name="rowIndicatorWidth"></param>
        public static void SetLayout(this GridView gridView,
            bool showGroupPanel = false,
            bool showFilterPanel = false,
            bool showFooter = false,
            bool showAutoFilterRow = true,
            bool columnAutoWidth = false,
            bool editable = false,
            bool readOnly = true,
            bool setAlternateRowColor = true,
            bool showCheckBoxRowSelect = true,
            int checkBoxSelectorColumnWidth = 30,
            int rowIndicatorWidth = 40
            )
        {
            gridView.OptionsBehavior.Editable = editable;

            gridView.OptionsBehavior.ReadOnly = readOnly;

            if (setAlternateRowColor)
            {
                gridView.OptionsView.EnableAppearanceEvenRow = true;
                gridView.OptionsView.EnableAppearanceOddRow = true;

                //gridView.Appearance.EvenRow.BackColor = Color.GreenYellow ;
                //gridView.Appearance.OddRow.BackColor = Color.WhiteSmoke;
                //gridView.Appearance.FocusedRow.BackColor = Color.DeepSkyBlue;
            }

            gridView.OptionsSelection.MultiSelect = true;
            gridView.OptionsSelection.UseIndicatorForSelection = false;

            if (showCheckBoxRowSelect)
            {
                if (checkBoxSelectorColumnWidth < 30)
                    gridView.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
                else
                    gridView.OptionsSelection.CheckBoxSelectorColumnWidth = checkBoxSelectorColumnWidth;

                gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            }
            else
            {
                gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            }

            gridView.OptionsView.ColumnAutoWidth = columnAutoWidth;

            gridView.OptionsView.ShowAutoFilterRow = showAutoFilterRow;

            gridView.OptionsView.ShowFooter = showFooter;

            gridView.OptionsView.ShowGroupPanel = showGroupPanel;

            if (showFilterPanel)
                gridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            else
                gridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;

            gridView.OptionsView.ShowIndicator = true;
            gridView.IndicatorWidth = rowIndicatorWidth;
        }

        /// <summary>
        /// Save Girdview Layout
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="layoutXmlName"></param>
        /// <param name="showMessage"></param>
        public static void SaveLayout(this GridView gridView, string layoutXmlName, bool showMessage = true)
        {
            var directoryPath = ".\\LayoutXml";

            if (!System.IO.Directory.Exists(directoryPath))
                System.IO.Directory.CreateDirectory(directoryPath);

            if (layoutXmlName.IndexOf(".xml") < 0)
                layoutXmlName = layoutXmlName + ".xml";

            var filePath = System.IO.Path.Combine(directoryPath, layoutXmlName);

            gridView.SaveLayoutToXml(filePath, OptionsLayoutBase.FullLayout);

            if (showMessage)
                DXMessage.ShowTips("样式保存成功 ！");
        }

        public static void LoadLayout(this GridView gridView, string layoutXmlName)
        {
            if (string.IsNullOrEmpty(layoutXmlName)) return;

            if (layoutXmlName.IndexOf(".xml") < 0)
                layoutXmlName = layoutXmlName + ".xml";

            var filePath = System.IO.Path.Combine(".\\LayoutXml", layoutXmlName);

            if (!System.IO.File.Exists(filePath)) return;

            gridView.RestoreLayoutFromXml(filePath, OptionsLayoutBase.FullLayout);
        }

        #endregion GridControl/GridView

        #region LookUpEdit

        /// <summary>
        /// Initialize LookUpEdit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lookUpEdit"></param>
        /// <param name="source"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        /// <param name="showHeader"></param>
        /// <param name="showFooter"></param>
        /// <param name="enableSearch"></param>
        public static void Initialize<T>(this LookUpEdit lookUpEdit, IList<T> source, string valueMember, string displayMember, bool showHeader = false, bool showFooter = false, bool enableSearch = false, int? searchColumnIndex = null)
        {
            lookUpEdit.Properties.BestFitMode = BestFitMode.None;
            lookUpEdit.Properties.ValueMember = valueMember;
            lookUpEdit.Properties.DisplayMember = displayMember;
            lookUpEdit.Properties.DataSource = source;
            lookUpEdit.Properties.DropDownRows = source.Count;
            lookUpEdit.Properties.ShowHeader = showHeader;
            lookUpEdit.Properties.ShowFooter = showFooter;
            lookUpEdit.Properties.NullText = "请选择...";
            lookUpEdit.ItemIndex = -1;

            if (enableSearch)
            {
                lookUpEdit.Properties.TextEditStyle = TextEditStyles.Standard;
                lookUpEdit.Properties.ImmediatePopup = true;
                lookUpEdit.Properties.SearchMode = SearchMode.AutoFilter;
                lookUpEdit.Properties.CaseSensitiveSearch = false;

                if (searchColumnIndex.HasValue)
                {
                    // lookUpEdit.Properties.SearchMode = SearchMode.OnlyInPopup;
                    lookUpEdit.Properties.AutoSearchColumnIndex = searchColumnIndex.Value;
                }
            }
        }

        /// <summary>
        /// Get LookUpEdit Selected Value
        /// </summary>
        /// <param name="lookUpEdit"></param>
        /// <returns></returns>
        public static string SelectedValue(this LookUpEdit lookUpEdit)
        {
            string value = null;

            if (lookUpEdit.EditValue != null && lookUpEdit.EditValue.ToString() != "nulltext")
            {
                value = lookUpEdit.EditValue.ToString();
            }

            return value;
        }

        #endregion LookUpEdit

        #region TextEdit

        /// <summary>
        /// Set TextEdit's percentage mask
        /// </summary>
        /// <param name="textEdit"></param>
        public static void SetPercentageMask(this TextEdit textEdit)
        {
            textEdit.Properties.DisplayFormat.FormatString = "#0.000%";
            textEdit.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            textEdit.Properties.Mask.EditMask = "#0.000%";
            textEdit.Properties.Mask.MaskType = MaskType.Numeric;
            textEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        /// <summary>
        /// Set TextEdit's Numeric mask
        /// </summary>
        /// <param name="textEdit"></param>
        public static void SetNumericMask(this TextEdit textEdit, int digits = 4)
        {
            var formatString = "#################0.0";

            if (digits > 1)
            {
                for (int i = 0; i < digits - 1; i++)
                {
                    formatString += "0";
                }
            }

            textEdit.Properties.DisplayFormat.FormatString = formatString;
            textEdit.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            textEdit.Properties.Mask.EditMask = formatString;
            textEdit.Properties.Mask.MaskType = MaskType.Numeric;
            textEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        /// <summary>
        /// Set TextEdit's Number mask
        /// </summary>
        /// <param name="textEdit"></param>
        /// <param name="length"></param>
        public static void SetNumberMask(this TextEdit textEdit , int length = 9 )
        {
            textEdit.Properties.MaxLength = length;
            textEdit.Properties.Mask.EditMask = "[0-9]*";
            textEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            textEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        #endregion TextEdit

        #region DateEdit

        /// <summary>
        /// 设置日期控件格式
        /// </summary>
        /// <param name=""></param>
        /// <param name="formatString"></param>
        public static void SetFormat(this DateEdit dateEdit, string formatString = "yyyy/MM/dd")
        {
            dateEdit.Properties.DisplayFormat.FormatString = formatString;
            dateEdit.Properties.DisplayFormat.FormatType = FormatType.DateTime;
            dateEdit.Properties.EditFormat.FormatString = formatString;
            dateEdit.Properties.EditFormat.FormatType = FormatType.DateTime;
            dateEdit.Properties.Mask.EditMask = formatString;
        }

        #endregion DateEdit

        #region TreeList

        /// <summary>
        ///  Initialize TreeList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="treeList"></param>
        /// <param name="source"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="parentFieldName"></param>
        /// <param name="expandAll"></param>
        public static void Initialize<T>(this TreeList treeList, IList<T> source, string keyFieldName, string parentFieldName, bool expandAll = true, bool editable = false, bool showCheckBox = false, bool multiSelect = false)
        {
            treeList.DataSource = source;
            treeList.KeyFieldName = keyFieldName;
            treeList.ParentFieldName = parentFieldName;

            if (expandAll)
                treeList.ExpandAll();

            treeList.OptionsBehavior.Editable = editable;

            treeList.OptionsView.ShowCheckBoxes = showCheckBox;

            treeList.OptionsSelection.MultiSelectMode = TreeListMultiSelectMode.RowSelect;
            treeList.OptionsSelection.MultiSelect = multiSelect;
            treeList.OptionsSelection.UseIndicatorForSelection = !showCheckBox;
        }

        /// <summary>
        ///Set TreeList Default Focused Node
        /// </summary>
        /// <param name="treeList"></param>
        /// <param name="keyId"></param>
        public static void SetDefaultFocusedNode(this TreeList treeList, int keyId)
        {
            var node = treeList.FindNodeByKeyID(keyId);
            treeList.SetFocusedNode(node);
        }

        #endregion TreeList
    }
}