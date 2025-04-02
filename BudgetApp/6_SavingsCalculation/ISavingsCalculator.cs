namespace BudgetApp._6_SavingsCalculation;

public interface ISavingsCalculator
{
    public Savings Calculate(IEnumerable<SavingData> savingsData); 
}