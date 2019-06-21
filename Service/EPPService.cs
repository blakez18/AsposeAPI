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
            using (ExcelPackage ex = new ExcelPackage())
            {
                ex.Workbook.Worksheets.Add("Chart");
                ex.Workbook.Worksheets.Add("Data_Cells");
                var format = new ExcelTextFormat { Delimiter = '\t', EOL = "\r" };
                if (foj.File != null) {
                    ex.Workbook.Worksheets[1].Cells["A1"].LoadFromText(foj.File.Extension, format); // might not work
                } else if (foj.PCCList != null) {
                    ex.Workbook.Worksheets[1].Cells.LoadFromCollection(foj.PCCList.position); // loads only position
                }
                ex.Save();
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



        //======================== old stuff
        #region FiletoWorkSeet
        public masterModel EPPFiletoWS(FileOrJson foj)
        {
            masterModel pccList = new masterModel();

            switch (fi.Extension)
            {
                case ".xlsx":
                    using (ExcelPackage ex = new ExcelPackage())
                    {
                        ExcelWorksheet ws = ex.Workbook.Worksheets.Add("Sheet 1");
                        ExcelWorksheet ws2 = ex.Workbook.Worksheets.Add("Sheet 2"); // Create WS
                        var format = new ExcelTextFormat { Delimiter = '\t', EOL = "\r" };
                        ws.Cells["A1"].LoadFromText(fi, format);
                        WStoExport(ex, pccList);
                    }
                    break;
                case ".csv":

                    break;
            }
            return null;
        }
        #endregion FileJsontoWorkSeet

        #region JsontoWorkSheet
        public ExcelPackage EPPJsontoWS(masterModel cList) // List<t>
        {

            using (ExcelPackage ex = new ExcelPackage())
            {
                return WStoExport(ex, cList);
            }
        }
        #endregion FiletoWork

        #region Functions




        public static ExcelPackage WStoExport(ExcelPackage ex, masterModel cList) // make chart here 
        {
            List<string> tempString = new List<string>();
            FileInfo nf = new FileInfo("C:\\Users\\bzaffiro\\workrepos\\New_Data.xlsx");
            if (cList == null)
                return null;

            // Adding Worksheets to Workbook
            ex.Workbook.Worksheets.Add("Cool Tab");
            ex.Workbook.Worksheets.Add("Data_File");

            // Adding data into Tab 2: Worksheet 1 == Data_File
            ex.Workbook.Worksheets[1].Cells.LoadFromCollection(cList.position);// puts position List<t> to worksheet 2                        
                                                                               //ExcelRange data = ex.Workbook.Worksheets[1].Cells;
                                                                               //ex = GetWSOneVals(ex);
                                                                               // var data2 = data.
            ex = CreatingChart(ex);
            // example to load code into list
            cList.position.ForEach(x => // thats how you loop with Linq
            {
                tempString.Add(Convert.ToString(x.CompanyId)); //looping through all of the candidateIds
            });
            ex.SaveAs(nf);

            return ex;
        }
    }

    #endregion Functions
}
