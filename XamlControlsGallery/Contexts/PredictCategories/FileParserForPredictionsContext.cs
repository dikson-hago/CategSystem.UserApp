using System;
using System.Collections.Generic;
using System.Linq;
using CategorisationSystem.ApplicationServices;
using CategorixationSystem.ApplicationServices.Excel.Models;
using MlServer.Client.Models;
using XamlControlsGallery.Connections;

namespace XamlControlsGallery.Contexts.PredictCategories;

public class FileParserForPredictionsContext
{
    private readonly ExcelFileParser _parser;
    private readonly ServersStorage _serversStorage;
    private string _tableName;
    private string _serverUrl;

    public FileParserForPredictionsContext(string tableName, string serverUrl)
    {
        _parser = new ExcelFileParser();
        _serversStorage = ServersStorage.GetInstance();
        _tableName = tableName;
        _serverUrl = serverUrl;
    }

    private void ValidateHeaders(List<string> headers)
    {
        var trueColumnNames = _serversStorage.GetTableColumns(_tableName, _serverUrl);
        trueColumnNames = trueColumnNames.GetRange(1, trueColumnNames.Count - 1);
        
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
        var headersAmount = file.Headers().Count();

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

    public ExcelFile Parse(string filePath)
    {
        var parsedData =  _parser.Parse(filePath);
        
        ValidateParsedData(parsedData);

        return parsedData;
    }
}