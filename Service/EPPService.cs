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
            var format = new ExcelTextFormat { Delimiter = '\t', EOL = "\r" };

            // Check for file, create workbook, send to function
            if (foj.File != null)
            {
                using (ExcelPackage ex = new ExcelPackage())
                {
                    using (var stream = File.OpenRead(foj.File.FullName))
                        ex.Load(stream);

                    if (ex.Workbook.Worksheets[1].Name != "Data_Cells")
                    {
                        ex.Workbook.Worksheets.Add("Charts");
                        ex.Workbook.Worksheets.Add("Data_Cells");
                        ex.Workbook.Worksheets.Copy("Data_Cells", ex.Workbook.Worksheets[0].Name);
                        ex.Workbook.Worksheets.Delete(0);
                    }

                    CreatingChart(ex);
                    ex.Save();
                    return ex.GetAsByteArray();

                }
            }
            else
            {
                FileInfo newFile = new FileInfo(@"Test.xlsx");

                using (ExcelPackage ex = new ExcelPackage(newFile))
                {
                    // Remove WS Data from staged
                    int wsCount = ex.Workbook.Worksheets.Count() - 1;
                    while (wsCount >= 0)
                    {
                        ex.Workbook.Worksheets.Delete(wsCount);
                        wsCount -= 1;
                    }

                    ex.Workbook.Worksheets.Add("Chart");
                    ex.Workbook.Worksheets.Add("Data_Cells");
                    ex.Workbook.Worksheets[1].Cells.LoadFromCollection(foj.PCCList.position, PrintHeaders: true);
                    CreatingChart(ex);
                    ex.Save();
                    return ex.GetAsByteArray();
                }
            }
        }

        public static ExcelPackage CreatingChart(ExcelPackage ex) // Create Chart code
        {
            int loopCounter = 0;


            // Find our ranges
            int headerRowPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).First().End.Row;
            int lastRowPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Row;
            int firstColumnPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).First().End.Column;
            int lastColumnPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Column;

            string range = "=Data_Cells!$";
            range += (nsEnum.LettEnums)firstColumnPos + "$" + Convert.ToString(firstColumnPos + 1);
            range += ":" + (nsEnum.LettEnums)lastColumnPos + "$" + Convert.ToString(lastColumnPos);
            String headerRow = "=Data_Cells!$";
            headerRow += (nsEnum.LettEnums)firstColumnPos + "$" + Convert.ToString(firstColumnPos + 1);
            headerRow += (nsEnum.LettEnums)lastColumnPos + "$" + Convert.ToString(firstColumnPos + 1);

            ex = ChartExample(ex, range, headerRow);

            return ex;
        }

        public static ExcelPackage ChartExample(ExcelPackage ex, string range, string header)
        {

            // Exxample 1
            ExcelChart chart0 = ex.Workbook.Worksheets[0].Drawings.AddChart("chartZero", eChartType.Pie3D); //adding chart
            chart0.Series.Add(range, header);
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
