using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CTM.Core.Util
{
    public static class NPOIHelper
    {
        public static void ExportToFile(DataSet dataSet, string fileFullPath)
        {
            List<DataTable> dts = new List<DataTable>();
            foreach (DataTable dt in dataSet.Tables) dts.Add(dt);
            ExportToFile(dts, fileFullPath);
        }

        public static void ExportToFile(DataTable dataTable, string fileFullPath)
        {
            List<DataTable> dts = new List<DataTable>();
            dts.Add(dataTable);
            ExportToFile(dts, fileFullPath);
        }

        public static void ExportToFile(IEnumerable<DataTable> dataTables, string fileFullPath)
        {
            IWorkbook workbook = new XSSFWorkbook();
            int i = 0;
            foreach (DataTable dt in dataTables)
            {
                string sheetName = string.IsNullOrEmpty(dt.TableName)
                    ? "Sheet " + (++i).ToString()
                    : dt.TableName;
                ISheet sheet = workbook.CreateSheet(sheetName);

                IRow headerRow = sheet.CreateRow(0);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string columnName = string.IsNullOrEmpty(dt.Columns[j].ColumnName)
                        ? "Column " + j.ToString()
                        : dt.Columns[j].ColumnName;
                    headerRow.CreateCell(j).SetCellValue(columnName);
                }

                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    DataRow dr = dt.Rows[a];
                    IRow row = sheet.CreateRow(a + 1);
                    for (int b = 0; b < dt.Columns.Count; b++)
                    {
                        row.CreateCell(b).SetCellValue(dr[b] != DBNull.Value ? dr[b].ToString() : string.Empty);
                    }
                }
            }

            using (FileStream fs = File.Create(fileFullPath))
            {
                workbook.Write(fs);
            }
        }

        public static List<DataTable> ImportAllSheetToDataTable(string xlsxFile)
        {
            if (!File.Exists(xlsxFile))
                throw new FileNotFoundException("文件不存在");

            List<DataTable> result = new List<DataTable>();
            Stream stream = new MemoryStream(File.ReadAllBytes(xlsxFile));
            IWorkbook workbook = new XSSFWorkbook(stream);
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                DataTable dt = new DataTable();
                ISheet sheet = workbook.GetSheetAt(i);

                //blank sheet or only one row
                if (sheet.PhysicalNumberOfRows < 2) continue;

                IRow headerRow = sheet.GetRow(sheet.FirstRowNum);

                int cellCount = headerRow.LastCellNum;
                for (int j = headerRow.FirstCellNum; j < cellCount; j++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(j).StringCellValue.Trim());
                    dt.Columns.Add(column);
                }

                for (int a = (sheet.FirstRowNum + 1); a < sheet.LastRowNum + 1; a++)
                {
                    IRow row = sheet.GetRow(a);
                    if (row == null) continue;

                    DataRow dr = dt.NewRow();
                    for (int b = row.FirstCellNum; b < cellCount; b++)
                    {
                        //if (row.GetCell(b) == null) continue;
                        //dr[b] = row.GetCell(b).ToString();
                        dr[b] = row.GetCellValue(b);
                    }

                    dt.Rows.Add(dr);
                }
                result.Add(dt);
            }
            stream.Close();

            return result;
        }

        public static DataTable ImportFirstSheetToDataTable(string excelFile)
        {
            if (!File.Exists(excelFile))
                throw new FileNotFoundException("文件不存在");

            DataTable result = new DataTable();
            try
            {
                IWorkbook workbook = null;

                Stream stream = new MemoryStream(File.ReadAllBytes(excelFile));

                var extension = Path.GetExtension(excelFile);
                if (extension == ".xls")
                    workbook = new HSSFWorkbook(stream);
                else if (extension == ".xlsx")
                    workbook = new XSSFWorkbook(stream);

                if (workbook == null || workbook.NumberOfSheets == 0) return result;

                ISheet sheet = workbook.GetSheetAt(0);

                //blank sheet or only one row
                if (sheet.PhysicalNumberOfRows < 2) return result;

                IRow headerRow = sheet.GetRow(sheet.FirstRowNum);

                int cellCount = headerRow.LastCellNum;
                for (int j = headerRow.FirstCellNum; j < cellCount; j++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(j).StringCellValue.Trim());
                    result.Columns.Add(column);
                }

                for (int a = (sheet.FirstRowNum + 1); a < sheet.LastRowNum + 1; a++)
                {
                    IRow row = sheet.GetRow(a);
                    if (row == null) continue;

                    DataRow dr = result.NewRow();
                    for (int b = row.FirstCellNum; b < cellCount; b++)
                    {
                        //if (row.GetCell(b) == null) continue;
                        //dr[b] = row.GetCell(b).ToString();
                        dr[b] = row.GetCellValue(b);
                    }

                    result.Rows.Add(dr);
                }

                stream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static object GetCellValue(this IRow row, int cellIndex)
        {
            object cellValue = null;

            ICell cell = row.GetCell(cellIndex);

            if (cell != null)
            {
                switch (cell.CellType)
                {
                    case CellType.Blank:
                        cellValue = string.Empty;
                        break;

                    case CellType.Boolean:
                        cellValue = cell.BooleanCellValue;
                        break;

                    case CellType.Numeric:
                        cellValue = cell.NumericCellValue.ToString();

                        break;

                    case CellType.String:
                        cellValue = cell.StringCellValue;
                        break;

                    case CellType.Formula:
                        cellValue = cell.StringCellValue;
                        break;

                    case CellType.Unknown:
                        cellValue = cell.StringCellValue;
                        break;

                    case CellType.Error:
                        cellValue = null;
                        break;
                }
            }

            return cellValue;
        }
    }
}