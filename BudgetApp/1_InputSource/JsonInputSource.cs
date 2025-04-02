using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace BudgetApp._1_InputSource;

public class JsonInputSource(IConfiguration config) : IInputSource
{
    private readonly string _fileName = config["Budget:InputData"] ?? string.Empty;

    public BudgetData Load()
    {
        using var streamReader = new StreamReader(_fileName);
        var json = streamReader.ReadToEnd();
        var budgetData = JsonSerializer.Deserialize<BudgetData>(json);
        return budgetData ?? throw new ArgumentException("Invalid JSON file");
    }
}