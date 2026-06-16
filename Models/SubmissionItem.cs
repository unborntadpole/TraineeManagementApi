namespace TraineeManagementApi.Models;
using TraineeManagementApi.DTO;

public class Submission
{
    public long Id { get; set; }
    public string Status { get; set; }
    public string SubmissionUrl { get; set; }
    public string Notes { get; set; }
    public DateTime SubmittedDate { get; set; }

    public long TaskAssignmentId { get; set; }
    public TaskAssignment TaskAssignment { get; set; }



    public Submission(SubmissionDTO submission)
    {
        Id = submission.Id;
        TaskAssignmentId = submission.TaskAssignmentId;
        SubmittedDate = DateTime.UtcNow;
        Notes = submission.Notes;
        SubmissionUrl = submission.SubmissionUrl;
        Status = submission.Status;
    }
    public Submission() {}

}