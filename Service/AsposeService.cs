using TempJson.Models;
using Aspose.Cells;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;

namespace AsposeService.Service
{
    public class AsposeExcel
    {
        #region FiletoWorkSheet
        public tempJson AsposeFiletoWB(FileInfo fi, IFormFile file)
        {
            switch (fi.Extension)
            {
                case ".csv":
                    using (ExcelPackage exPack = new ExcelPackage())
                    {
                        ExcelWorksheet ws = exPack.Workbook.Worksheets.Add("Sheet 1"); // Create WS
                        var format = new ExcelTextFormat { Delimiter = '\t', EOL = "\r" };
                        ws.Cells["A1"].LoadFromText(fi, format);
                        WStoExport(ws);
                    }
                    break;
                case ".xlsx":
                    
                    break;
            }
            return null;
        }
        #endregion FiletoWorkSheet

        #region JsontoWorkSheet
        public tempJson AsposeJsontoWB(tempJson tj)
        {
            return null;
        }
        #endregion JsontoWorkSheet

        #region Functions
        public void WStoExport(ExcelWorksheet ws) // Setup chart
        {

        }
        #endregion Functions
    }
}