using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CategorixationSystem.ApplicationServices.Excel.Models;

public class ExcelFile
{
    public ExcelFile()
    {
        Rows = new List<ExcelRow>();
    }

    public List<ExcelRow> Rows { get; set; }

    public List<string> Headers()
    {
        if (!Rows.Any())
        {
            throw new Exception("File is empty");
        }

        return Rows[0].Columns;
    }

    public List<ExcelRow> RowsWithoutHeader()
    {
        return Rows.GetRange(1, Rows.Count() - 1);
    }

    public string Get(int row, int column) => Rows[row].Columns[column];

    public void AddRow(List<string> row)
    {
        Rows.Add(new ExcelRow()
        {
            Columns = row
        });
    }

    public void AddRow(ExcelRow row)
    {
        Rows.Add(row);
    }
}

public class ExcelRow
{
    public ExcelRow()
    {
        Columns = new List<string>();
    }
    
    public List<string> Columns { get; set; }

    public List<string> GetColumns()
    {
        return new List<string>(Columns);
    }
}