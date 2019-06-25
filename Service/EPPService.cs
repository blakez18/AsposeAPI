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
                //================= Setup WorkSheet ==================//
                // Remove WS Data from staged
                int wsCount = ex.Workbook.Worksheets.Count() - 1;
                while (wsCount >= 0)
                {
                    ex.Workbook.Worksheets.Delete(wsCount);
                    wsCount -= 1;
                }

                ex.Workbook.Worksheets.Add("Chart");
                var sheet = ex.Workbook.Worksheets.Add("Data_Cells");
                var format = new ExcelTextFormat { Delimiter = '\t', EOL = "\r" };

                //================= Read or Load ==================//
                if (foj.File != null)
                {
                    if (foj.IsCustom)
                    {

                    }
                    else
                    {
                        ex.Workbook.Worksheets[1].Cells["A1"].LoadFromText(foj.File.Extension, format); // might not work
                    }
                }
                else if (foj.PCCList != null)
                {
                    ex.Workbook.Worksheets[1].Cells.LoadFromCollection(foj.PCCList.position, PrintHeaders: true);

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


            // Find our ranges
            int headerRowPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).First().End.Row;
            int lastRowPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Row;
            int firstColumnPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).First().End.Column;
            int lastColumnPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Column;

            // Set Header String
            while (lastRowPos >= loopCounter)
            {
                string begRow = (nsEnum.AlphabetEPP)firstColumnPos + Convert.ToString((loopCounter + 1));
                string endRow = (nsEnum.AlphabetEPP)lastColumnPos + Convert.ToString((loopCounter + 1));
                string currentRow = begRow + ":" + endRow;
                range.Add(currentRow);
                // Range.add("A2:J2");
                loopCounter++;
            }

            // Loop through our lest and set chart data
            String headerRowRef = (nsEnum.AlphabetEPP)headerRowPos + Convert.ToString(headerRowPos) + ":" + (nsEnum.AlphabetEPP)lastColumnPos + Convert.ToString(headerRowPos); // "A1:J1" 
            range.ForEach(x =>
            {
            });
            ex = ChartExample(ex);

            return ex;
        }

        public static ExcelPackage ChartExample(ExcelPackage ex)
        {

            // Exxample 1
            ExcelChart chart0 = ex.Workbook.Worksheets[0].Drawings.AddChart("chartZero", eChartType.Pie3D); //adding chart
            chart0.Series.Add("=Data_Cells!$A$2:$J$6", "=Data_Cells!$A$1:$J$1");
            chart0.Title.Text = "Test Chart 0";
            chart0.SetSize(400, 400);
            chart0.SetPosition(0, 0, 0, 0);

            // Example 2
            ExcelChart chart1 = ex.Workbook.Worksheets[0].Drawings.AddChart("chartOne", eChartType.PieExploded3D); //adding chart
            chart1.Series.Add("=Data_Cells!$A$2:$J$6", "Data_Cells!$A$1:$J$1");
            chart1.Title.Text = "Test Chart 1";
            chart1.SetSize(400, 400);
            chart1.SetPosition(23, 0, 0, 0);

            // Example 3
            ExcelChart chart2 = ex.Workbook.Worksheets[0].Drawings.AddChart("chartTwo", eChartType.XYScatterLinesNoMarkers); //adding chart
            chart2.Series.Add("=Data_Cells!$A$2:$J$6", "Data_Cells!$A$1:$J$1");
            chart2.Title.Text = "Test Chart 2";
            chart2.SetSize(400, 400);
            chart2.SetPosition(0, 0, 10, 0);

            // Example 4
            ExcelChart chart3 = ex.Workbook.Worksheets[0].Drawings.AddChart("chartThree", eChartType.DoughnutExploded); //adding chart
            chart3.Series.Add("=Data_Cells!$A$2:$J$6", "Data_Cells!$A$1:$J$1");
            chart3.Title.Text = "Test Chart 3";
            chart3.SetSize(400, 400);
            chart3.SetPosition(0, 0, 10, 0);

            return ex;
        }
    }
}
