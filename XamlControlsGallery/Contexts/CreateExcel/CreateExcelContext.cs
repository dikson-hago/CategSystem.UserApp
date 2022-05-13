using System.IO;
using CategorixationSystem.ApplicationServices.Excel.Models;
using CategorixationSystem.ApplicationServices.ExcelCreator;
using ClosedXML.Excel;

namespace XamlControlsGallery.Contexts;

public class CreateExcelContext
{
    private string CreateTargetFilePath(string folderPath)
    {
        return folderPath + @"\result.xlsx";
    }
    
    private MemoryStream CreateFileAsStream(
        ExcelFile file)
    {
        var excelCreator = new ExcelFileCreator();
            
        return excelCreator.CreateFileAndConvertToStream(file);
    }

    private void SaveFile(Stream fileStream, string targetPath)
    {
        var workbook = new XLWorkbook(fileStream);
        workbook.SaveAs(targetPath);
    }

    public void Create(ExcelFile file, string targetFolder)
    {
        Stream fileStream = CreateFileAsStream(file);
            
        SaveFile(fileStream, CreateTargetFilePath(targetFolder));
    }
}