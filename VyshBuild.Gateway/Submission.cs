using System;

public class Submission
{
    public int Id { get; }
    public string Student { get; }
    public int AssignmentId { get; }
    public int ArtifactId { get; }
    public DateTime SubmittedAt { get; }

    public Submission(
        int id,
        string student,
        int assignmentId,
        int artifactId,
        DateTime submittedAt)
    {
        Id = id;
        Student = student;
        AssignmentId = assignmentId;
        ArtifactId = artifactId;
        SubmittedAt = submittedAt;
    }
}
