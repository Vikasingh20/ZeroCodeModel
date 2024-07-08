using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FirstKeywordDrivenFramework
{
    public class WriteExcel
    {

        public static void Write<T>(List<T> lst, string filePath)
        {
            T obj;
            if (lst.Count > 0)
            {
                obj = lst[0];
            }
            else
            {
                obj = Activator.CreateInstance<T>();
            }

            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFFont myFont = (HSSFFont)workbook.CreateFont();
            myFont.FontHeightInPoints = 11;
            myFont.FontName = "Tahoma";


            // Defining a border
            HSSFCellStyle borderedCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            borderedCellStyle.SetFont(myFont);
            borderedCellStyle.BorderLeft = BorderStyle.Medium;
            borderedCellStyle.BorderTop = BorderStyle.Medium;
            borderedCellStyle.BorderRight = BorderStyle.Medium;
            borderedCellStyle.BorderBottom = BorderStyle.Medium;
            borderedCellStyle.VerticalAlignment = VerticalAlignment.Center;

            ISheet Sheet = workbook.CreateSheet("Report");
            //Creat The Headers of the excel
            IRow HeaderRow = Sheet.CreateRow(0);

            var properties = obj.GetType().GetProperties();
            int i = 0;
            foreach (var item in properties)
            {

                CreateCell(HeaderRow, i++, item.Name, borderedCellStyle);
            }

            HSSFCellStyle borderedCellDataStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            borderedCellDataStyle.BorderLeft = BorderStyle.Medium;
            borderedCellDataStyle.BorderTop = BorderStyle.Medium;
            borderedCellDataStyle.BorderRight = BorderStyle.Medium;
            borderedCellDataStyle.BorderBottom = BorderStyle.Medium;
            borderedCellDataStyle.VerticalAlignment = VerticalAlignment.Center;


            int RowIndex = 1;
            foreach (var item in lst)
            {
                IRow CurrentRow = Sheet.CreateRow(RowIndex++);

                int k = 0;
                foreach (var itemP in properties)
                {

                    CreateCell(CurrentRow, k++, itemP.GetValue(item)?.ToString(), borderedCellDataStyle);
                }

            }

            int lastColumNum = Sheet.GetRow(0).LastCellNum;
            for (int s = 0; s <= lastColumNum; s++)
            {
                Sheet.AutoSizeColumn(s);
                GC.Collect();
            }

            using (var fileData = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fileData);
            }

        }
        private static void CreateCell(IRow CurrentRow, int CellIndex, string Value, HSSFCellStyle Style)
        {
            ICell Cell = CurrentRow.CreateCell(CellIndex);
            Cell.SetCellValue(Value);
            Cell.CellStyle = Style;
        }



        public static void WriteAsSequence<T>(List<T> lst, string filePath, List<string> propertyNames)
        {
            T obj;
            if (lst.Count > 0)
            {
                obj = lst[0];
            }
            else
            {
                obj = Activator.CreateInstance<T>();
            }

            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFFont myFont = (HSSFFont)workbook.CreateFont();
            myFont.FontHeightInPoints = 11;
            myFont.FontName = "Tahoma";


            // Defining a border
            HSSFCellStyle borderedCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            borderedCellStyle.SetFont(myFont);
            borderedCellStyle.BorderLeft = BorderStyle.Medium;
            borderedCellStyle.BorderTop = BorderStyle.Medium;
            borderedCellStyle.BorderRight = BorderStyle.Medium;
            borderedCellStyle.BorderBottom = BorderStyle.Medium;
            borderedCellStyle.VerticalAlignment = VerticalAlignment.Center;

            ISheet Sheet = workbook.CreateSheet("testSteps");
            //Creat The Headers of the excel
            IRow HeaderRow = Sheet.CreateRow(0);

            PropertyInfo[] properties = obj.GetType().GetProperties();
            int i = 0;
            foreach (var item in propertyNames)
            {

                CreateCell(HeaderRow, i++, item, borderedCellStyle);
            }

            HSSFCellStyle borderedCellDataStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            borderedCellDataStyle.BorderLeft = BorderStyle.Medium;
            borderedCellDataStyle.BorderTop = BorderStyle.Medium;
            borderedCellDataStyle.BorderRight = BorderStyle.Medium;
            borderedCellDataStyle.BorderBottom = BorderStyle.Medium;
            borderedCellDataStyle.VerticalAlignment = VerticalAlignment.Center;


            int RowIndex = 1;
            foreach (var item in lst)
            {
                IRow CurrentRow = Sheet.CreateRow(RowIndex++);

                int k = 0;
                foreach (var itemStr in propertyNames)
                {
                    var itemP = obj.GetType().GetProperty(itemStr);
                    string strValue = itemP.GetValue(item)?.ToString();

                    if (itemStr.Equals("Value") || itemStr.Equals("ActualValue"))
                    {
                        strValue = "'" + strValue;
                    }
                    else if (itemStr.Equals("IsRun"))
                    {
                        if (strValue.Equals("TRUE", StringComparison.OrdinalIgnoreCase))
                        {
                            strValue = "Yes";
                        }
                        else
                        {
                            strValue = "No";
                        }

                    }

                    CreateCell(CurrentRow, k++, strValue, borderedCellDataStyle);
                }

            }

            int lastColumNum = Sheet.GetRow(0).LastCellNum;
            for (int s = 0; s <= lastColumNum; s++)
            {
                Sheet.AutoSizeColumn(s);
                GC.Collect();
            }



            using (var fileData = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fileData);
            }

        }




        public static void WriteTestSequence<T>(List<T> lst, string filePath, List<string> propertyNames)
        {
            T obj;
            if (lst.Count > 0)
            {
                obj = lst[0];
            }
            else
            {
                obj = Activator.CreateInstance<T>();
            }

            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFFont myFont = (HSSFFont)workbook.CreateFont();
            myFont.FontHeightInPoints = 11;
            myFont.FontName = "Tahoma";


            // Defining a border
            HSSFCellStyle borderedCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            borderedCellStyle.SetFont(myFont);
            borderedCellStyle.BorderLeft = BorderStyle.Medium;
            borderedCellStyle.BorderTop = BorderStyle.Medium;
            borderedCellStyle.BorderRight = BorderStyle.Medium;
            borderedCellStyle.BorderBottom = BorderStyle.Medium;
            borderedCellStyle.VerticalAlignment = VerticalAlignment.Center;

            ISheet Sheet = workbook.CreateSheet("testSteps");
            //Creat The Headers of the excel
            IRow HeaderRow = Sheet.CreateRow(0);

            PropertyInfo[] properties = obj.GetType().GetProperties();
            int i = 0;
            foreach (var item in propertyNames)
            {

                CreateCell(HeaderRow, i++, item, borderedCellStyle);
            }

            HSSFCellStyle borderedCellDataStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            borderedCellDataStyle.BorderLeft = BorderStyle.Medium;
            borderedCellDataStyle.BorderTop = BorderStyle.Medium;
            borderedCellDataStyle.BorderRight = BorderStyle.Medium;
            borderedCellDataStyle.BorderBottom = BorderStyle.Medium;
            borderedCellDataStyle.VerticalAlignment = VerticalAlignment.Center;


            int RowIndex = 1;
            foreach (var item in lst)
            {
                IRow CurrentRow = Sheet.CreateRow(RowIndex++);

                int k = 0;
                foreach (var itemStr in propertyNames)
                {
                    var itemP = obj.GetType().GetProperty(itemStr);
                    string strValue = itemP.GetValue(item)?.ToString();

                    if (itemStr.Equals("Value") || itemStr.Equals("ActualValue"))
                    {
                        strValue = "'" + strValue;
                    }
                    else if (itemStr.Equals("IsRun"))
                    {
                        if (strValue.Equals("TRUE", StringComparison.OrdinalIgnoreCase))
                        {
                            strValue = "Yes";
                        }
                        else
                        {
                            strValue = "No";
                        }

                    }
                    else if (string.IsNullOrWhiteSpace(strValue))
                    {
                        strValue = "'";
                    }

                    CreateCell(CurrentRow, k++, strValue, borderedCellDataStyle);
                }

            }

            int lastColumNum = Sheet.GetRow(0).LastCellNum;
            for (int s = 0; s <= lastColumNum; s++)
            {
                Sheet.AutoSizeColumn(s);
                GC.Collect();
            }


            Sheet = workbook.CreateSheet("testCases");
            //Creat The Headers of the excel
            HeaderRow = Sheet.CreateRow(0);

            ICell Cell = HeaderRow.CreateCell(0);
            Cell.SetCellValue("TestCaseNo");
            Cell = HeaderRow.CreateCell(1);
            Cell.SetCellValue("TestCaseDescription");
            Cell = HeaderRow.CreateCell(2);
            Cell.SetCellValue("SheetName");
            Cell = HeaderRow.CreateCell(3);
            Cell.SetCellValue("IsRun");


            HeaderRow = Sheet.CreateRow(1);

            Cell = HeaderRow.CreateCell(0);
            Cell.SetCellValue("1");
            Cell = HeaderRow.CreateCell(1);
            Cell.SetCellValue("Auto Test Case");
            Cell = HeaderRow.CreateCell(2);
            Cell.SetCellValue("testSteps");
            Cell = HeaderRow.CreateCell(3);
            Cell.SetCellValue("Yes");

            using (var fileData = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fileData);
            }

        }



    }
}
