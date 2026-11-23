public class Report
{
    public int Id { get; }
    public int SubmissionId { get; }
    public int AssignmentId { get; }
    public bool Success { get; }
    public string Status { get; }
    public string Details { get; }

    public Report(
        int id,
        int submissionId,
        int assignmentId,
        bool success,
        string status,
        string details)
    {
        Id = id;
        SubmissionId = submissionId;
        AssignmentId = assignmentId;
        Success = success;
        Status = status;
        Details = details;
    }
}