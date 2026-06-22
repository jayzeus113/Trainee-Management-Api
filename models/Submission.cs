using TraineeManagement.DTOs;
using TraineeManagement.Extensions;
namespace TraineeManagement.Models;
public class Submission
{
    public int Id {get; set;}

    public int TaskAssignmentId {get; set;}
    public string SubmissionUrl {get; set;} = "";
    public string Notes {get; set;} = "";

    public DateTime SubmittedDate {get; set;}

    public string Status {get; set;} = "";

    public Submission(CreateSubmissionRequest createSubmissionRequest)
    {
        TaskAssignmentId = createSubmissionRequest.TaskAssignmentId;
        SubmissionUrl = createSubmissionRequest.SubmissionUrl;
        Notes = createSubmissionRequest.Notes;
        SubmittedDate = createSubmissionRequest.SubmittedDate.ToUtcSecondPrecision();
        Status = createSubmissionRequest.Status;
    }
    Submission() {}
}