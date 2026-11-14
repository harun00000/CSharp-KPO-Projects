using System.Collections.Generic;

// weighted strategy for final score
public class WeightedFinalScoreStrategy : IFinalScoreStrategy
{
    public decimal Calculate(
        Student student,
        List<Evaluation> evaluations,
        Rubric rubric)
    {
        decimal sum = 0;
        decimal weights = 0;

        foreach (Criterion criterion in rubric.GetAll())
        {
            decimal scoreValue = 0;

            foreach (Evaluation evaluation in evaluations)
            {
                // find student's score for this criterion
                if (evaluation.Student.Id == student.Id &&
                    evaluation.Criterion.Id == criterion.Id)
                {
                    scoreValue = evaluation.Score.Value;
                    break;
                }
            }

            decimal scorePart = scoreValue / criterion.MaxScore;

            sum += scorePart * criterion.Weight;
            weights += criterion.Weight;
        }

        if (weights == 0)
        {
            return 0;
        }

        return sum / weights * 10;
    }
}
