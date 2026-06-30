using System.ComponentModel.DataAnnotations;
using TraineeManagement.DTOs;
using TraineeManagement.Extensions;
namespace TraineeManagement.Models;
public class TaskAssignment
{
    [Key]
    public int Id {get; set;}

    [Required]
    public int TraineeId {get; set;}

    [Required]
    public int MentorId {get; set;}

    [Required]
    public int LearningTaskId {get; set;}

    [Required]
    public DateTime AssigenedDate {get; set;}

    [Required]
    public DateTime DueDate {get; set;}

    [Required]
    public string Status {get; set;} = null!;

    [Required]
    [MaxLength(200)]
    public string Remarks {get; set;} = null!;

    public TaskAssignment(CreateTaskAssignmentRequest createTaskAssignment)
    {
        TraineeId = createTaskAssignment.TraineeId;
        MentorId = createTaskAssignment.MentorId;
        LearningTaskId = createTaskAssignment.LearningTaskId;
        AssigenedDate = createTaskAssignment.AssigenedDate.ToUtcSecondPrecision();
        DueDate = createTaskAssignment.DueDate.ToUtcSecondPrecision();
        Status = createTaskAssignment.Status;
        Remarks = createTaskAssignment.Remarks;
    }
    TaskAssignment() {}
}