using System.Text.Json.Serialization;

namespace BudgetApp;
public record BudgetData(
    [property: JsonPropertyName("income")] IReadOnlyList<IncomeData> Income,
    [property: JsonPropertyName("deductions")] IReadOnlyList<DeductionData> Deductions,
    [property: JsonPropertyName("expenses")] IReadOnlyList<ExpenseData> Expenses,
    [property: JsonPropertyName("investments")] IReadOnlyList<InvestmentData> Investments,
    [property: JsonPropertyName("savings")] IReadOnlyList<SavingData> Savings
);

public record DeductionData(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("value")] string Value
);

public record ExpenseData(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("amount")] int Amount
);

public record IncomeData(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("frequency")] string Frequency,
    [property: JsonPropertyName("amount")] int Amount
);

public record InvestmentData(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("frequency")] string Frequency,
    [property: JsonPropertyName("amount")] int Amount
);

public record SavingData(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("current_amount")] int CurrentAmount,
    [property: JsonPropertyName("frequency")] string Frequency,
    [property: JsonPropertyName("amount")] int Amount
);


