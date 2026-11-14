using System;
using System.Collections.Generic;

public class EvaluationService
{
    private readonly IStudentRepository studentRepository;
    private readonly IEvaluationRepository evaluationRepository;
    private readonly Rubric rubric;
    private readonly EvaluationFactory evaluationFactory;

    public EvaluationService(
        IStudentRepository studentRepository,
        IEvaluationRepository evaluationRepository,
        Rubric rubric,
        EvaluationFactory evaluationFactory)
    {
        this.studentRepository = studentRepository;
        this.evaluationRepository = evaluationRepository;
        this.rubric = rubric;
        this.evaluationFactory = evaluationFactory;
    }

    public void AddEvaluation(int id, int studentId, int criterionId, decimal value)
    {
        Student? student = studentRepository.GetById(studentId);

        if (student == null)
        {
            throw new ArgumentException("Студент не найден:(");
        }

        Criterion? criterion = rubric.GetById(criterionId);

        if (criterion == null)
        {
            throw new ArgumentException("Критерий не найден:(");
        }

        List<Evaluation> evaluations =
            evaluationRepository.GetByStudentId(studentId);

        foreach (Evaluation item in evaluations)
        {
            if (item.Criterion.Id == criterionId)
            {
                throw new ArgumentException("Оценка по этому критерию уже выставлена:(");
            }
        }

        Score score = new Score(value);

        Evaluation evaluation = evaluationFactory.Create(id, student, criterion, score, rubric);
        evaluationRepository.Add(evaluation);
    }

    public List<Evaluation> GetEvaluations()
    {
        return evaluationRepository.GetAll();
    }

    public List<Evaluation> GetEvaluationsByStudentId(int studentId)
    {
        return evaluationRepository.GetByStudentId(studentId);
    }

    public void UpdateEvaluation(int id, int studentId, int criterionId, decimal value)
    {
        Student? student = studentRepository.GetById(studentId);

        if (student == null)
        {
            throw new ArgumentException("Студент не найден:(");
        }

        Criterion? criterion = rubric.GetById(criterionId);

        if (criterion == null)
        {
            throw new ArgumentException("Критерий не найден:(");
        }

        Score score = new Score(value);

        Evaluation evaluation = evaluationFactory.Create(id, student, criterion, score, rubric);
        evaluationRepository.Update(evaluation);
    }

    public void RemoveEvaluation(int id)
    {
        evaluationRepository.Remove(id);
    }
}
