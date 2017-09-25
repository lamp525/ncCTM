using System;
using Excel = Microsoft.Office.Interop.Excel;

namespace CTM.Win.Util
{
    public class ExcelHelper
    {
        public string _fileName;
        public Excel.Application _app;
        public Excel.Workbooks _wbs;
        public Excel.Workbook _wb;
        //public  Excel.Worksheets wss;
        //public  Excel.Worksheet ws;

        public ExcelHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 创建一个 Excel对象
        /// </summary>
        public void Create()
        {
            _app = new Excel.Application();
            _app.DisplayAlerts = false;
            _wbs = _app.Workbooks;
            _wb = _wbs.Add(true);
        }

        /// <summary>
        /// 打开一个 Excel文件
        /// </summary>
        /// <param name="FileName"></param>
        public void Open(string FileName)
        {
            _app = new Excel.Application();
            _app.DisplayAlerts = false;
            _wbs = _app.Workbooks;
            _wb = _wbs.Add(FileName);

            _fileName = FileName;
        }

        /// <summary>
        ///  获取一个工作表
        /// </summary>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public Excel.Worksheet GetSheet(string SheetName)
        {
            Excel.Worksheet s = (Excel.Worksheet)_wb.Worksheets[SheetName];
            return s;
        }

        /// <summary>
        /// 拷贝一个工作表
        /// </summary>
        /// <param name="sourceSheet"></param>
        public void CopySheetToEnd(Excel.Worksheet sourceSheet, string newSheetName)
        {
            int sheetCount = _wb.Sheets.Count;

            Excel.Worksheet targetSheet = _wb.Sheets[sheetCount] as Excel.Worksheet;
            sourceSheet.Copy(Type.Missing, targetSheet);
            Excel.Worksheet newSheet = _wb.Sheets[sheetCount + 1] as Excel.Worksheet;
            newSheet.Name = newSheetName;
        }

        /// <summary>
        /// 添加一个工作表
        /// </summary>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public Excel.Worksheet AddSheet(string SheetName)
        {
            Excel.Worksheet s = (Excel.Worksheet)_wb.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            s.Name = SheetName;
            return s;
        }

        /// <summary>
        /// 删除一个工作表
        /// </summary>
        /// <param name="SheetName"></param>
        public void DeleteSheet(string SheetName)
        {
            ((Excel.Worksheet)_wb.Worksheets[SheetName]).Delete();
        }

        /// <summary>
        /// 重命名一个工作表
        /// </summary>
        /// <param name="OldSheetName"></param>
        /// <param name="NewSheetName"></param>
        /// <returns></returns>
        public Excel.Worksheet ReNameSheet(string OldSheetName, string NewSheetName)
        {
            Excel.Worksheet s = (Excel.Worksheet)_wb.Worksheets[OldSheetName];
            s.Name = NewSheetName;
            return s;
        }

        /// <summary>
        /// 重命名一个工作表
        /// </summary>
        /// <param name="Sheet"></param>
        /// <param name="NewSheetName"></param>
        /// <returns></returns>
        public Excel.Worksheet ReNameSheet(Excel.Worksheet Sheet, string NewSheetName)
        {
            Sheet.Name = NewSheetName;

            return Sheet;
        }

        /// <summary>
        /// 设置单元格值
        /// </summary>
        /// <param name="ws">要设值的工作表</param>
        /// <param name="x"> X行</param>
        /// <param name="y">Y列 </param>
        /// <param name="value">值</param>
        public void SetCellValue(Excel.Worksheet ws, int x, int y, object value)
        {
            ws.Cells[x, y] = value;
        }

        public void SetCellValue(string ws, int x, int y, object value)
        //ws：要设值的工作表的名称 X行Y列 value 值
        {
            GetSheet(ws).Cells[x, y] = value;
        }

