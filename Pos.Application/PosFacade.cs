using System.Collections.Generic;

// Facade for console use cases
public class PosFacade
{
    private readonly StudentService studentService;
    private readonly RubricService rubricService;
    private readonly EvaluationService evaluationService;
    private readonly ResultService resultService;

    public PosFacade(
        StudentService studentService,
        RubricService rubricService,
        EvaluationService evaluationService,
        ResultService resultService)
    {
        this.studentService = studentService;
        this.rubricService = rubricService;
        this.evaluationService = evaluationService;
        this.resultService = resultService;
    }

    public void AddStudent(int id, string name)
    {
        studentService.AddStudent(id, name);
    }

    public List<Student> GetStudents()
    {
        return studentService.GetStudents();
    }

    public void AddCriterion(int id, string name, decimal maxScore, decimal weight)
    {
        rubricService.AddCriterion(id, name, maxScore, weight);
    }

    public List<Criterion> GetCriteria()
    {
        return rubricService.GetCriteria();
    }

    public void AddEvaluation(int id, int studentId, int criterionId, decimal value)
    {
        evaluationService.AddEvaluation(id, studentId, criterionId, value);
    }

    public List<Evaluation> GetEvaluations()
    {
        return evaluationService.GetEvaluations();
    }

    public decimal GetFinalScore(int studentId)
    {
        return resultService.GetFinalScore(studentId);
    }

    public void UpdateEvaluation(int id, int studentId, int criterionId, decimal value)
    {
        evaluationService.UpdateEvaluation(id, studentId, criterionId, value);
    }

    public void RemoveEvaluation(int id)
    {
        evaluationService.RemoveEvaluation(id);
    }

    public void ExportFinalScore(int studentId)
    {
        resultService.ExportFinalScore(studentId);
    }
}
