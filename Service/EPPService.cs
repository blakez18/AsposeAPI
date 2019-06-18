using System.Linq;
using TempJson.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;

namespace EPPService.Service

{
    public class EPlusPlus
    {
        #region FiletoWorkSeet
        public tempJson EPPFiletoWS(FileInfo fi, IFormFile file)
        {
            switch (fi.Extension)
            {
                case ".xlsx":
                    using (ExcelPackage exPack = new ExcelPackage())
                    {
                        ExcelWorksheet ws = exPack.Workbook.Worksheets.Add("Sheet 1"); // Create WS
                        var format = new ExcelTextFormat { Delimiter = '\t', EOL = "\r" };
                        ws.Cells["A1"].LoadFromText(fi, format);
                        WStoExport(ws);
                    }

                    break;
                case ".csv":

                    break;
            }
            return null;
        }
        #endregion FileJsontoWorkSeet

        #region JsontoWorkSheet
        public tempJson EPPJsontoWS(tempJson tj)
        {
            return null;
        }
        #endregion FiletoWork


        #region Functions
        public void WStoExport(ExcelWorksheet ws)
        {

        }
        #endregion Functions
    }
}