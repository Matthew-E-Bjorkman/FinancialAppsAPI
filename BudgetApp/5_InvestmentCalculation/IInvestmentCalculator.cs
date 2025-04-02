namespace BudgetApp._5_InvestmentCalculation;

public interface IInvestmentCalculator
{
    public Investments Calculate(IEnumerable<InvestmentData> investmentData);
}