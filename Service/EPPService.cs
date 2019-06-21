using TempJson.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPPService.Service

{
    public class EPlusPlus
    {
        private static object xlWorkSheet;
        private static object wsConfig;
        #region FiletoWorkSeet
        public tempJson EPPFiletoWS(FileInfo fi, IFormFile file)
        {
            tempJson tempjson = new tempJson();

            switch (fi.Extension)
            {
                case ".xlsx":
                    using (ExcelPackage ex = new ExcelPackage())
                    {
                        ExcelWorksheet ws = ex.Workbook.Worksheets.Add("Sheet 1");
                        ExcelWorksheet ws2 = ex.Workbook.Worksheets.Add("Sheet 2"); // Create WS
                        var format = new ExcelTextFormat { Delimiter = '\t', EOL = "\r" };
                        ws.Cells["A1"].LoadFromText(fi, format);
                        WStoExport(ex, tempjson);
                    }
                    break;
                case ".csv":

                    break;
            }
            return null;
        }
        #endregion FileJsontoWorkSeet

        #region JsontoWorkSheet
        public ExcelPackage EPPJsontoWS(tempJson cList) // List<t>
        {

            using (ExcelPackage ex = new ExcelPackage())
            {
                return WStoExport(ex, cList);
            }
        }
        #endregion FiletoWork

        #region Functions
        public static ExcelPackage WStoExport(ExcelPackage ex, tempJson cList) // make chart here 
        {
            List<string> tempString = new List<string>();
            FileInfo nf = new FileInfo("C:\\Users\\bzaffiro\\workrepos\\New_Data.xlsx");
            if (cList == null)
                return null;

            ex.Workbook.Worksheets.Add("Cool Tab"); // Adds worksheet to workbook     
            ex.Workbook.Worksheets.Add("Data_File");

            // Adding data into Tab 2: Worksheet 1 == Data_File
            ex.Workbook.Worksheets[1].Cells.LoadFromCollection(cList.position);// puts position List<t> to worksheet 2                        
                                                                               //ExcelRange data = ex.Workbook.Worksheets[1].Cells;
                                                                               //ex = GetWSOneVals(ex);
                                                                               // var data2 = data.
            ex = CreatingChart(ex, cList);
            // example to load code into list
            cList.position.ForEach(x => // thats how you loop with Linq
            {
                tempString.Add(Convert.ToString(x.CompanyId)); //looping through all of the candidateIds
            });
            ex.SaveAs(nf);
            return ex;
        }
        public static ExcelPackage CreatingChart(ExcelPackage ex, tempJson cList) // Create Chart code
        {
            String cellData;
            var chart = ex.Workbook.Worksheets[0].Drawings.AddChart("Cool Chart", OfficeOpenXml.Drawing.Chart.eChartType.Line); //adding chart
            int firstRowPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).First().End.Row;
            int lastRowPos = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Row;            
            int lastColumn = ex.Workbook.Worksheets[1].Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Column;
            for (int i = 1; i <= lastRowPos; i++ ){
                for (int j = 1; j <= lastColumn; j++){
                    string beg = ex.Workbook.Worksheets[0].Cells['A1'];
                    ExcelRange range = ex.Workbook.Worksheets[1].Cells[j,i];
                    String columnLetter = 1; // for end of row;
                    cellData =  + 1 + ":" + lastColumn + 1;
                    chart.Series.Add(range, ex.Workbook.Worksheets[1].Cells["A1:lastColumn"]);


                }
            }

            chart.Series.Add(ex.Workbook.Worksheets[1].Cells["A1:A + lastRow"], ex.Workbook.Worksheets[1].Cells["A1:lastRow"]);

            // Loop to determine chart
            #region code
            // foreach (var cell in ex.Workbook.Worksheets[1].Cells[1, 1, 1, ex.Workbook.Worksheets[1].Dimension.End.Column])
            // {
            //     var data = cell.Value;

            //     //    for (int i = 2; i <= ex.Workbook.Worksheets[1].Dimension.End.Row; i++) {

            //     // Enumerable.Range(ex.Workbook.Worksheets[1].Dimension.Start.Row + 1, ex.Workbook.Worksheets[1].Dimension.End.Row).Select(i => Convert.ToString(ex.Workbook.Worksheets[1].Cells[i, 1].Value));

            //     //ExcelRange range = ex.Workbook.Worksheets[1].Cells;
            //     // chart.Series.Add(ex.Workbook.Worksheets[1].Cells[data], ex.Workbook.Worksheets[1].Cells[data]);
            //     // }
            // }
            #endregion code

            // ex.Workbook.Worksheets[0].Drawings[0].SetSize(800, 600);
            //ex.Workbook.Worksheets[0].Drawings[0]. System.Drawing.Color.Green;

            //myChart.Border.Fill.Color = System.Drawing.Color.Green;
            //consumptionWorksheet.Cells[1, 1].LoadFromCollection(consumptionComparisonDetails, false, OfficeOpenXml.Table.TableStyles.Medium1);  

            return ex;
        }
    }

    #endregion Functions
}
