using ClosedXML.Excel;

namespace LabProject
{
    public class MyService : IMyService
    {
        public int GetIntValue()
        {
            return 42;
        }

        public string GetTextValue()
        {
            return "Hello, world!";
        }

        public XLWorkbook GetExcelFile()
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");

            worksheet.Column(1).Width = 20;
            worksheet.Column(2).Width = 20;
            worksheet.Cell("A1").Value = "First Cell";
            worksheet.Cell("B1").Value = "Last Cell";

            var path = @"C:\Temp\EmptyExcelFile.xlsx";
            workbook.SaveAs(path);

            return workbook;
        }
    }
}
