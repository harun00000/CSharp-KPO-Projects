public class ReportRequest
{
    public int SubmissionId { get; set; }
    public int AssignmentId { get; set; }
    public string FileName { get; set; } = "";
    public long FileSize { get; set; }
}
