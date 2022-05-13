using CategorixationSystem.ApplicationServices.Excel.Models;
using ClosedXML.Excel;

namespace CategorixationSystem.ApplicationServices.ExcelCreator;

public class ExcelFileCreator
{
    public MemoryStream CreateFileAndConvertToStream(ExcelFile file)
    {
        var wbook = new XLWorkbook();
        var wSheet = wbook.Worksheets.Add("Sheet1");
        
        AddRows(wSheet, file);
        
        return ConvertFileToStream(wbook);
    }

    private MemoryStream ConvertFileToStream(XLWorkbook wbook)
    {
        var stream = new MemoryStream();
        wbook.SaveAs(stream);
        stream.Flush();
        stream.Position = 0;
        return stream;
    }

    private void AddRows(IXLWorksheet worksheet, ExcelFile file)
    {
        for (int rowId = 0; rowId < file.Rows.Count; rowId++)
        {
            for (int columnId = 0; columnId < file.Rows[rowId].Columns.Count; columnId++)
            {
                worksheet.Cell(rowId + 1, columnId + 1).Value = 
                    file.Get(rowId, columnId);
            }
        }
    }
}