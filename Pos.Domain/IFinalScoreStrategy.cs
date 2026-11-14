using System.Collections.Generic;

// strategy for final score calculation
public interface IFinalScoreStrategy
{
    decimal Calculate(
        Student student,
        List<Evaluation> evaluations,
        Rubric rubric);
}
