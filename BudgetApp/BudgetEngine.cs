using BudgetApp._1_InputSource;
using BudgetApp._2_IncomeCalculation;
using BudgetApp._3_DeductionCalculation;
using BudgetApp._4_ExpenseCalculation;
using BudgetApp._5_InvestmentCalculation;
using BudgetApp._6_SavingsCalculation;
using BudgetApp._7_BudgetAnalysis;
using BudgetApp._8_OutputDestination;

namespace BudgetApp;

public class BudgetEngine
{
    private BudgetData BudgetData { get; set; }
    
    private IInputSource InputSource { get; set; }
    private IIncomeCalculator IncomeCalculator { get; set; }
    private IDeductionCalculator DeductionCalculator { get; set; }
    private IExpenseCalculator ExpenseCalculator { get; set; }
    private IInvestmentCalculator InvestmentCalculator { get; set; }
    private ISavingsCalculator SavingsCalculator { get; set; }
    private IBudgetAnalyzer BudgetAnalyzer { get; set; }
    private IOutputDestination OutputDestination { get; set; }
    

    public class BudgetEngineConfiguration(string inputSource, string incomeCalculator, string deductionCalculator,
        string expenseCalculator, string investmentCalculator, string savingsCalculator, string budgetAnalyzer, string outputDestination)
    {
        // TODO: Refactor this to use enums once I have the different types
        public IInputSource InputSource { get; set; } = GetConcreteFromStepInput<IInputSource>(inputSource);
        public IIncomeCalculator IncomeCalculator { get; set; } = GetConcreteFromStepInput<IIncomeCalculator>(incomeCalculator);
        public IDeductionCalculator DeductionCalculator { get; set; } = GetConcreteFromStepInput<IDeductionCalculator>(deductionCalculator);
        public IExpenseCalculator ExpenseCalculator { get; set; } = GetConcreteFromStepInput<IExpenseCalculator>(expenseCalculator);
        public IInvestmentCalculator InvestmentCalculator { get; set; } = GetConcreteFromStepInput<IInvestmentCalculator>(investmentCalculator);
        public ISavingsCalculator SavingsCalculator { get; set; } = GetConcreteFromStepInput<ISavingsCalculator>(savingsCalculator);
        public IBudgetAnalyzer BudgetAnalyzer { get; set; } = GetConcreteFromStepInput<IBudgetAnalyzer>(budgetAnalyzer);
        public IOutputDestination OutputDestination { get; set; } = GetConcreteFromStepInput<IOutputDestination>(outputDestination);

        private static readonly Dictionary<string, string> StepNamespaces = new Dictionary<string, string>()
        {
            { "iinputsource", "BudgetApp._1_InputSource" },
            { "iincomecalculator", "BudgetApp._2_IncomeCalculation" },
            { "ideductioncalculator", "BudgetApp._3_DeductionCalculation" },
            { "iexpensecalculator", "BudgetApp._4_ExpenseCalculation" },
            { "iinvestmentcalculator", "BudgetApp._5_InvestmentCalculation" },
            { "isavingscalculator", "BudgetApp._6_SavingsCalculation" },
            { "ibudgetanalyzer", "BudgetApp._7_BudgetAnalysis" },
            { "ioutputdestination", "BudgetApp._8_OutputDestination" }
        };

        private static T GetConcreteFromStepInput<T>(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || !StepNamespaces.TryGetValue(typeof(T).Name.ToLower(), out var namespacePath))
                throw new ArgumentException($"Invalid input or no namespace mapping for type '{typeof(T).Name}'.");
        
            var type = Type.GetType($"{namespacePath}.{input}");
            if (type == null || !typeof(T).IsAssignableFrom(type))
                throw new ArgumentException($"The specified type '{input}' could not be found or does not implement '{typeof(T).Name}'.");
        
            //TODO: Figure out how to create instance with DI for IConfiguration
            return (T)Activator.CreateInstance(type)!;
        }
        
        public static BudgetEngineConfiguration DefaultConfiguration => new BudgetEngineConfiguration("JsonInputSource","IncomeCalculator","DeductionCalculator","ExpenseCalculator","InvestmentCalculator","SavingsCalculator","BudgetAnalyzer","JsonOutputDestination");
    }
    
    public BudgetEngine(BudgetEngineConfiguration? config = null)
    {
        config ??= BudgetEngineConfiguration.DefaultConfiguration;
    }

    public Analysis Run()
    {
        var budgetData = InputSource.Load();
        var income = IncomeCalculator.Calculate(budgetData.Income);
        var deductions = DeductionCalculator.Calculate(income, budgetData.Deductions);
        var expenses = ExpenseCalculator.Calculate(budgetData.Expenses);
        var investments = InvestmentCalculator.Calculate(budgetData.Investments);
        var savings = SavingsCalculator.Calculate(budgetData.Savings);
        
        Budget budget = new(income, deductions, expenses, investments, savings);
        OutputDestination.SaveInput(budget);
        
        var analysis = BudgetAnalyzer.Analyze(budget);
        OutputDestination.SaveOutput(analysis);
        
        return analysis;
    }
}