using TraineeManagement.DTOs;

namespace TraineeManagement.Services
{
    public interface ITraineeService
    {
        Task<IEnumerable<TraineeResponse>> GetAll(string? search);
        Task<TraineeResponse?> GetById(int Id);
        Task<TraineeResponse> Create(CreateTraineeRequest createTraineeRequest);

        Task<TraineeResponse?> Update(int Id, UpdateTraineeRequest updateTraineeRequest);

        Task<bool> Delete(int Id);
    }
}