        public void SetCellProperty(Excel.Worksheet ws, int Startx, int Starty, int Endx, int Endy, int size, string name, Excel.Constants color, Excel.Constants HorizontalAlignment)
        //设置一个单元格的属性   字体，   大小，颜色   ，对齐方式
        {
            name = "宋体";
            size = 12;
            color = Excel.Constants.xlAutomatic;
            HorizontalAlignment = Excel.Constants.xlRight;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }

        public void SetCellProperty(string wsn, int Startx, int Starty, int Endx, int Endy, int size, string name, Excel.Constants color, Excel.Constants HorizontalAlignment)
        {
            //name = "宋体";
            //size = 12;
            //color =  Excel.Constants.xlAutomatic;
            //HorizontalAlignment =  Excel.Constants.xlRight;

            Excel.Worksheet ws = GetSheet(wsn);
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;

            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void UniteCells(Excel.Worksheet ws, int x1, int y1, int x2, int y2)
        {
            ws.get_Range(ws.Cells[x1, y1], ws.Cells[x2, y2]).Merge(Type.Missing);
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void UniteCells(string ws, int x1, int y1, int x2, int y2)
        {
            GetSheet(ws).get_Range(GetSheet(ws).Cells[x1, y1], GetSheet(ws).Cells[x2, y2]).Merge(Type.Missing);
        }

        /// <summary>
        ///   将内存中数据表格插入到 Excel指定工作表的指定位置 为在使用模板时控制格式时使用一
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ws"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        public void InsertTable(System.Data.DataTable dt, string ws, int startX, int startY)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    GetSheet(ws).Cells[startX + i, j + startY] = dt.Rows[i][j].ToString();
                }
            }
        }

        public void InsertTable(System.Data.DataTable dt, Excel.Worksheet ws, int startX, int startY)
        //将内存中数据表格插入到 Excel指定工作表的指定位置二
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    ws.Cells[startX + i, j + startY] = dt.Rows[i][j];
                }
            }
        }

        public void AddTable(System.Data.DataTable dt, string ws, int startX, int startY)
        //将内存中数据表格添加到 Excel指定工作表的指定位置一
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    GetSheet(ws).Cells[i + startX, j + startY] = dt.Rows[i][j];
                }
            }
        }

        public void AddTable(System.Data.DataTable dt, Excel.Worksheet ws, int startX, int startY)
        //将内存中数据表格添加到 Excel指定工作表的指定位置二
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    ws.Cells[i + startX, j + startY] = dt.Rows[i][j];
                }
            }
        }

        public void InsertActiveChart(Excel.XlChartType ChartType, string ws, int DataSourcesX1, int DataSourcesY1, int DataSourcesX2, int DataSourcesY2, Excel.XlRowCol ChartDataType)
        //插入图表操作
        {
            ChartDataType = Excel.XlRowCol.xlColumns;
            _wb.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            {
                _wb.ActiveChart.ChartType = ChartType;
                _wb.ActiveChart.SetSourceData(GetSheet(ws).get_Range(GetSheet(ws).Cells[DataSourcesX1, DataSourcesY1], GetSheet(ws).Cells[DataSourcesX2, DataSourcesY2]), ChartDataType);
                _wb.ActiveChart.Location(Excel.XlChartLocation.xlLocationAsObject, ws);
            }
        }

        /// <summary>
        /// 保存文档
        /// </summary>
        /// <returns></returns>
        public void Save()
        {
            if (_fileName == "")
            {
                throw new Exception("File name is null!");
            }
            else
            {
                try
                {
                    _wb.Save();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 文档另存为Excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void SaveAsExcel(object fileName)
        {
            try
            {
                _wb.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 文档另存为PDF
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void SaveAsPDF(object fileName)
        {
            try
            {
                _wb.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, fileName, Excel.XlFixedFormatQuality.xlQualityStandard, true, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 关闭一个 Excel对象，销毁对象
        /// </summary>
        public void Close()
        {
            //wb.Save();
            _wb.Close(Type.Missing, Type.Missing, Type.Missing);
            _wbs.Close();
            _app.Quit();
            _wb = null;
            _wbs = null;
            _app = null;
            GC.Collect();
        }
    }
}