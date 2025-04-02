namespace BudgetApp._3_DeductionCalculation;

public interface IDeductionCalculator
{
    public Deductions Calculate(Income income, IEnumerable<DeductionData> deductionsData); 
}