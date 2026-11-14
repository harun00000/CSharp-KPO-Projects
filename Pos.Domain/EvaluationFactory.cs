using System;

public class EvaluationFactory
{
    public Evaluation Create(
        int id,
        Student student,
        Criterion criterion,
        Score score,
        Rubric rubric)
    {
        // invariant: criterion must be in rubric
        if (!rubric.Contains(criterion))
        {
            throw new ArgumentException("Критерия нет в рубрикаторе:(");
        }

        // invariant: score can't be greater than criterion maximum
        if (score.Value > criterion.MaxScore)
        {
            throw new ArgumentException("Балл выше максимума критерия:(");
        }

        return new Evaluation(id, student, criterion, score);
    }
}