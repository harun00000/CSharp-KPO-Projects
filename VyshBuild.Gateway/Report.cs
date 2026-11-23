public class Report
{
    public int Id { get; set; }
    public int SubmissionId { get; set; }
    public int AssignmentId { get; set; }
    public bool Success { get; set; }
    public string Status { get; set; } = "";
    public string Details { get; set; } = "";
}
