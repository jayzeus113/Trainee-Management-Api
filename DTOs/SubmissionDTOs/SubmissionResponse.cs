using TraineeManagement.Models;

namespace TraineeManagement.DTOs;

public class SubmissionResponse
{
    public int TaskAssignmentId {get; set;}

    public string SubmissionUrl {get; set;}

    public string Notes {get; set;}
    
    public DateTime SubmittedDate {get; set;}
    public string Status {get; set;}
    public SubmissionResponse(Submission s)
    {
        TaskAssignmentId = s.TaskAssignmentId;
        SubmissionUrl = s.SubmissionUrl;
        Notes = s.Notes;
        SubmittedDate = s.SubmittedDate;
        Status = s.Status;
    }
}