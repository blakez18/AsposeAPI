using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;

using Containers.Models;
using OfficeOpenXml.Drawing.Chart;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using Aspose.Cells;

namespace EPPService.Service
{
    public class EPlusPlus
    {
        private static object xlWorkSheet;
        private static object wsConfig;
        private static object xls;
        private static int missing;
        private static object xlDirection;
        private object row;

        public static object App { get; private set; }
        public static object Globals { get; private set; }

        public Byte[] EPPlusDatatoFormat(FileorJson foj)
        {
            //FileInfo nf = new FileInfo("C:\\Users\\bzaffiro\\workrepos\\New_Data.xlsx");
            FileInfo newFile = new FileInfo(@"Test.xlsx");
            using (ExcelPackage ex = new ExcelPackage(newFile))
            {
                int wsCount = ex.Workbook.Worksheets.Count() - 1;
                while (wsCount >= 0)
                {
                    ex.Workbook.Worksheets.Delete(wsCount);
                    wsCount -= 1;
                }

                ex.Workbook.Worksheets.Add("Chart");
                var sheet = ex.Workbook.Worksheets.Add("Data_Cells");
                var format = new ExcelTextFormat { Delimiter = '\t', EOL = "\r" };


                // Microsoft.Office.Interop.Excel.Range r = ex.Workbook.Worksheets[1].Select("A" + row.ToString(), "A" + row.ToString()).EntireRow;
                //r.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);


                //Microsoft.Office.Interop.Excel.Range a1 = (ExcelWorksheet.Row) (ex.Workbook.Worksheets[1].InsertRow(1,1));
                //a1.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 
                //Type.Missing );

                if (foj.File != null)
                {
                    ex.Workbook.Worksheets[1].Cells["A1"].LoadFromText(foj.File.Extension, format); // might not work
                }
                else if (foj.PCCList != null)
                {
                    ex.Workbook.Worksheets[1].Cells.LoadFromCollection(foj.PCCList.position, PrintHeaders: true); // loads only position
                                                                                                                  // create header then apply data to row 0
                                                                                                                  //  List<string> headerDets = new List<string>();
                                                                                                                  // foj.PCCList.position.FindIndex(x => x.)
                                                                                                                  //ex.Workbook.Worksheets[1].SelectedRange["A1:A1"].l("a", "b", "c", "d");

                }
                CreatingChart(ex);
                ex.Save();
                return ex.GetAsByteArray();
            }
        }

        public static ExcelPackage CreatingChart(ExcelPackage ex) // Create Chart code
        {
            List<string> range = new List<string>();
            int loopCounter = 0;

            ExcelChart chart = ex.Workbook.Worksheets[0].Drawings.AddChart("Cool Chart", eChartType.Pie3D); //adding chart

            // Find our ranges
            int headerRowPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).First().End.Row;
            int lastRowPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Row;
            int firstColumnPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).First().End.Column;
            int lastColumnPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Column;

            // Set Header String
            while (lastRowPos >= loopCounter)
            {
                // Range.add("A2:J2");
                range.Add((nsEnum.AlphabetEPP)firstColumnPos + (loopCounter + 1) + ":" + (nsEnum.AlphabetEPP)lastColumnPos + (loopCounter + 1));
                loopCounter++;
            }

            // Loop through our lest and set chart data
            String headerRowRef = (nsEnum.AlphabetEPP)headerRowPos + Convert.ToString(headerRowPos) + ":" + (nsEnum.AlphabetEPP)lastColumnPos + Convert.ToString(headerRowPos); // "A1:J1" 
            range.ForEach(x =>
            {
                chart.Series.Add(x, headerRowRef);
            });

            return ex;
        }
    }
}
