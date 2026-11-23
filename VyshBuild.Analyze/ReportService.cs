public class ReportService
{
    private readonly List<Report> reports;
    private int nextId;

    public ReportService()
    {
        reports = new List<Report>();
        nextId = 0;
    }

    // creates and saves report
    public Report CreateReport(
        int submissionId,
        int assignmentId,
        string fileName,
        long fileSize)
    {
        bool success;
        string status;
        string details;

        if (fileSize <= 0)
        {
            success = false;
            status = "Ошибка";
            details = "Файл пустой:(";
        }
        else if (!fileName.EndsWith(".zip"))
        {
            success = false;
            status = "Ошибка";
            details = "Нужен файл расширения .zip";
        }
        else
        {
            success = true;
            status = "Успешно";
            details = "Файл прошёл проверку:)";
        }

        nextId++;

        Report report = new Report(
            nextId,
            submissionId,
            assignmentId,
            success,
            status,
            details
        );

        reports.Add(report);

        return report;
    }

    // finds report by Id
    public Report? GetById(int id)
    {
        foreach (Report report in reports)
        {
            if (report.Id == id)
            {
                return report;
            }
        }

        return null;
    }

    // returns all reports for one assignment
    public List<Report> GetByAssignmentId(int assignmentId)
    {
        List<Report> result = new List<Report>();

        foreach (Report report in reports)
        {
            if (report.AssignmentId == assignmentId)
            {
                result.Add(report);
            }
        }

        return result;
    }
}