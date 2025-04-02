namespace BudgetApp._4_ExpenseCalculation;

public interface IExpenseCalculator
{
    public Expenses Calculate(IEnumerable<ExpenseData> expenseData);
}