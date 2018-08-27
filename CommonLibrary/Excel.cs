using Microsoft.Office.Interop.Excel;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace CommonLibrary
{
    public class Excel
    {
        private static Application _excelApplication;

        private static Workbook _excel;

        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);

        private static void KillExcelProcess(Application excelApp)
        {
            int id;
            GetWindowThreadProcessId(excelApp.Hwnd, out id);
            Process.GetProcessById(id).Kill();
        }

        private static int GetRowNumber(string address)
        {
            if (address == null || address == string.Empty)
            {
                return -1;
            }

            string[] items = address.Split('$');
            if (items == null || items.Length != 2)
            {
                return -1;
            }

            int number = -1;
            int.TryParse(items[1], out number);
            return number;
        }

        private static string GetColumnString(string address)
        {
            if (address == null || address == string.Empty)
            {
                return string.Empty;
            }

            string[] items = address.Split('$');
            if (items == null || items.Length != 2)
            {
                return string.Empty;
            }

            return items[0];
        }

        private static int GetColumnNumber(string address)
        {
            string columnString = GetColumnString(address);
            if (columnString == string.Empty)
            {
                return -1;
            }

            byte[] convertedByte = Encoding.ASCII.GetBytes(columnString);
            return BitConverter.ToInt32(convertedByte, 0) - (65/*ASCII Code Number of A*/ + 1/*Column A should be column number of 1*/);
        }

        public static Workbook Open(string fileName)
        {
            if (_excelApplication != null)
            {
                Close();
            }

            _excelApplication = new Application();
            _excel = _excelApplication.Workbooks.Open(fileName);
            if (_excel == null)
            {
                Close();
            }

            return _excel;
        }

        public static void Close()
        {
            if (_excel != null)
            {
                _excel.Close();
                _excel = null;
            }

            if (_excelApplication != null)
            {
                _excelApplication.Workbooks.Close();
                _excelApplication.Quit();
                KillExcelProcess(_excelApplication);
                _excelApplication = null;
            }

            //Marshal.FinalReleaseComObject(_excel);
            //Marshal.FinalReleaseComObject(_excelApplication);
        }

        public static Worksheet GetWorksheet(Workbook workbook, int sheetIndex)
        {
            if (workbook == null)
            {
                return null;
            }

            return workbook.Worksheets[sheetIndex];
        }

        public static Range GetRow(Worksheet sheet, int row)
        {
            if (sheet == null)
            {
                return null;
            }

            return sheet.Rows[row];
        }

        public static Range GetColumn(Worksheet sheet, int column)
        {
            if (sheet == null)
            {
                return null;
            }

            return sheet.Columns[column];
        }

        public static Range GetCell(Worksheet sheet, int row, int column)
        {
            if (sheet == null)
            {
                return null;
            }

            return sheet.Cells[row, column];
        }

        public static Range FindRowByCellContent(Worksheet sheet, string content)
        {
            if (sheet == null)
            {
                return null;
            }

            var matched = sheet.Cells.Find(content, LookAt: XlLookAt.xlPart) as Range;
            string address = matched.Address;
            int rowNumber = GetRowNumber(address);
            if (rowNumber == -1)
            {
                return null;
            }

            return sheet.Rows[rowNumber];
        }

        public static Range FindColumnByCellContent(Worksheet sheet, string content)
        {
            if (sheet == null)
            {
                return null;
            }

            var matched = sheet.Cells.Find(content, LookAt: XlLookAt.xlPart) as Range;
            string address = matched.Address;
            int columnNumber = GetColumnNumber(address);
            if (columnNumber == -1)
            {
                return null;
            }

            return sheet.Columns[columnNumber];
        }

        public static Range FindCellByCellContent(Worksheet sheet, string content)
        {
            if (sheet == null)
            {
                return null;
            }

            var matched = sheet.Cells.Find(content, LookAt: XlLookAt.xlPart) as Range;
            string address = matched.Address;
            int rowNumber = GetRowNumber(address);
            int columnNumber = GetColumnNumber(address);
            if (rowNumber == -1 || columnNumber == -1)
            {
                return null;
            }

            return sheet.Cells[rowNumber, columnNumber];
        }
    }
}
