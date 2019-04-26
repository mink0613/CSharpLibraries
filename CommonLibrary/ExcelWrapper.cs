using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace CommonLibrary
{
    public class ExcelWrapper
    {
        public enum FileMode
        {
            Read = 0,
            Write,
            Create
        }

        private static Application _excelApplication;

        private static Workbook _excel;

        private static Worksheet _currentWorksheet;

        private static string _currentFileName;

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

        private static Range Find(string content, XlLookAt match)
        {
            if (_currentWorksheet == null)
            {
                return null;
            }

            return _currentWorksheet.Cells.Find(content, LookAt: match) as Range;
        }

        public static Workbook Open(string fileName, FileMode mode = FileMode.Write)
        {
            if (_excelApplication != null)
            {
                Close();
            }

            _excelApplication = new Application();

            switch (mode)
            {
                case FileMode.Read:

                    _excel = _excelApplication.Workbooks.Open(fileName, ReadOnly: true);

                    break;
                case FileMode.Write:

                    _excel = _excelApplication.Workbooks.Open(fileName, ReadOnly: false, IgnoreReadOnlyRecommended: true, Editable: true);
                    if (_excel == null)
                    {
                        _excel = _excelApplication.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                    }
                    
                    break;
                case FileMode.Create:

                    _excel = _excelApplication.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

                    break;
            }
            
            if (_excel == null)
            {
                Close();
            }

            _currentFileName = fileName;

            return _excel;
        }

        public static bool Save()
        {
            if (_excel == null || _currentFileName == null)
            {
                return false;
            }

            _excelApplication.DisplayAlerts = false;
            _excel.Save();
            return true;
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

            Marshal.ReleaseComObject(_currentWorksheet);
            Marshal.ReleaseComObject(_excel);
            Marshal.ReleaseComObject(_excelApplication);

            _currentWorksheet = null;
            _excel = null;
            _excelApplication = null;
            _currentFileName = null;

            GC.Collect();
        }

        public static void AddWorksheet()
        {
            if (_excel == null)
            {
                return;
            }

            _excel.Worksheets.Add(After: _excel.Sheets[_excel.Sheets.Count]);
        }

        public static bool SetCurrentWorksheet(int sheetIndex = 1, string sheetName = null)
        {
            if (_excel == null)
            {
                return false;
            }

            if (_excel.Worksheets.Count < sheetIndex)
            {
                return false;
            }

            _currentWorksheet = _excel.Worksheets[sheetIndex];
            if (sheetName != null)
            {
                _currentWorksheet.Name = sheetName;
            }
            _currentWorksheet.Select();

            return true;
        }

        public static int GetWorksheetCount()
        {
            if (_excel == null)
            {
                return -1;
            }
            return _excel.Worksheets.Count;
        }

        public static Worksheet GetWorksheet(int sheetIndex)
        {
            if (_excel == null)
            {
                return null;
            }

            _currentWorksheet = _excel.Worksheets[sheetIndex];
            return _currentWorksheet;
        }

        public static Range GetRows()
        {
            if (_currentWorksheet == null)
            {
                return null;
            }

            return _currentWorksheet.Rows;
        }

        public static Range GetColumns()
        {
            if (_currentWorksheet == null)
            {
                return null;
            }

            return _currentWorksheet.Columns;
        }

        public static Range GetUsedRangeRows()
        {
            if (_currentWorksheet == null)
            {
                return null;
            }

            return _currentWorksheet.UsedRange.Rows;
        }

        public static Range GetUsedRangeColumns()
        {
            if (_currentWorksheet == null)
            {
                return null;
            }

            return _currentWorksheet.UsedRange.Columns;
        }

        public static int GetUsedRangeRowsCount()
        {
            if (_currentWorksheet == null)
            {
                return -1;
            }

            return _currentWorksheet.UsedRange.Rows.Count;
        }

        public static int GetUsedRangeColumnsCount()
        {
            if (_currentWorksheet == null)
            {
                return -1;
            }

            return _currentWorksheet.UsedRange.Columns.Count;
        }

        public static int GetUsedRangeRowsStartIndex()
        {
            if (_currentWorksheet == null)
            {
                return -1;
            }

            string address = _currentWorksheet.UsedRange.Rows[1].Address;
            return GetRowNumber(address);
        }

        public static int GetUsedRangeColumnsStartIndex()
        {
            if (_currentWorksheet == null)
            {
                return -1;
            }

            int column = -1;
            for (int row = 1; row < 15; row++)
            {
                for (int col = 1; col < 4; col++)
                {
                    Range cell = _currentWorksheet.Cells[row, col];
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
        }

        public static Range GetRow(int row)
        {
            if (_currentWorksheet == null)
            {
                return null;
            }

            return _currentWorksheet.Rows[row];
        }

        public static Range GetColumn(int column)
        {
            if (_currentWorksheet == null)
            {
                return null;
            }

            return _currentWorksheet.Columns[column];
        }

        public static Range GetCell(int row, int column)
        {
            if (_currentWorksheet == null)
            {
                return null;
            }
            
            return _currentWorksheet.Cells[row, column];
        }

        public static void SetColumnWidth(int columnIndex, int width)
        {
            if (_currentWorksheet == null)
            {
                return;
            }

            _currentWorksheet.Columns[columnIndex].ColumnWidth = width;
        }

        public static void SetRowColor(int rowIndex, Color color)
        {
            if (_currentWorksheet == null)
            {
                return;
            }

            _currentWorksheet.Rows[rowIndex].Interior.Color = ColorTranslator.ToOle(color);
        }

        public static void SetColumnColor(int columnIndex, Color color)
        {
            if (_currentWorksheet == null)
            {
                return;
            }

            _currentWorksheet.Columns[columnIndex].Interior.Color = ColorTranslator.ToOle(color);
        }

        public static void SetCellColor(int rowIndex, int columnIndex, Color color)
        {
            if (_currentWorksheet == null)
            {
                return;
            }

            _currentWorksheet.Cells[rowIndex, columnIndex].Interior.Color = ColorTranslator.ToOle(color);
        }

        public static void MergeCells(int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex)
        {
            if (_currentWorksheet == null)
            {
                return;
            }

            _currentWorksheet.Range[_currentWorksheet.Cells[startRowIndex, startColumnIndex], _currentWorksheet.Cells[endRowIndex, endColumnIndex]].Merge();
        }

        public static bool WriteCell(int row, int column, string data, bool isNumberFormat = false, bool isBold = false)
        {
            if (_currentWorksheet == null)
            {
                return false;
            }

            Range cell = GetCell(row, column);
            if (isNumberFormat == true)
            {
                cell.NumberFormat = "0.0";
            }
            
            cell.Value = data;
            cell.Font.Bold = isBold;

            return true;
        }

        public static bool WriteCellFormula(int row, int column, string formula, bool isNumberFormat = false, bool isBold = false)
        {
            if (_currentWorksheet == null)
            {
                return false;
            }

            Range cell = GetCell(row, column);
            if (isNumberFormat == true)
            {
                cell.NumberFormat = "0.0";
            }

            cell.Formula = formula;
            cell.Font.Bold = isBold;

            return true;
        }

        public static string ConvertToColumnString(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        public static Range FindRowByCellContent(string content, bool isWholeMatch = false)
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

            var matched = Find(content, lookAt);
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

            return _currentWorksheet.Rows[rowNumber];
        }

        public static Range FindColumnByCellContent(string content, bool isWholeMatch = false)
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

            var matched = Find(content, lookAt);
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

            return _currentWorksheet.Columns[columnNumber];
        }

        public static int FindRowIndexByCellContent(string content, bool isWholeMatch = false)
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

            var matched = Find(content, lookAt);
            if (matched == null)
            {
                return -1;
            }

            string address = matched.Address;
            return GetRowNumber(address);
        }

        public static int FindColumnIndexByCellContent(string content, bool isWholeMatch = false)
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

            var matched = Find(content, lookAt);
            if (matched == null)
            {
                return -1;
            }

            string address = matched.Address;
            return GetColumnNumber(address);
        }

        public static Range FindCellByCellContent(string content, bool isWholeMatch = false)
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

            var matched = Find(content, lookAt);
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

            return _currentWorksheet.Cells[rowNumber, columnNumber];
        }

        public static List<string> GetSheetNameList(int startIndex = 1)
        {
            int totalSheets = GetWorksheetCount();

            List<string> nameList = new List<string>();
            for (int sheetNumber = startIndex; sheetNumber <= totalSheets; sheetNumber++)
            {
                Worksheet workSheet = GetWorksheet(sheetNumber); // Get a sheet from Opened Excel
                if (workSheet.Visible == XlSheetVisibility.xlSheetHidden)
                {
                    continue;
                }

                nameList.Add(workSheet.Name);
            }

            return nameList;
        }
    }
}
