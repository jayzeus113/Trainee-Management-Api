using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class TraineeSearchParameters
{
    public string? Search { get; set; }

    public string? Status {get; set;}

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}