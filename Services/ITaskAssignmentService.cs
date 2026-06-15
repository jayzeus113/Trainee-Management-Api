using TraineeManagement.DTOs;

namespace TraineeManagement.Services
{
    public interface ITaskAssignmentService
    {
        Task<List<TaskAssignmentResponse>>  GetAll();
        Task<TaskAssignmentResponse> GetById(int Id);
        Task<TaskAssignmentResponse> Create(CreateTaskAssignmentRequest createTaskAssignmentRequest);

        Task<TaskAssignmentResponse> Update(int Id, UpdateTaskAssignmentRequest updateTaskAssignmentRequest);
    }
}