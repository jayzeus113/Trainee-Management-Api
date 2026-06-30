using System.ComponentModel.DataAnnotations;
using TraineeManagement.Constants;

namespace TraineeManagement.DTOs;

public class MentorSearchParameters
{
    public string? Search { get; set; }

    public string? Status {get; set;}

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}
