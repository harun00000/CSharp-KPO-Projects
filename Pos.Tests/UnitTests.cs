using System;
using System.Collections.Generic;
using Xunit;

public class DomainTests
{
    [Fact]
    public void RejectNegativeScore()
    {
        // Arrange
        decimal value = -1;

        // Act + Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Score(value));
    }

    [Fact]
    public void RejectScoreAboveMaximum()
    {
        // Arrange
        var student = new Student(1, "Amir");
        var criterion = new Criterion(1, "Exam", 10, 1);
        var rubric = new Rubric();
        rubric.Add(criterion);

        var score = new Score(12);
        var factory = new EvaluationFactory();

        // Act + Assert
        Assert.Throws<ArgumentException>(() =>
            factory.Create(1, student, criterion, score, rubric));
    }

    [Fact]
    public void RejectCriterionOutsideRubric()
    {
        // Arrange
        var student = new Student(1, "Amir");

        var criterion1 = new Criterion(1, "Exam", 10, 1);
        var criterion2 = new Criterion(2, "Project", 10, 1);

        var rubric = new Rubric();
        rubric.Add(criterion1);

        var score = new Score(8);
        var factory = new EvaluationFactory();

        // Act + Assert
        Assert.Throws<ArgumentException>(() =>
            factory.Create(1, student, criterion2, score, rubric));
    }

    [Fact]
    public void RejectDuplicateCriterion()
    {
        // Arrange
        var rubric = new Rubric();

        var criterion1 = new Criterion(1, "Exam", 10, 1);
        var criterion2 = new Criterion(1, "Project", 10, 1);

        rubric.Add(criterion1);

        // Act + Assert
        Assert.Throws<ArgumentException>(() =>
            rubric.Add(criterion2));
    }

    [Theory]
    [InlineData(0, "Amir")]
    [InlineData(-1, "Amir")]
    [InlineData(1, "")]
    [InlineData(1, "   ")]
    public void RejectInvalidStudent(int id, string name)
    {
        // Act + Assert
        Assert.ThrowsAny<ArgumentException>(() =>
            new Student(id, name));
    }

    [Fact]
    public void CalculateFinalScore()
    {
        // Arrange
        var student = new Student(1, "Amir");
        var criterion = new Criterion(1, "Exam", 10, 1);

        var rubric = new Rubric();
        rubric.Add(criterion);

        var factory = new EvaluationFactory();

        var evaluation = factory.Create(
            1,
            student,
            criterion,
            new Score(8),
            rubric
        );

        var evaluations = new List<Evaluation>();
        evaluations.Add(evaluation);

        var strategy = new WeightedFinalScoreStrategy();
        var service = new FinalScoreService(strategy);

        // Act
        decimal result = service.Calculate(student, evaluations, rubric);

        // Assert
        Assert.Equal(8, result);
    }

    [Fact]
    public void CalculateWeightedFinalScore()
    {
        // Arrange
        var student = new Student(1, "Amir");

        var exam = new Criterion(1, "Exam", 10, 2);
        var project = new Criterion(2, "Project", 10, 1);

        var rubric = new Rubric();
        rubric.Add(exam);
        rubric.Add(project);

        var factory = new EvaluationFactory();

        var evaluation1 = factory.Create(
            1,
            student,
            exam,
            new Score(8),
            rubric
        );

        var evaluation2 = factory.Create(
            2,
            student,
            project,
            new Score(5),
            rubric
        );

        var evaluations = new List<Evaluation>();
        evaluations.Add(evaluation1);
        evaluations.Add(evaluation2);

        var strategy = new WeightedFinalScoreStrategy();
        var service = new FinalScoreService(strategy);

        // Act
        decimal result = service.Calculate(student, evaluations, rubric);

        // Assert
        Assert.Equal(7, result);
    }

    [Fact]
    public void UpdateEvaluation()
    {
        var students = new InMemoryStudentRepository();
        students.Add(new Student(1, "Amir"));
        var rubric = new Rubric();
        rubric.Add(new Criterion(1, "Exam", 10, 1));
        var evaluations = new InMemoryEvaluationRepository();
        var service = new EvaluationService(students, evaluations, rubric, new EvaluationFactory());
        service.AddEvaluation(1, 1, 1, 8);

        service.UpdateEvaluation(1, 1, 1, 9);
        Assert.Equal(9, evaluations.GetAll()[0].Score.Value);
    }

    [Fact]
    public void RemoveEvaluation()
    {
        var students = new InMemoryStudentRepository();
        students.Add(new Student(1, "Amir"));
        var rubric = new Rubric();
        rubric.Add(new Criterion(1, "Exam", 10, 1));
        var evaluations = new InMemoryEvaluationRepository();
        var service = new EvaluationService(students, evaluations, rubric, new EvaluationFactory());
        service.AddEvaluation(1, 1, 1, 8);

        service.RemoveEvaluation(1);
        Assert.Empty(evaluations.GetAll());
    }
    [Fact]
    public void RejectDuplicateEvaluation()
    {
        var students = new InMemoryStudentRepository();
        students.Add(new Student(1, "Amir"));
        var rubric = new Rubric();
        rubric.Add(new Criterion(1, "Exam", 10, 1));
        var evaluations = new InMemoryEvaluationRepository();
        var service = new EvaluationService(students, evaluations, rubric, new EvaluationFactory());
        service.AddEvaluation(1, 1, 1, 8);

        Assert.Throws<ArgumentException>(() =>
            service.AddEvaluation(2, 1, 1, 9));
    }
}