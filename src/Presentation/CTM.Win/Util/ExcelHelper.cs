using System;
using Excel = Microsoft.Office.Interop.Excel;
namespace CTM.Win.Util
{
    public class ExcelHelper
    {
        public string mFilename;
        public  Excel.Application app;
        public  Excel.Workbooks wbs;
        public  Excel.Workbook wb;
        //public  Excel.Worksheets wss;
        //public  Excel.Worksheet ws;

        public ExcelHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public void Create()//创建一个 Excel对象
        {
            app = new  Excel.Application();
            wbs = app.Workbooks;
            wb = wbs.Add(true);
        }

        public void Open(string FileName)//打开一个 Excel文件
        {
            app = new  Excel.Application();
            wbs = app.Workbooks;
            wb = wbs.Add(FileName);

            mFilename = FileName;
        }

        public  Excel.Worksheet GetSheet(string SheetName)
        //获取一个工作表
        {
             Excel.Worksheet s = ( Excel.Worksheet)wb.Worksheets[SheetName];
            return s;
        }

        public void CopySheetToEnd(Excel.Worksheet  sourceSheet)
        //拷贝一个工作表
        {
            int sheetCount = wb.Sheets.Count;
            sourceSheet.Copy(Type.Missing, wb.Sheets[sheetCount]);
        }

        public  Excel.Worksheet AddSheet(string SheetName)
        //添加一个工作表
        {
             Excel.Worksheet s = ( Excel.Worksheet)wb.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            s.Name = SheetName;   
            return s;
        }

        public void DeleteSheet(string SheetName)//删除一个工作表
        {
            (( Excel.Worksheet)wb.Worksheets[SheetName]).Delete();
        }

        public  Excel.Worksheet ReNameSheet(string OldSheetName, string NewSheetName)//重命名一个工作表一
        {
             Excel.Worksheet s = ( Excel.Worksheet)wb.Worksheets[OldSheetName];
            s.Name = NewSheetName;
            return s;
        }

        public  Excel.Worksheet ReNameSheet( Excel.Worksheet Sheet, string NewSheetName)//重命名一个工作表二
        {
            Sheet.Name = NewSheetName;         

            return Sheet;
        }

        public void SetCellValue( Excel.Worksheet ws, int x, int y, object value)
        //ws：要设值的工作表     X行Y列     value   值
        {
            ws.Cells[x, y] = value;
        }

        public void SetCellValue(string ws, int x, int y, object value)
        //ws：要设值的工作表的名称 X行Y列 value 值
        {
            GetSheet(ws).Cells[x, y] = value;
        }

        public void SetCellProperty( Excel.Worksheet ws, int Startx, int Starty, int Endx, int Endy, int size, string name,  Excel.Constants color,  Excel.Constants HorizontalAlignment)
        //设置一个单元格的属性   字体，   大小，颜色   ，对齐方式
        {
            name = "宋体";
            size = 12;
            color =  Excel.Constants.xlAutomatic;
            HorizontalAlignment =  Excel.Constants.xlRight;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }

        public void SetCellProperty(string wsn, int Startx, int Starty, int Endx, int Endy, int size, string name,  Excel.Constants color,  Excel.Constants HorizontalAlignment)
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

        public void UniteCells( Excel.Worksheet ws, int x1, int y1, int x2, int y2)
        //合并单元格
        {
            ws.get_Range(ws.Cells[x1, y1], ws.Cells[x2, y2]).Merge(Type.Missing);
        }

        public void UniteCells(string ws, int x1, int y1, int x2, int y2)
        //合并单元格
        {
            GetSheet(ws).get_Range(GetSheet(ws).Cells[x1, y1], GetSheet(ws).Cells[x2, y2]).Merge(Type.Missing);
        }

        public void InsertTable(System.Data.DataTable dt, string ws, int startX, int startY)
        //将内存中数据表格插入到 Excel指定工作表的指定位置 为在使用模板时控制格式时使用一
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= dt.Columns.Count - 1; j++)
                {
                    GetSheet(ws).Cells[startX + i, j + startY] = dt.Rows[i][j].ToString();
                }
            }
        }

        public void InsertTable(System.Data.DataTable dt,  Excel.Worksheet ws, int startX, int startY)
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

        public void AddTable(System.Data.DataTable dt,  Excel.Worksheet ws, int startX, int startY)
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

        public void InsertActiveChart( Excel.XlChartType ChartType, string ws, int DataSourcesX1, int DataSourcesY1, int DataSourcesX2, int DataSourcesY2,  Excel.XlRowCol ChartDataType)
        //插入图表操作
        {
            ChartDataType =  Excel.XlRowCol.xlColumns;
            wb.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            {
                wb.ActiveChart.ChartType = ChartType;
                wb.ActiveChart.SetSourceData(GetSheet(ws).get_Range(GetSheet(ws).Cells[DataSourcesX1, DataSourcesY1], GetSheet(ws).Cells[DataSourcesX2, DataSourcesY2]), ChartDataType);
                wb.ActiveChart.Location( Excel.XlChartLocation.xlLocationAsObject, ws);
            }
        }

        public bool Save()
        //保存文档
        {
            if (mFilename == "")
            {
                return false;
            }
            else
            {
                try
                {
                    wb.Save();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool SaveAs(object FileName)
        //文档另存为
        {
            try
            {
                wb.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,  Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Close()
        //关闭一个 Excel对象，销毁对象
        {
            //wb.Save();
            wb.Close(Type.Missing, Type.Missing, Type.Missing);
            wbs.Close();
            app.Quit();
            wb = null;
            wbs = null;
            app = null;
            GC.Collect();
        }
    }
}