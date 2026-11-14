using System.Collections.Generic;

public class RubricService
{
    private readonly Rubric rubric;

    public RubricService(Rubric rubric)
    {
        this.rubric = rubric;
    }

    public void AddCriterion(int id, string name, decimal maxScore, decimal weight)
    {
        Criterion criterion = new Criterion(id, name, maxScore, weight);
        rubric.Add(criterion);
    }

    public List<Criterion> GetCriteria()
    {
        return rubric.GetAll();
    }
}
