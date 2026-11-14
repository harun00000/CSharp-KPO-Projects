using System;
using System.Collections.Generic;

// Ports & Adapters: adapter for IEvaluationRepository
public class InMemoryEvaluationRepository : IEvaluationRepository
{
    private readonly List<Evaluation> evaluations;

    public InMemoryEvaluationRepository()
    {
        evaluations = new List<Evaluation>();
    }

    public void Add(Evaluation evaluation)
    {
        foreach (Evaluation item in evaluations)
        {
            if (item.Id == evaluation.Id)
            {
                throw new ArgumentException("Оценка с таким Id уже есть:(");
            }
        }

        evaluations.Add(evaluation);
    }

    public List<Evaluation> GetAll()
    {
        // return copy
        return new List<Evaluation>(evaluations);
    }

    public List<Evaluation> GetByStudentId(int studentId)
    {
        List<Evaluation> result = new List<Evaluation>();

        foreach (Evaluation evaluation in evaluations)
        {
            if (evaluation.Student.Id == studentId)
            {
                result.Add(evaluation);
            }
        }

        return result;
    }

    public void Update(Evaluation evaluation)
    {
        for (int idx = 0; idx < evaluations.Count; idx++)
        {
            if (evaluations[idx].Id == evaluation.Id)
            {
                evaluations[idx] = evaluation;
                return;
            }
        }

        throw new ArgumentException("Оценка не найдена:(");
    }

    public void Remove(int id)
    {
        for (int idx = 0; idx < evaluations.Count; idx++)
        {
            if (evaluations[idx].Id == id)
            {
                evaluations.RemoveAt(idx);
                return;
            }
        }

        throw new ArgumentException("Оценка не найдена:(");
    }
}
