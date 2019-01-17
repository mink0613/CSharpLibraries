using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace CommonLibrary
{
    public class ExcelWrapper
    {
        private static Application _excelApplication;

        private static Workbook _excel;

        private static void KillExcelProcess(Application excelApp)
        {
            int id;
            Win32API.GetWindowHandleId(excelApp.Hwnd, out id);
            Process.GetProcessById(id).Kill();
        }

        private static int GetRowNumber(string address)
        {
            if (address == null || address == string.Empty)
            {
                return -1;
            }

            string[] items = address.Split("$:".ToCharArray());
            if (items == null || items.Length < 3)
            {
                return -1;
            }

            int number = -1;
            int.TryParse(items[2], out number);
            return number;
        }

        private static string GetColumnString(string address)
        {
            if (address == null || address == string.Empty)
            {
                return string.Empty;
            }

            string[] items = address.Split("$:".ToCharArray());
            if (items == null || items.Length < 3)
            {
                return string.Empty;
            }

            return items[1];
        }

        private static int GetColumnNumber(string address)
        {
            string columnString = GetColumnString(address);
            if (columnString == string.Empty)
            {
                return -1;
            }

            byte[] convertedByte = Encoding.ASCII.GetBytes(columnString);
            int length = convertedByte.Length;
            return convertedByte[length - 1] - (65/*ASCII Code Number of A*/ - 1/*Column A should be column number of 1*/) + (26 * (length - 1));
        }

        private static Range Find(Worksheet sheet, string content, XlLookAt match)
        {
            if (sheet == null)
            {
                return null;
            }

            return sheet.Cells.Find(content, LookAt: match) as Range;
        }

        public static Workbook Open(string fileName)
        {
            if (_excelApplication != null)
            {
                Close();
            }

            _excelApplication = new Application();
            _excel = _excelApplication.Workbooks.Open(fileName, ReadOnly: true);
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
                _excel.Close(0);
            }

            if (_excelApplication != null)
            {
                _excelApplication.Workbooks.Close();
                _excelApplication.Quit();
                KillExcelProcess(_excelApplication);
            }

            Marshal.ReleaseComObject(_excel);
            Marshal.ReleaseComObject(_excelApplication);

            _excel = null;
            _excelApplication = null;

            GC.Collect();
        }

        public static int GetWorksheetCount(Workbook workBook)
        {
            if (workBook == null)
            {
                return -1;
            }
            return workBook.Worksheets.Count;
        }

        public static Worksheet GetWorksheet(Workbook workbook, int sheetIndex)
        {
            if (workbook == null)
            {
                return null;
            }

            return workbook.Worksheets[sheetIndex];
        }

        public static Range GetRows(Worksheet sheet)
        {
            if (sheet == null)
            {
                return null;
            }

            return sheet.Rows;
        }

        public static Range GetColumns(Worksheet sheet)
        {
            if (sheet == null)
            {
                return null;
            }

            return sheet.Columns;
        }

        public static Range GetUsedRangeRows(Worksheet sheet)
        {
            if (sheet == null)
            {
                return null;
            }

            return sheet.UsedRange.Rows;
        }

        public static Range GetUsedRangeColumns(Worksheet sheet)
        {
            if (sheet == null)
            {
                return null;
            }

            return sheet.UsedRange.Columns;
        }

        public static int GetUsedRangeRowsCount(Worksheet sheet)
        {
            if (sheet == null)
            {
                return -1;
            }

            return sheet.UsedRange.Rows.Count;
        }

        public static int GetUsedRangeColumnsCount(Worksheet sheet)
        {
            if (sheet == null)
            {
                return -1;
            }

            return sheet.UsedRange.Columns.Count;
        }

        public static int GetUsedRangeRowsStartIndex(Worksheet sheet)
        {
            if (sheet == null)
            {
                return -1;
            }

            string address = sheet.UsedRange.Rows[1].Address;
            return GetRowNumber(address);
        }

        public static int GetUsedRangeColumnsStartIndex(Worksheet sheet)
        {
            if (sheet == null)
            {
                return -1;
            }

            int column = -1;
            for (int row = 1; row < 15; row++)
            {
                for (int col = 1; col < 4; col++)
                {
                    Range cell = sheet.Cells[row, col];
                    if (cell != null && cell.Value != null)
                    {
                        column = col;
                        break;
                    }
                }

                if (column != -1)
                {
                    break;
                }
            }
            return column;
            //string address = sheet.UsedRange.Columns[1].Address;
            //return GetColumnNumber(address);
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

        public static Range FindRowByCellContent(Worksheet sheet, string content, bool isWholeMatch = false)
        {
            XlLookAt lookAt;
            if (isWholeMatch == true)
            {
                lookAt = XlLookAt.xlWhole;
            }
            else
            {
                lookAt = XlLookAt.xlPart;
            }

            var matched = Find(sheet, content, lookAt);
            if (matched == null)
            {
                return null;
            }

            string address = matched.Address;
            int rowNumber = GetRowNumber(address);
            if (rowNumber == -1)
            {
                return null;
            }

            return sheet.Rows[rowNumber];
        }

        public static Range FindColumnByCellContent(Worksheet sheet, string content, bool isWholeMatch = false)
        {
            XlLookAt lookAt;
            if (isWholeMatch == true)
            {
                lookAt = XlLookAt.xlWhole;
            }
            else
            {
                lookAt = XlLookAt.xlPart;
            }

            var matched = Find(sheet, content, lookAt);
            if (matched == null)
            {
                return null;
            }

            string address = matched.Address;
            int columnNumber = GetColumnNumber(address);
            if (columnNumber == -1)
            {
                return null;
            }

            return sheet.Columns[columnNumber];
        }

        public static int FindRowIndexByCellContent(Worksheet sheet, string content, bool isWholeMatch = false)
        {
            XlLookAt lookAt;
            if (isWholeMatch == true)
            {
                lookAt = XlLookAt.xlWhole;
            }
            else
            {
                lookAt = XlLookAt.xlPart;
            }

            var matched = Find(sheet, content, lookAt);
            if (matched == null)
            {
                return -1;
            }

            string address = matched.Address;
            return GetRowNumber(address);
        }

        public static int FindColumnIndexByCellContent(Worksheet sheet, string content, bool isWholeMatch = false)
        {
            XlLookAt lookAt;
            if (isWholeMatch == true)
            {
                lookAt = XlLookAt.xlWhole;
            }
            else
            {
                lookAt = XlLookAt.xlPart;
            }

            var matched = Find(sheet, content, lookAt);
            if (matched == null)
            {
                return -1;
            }

            string address = matched.Address;
            return GetColumnNumber(address);
        }

        public static Range FindCellByCellContent(Worksheet sheet, string content, bool isWholeMatch = false)
        {
            XlLookAt lookAt;
            if (isWholeMatch == true)
            {
                lookAt = XlLookAt.xlWhole;
            }
            else
            {
                lookAt = XlLookAt.xlPart;
            }

            var matched = Find(sheet, content, lookAt);
            if (matched == null)
            {
                return null;
            }

            string address = matched.Address;
            int rowNumber = GetRowNumber(address);
            int columnNumber = GetColumnNumber(address);
            if (rowNumber == -1 || columnNumber == -1)
            {
                return null;
            }

            return sheet.Cells[rowNumber, columnNumber];
        }

        public static List<string> GetSheetNameList(Workbook excel, int startIndex = 1)
        {
            int totalSheets = GetWorksheetCount(excel);

            List<string> poList = new List<string>();
            for (int sheetNumber = startIndex; sheetNumber <= totalSheets; sheetNumber++)
            {
                Worksheet workSheet = GetWorksheet(excel, sheetNumber); // Get a sheet from Opened Excel
                if (workSheet.Visible == XlSheetVisibility.xlSheetHidden)
                {
                    continue;
                }

                poList.Add(workSheet.Name);
            }

            return poList;
        }
    }
}
