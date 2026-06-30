using System.ComponentModel.DataAnnotations;
using TraineeManagement.DTOs;
using TraineeManagement.Extensions;
namespace TraineeManagement.Models;
public class Submission
{
    [Key]
    public int Id {get; set;}
    [Required]
    public int TaskAssignmentId {get; set;}
    [Required]
    [MaxLength(200)]
    public string SubmissionUrl {get; set;} = null!;
    [Required]
    [MaxLength(400)]
    public string Notes {get; set;} = null!;

    [Required]
    public DateTime SubmittedDate {get; set;}

    [Required]
    public string Status {get; set;} = null!;

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