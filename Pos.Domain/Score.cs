using System;

// value object
// Ubiquitous Language: Score - балл
public class Score
{
    public decimal Value { get; }

    public Score(decimal value)
    {
        // invariant: score value can't be negative
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException("value", "Score не может быть отрицательным");
        }

        Value = value;
    }

    // 
    public override bool Equals(object? obj)
    {
        // if we get another Score -> compare values
        if (obj is not Score other)
        {
            return false;
        }

        return Value == other.Value;
    }

    // equal Scores has equal hash
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
