using System;
using System.Collections.Generic;
using System.Linq;

namespace CTM.Win.UI.Common
{
    public partial class _dialogImportDataTemplate : BaseForm
    {
        #region Fields

        private string _templateFilePath = string.Empty;

        #endregion Fields

        #region Properties

        public string TemplateFilePath
        {
            get { return this._templateFilePath; }
            set { this._templateFilePath = value; }
        }

        #endregion Properties

        #region Constructors

        public _dialogImportDataTemplate()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Utilities

        private void DisplayTemplateExcelFile()
        {
            if (string.IsNullOrEmpty(_templateFilePath)) return;

            if (System.IO.File.Exists(_templateFilePath))
            {
                DevExpress.Spreadsheet.IWorkbook workbook = spreadsheetControl1.Document;
                workbook.LoadDocument(_templateFilePath);
            }
        }

        #endregion Utilities

        #region Events

        private void dialogImportDataTemplate_Load(object sender, EventArgs e)
        {
            DisplayTemplateExcelFile();
        }

        #endregion Events
    }
}