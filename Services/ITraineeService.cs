using TraineeManagement.DTOs;
using TraineeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace TraineeManagement.Services
{
    public interface ITraineeService
    {
        IEnumerable<TraineeResponse> GetAll();
        TraineeResponse? GetById(int id);
        TraineeResponse Create(CreateTraineeRequest createTraineeRequest);

        TraineeResponse? Update(int id, UpdateTraineeRequest updateTraineeRequest);

        bool Delete(int id);
    }
}