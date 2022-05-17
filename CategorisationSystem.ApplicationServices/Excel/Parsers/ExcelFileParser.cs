using System.IO;
using System.Linq;
using CategorixationSystem.ApplicationServices.Excel.Models;
using ClosedXML.Excel;

namespace CategorisationSystem.ApplicationServices;

public class ExcelFileParser
{
    public ExcelFile Parse(string path)
    {
        using (MemoryStream newFileStream = new MemoryStream())
        {
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                file.CopyTo(newFileStream);
                using var workbook = new XLWorkbook(newFileStream);

                var workSheets = workbook.Worksheets.First();

                var lastRow = workSheets.RowsUsed().Last().RowNumber();

                var rows = workSheets.Rows(1, lastRow);

                return ParseRows(rows);
            }
        }
    }

    private ExcelRow ParseRow(IXLRow row)
    {
        var result = new ExcelRow();

        foreach (var cell in row.Cells())
        {
            var value = 
                cell.Value == null 
                    ? "" 
                    : cell.Value.ToString();
            result.Columns.Add(value ?? "");
        }

        return result;
    }

    private ExcelFile ParseRows(IXLRows rows)
    {
        ExcelFile file = new ExcelFile();

        foreach (var row in rows)
        {
            file.AddRow(ParseRow(row));
        }

        return file;
    }
}