using System.Collections.Generic;

// domain service for final score calculation
public class FinalScoreService
{
    private readonly IFinalScoreStrategy strategy;

    public FinalScoreService(IFinalScoreStrategy strategy)
    {
        this.strategy = strategy;
    }

    public decimal Calculate(
        Student student,
        List<Evaluation> evaluations,
        Rubric rubric)
    {
        return strategy.Calculate(student, evaluations, rubric);
    }
}
