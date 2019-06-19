using System.Linq;
using TempJson.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;

namespace EPPService.Service

{
    public class EPlusPlus
    {
        #region FiletoWorkSeet
        public tempJson EPPFiletoWS(FileInfo fi, IFormFile file)
        {
            tempJson tempjson = new tempJson();

            switch (fi.Extension)
            {
                case ".xlsx":
                    using (ExcelPackage exPack = new ExcelPackage())
                    {
                        ExcelWorksheet ws = exPack.Workbook.Worksheets.Add("Sheet 1");
                        ExcelWorksheet ws2 = exPack.Workbook.Worksheets.Add("Sheet 2"); // Create WS
                        var format = new ExcelTextFormat { Delimiter = '\t', EOL = "\r" };
                        ws.Cells["A1"].LoadFromText(fi, format);
                        WStoExport(ws, tempjson);
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

            if (cList == null)
                return null;

            ExcelWorksheet ws = ex.Workbook.Worksheets.Add("test"); // Adds worksheet to workbook                
            ws.Cells.LoadFromCollection(cList.position);// puts position List<t> to worksheet 2
            
            var myChart = ws.Drawings.AddChart("chart", OfficeOpenXml.Drawing.Chart.eChartType.BarOfPie);
            myChart.SetSize(800, 600);
            myChart.Name = "New Chart";
            myChart.Border.Fill.Color = System.Drawing.Color.Green;
            //consumptionWorksheet.Cells[1, 1].LoadFromCollection(consumptionComparisonDetails, false, OfficeOpenXml.Table.TableStyles.Medium1);  

            // example to load code into list
            cList.position.ForEach(x => // thats how you loop with Linq
            {
                tempString.Add(Convert.ToString(x.CompanyId)); //looping through all of the candidateIds
            });
            return ex;
        }

        public ExcelPackage AddChart(ExcelPackage ex) // Create Chart code
        {
            return ex;
        }

        #endregion Functions
    }
}