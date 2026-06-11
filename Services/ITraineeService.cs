using TraineeManagement.DTOs;
using TraineeManagement.Models;

namespace TraineeManagement.Services
{
    public interface ITraineeService
    {
        Task<PagedResponse<TraineeResponse>>  GetAll(TraineeSearchParameters traineeSearchParameters);
        Task<TraineeResponse?> GetById(int Id);
        Task<TraineeResponse> Create(CreateTraineeRequest createTraineeRequest);

        Task<TraineeResponse?> Update(int Id, UpdateTraineeRequest updateTraineeRequest);

        Task<bool> Delete(int Id);
    }
}