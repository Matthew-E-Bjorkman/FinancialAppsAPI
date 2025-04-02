namespace BudgetApp._2_IncomeCalculation;

public interface IIncomeCalculator
{
    public Income Calculate(IEnumerable<IncomeData> incomeData);
}