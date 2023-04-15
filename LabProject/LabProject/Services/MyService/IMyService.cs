using ClosedXML.Excel;

namespace LabProject
{
    public interface IMyService
    {
        int GetIntValue();
        string GetTextValue();
        XLWorkbook GetExcelFile();
    }

}
