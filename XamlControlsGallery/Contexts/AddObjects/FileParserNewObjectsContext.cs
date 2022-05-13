using System;
using System.Collections.Generic;
using System.Linq;
using CategorisationSystem.ApplicationServices;
using CategorixationSystem.ApplicationServices.Excel.Models;
using MlServer.Client.Models;
using XamlControlsGallery.Connections;

namespace XamlControlsGallery.Contexts.AddObjects;

class FileParserNewObjectsContext
{
    private readonly ExcelFileParser _parser;
    private string _tableName;
    private string _serverUrl;
    private ServersStorage _serversStorage;
    public FileParserNewObjectsContext(string tableName, string serverUrl)
    {
        _parser = new ExcelFileParser();
        _tableName = tableName;
        _serverUrl = serverUrl;
        _serversStorage = ServersStorage.GetInstance();
    }

    private void ValidateHeaders(List<string> headers)
    {
        var trueColumnNames = _serversStorage.GetTableColumns(_tableName, _serverUrl)
            .Where(item => item.Length != 0).ToList();

        if (headers.Count() != trueColumnNames.Count)
        {
            throw new Exception("Error of columns amount");
        }

        for (int ind = 0; ind < trueColumnNames.Count; ind++)
        {
            if (!headers[ind].Equals(trueColumnNames[ind]))
            {
                throw new Exception("Incorrect place of column");
            }
        }
        
    }

    private void ValidateSize(ExcelFile file)
    {
        var headersAmount = file.Headers().Count;

        var incorrectRowsAmount = file.Rows.Count(row => 
            row.Columns.Count() > headersAmount);

        if (incorrectRowsAmount > 0)
        {
            throw new Exception("Incorrect row data in file");
        }
    }

    private void ValidateParsedData(ExcelFile file)
    {
        ValidateHeaders(file.Headers());
        ValidateSize(file);
    }

    public ExcelFile ParseFile(string filePath)
    {
        var parsedResult = _parser.Parse(filePath);
        
        ValidateParsedData(parsedResult);

        return parsedResult;
    }
}