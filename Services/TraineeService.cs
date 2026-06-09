using TraineeManagement.Models;
using TraineeManagement.DTOs;
using TraineeManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace TraineeManagement.Services
{
    public class TraineeService : ITraineeService
    {
        private readonly AppDbContext _context;

        public TraineeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TraineeResponse>> GetAll(string? search)
        {
            IQueryable<Trainee> query = _context.Trainees.AsQueryable();
            if(!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(t => t.FirstName.ToLower().Contains(search) ||
                t.LastName.ToLower().Contains(search) ||
                t.Email.ToLower().Contains(search) ||
                t.TechStack.ToLower().Contains(search)
                );
            }

            return await query.Select(t => new TraineeResponse(t)).ToListAsync();
        }
        public async Task<TraineeResponse?> GetById(int Id)
        {
            Trainee? trainee = await _context.Trainees.FindAsync(Id);
            if(trainee == null) return null;
            return new TraineeResponse(trainee);
        }

        public async Task<TraineeResponse> Create(CreateTraineeRequest createTraineeRequest)
        {
            Trainee trainee = new Trainee(createTraineeRequest);
            await _context.Trainees.AddAsync(trainee);
            await _context.SaveChangesAsync();
            return new TraineeResponse(trainee);
        }

        public async Task<TraineeResponse?> Update(int Id, UpdateTraineeRequest updateTraineeRequest)
        {
            Trainee? trainee = await _context.Trainees.FindAsync(Id);
            if(trainee == null) return null;
            trainee.FirstName = updateTraineeRequest.FirstName;
            trainee.LastName = updateTraineeRequest.LastName;
            trainee.Email = updateTraineeRequest.Email;
            trainee.TechStack = updateTraineeRequest.TechStack;
            trainee.Status = updateTraineeRequest.Status;
            DateTime dt = DateTime.Now;
            DateTime date = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            trainee.UpdatedDate = date;
            await _context.SaveChangesAsync();
            return new TraineeResponse(trainee);
        }

        public async Task<bool> Delete(int Id)
        {
            Trainee? trainee = await _context.Trainees.FindAsync(Id);
            if(trainee == null) return false;
            _context.Trainees.Remove(trainee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}