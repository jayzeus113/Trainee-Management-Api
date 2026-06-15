using TraineeManagement.Models;

namespace TraineeManagement.DTOs;

public class TaskAssignmentResponse
{
    public int TraineeId {get; set;}

    public int MentorId {get; set;}

    public int LearningTaskId {get; set;}

    public DateTime AssigenedDate {get; set;}

    public DateTime DueDate {get; set;}
    public string Status {get; set;} = "";
    public string Remarks {get; set;} = "";

    public TaskAssignmentResponse(TaskAssignment ta)
    {
        TraineeId = ta.TraineeId;
        MentorId = ta.MentorId;
        LearningTaskId = ta.LearningTaskId;
        AssigenedDate = ta.AssigenedDate;
        DueDate = ta.DueDate;
        Status = ta.Status;
        Remarks = ta.Remarks;
    }

    public TaskAssignmentResponse() {}
}