using System;

public class ResultService
{
    private readonly IStudentRepository studentRepository;
    private readonly IEvaluationRepository evaluationRepository;
    private readonly FinalScoreService finalScoreService;

    private readonly Rubric rubric;
    private readonly IResultExporter resultExporter;

    public ResultService(
        IStudentRepository studentRepository,
        IEvaluationRepository evaluationRepository,
        FinalScoreService finalScoreService,
        Rubric rubric,
        IResultExporter resultExporter)
    {
        this.studentRepository = studentRepository;
        this.evaluationRepository = evaluationRepository;
        this.finalScoreService = finalScoreService;
        this.rubric = rubric;
        this.resultExporter = resultExporter;
    }

    public decimal GetFinalScore(int studentId)
    {
        Student? student = studentRepository.GetById(studentId);

        if (student == null)
        {
            throw new ArgumentException("Студент не найден:(");
        }

        return finalScoreService.Calculate(
            student,
            evaluationRepository.GetByStudentId(studentId),
            rubric
        );
    }

    public void ExportFinalScore(int studentId)
    {
        Student? student = studentRepository.GetById(studentId);

        if (student == null)
        {
            throw new ArgumentException("Студент не найден:(");
        }

        decimal result = GetFinalScore(studentId);
        resultExporter.Export(student, result);
    }
}
