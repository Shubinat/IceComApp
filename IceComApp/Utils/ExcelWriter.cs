using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace IceComApp.Utils
{
    public class ExcelWriter : IDisposable
    {
        private Excel.Application _application;
        public Excel.Worksheet Sheet { get; }
        public int RowIndex { get; private set; } = 1;
        public ExcelWriter()
        {
            _application = new Excel.Application();
            Excel.Workbook workBook = _application.Workbooks.Add();
            Sheet = workBook.Worksheets[1];
        }
        public void CreateRow(IList<string> data)
        {
            for (int i = 0; i < data.Count(); i++)
            {
                Sheet.Cells[RowIndex, i + 1].Value = data[i];
            }
            RowIndex++;
        }
        public void CreateHeaders(IList<string> data, Excel.XlRgbColor background = Excel.XlRgbColor.rgbWhite, Excel.XlRgbColor foreground = Excel.XlRgbColor.rgbBlack)
        {
            Excel.Range headersRange = Sheet.Range[Sheet.Cells[RowIndex, 1], Sheet.Cells[RowIndex, data.Count()]];
            headersRange.Font.Bold = true;
            headersRange.Interior.Color = background;
            headersRange.Font.Color = foreground;
            CreateRow(data);
        }
        public void CreateSum(string text, int sumCellIndex, string formula)
        {
            Excel.Range sumRange = Sheet.Range[Sheet.Cells[RowIndex, 1], Sheet.Cells[RowIndex, sumCellIndex - 1]];
            sumRange.Merge(sumCellIndex);
            sumRange.Value = text;
            sumRange.Font.Bold = true;
            sumRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            Sheet.Cells[RowIndex, sumCellIndex].Value = formula;
            Sheet.Cells[RowIndex, sumCellIndex].Font.Bold = true;
            RowIndex++;
        }
        public void Dispose()
        {
            Sheet.UsedRange.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                Sheet.UsedRange.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle =
                Sheet.UsedRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Sheet.UsedRange.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Sheet.UsedRange.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Sheet.UsedRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Excel.XlLineStyle.xlContinuous;
            Sheet.Columns.AutoFit();
            _application.Visible = true;
        }
    }
}
