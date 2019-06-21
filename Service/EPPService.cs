using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;

using Containers.Models;

namespace EPPService.Service
{
    public class EPlusPlus
    {
        private static object xlWorkSheet;
        private static object wsConfig;
        public Byte[] EPPlusDatatoFormat(FileorJson foj)
        {
            FileInfo nf = new FileInfo("C:\\Users\\bzaffiro\\workrepos\\New_Data.xlsx");

            using (ExcelPackage ex = new ExcelPackage())
            {
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
                return ex.GetAsByteArray();
            }
        }

        public static Byte[] EPPBlobData(ExcelPackage ex)
        {
            ex = CreatingChart(ex);
            return null;
        }

        public static ExcelPackage CreatingChart(ExcelPackage ex) // Create Chart code
        {
            var chart = ex.Workbook.Worksheets[0].Drawings.AddChart("Cool Chart", OfficeOpenXml.Drawing.Chart.eChartType.Line); //adding chart
            int lastRow = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Row;
            int lastColumn = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Column;
            var a = "A";
            var range1 = string.Concat(a, lastRow);
            var range2 = string.Concat(a, lastColumn);
            chart.Series.Add(range1, range2);
            return ex;
        }
    }
}
