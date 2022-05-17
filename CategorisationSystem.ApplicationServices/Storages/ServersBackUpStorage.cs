using CategorixationSystem.ApplicationServices.Txt;

namespace CategorisationSystem.ApplicationServices.Storages;

class TxtConnectionsFile
{
    public const string FileName = "Connections.txt";
}

public class ServersBackUpStorage
{
    private readonly TxtFilesContext _txtFilesContext;

    public ServersBackUpStorage()
    {
        _txtFilesContext = TxtFilesContext.GetInstance();
    }

    public void AddConnection(string connectionInfo)
    {
        _txtFilesContext.AddLine(TxtConnectionsFile.FileName, connectionInfo);
    }

    public List<string> GetAllConnections()
    {
        return _txtFilesContext.ReadAllData(TxtConnectionsFile.FileName);
    }

    public async Task Rewrite(List<string> connections)
    {
        await _txtFilesContext.Rewrite(TxtConnectionsFile.FileName, connections);
    }
}