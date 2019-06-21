using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;

using Containers.Models;
using OfficeOpenXml.Drawing.Chart;
using Microsoft.Office.Interop.Excel;

namespace EPPService.Service
{
    public class EPlusPlus
    {
        private static object xlWorkSheet;
        private static object wsConfig;

        public static object App { get; private set; }

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
                ex.Workbook.Worksheets.Add("Data_Cells");
                var format = new ExcelTextFormat { Delimiter = '\t', EOL = "\r" };
                if (foj.File != null)
                {
                    ex.Workbook.Worksheets[1].Cells["A1"].LoadFromText(foj.File.Extension, format); // might not work
                }
                else if (foj.PCCList != null)
                {
                    ex.Workbook.Worksheets[1].Cells.LoadFromCollection(foj.PCCList.position); // loads only position
                }
                CreatingChart(ex);
                ex.Save();
                return ex.GetAsByteArray();
            }
        }

        public static ExcelPackage CreatingChart(ExcelPackage ex) // Create Chart code
        {
            String cellData;

            ExcelChart chart = ex.Workbook.Worksheets[0].Drawings.AddChart("Cool Chart", eChartType.Pie3D); //adding chart
            int firstRowPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).First().End.Row;
            int lastRowPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Row;
            int lastColumnPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Column;

            var a = "A";
            var range1 = string.Concat(a, lastRowPos);
            var range2 = string.Concat(a, lastColumnPos);

            List<string> range = new List<string>();
            int loopCounter = 0;
            //ex.Workbook.Worksheets[1].Cells.Rows.(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown,false);
            
            while (lastRowPos >= loopCounter)
            {  
                Char r = (Char)((61) + (lastRowPos - 1));
                Char c = (Char)((65) + (lastColumnPos - 1));
                range.Add(Convert.ToString(r + lastRowPos + ":" + c + lastColumnPos));
                range.Add(Convert.ToString(r + lastRowPos + ":" + c + lastColumnPos));
                loopCounter++;
                if (loopCounter == lastRowPos + 1){
                break;
                }
                
            }

            range.ForEach(x =>
            {
                chart.Series.Add(x, "");
            });
            // for (int i = 1; i <= lastRowPos; i++)
            // {
            //     for (int j = 1; j <= lastColumn; j++)
            //     {
            //         //string beg = ex.Workbook.Worksheets[0].Cells["A1:Aj", ""];
            //         String rangeData =
            //        // ExcelRange ranger = "A1:B1";
            //         ExcelRange range = ex.Workbook.Worksheets[1].Cells[j, i];
            //         String columnLetter = Convert.ToString(1); // for end of row;
            //         cellData = +1 + ":" + lastColumn + 1;
            //         chart.Series.Add(range, ex.Workbook.Worksheets[1].Cells["A1:lastColumn"]);


            //     }
            // }

            // Range should = .Cells["A1:A5]
            // Series.Add[Range, RangeLabel]


            chart.Series.Add(ex.Workbook.Worksheets[1].Cells["A2:J2"], ex.Workbook.Worksheets[0].Cells["A1:J1"]);
            return ex;
        }
    }
}
