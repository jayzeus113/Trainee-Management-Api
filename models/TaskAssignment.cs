using TraineeManagement.DTOs;
using TraineeManagement.Extensions;
namespace TraineeManagement.Models;
public class TaskAssignment
{
    public int Id {get; set;}

    public int TraineeId {get; set;}
    public int MentorId {get; set;}
    public int LearningTaskId {get; set;}

    public DateTime AssigenedDate {get; set;}

    public DateTime DueDate {get; set;}

    public string Status {get; set;} = "";

    public string Remarks {get; set;} = "";

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