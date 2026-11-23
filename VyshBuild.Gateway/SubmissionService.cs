using System;
using System.Collections.Generic;

public class SubmissionService
{
    private readonly List<Submission> submissions;
    private int nextId;

    public SubmissionService()
    {
        submissions = new List<Submission>();
        nextId = 0;
    }

    public Submission Create(
        string student,
        int assignmentId,
        int artifactId)
    {
        nextId++;

        Submission submission = new Submission(
            nextId,
            student,
            assignmentId,
            artifactId,
            DateTime.Now                            // current time
        );

        submissions.Add(submission);
        return submission;
    }

    public List<Submission> GetAll()
    {
        return new List<Submission>(submissions);   // copy
    }
}
