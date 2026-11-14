using System;

// entity
// Ubiquitous Language: Evaluation - факт выставленной оценки
public class Evaluation
{
    public int Id { get; }
    public Student Student { get; }
    public Criterion Criterion { get; }
    public Score Score { get; }

    // internal (means we cann call constructor only inside Pos.Domain) because we can't do new Evaluation
    internal Evaluation(int id, Student student, Criterion criterion, Score score)
    {
        // invariant: evaluation id must be positive
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException("id", "Идентификатор должен быть > 0");
        }

        // invariant: score can't be greater than criterion maximum
        if (score.Value > criterion.MaxScore)
        {
            throw new ArgumentException("Балл выше максимума критерия:(");
        }

        Id = id;
        Student = student;
        Criterion = criterion;
        Score = score;
    }
}