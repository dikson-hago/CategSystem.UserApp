namespace CategorixationSystem.ApplicationServices.Txt;

public class TxtFilesContext
{
    private static TxtFilesContext? _context;

    private TxtFilesContext()
    {
    }

    public static TxtFilesContext GetInstance()
    {
        if (_context is null)
        {
            _context = new TxtFilesContext();
        }

        return _context;
    }
    
    public async Task Rewrite(string path, List<string> data)
    {
        await File.WriteAllLinesAsync(path, data);
    }

    public async Task Clear(string path)
    {
        await File.WriteAllLinesAsync(path, new List<string>());
    }

    public async Task AddLine(string path, string line)
    {
        using StreamWriter file = new(path, append: true);
        await file.WriteLineAsync(line);
    }

    public List<string> ReadAllData(string path)
    {
        var data = new List<string>();

        if (!File.Exists(path))
        {
            using (File.Create(path))
            {
            }

            return data;
        }
        
        using (StreamReader sr = File.OpenText(path))
        {
            string? line = "";
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Length != 0)
                {
                    data.Add(line);
                }
            }
        }

        return data;
    }
    
}