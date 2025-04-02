using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BudgetApp;

public class BudgetCalculator
{
    public async Task Run()
    {
        // Load input
        var inputData = LoadInputData();
        
        // Run threads to calculate income and expenditure separately
        var incomeTask = Task.Run(CalculateIncomeAndDeductions);
        var expenditureTask = Task.Run(CalculateAllExpenditure);

        await Task.WhenAll(incomeTask, expenditureTask);
        
        var incomeAndDeductions = incomeTask.Result!;
        var allExpenditure = expenditureTask.Result!;
        
        // Run analysis on data
        var budgetAnalysis = AnalyzeBudget(incomeAndDeductions, allExpenditure);
    }

    private object AnalyzeBudget(IncomeAndDeductions incomeAndDeductions, AllExpenditure allExpenditure)
    {
        throw new NotImplementedException();
    }

    private static BudgetInputData LoadInputData()
    {
        var rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var fileName = Path.Combine(rootDirectory, "Data", "BudgetInput.json");
        
        using var streamReader = new StreamReader(fileName);
        var json = streamReader.ReadToEnd();
        
        return JsonSerializer.Deserialize<BudgetInputData>(json) 
                ?? throw new ArgumentException("Invalid JSON file");
    }

    private IncomeAndDeductions CalculateIncomeAndDeductions()
    {
        var incomeAndDeductions = new IncomeAndDeductions();
        
        
        
        return incomeAndDeductions;
    }

    private AllExpenditure CalculateAllExpenditure()
    {
        var allExpenditure = new AllExpenditure();
        
        
        
        return allExpenditure;
    }
}

#region Budget Data

public record Budget(IncomeAndDeductions IncomeAndDeductions, AllExpenditure AllExpenditure)
{
    
}

public record IncomeAndDeductions
{
    
}

public record AllExpenditure
{
    
}
#endregion

#region Input Data
public record BudgetInputData
{
    [JsonPropertyName("user_info")] IReadOnlyList<UserInfo> UserInfo { get; init; }
    [JsonPropertyName("holdings")] IReadOnlyList<Holding> Holdings { get; init; }
    [JsonPropertyName("income_sources")] IReadOnlyList<IncomeSource> IncomeSources { get; init; }
    [JsonPropertyName("expenditures")] IReadOnlyList<Expenditures> Expenditures { get; init; }
};

public record UserInfo
{
    [JsonPropertyName("state")] string State { get; init; }
    [JsonPropertyName("age")] int Age { get; init; }
    [JsonPropertyName("retirement_age")] int RetirementAge { get; init; }
}

public record Holding
{
    [JsonPropertyName("name")] string Name { get; init; }
    [JsonPropertyName("amount")] decimal Amount { get; init; }
    [JsonPropertyName("interest_rate")] decimal InterestRate { get; init; }
}

public record IncomeSource
{
    [JsonPropertyName("name")] string Name { get; init; }
    [JsonPropertyName("amount")] decimal Amount { get; init; }
}

public record Expenditures
{
    [JsonPropertyName("name")] string Name { get; init; }
    [JsonPropertyName("category")] string Category { get; init; }
    [JsonPropertyName("pre-tax")] decimal PreTax { get; init; }
    [JsonPropertyName("calc_type")] string CalcType { get; init; }
    [JsonPropertyName("calc_value")] decimal CalcValue { get; init; }
    [JsonPropertyName("amount")] decimal Amount { get; init; }
}
#endregion