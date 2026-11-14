using System;

// entity
// Ubiquitous Language: Criterion - критерий оценивания
public class Criterion
{
    public int Id { get; }
    public string Name { get; }
    public decimal MaxScore { get; }
    public decimal Weight { get; }

    public Criterion(int id, string name, decimal maxScore, decimal weight)
    {
        // invariant: criterion  id must be positive
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException("id", "Идентификатор должен быть > 0");
        }

        // invariant: criterion name can't be empty
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Имя критерия не может быть пустым");
        }

        // invariant: max score must be positive
        if (maxScore <= 0)
        {
            throw new ArgumentOutOfRangeException("maxScore");
        }

        // invariant: criterion weight must be positive
        if (weight <= 0)
        {
            throw new ArgumentOutOfRangeException("weight");
        }

        Id = id;
        Name = name;
        MaxScore = maxScore;
        Weight = weight;
    }
}