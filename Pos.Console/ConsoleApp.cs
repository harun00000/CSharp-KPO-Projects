using System;

public class ConsoleApp
{
    private readonly PosFacade facade;

    public ConsoleApp(PosFacade facade)
    {
        this.facade = facade;
    }

    public void Run()
    {
        while (true)
        {
            ShowMenu();
            Console.Write("Выберите действие: ");
            string? choice = Console.ReadLine();

            if (choice == null)
            {
                return;
            }

            try
            {
                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ShowStudents();
                        break;
                    case "3":
                        AddCriterion();
                        break;
                    case "4":
                        ShowCriteria();
                        break;
                    case "5":
                        AddEvaluation();
                        break;
                    case "6":
                        ShowEvaluations();
                        break;
                    case "7":
                        ShowResult();
                        break;
                    case "8":
                        UpdateEvaluation();
                        break;
                    case "9":
                        RemoveEvaluation();
                        break;
                    case "10":
                        ExportFinalScore();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неизвестный пункт меню");
                        break;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Ошибка: {exception.Message}");
            }
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1 - Добавить студента");
        Console.WriteLine("2 - Показать студентов");
        Console.WriteLine("3 - Добавить критерий");
        Console.WriteLine("4 - Показать критерии");
        Console.WriteLine("5 - Выставить оценку");
        Console.WriteLine("6 - Показать оценки");
        Console.WriteLine("7 - Посчитать итог");
        Console.WriteLine("8 - Исправить оценку");
        Console.WriteLine("9 - Отменить оценку");
        Console.WriteLine("10 - Экспортировать итог");
        Console.WriteLine("0 - Выход");
    }

    private void AddStudent()
    {
        int id = ReadInt("Id студента: ");

        string name = ReadText("Имя: ");

        facade.AddStudent(id, name);

        Console.WriteLine("Студент добавлен");
    }

    private void ShowStudents()
    {
        foreach (Student student in facade.GetStudents())
        {
            Console.WriteLine($"{student.Id} - {student.Name}");
        }
    }

    private void AddCriterion()
    {
        int id = ReadInt("Id критерия: ");

        string name = ReadText("Название: ");

        decimal maxScore = ReadDecimal("Максимальный балл: ");

        decimal weight = ReadDecimal("Вес: ");

        facade.AddCriterion(id, name, maxScore, weight);

        Console.WriteLine("Критерий добавлен");
    }

    private void ShowCriteria()
    {
        foreach (Criterion criterion in facade.GetCriteria())
        {
            Console.WriteLine(
                $"{criterion.Id} - {criterion.Name}, максимум: {criterion.MaxScore}, вес: {criterion.Weight}"
            );
        }
    }

    private void AddEvaluation()
    {
        int id = ReadInt("Id оценки: ");

        int studentId = ReadInt("Id студента: ");

        int criterionId = ReadInt("Id критерия: ");

        decimal value = ReadDecimal("Балл: ");

        facade.AddEvaluation(
            id,
            studentId,
            criterionId,
            value
        );

        Console.WriteLine("Оценка выставлена");
    }

    private void ShowEvaluations()
    {
        foreach (Evaluation evaluation in facade.GetEvaluations())
        {
            Console.WriteLine(
                $"Id: {evaluation.Id} | {evaluation.Student.Name} | {evaluation.Criterion.Name} | {evaluation.Score.Value}"
            );
        }
    }

    private void ShowResult()
    {
        int studentId = ReadInt("Id студента: ");

        decimal result = facade.GetFinalScore(studentId);

        Console.WriteLine($"Итоговый балл: {result}");
    }

    private static string ReadText(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? "";
    }

    private static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (input == null)
            {
                throw new InvalidOperationException("Ввод завершён");
            }

            if (int.TryParse(input, out int value))
            {
                return value;
            }

            Console.WriteLine("Введите целое число");
        }
    }

    private static decimal ReadDecimal(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (input == null)
            {
                throw new InvalidOperationException("Ввод завершён");
            }

            if (decimal.TryParse(input, out decimal value))
            {
                return value;
            }

            Console.WriteLine("Введите число");
        }
    }


    private void UpdateEvaluation()
    {
        int id = ReadInt("Id оценки: ");
        int studentId = ReadInt("Id студента: ");
        int criterionId = ReadInt("Id критерия: ");
        decimal value = ReadDecimal("Новый балл: ");
        facade.UpdateEvaluation(id, studentId, criterionId, value);
        Console.WriteLine("Оценка исправлена.");
    }

    private void RemoveEvaluation()
    {
        int id = ReadInt("Id оценки: ");
        facade.RemoveEvaluation(id);
        Console.WriteLine("Оценка отменена.");
    }

    private void ExportFinalScore()
    {
        int studentId = ReadInt("Id студента: ");
        facade.ExportFinalScore(studentId);
        Console.WriteLine("Итог экспортирован в final_result.csv");
    }
}
