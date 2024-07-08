using Microsoft.Win32.SafeHandles;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZeroCodeFramework
{
    public class Readxlxscs
    {
        IWorkbook workbook = null;
        IFormulaEvaluator formula = null;
        ISheet sheet = null;


        public void OpenExcel(string path)
        {
            FileStream inputstream = null;
            try
            {
                inputstream = new FileStream(path, FileMode.Open, FileAccess.Read);
            }
            catch (FileNotFoundException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }



            try
            {
                string extension = Path.GetExtension(path)?.ToLower();
                if (".xls".Equals(extension))
                {

                    workbook = new HSSFWorkbook(inputstream);
                    formula = new HSSFFormulaEvaluator(workbook);
                }
                else if (".xlsx".Equals(extension))
                {

                    workbook = new XSSFWorkbook(inputstream);
                    formula = new XSSFFormulaEvaluator(workbook);
                }

            }
            catch (IOException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }
        public void ReadSheet(string sheetname)
        {


            sheet = workbook.GetSheet(sheetname);
            //if (sheet == null)
            //{
            //    sheet = workbook.GetSheetAt(0);
            //}
        }
        public int GetRowCount()
        {
            return sheet.LastRowNum;
        }
        public TestStep GetTestStep(int rowNumber)
        {
            IRow row = sheet.GetRow(rowNumber);
            TestStep testStep = new TestStep();
          //  testStep.TestCaseId = row.GetCell(0).GetStringValue(formula);
            testStep.Steps = row.GetCell(1).GetStringValue(formula);
            testStep.LocatorType = row.GetCell(2).GetStringValue(formula);
            testStep.LocatorTypeValue = row.GetCell(3).GetStringValue(formula);
            testStep.Action = row.GetCell(4).GetStringValue(formula);
            testStep.Value = row.GetCell(5).GetStringValue(formula).Trim();
            if (testStep.Value.StartsWith("'"))
            {
                testStep.Value = testStep.Value.Substring(1);
            }
            testStep.IsRun = row.GetCell(6).GetStringValue().Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase) ? true : false;
            if (row.Cells.Count > 7)
            {
                testStep.Condition = row.GetCell(7).GetStringValue(formula);
            }
            else
            {
                testStep.Condition = "";
            }
            if (row.Cells.Count > 8)
            {
                testStep.ActualValue = row.GetCell(8).GetStringValue(formula);
            }
            else
            {
                testStep.ActualValue = "";
            }
            return testStep;
        }

        public List<TestStep> GetTestSteps(string sheetName)
        {
            ReadSheet(sheetName);
            List<TestStep> testSteps = new List<TestStep>();
            int rowCount = GetRowCount();
            for (int r = 1; r <= rowCount; r = r + 1)
            {
                testSteps.Add(GetTestStep(r));
            }

            return testSteps;
        }


        public List<Dictionary<string, string>> GetTestData(string sheetName)
        {
            ReadSheet(sheetName);
            List<string> testHeader = new List<string>();
            List<Dictionary<string, string>> listKeyValue = new List<Dictionary<string, string>>();
            int rowCount = GetRowCount();
            for (int r = 0; r <= 0; r = r + 1)
            {
                IRow row = sheet.GetRow(r);
                for (int c = 0; c <= row?.LastCellNum; c = c + 1)
                {
                    ICell cell = row.GetCell(c);
                    if (cell != null && !string.IsNullOrEmpty(cell.GetStringValue()))
                    {
                        testHeader.Add(row.GetCell(c).GetStringValue().Trim());
                    }
                    else
                    {
                        testHeader.Add($"Hearder{c}");
                    }
                }
            }

            for (int r = 1; r <= rowCount; r = r + 1)
            {
                IRow row = sheet.GetRow(r);
                Dictionary<string, string> dct = new Dictionary<string, string>();
                for (int i = 0; i < testHeader.Count; i++)
                {
                    ICell cell = row.GetCell(i);
                    if (cell != null)
                    {
                        string str = cell.GetStringValue(formula);
                        str = str.Trim();
                        if (str.StartsWith("'"))
                        {
                            str = str.Substring(1);
                        }
                        dct.Add($"row{r}Header{testHeader[i]}".ToLower(), str);
                    }
                }

                listKeyValue.Add(dct);
            }

            return listKeyValue;
        }

        public string GetActualValueFromDataSheet(TestStep testStep, List<Dictionary<string, string>> listKeyValue, int rowIndex)
        {
            if (!testStep.ActualValue.StartsWith("!H->", StringComparison.OrdinalIgnoreCase))
            {
                return testStep.ActualValue;
            }
            string header = testStep.ActualValue.Substring(4).Trim();
            var dct = listKeyValue[rowIndex];

            if (dct != null)
            {
                if (dct.ContainsKey($"row{rowIndex + 1}Header{header}".ToLower()))
                {
                    return dct[$"row{rowIndex + 1}Header{header}".ToLower()];
                }
            }

            return testStep.ActualValue;
        }

        public string GetValueFromDataSheet(TestStep testStep, List<Dictionary<string, string>> listKeyValue, int rowIndex)
        {
            if (!testStep.Value.StartsWith("!H->", StringComparison.OrdinalIgnoreCase))
            {
                return testStep.Value;
            }
            string header = testStep.Value.Substring(4).Trim();
            var dct = listKeyValue[rowIndex];

            if (dct != null)
            {
                if (dct.ContainsKey($"row{rowIndex + 1}Header{header}".ToLower()))
                {
                    return dct[$"row{rowIndex + 1}Header{header}".ToLower()];
                }
            }

            return testStep.Value;
        }


        public TestCase GetTestCase(int rowNumber)
        {
            IRow row = sheet.GetRow(rowNumber);
            TestCase testcase = new TestCase();
           // testcase.TestCaseId = row.GetCell(0).GetStringValue();
            testcase.Case = row.GetCell(1).GetStringValue();
            //testcase.SheetName = row.GetCell(2).GetStringValue();

            testcase.IsRun = row.GetCell(3).GetStringValue().Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase) ? true : false;
            if (row.Cells.Count > 4)
            {
                //testcase.DataSheetName = row.GetCell(4).GetStringValue();
            }
            return testcase;
        }



        public List<TestCase> GetTestCases(string sheetName)
        {
            ReadSheet(sheetName);
            List<TestCase> testCases = new List<TestCase>();
            int rowCount = GetRowCount();
            for (int r = 1; r <= rowCount; r = r + 1)
            {
                testCases.Add(GetTestCase(r));
            }

            return testCases;
        }


        public void CloseWorkBook()
        {
            workbook.Close();
        }

        public bool HasSheet(string sheetname)
        {
            sheet = workbook.GetSheet(sheetname);
            if (sheet == null)
            {
                return false;
            }

            return true;
        }
    }


    public static class ExcelExtentions
    {
        public static string GetStringValue(this ICell cell)
        {
            if (cell == null)
            {
                return "";
            }
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue.ToString();
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Formula:

                    return cell.CellFormula.ToString();
                case CellType.Blank:
                    return "";
                case CellType.Error:
                    return "";
                case CellType.Unknown:
                    return "";
            }
            return "";
        }

        public static string GetStringValue(this ICell cell, IFormulaEvaluator formula)
        {
            if (cell == null)
            {
                return "";
            }
            switch (cell.CellType)
            {
                case CellType.String:
                    var str = cell.StringCellValue.ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (str.StartsWith("="))
                        {
                            formula.EvaluateInCell(cell);
                            return formula.Evaluate(cell).StringValue;
                        }
                    }
                    return str;
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Formula:
                    formula.EvaluateInCell(cell);
                    return formula.Evaluate(cell).StringValue;
                case CellType.Blank:
                    return "";
                case CellType.Error:
                    return "";
                case CellType.Unknown:
                    return "";
            }
            return "";
        }
    }
}
