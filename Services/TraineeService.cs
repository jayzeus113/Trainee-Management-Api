using TraineeManagement.Models;
using TraineeManagement.DTOs;

namespace TraineeManagement.Services
{
    public class TraineeService : ITraineeService
    {
        private static List<Trainee> trainees = new();

        public IEnumerable<TraineeResponse> GetAll()
        {
            return trainees.Select(t => new TraineeResponse(t));
        }
        public TraineeResponse? GetById(int id)
        {
            var trainee = trainees.FirstOrDefault(x => x.Id == id);
            if(trainee == null) return null;
            return new TraineeResponse(trainee);
        }

        public TraineeResponse Create(CreateTraineeRequest createTraineeRequest)
        {
            Trainee trainee = new Trainee(createTraineeRequest);
            trainees.Add(trainee);
            return new TraineeResponse(trainee);
        }

        public TraineeResponse? Update(int Id, UpdateTraineeRequest updateTraineeRequest)
        {
            Trainee? trainee = trainees.FirstOrDefault(t => t.Id == Id);
            if(trainee == null) return null;
            trainee.FirstName = updateTraineeRequest.FirstName;
            trainee.LastName = updateTraineeRequest.LastName;
            trainee.Email = updateTraineeRequest.Email;
            trainee.TechStack = updateTraineeRequest.TechStack;
            trainee.Status = updateTraineeRequest.Status;
            trainee.UpdatedDate = DateTime.UtcNow;
            return new TraineeResponse(trainee);
        }

        public bool Delete(int Id)
        {
            Trainee? trainee = trainees.FirstOrDefault(t => t.Id == Id);
            if(trainee == null) return false;
            trainees.Remove(trainee);
            return true;
        }
    }
}