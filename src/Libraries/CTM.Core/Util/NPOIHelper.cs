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

        public static List<DataTable> ImportAllSheetToDataTable(string excelFile)
        {
            List<DataTable> result = new List<DataTable>();
            string importExcelFile = string.Empty;
            string extension = string.Empty;
            Stream stream = null;

            try
            {
                if (!File.Exists(excelFile))
                    throw new FileNotFoundException("文件不存在!");
                else
                {
                    extension = Path.GetExtension(excelFile);
                    importExcelFile = Path.Combine(Path.GetDirectoryName(excelFile), "B667F371C4C3BD7A14346BF2F7307E31" + extension);
                    File.Copy(excelFile, importExcelFile, true);
                }

                IWorkbook workbook = null;
                stream = new MemoryStream(File.ReadAllBytes(excelFile));
                if (extension == ".xls")
                    workbook = new HSSFWorkbook(stream);
                else if (extension == ".xlsx")
                    workbook = new XSSFWorkbook(stream);

                if (workbook == null || workbook.NumberOfSheets == 0) return result;

                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    DataTable dt = new DataTable();
                    ISheet sheet = workbook.GetSheetAt(i);

                    if (sheet.PhysicalNumberOfRows > 0)
                    {
                        IRow headerRow = sheet.GetRow(sheet.FirstRowNum);

                        int cellCount = headerRow.Cells.Count;
                        for (int j = headerRow.FirstCellNum; j < cellCount; j++)
                        {
                            string cellValue = headerRow.GetCellValue(j) == null ? null : headerRow.GetCellValue(j).ToString().Trim();

                            DataColumn column = new DataColumn(cellValue);
                            if (dt.Columns.Contains(column.ColumnName))
                                column.ColumnName += "1";
                            dt.Columns.Add(column);
                        }

                        if (sheet.PhysicalNumberOfRows > 1)
                        {
                            for (int a = (sheet.FirstRowNum + 1); a < sheet.LastRowNum + 1; a++)
                            {
                                IRow row = sheet.GetRow(a);
                                if (row == null) continue;

                                DataRow dr = dt.NewRow();
                                for (int b = row.FirstCellNum; b < cellCount; b++)
                                {
                                    dr[b] = row.GetCellValue(b);
                                }

                                dt.Rows.Add(dr);
                            }
                        }
                    }

                    result.Add(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                    stream.Close();

                if (File.Exists(importExcelFile))
                    File.Delete(importExcelFile);
            }
            return result;
        }

        public static DataTable ImportFirstSheetToDataTable(string excelFile)
        {
            DataTable result = new DataTable();

            string importExcelFile = string.Empty;
            string extension = string.Empty;
            Stream stream = null;

            try
            {
                if (!File.Exists(excelFile))
                    throw new FileNotFoundException("文件不存在!");
                else
                {
                    extension = Path.GetExtension(excelFile);
                    importExcelFile = Path.Combine(Path.GetDirectoryName(excelFile), "B667F371C4C3BD7A14346BF2F7307E31" + extension);
                    File.Copy(excelFile, importExcelFile, true);
                }

                if (!File.Exists(importExcelFile)) return result;

                IWorkbook workbook = null;

                stream = new MemoryStream(File.ReadAllBytes(importExcelFile));

                if (extension == ".xls")
                    workbook = new HSSFWorkbook(stream);
                else if (extension == ".xlsx")
                    workbook = new XSSFWorkbook(stream);

                if (workbook == null || workbook.NumberOfSheets == 0) return result;

                ISheet sheet = workbook.GetSheetAt(0);

                if (sheet.PhysicalNumberOfRows > 0)
                {
                    IRow headerRow = sheet.GetRow(sheet.FirstRowNum);

                    int cellCount = headerRow.Cells.Count;
                    for (int j = headerRow.FirstCellNum; j < cellCount + 1; j++)
                    {
                        string cellValue = headerRow.GetCellValue(j) == null ? null : headerRow.GetCellValue(j).ToString().Trim();

                        DataColumn column = new DataColumn(cellValue);
                        if (result.Columns.Contains(column.ColumnName))
                            column.ColumnName += "1";
                        result.Columns.Add(column);
                    }

                    if (sheet.PhysicalNumberOfRows > 1)
                    {
                        for (int a = (sheet.FirstRowNum + 1); a < sheet.LastRowNum + 1; a++)
                        {
                            IRow row = sheet.GetRow(a);
                            if (row == null) continue;

                            DataRow dr = result.NewRow();
                            for (int b = row.FirstCellNum; b < cellCount + 1; b++)
                            {
                                dr[b] = row.GetCellValue(b);
                            }

                            result.Rows.Add(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                    stream.Close();

                if (File.Exists(importExcelFile))
                    File.Delete(importExcelFile);
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