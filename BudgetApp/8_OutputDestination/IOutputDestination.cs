namespace BudgetApp._8_OutputDestination;

public interface IOutputDestination
{
   void SaveInput(Budget budget);
   void SaveOutput(Analysis analysis);
}