using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
    }
}
