using TempJson.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;



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

           ExcelWorksheet ws = ex.Workbook.Worksheets.Add("Cool Tab"); // Adds worksheet to workbook     
           ExcelWorksheet ws2 = ex.Workbook.Worksheets.Add("Data_File");

            // Adding data into Tab 2: Worksheet 1 == Data_File
            ex.Workbook.Worksheets[1].Cells.LoadFromCollection(cList.position);// puts position List<t> to worksheet 2                        
            
            ex = GetWSOneVals(ex, ws, ws2);
            
            ex = CreatingChart(ex, cList);
            // example to load code into list
            cList.position.ForEach(x => // thats how you loop with Linq
            {
                tempString.Add(Convert.ToString(x.CompanyId)); //looping through all of the candidateIds
            });
            ex.SaveAs(nf);
            return ex;
        }

        public static ExcelPackage GetWSOneVals(ExcelPackage ex, ExcelWorksheet ws, ExcelWorksheet ws2)
        {      
             var data  = ws.Cells.Value;

            // //Iterate the rows and colums in  in the used range
            //   foreach(Excel.Range row in Range.Rows){
            //     foreach(Excel.Range col in Range.Columns){
            //       ex.Workbook.Worksheets[0].Drawings[0].Series.Add(Range);
                  
            
        //Do something
            

        //Ex. Iterate through the row's data and put in a string array
        String[] rowData = new String[row.Columns.Count];
        for(int i = 0; i < row.Columns.Count; i++) {
            rowData[i] = row.Cells[1, i + 1].value.ToString();
    
    }
            

                        }
                


              }
                
        //OfficeOpenXml, how to loop through a worksheet in c#, how to loop through the cells to find value
        //figure out debugging c# for vs code 
            return ex;
              
        }
        public static ExcelPackage CreatingChart(ExcelPackage ex, tempJson cList) // Create Chart code
        {
            // Worksheet 0 == Cool Tab
            // Adding Chart
            var chart = ex.Workbook.Worksheets[0].Drawings.AddChart("Cool Chart", OfficeOpenXml.Drawing.Chart.eChartType.BarOfPie);
            // Format Chart
             ex.Workbook.Worksheets[0].Drawings[0].SetSize(800, 600);
            //ex.Workbook.Worksheets[0].Drawings[0]. System.Drawing.Color.Green;
            
            //myChart.Border.Fill.Color = System.Drawing.Color.Green;
            //consumptionWorksheet.Cells[1, 1].LoadFromCollection(consumptionComparisonDetails, false, OfficeOpenXml.Table.TableStyles.Medium1);  

            return ex;
        }

        #endregion Functions
    
