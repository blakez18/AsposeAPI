using System.Linq;
using TempJson.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;
using System;


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
        public List<string> EPPJsontoWS(tempJson cList) // List<t>
        {
            using (ExcelPackage ex = new ExcelPackage())
            {
                ExcelWorksheet worksheet = ex.Workbook.Worksheets.Add("test");
                worksheet.Cells.LoadFromCollection(cList.position);// puts position List<t> to worksheet 2
                return WStoExport(worksheet, cList);
            }
        }


        #endregion FiletoWork


        #region Functions
        public static List<string> WStoExport(ExcelWorksheet ws, tempJson cList) // make chart here 
        {
            if (cList == null)
                return null;
            // TODO: need to ctrl + . to get the using reference... i cant do it from my end
            //using reference for what
            //create another tab on the worksheet with the chart
            var myChart = ws.Drawings.AddChart("chart", OfficeOpenXml.Drawing.Chart.eChartType.BarOfPie);
            List<string> tempString;
            myChart.SetSize(800, 600);
            myChart.Name = "New Chart";

            // fyi use constants for text or varioable which never change
            // lol this isnt vb
            //omg what does it need to be in vb in c#

            cList.position.ForEach(x => // thats how you loop with Linq
            {
                tempString.Add(x.CandidateId);
            });

            // regular c#
            // foreach (var p in cList.position)
            // {

            // }

            return tempString;
        }
        #endregion Functions
    }
}