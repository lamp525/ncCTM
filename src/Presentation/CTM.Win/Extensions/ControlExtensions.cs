using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CTM.Core;
using CTM.Win.Models;
using CTM.Win.Util;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Handler;

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
        public static void Initialize(this ComboBoxEdit comboBox, IList<string> source, bool displayAdditionalItem = false, string additionalItemText = "全部")
        {
            var count = source.Count;

            if (displayAdditionalItem)
            {
                
                comboBox.Properties.Items.Add(additionalItemText);

                count += 1;
            }
            comboBox.Properties.Items.AddRange(source.ToList());
            comboBox.Properties.NullText = "请选择...";
            comboBox.Properties.DropDownRows = count;
            comboBox.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
        }

        /// <summary>
        ///   Initialize ComboBoxEdit
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="source"></param>
        /// <param name="displayAdditionalItem"></param>
        /// <param name="additionalItemText"></param>
        /// <param name="additionalItemValue"></param>
        public static void Initialize(this ComboBoxEdit comboBox, IList<ComboBoxItemModel> source, bool displayAdditionalItem = false, string additionalItemText = " 全部 ", string additionalItemValue = "0")
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
            comboBox.Properties.Items.AddRange(source.ToList());
            comboBox.Properties.NullText = "请选择...";
            comboBox.Properties.DropDownRows = count;
            comboBox.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
        }

        /// <summary>
        ///   Initialize RepositoryItemImageComboBox
        /// </summary>
        /// <param name="imageComboBox"></param>
        /// <param name="source"></param>
        /// <param name="displayAdditionalItem"></param>
        /// <param name="additionalItemText"></param>
        /// <param name="additionalItemValue"></param>
        public static void Initialize(this ImageComboBoxEdit imageComboBox, IList<ImageComboBoxItem> source, bool displayAdditionalItem = false, string additionalItemText = " 全部 ", string additionalItemValue = "0")
        {
            var count = source.Count;

            if (displayAdditionalItem)
            {
                var allSelect = new ImageComboBoxItem
                {
                    Description = additionalItemText,
                    ImageIndex = 0,
                    Value = additionalItemValue,
                };

                imageComboBox.Properties.Items.Add(allSelect);
                count += 1;
            }
            imageComboBox.Properties.Items.AddRange(source.ToList ());
            imageComboBox.Properties.NullText = "请选择...";
            imageComboBox.Properties.DropDownRows = count;
            imageComboBox.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
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
        /// Draw Row indicator
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="e"></param>
        public static void DrawRowIndicator(this GridView gv, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// Expand a master row recursively
        /// </summary>
        /// <param name="masterView"></param>
        /// <param name="masterRowHandle"></param>
        public static void RecursExpand(this GridView masterView, int masterRowHandle)
        {
            // Prevent excessive visual updates.
            masterView.BeginUpdate();
            try
            {
                // Get the number of master-detail relationships.
                int relationCount = masterView.GetRelationCount(masterRowHandle);
                // Iterate through relationships.
                for (int index = relationCount - 1; index >= 0; index--)
                {
                    // Open the detail View for the current relationship.
                    masterView.ExpandMasterRow(masterRowHandle, index);
                    // Get the detail View.
                    GridView childView = masterView.GetDetailView(masterRowHandle, index) as GridView;
                    if (childView != null)
                    {
                        // Get the number of rows in the detail View.
                        int childRowCount = childView.DataRowCount;
                        // Expand child rows recursively.
                        for (int handle = 0; handle < childRowCount; handle++)
                            RecursExpand(childView, handle);
                    }
                }
            }
            finally
            {
                // Enable visual updates.
                masterView.EndUpdate();
            }
        }

        /// <summary>
        /// Expand / Collapse all rows of the view
        /// </summary>
        /// <param name="gv"></param>
        public static void SetAllRowsExpanded(this GridView gv, bool expand)
        {
            gv.BeginUpdate();
            try
            {
                int dataRowCount = gv.DataRowCount;
                for (int rHandle = 0; rHandle < dataRowCount; rHandle++)
                    gv.SetMasterRowExpanded(rHandle, expand);
            }
            finally
            {
                gv.EndUpdate();
            }
        }

        /// <summary>
        /// Set ColumnHeader Appearance
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="hAlignment"></param>
        /// <param name="vAligment"></param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontStyle"></param>
        public static void SetColumnHeaderAppearance(
            this GridView gv
            , HorzAlignment hAlignment = HorzAlignment.Center
            , VertAlignment vAligment = VertAlignment.Center
            , string fontName = "Tahoma"
            , float fontSize = 9F
            , System.Drawing.FontStyle fontStyle = System.Drawing.FontStyle.Bold)
        {
            foreach (GridColumn column in gv.Columns)
            {
                column.AppearanceHeader.TextOptions.HAlignment = hAlignment;
                column.AppearanceHeader.TextOptions.VAlignment = vAligment;
                column.AppearanceHeader.Font = new System.Drawing.Font(fontName, fontSize, fontStyle);
            }
        }

        /// <summary>
        /// Set GridView Layout
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="allowCellMerge"></param>
        /// <param name="showGroupPanel"></param>
        /// <param name="showFilterPanel"></param>
        /// <param name="showFooter"></param>
        /// <param name="showAutoFilterRow"></param>
        /// <param name="columnAutoWidth"></param>
        /// <param name="editable"></param>
        /// <param name="editorShowMode"></param>
        /// <param name="readOnly"></param>
        /// <param name="setAlternateRowColor"></param>
        /// <param name="multiSelect"></param>
        /// <param name="showCheckBoxRowSelect"></param>
        /// <param name="checkBoxSelectorColumnWidth"></param>
        /// <param name="rowIndicatorWidth"></param>
        public static void SetLayout(
            this GridView gv,
            bool allowCellMerge = false,
            bool showGroupPanel = false,
            bool showFilterPanel = false,
            bool showFooter = false,
            bool showAutoFilterRow = true,
            bool columnAutoWidth = false,
            bool editable = false,
            EditorShowMode editorShowMode = EditorShowMode.MouseDown,
            bool readOnly = true,
            bool setAlternateRowColor = true,
            bool multiSelect = true,
            bool showCheckBoxRowSelect = true,
            int checkBoxSelectorColumnWidth = 30,
            int rowIndicatorWidth = 40,
            int columnPanelRowHeight = 30)
        {
            gv.ColumnPanelRowHeight = columnPanelRowHeight;

            gv.OptionsBehavior.Editable = editable;
            gv.OptionsBehavior.EditorShowMode = editorShowMode;
            gv.OptionsBehavior.ReadOnly = readOnly;

            if (setAlternateRowColor)
            {
                gv.OptionsView.EnableAppearanceEvenRow = true;
                gv.OptionsView.EnableAppearanceOddRow = true;

                //gridView.Appearance.EvenRow.BackColor = Color.GreenYellow ;
                //gridView.Appearance.OddRow.BackColor = Color.WhiteSmoke;
                //gridView.Appearance.FocusedRow.BackColor = Color.DeepSkyBlue;
            }

            gv.OptionsSelection.MultiSelect = multiSelect;
            gv.OptionsSelection.UseIndicatorForSelection = true;

            if (showCheckBoxRowSelect)
            {
                if (checkBoxSelectorColumnWidth < 30)
                    gv.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
                else
                    gv.OptionsSelection.CheckBoxSelectorColumnWidth = checkBoxSelectorColumnWidth;

                gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            }
            else
            {
                gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            }

            gv.OptionsView.AllowCellMerge = allowCellMerge;
            gv.OptionsView.ColumnAutoWidth = columnAutoWidth;

            gv.OptionsView.ShowAutoFilterRow = showAutoFilterRow;

            gv.OptionsView.ShowFooter = showFooter;

            gv.OptionsView.ShowGroupPanel = showGroupPanel;

            if (showGroupPanel)
            {
                gv.OptionsView.ShowGroupedColumns = true;
            }

            if (showFilterPanel)
                gv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            else
                gv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;

            gv.OptionsView.ShowIndicator = true;
            gv.IndicatorWidth = rowIndicatorWidth;
        }

        /// <summary>
        /// Save Girdview Layout
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="layoutXmlName"></param>
        /// <param name="showMessage"></param>
        public static void SaveLayout(this GridView gv, string layoutXmlName, bool showMessage = true)
        {
            var directoryPath = ".\\LayoutXml";

            if (!System.IO.Directory.Exists(directoryPath))
                System.IO.Directory.CreateDirectory(directoryPath);

            if (layoutXmlName.IndexOf(".xml") < 0)
                layoutXmlName = layoutXmlName + ".xml";

            var filePath = System.IO.Path.Combine(directoryPath, layoutXmlName);

            gv.SaveLayoutToXml(filePath, OptionsLayoutBase.FullLayout);

            if (showMessage)
                DXMessage.ShowTips("样式保存成功 ！");
        }

        public static void LoadLayout(this GridView gv, string layoutXmlName)
        {
            if (string.IsNullOrEmpty(layoutXmlName)) return;

            if (layoutXmlName.IndexOf(".xml") < 0)
                layoutXmlName = layoutXmlName + ".xml";

            var filePath = System.IO.Path.Combine(".\\LayoutXml", layoutXmlName);

            if (!System.IO.File.Exists(filePath)) return;

            gv.RestoreLayoutFromXml(filePath, OptionsLayoutBase.FullLayout);
        }

        public static void ExportToExcelAndOpen(this GridView gv, string fileName)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "请选择文件存放路径";
            saveFile.Filter = "Excel文档(*.xlsx)|*.xlsx";
            saveFile.FileName = fileName;
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                options.SheetName = fileName;
                gv.OptionsPrint.AutoWidth = false;
                gv.OptionsPrint.AllowCancelPrintExport = false;
                gv.AppearancePrint.Row.Font = new System.Drawing.Font("宋体", 9);
                try
                {
                    gv.ExportToXlsx(saveFile.FileName, options);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (DXMessage.ShowYesNoAndTips("导出成功,是否打开文件？") == DialogResult.Yes)
                    System.Diagnostics.Process.Start(saveFile.FileName);
            }
        }

        #endregion GridControl/GridView

        #region Column

        /// <summary>
        /// Set GridColumn's display format to Boolean format
        /// </summary>
        /// <param name="column"></param>
        /// <param name="trueString"></param>
        /// <param name="falseString"></param>
        public static void SetDisplayFormatToBoolean(this GridColumn column, string trueString, string falseString)
        {
            column.ColumnEdit = column.View.GridControl.RepositoryItems.Add("TextEdit");
            column.DisplayFormat.FormatType = FormatType.Custom;
            column.DisplayFormat.Format = new BooleanFormatter(trueString, falseString);
        }

        #endregion Column

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
            lookUpEdit.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
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

        #region SpinEdit

        /// <summary>
        /// Set SpinEdit Properties
        /// </summary>
        /// <param name="spinEdit"></param>
        /// <param name="isFloatValue"></param>
        /// <param name="increment"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="initValue"></param>
        public static void SetProperties(this SpinEdit spinEdit, bool isFloatValue = false, decimal increment = 1, decimal minValue = 1, decimal maxValue = 99, decimal initValue = 1)
        {
            spinEdit.Properties.IsFloatValue = false;
            spinEdit.Properties.Increment = 1;
            spinEdit.Properties.MinValue = 1;
            spinEdit.Properties.MaxValue = 99;
            spinEdit.Properties.Mask.MaskType = MaskType.RegEx;
            spinEdit.Properties.Mask.EditMask = @"\d{1,2}?";
            spinEdit.Value = 1;
        }

        #endregion SpinEdit

        #region TreeListLookUpEdit

        /// <summary>
        /// Get TreeListLookUpEdit Selected Value
        /// </summary>
        /// <param name="tlLookUpEdit"></param>
        /// <returns></returns>
        public static string SelectedValue(this TreeListLookUpEdit tlLookUpEdit)
        {
            string value = null;

            if (tlLookUpEdit.EditValue != null && tlLookUpEdit.EditValue.ToString() != "nullText")
            {
                value = tlLookUpEdit.EditValue.ToString();
            }

            return value;
        }

        #endregion TreeListLookUpEdit

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
        public static void SetNumberMask(this TextEdit textEdit, int length = 9)
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
        public static void Initialize<T>
            (
            this TreeList treeList, IList<T> source,
            string keyFieldName,
            string parentFieldName,
            bool expandAll = true,
            bool editable = false,
            bool showCheckBox = false,
            bool multiSelect = false,
            bool autoWidth = false,
            bool showColumns = true,
            bool showVertLines = true,
            bool showHorzLines = true
            )
        {
            treeList.DataSource = source;
            treeList.KeyFieldName = keyFieldName;
            treeList.ParentFieldName = parentFieldName;

            if (expandAll)
                treeList.ExpandAll();

            treeList.OptionsBehavior.Editable = editable;
            treeList.OptionsBehavior.ReadOnly = !editable;

            treeList.OptionsView.AutoWidth = autoWidth;
            treeList.OptionsView.ShowCheckBoxes = showCheckBox;
            treeList.OptionsView.ShowColumns = showColumns;
            treeList.OptionsView.ShowVertLines = showVertLines;
            treeList.OptionsView.ShowHorzLines = showHorzLines;

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

        /// <summary>
        /// Get TreeList DragInsertPosition
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static DragInsertPosition GetDragInsertPosition(this TreeList tree)
        {
            PropertyInfo pi = typeof(TreeList).GetProperty("Handler", BindingFlags.Instance | BindingFlags.NonPublic);
            TreeListHandler handler = (TreeListHandler)pi.GetValue(tree, null);
            FieldInfo fi2 = typeof(TreeListHandler).GetField("fStateData", BindingFlags.Instance | BindingFlags.NonPublic);
            StateData stateData = (StateData)fi2.GetValue(handler);
            FieldInfo fi = typeof(DragScrollInfo).GetField("dragInsertPosition", BindingFlags.Instance | BindingFlags.NonPublic);
            return (DragInsertPosition)fi.GetValue(stateData.DragInfo);
        }

        #endregion TreeList
    }
}