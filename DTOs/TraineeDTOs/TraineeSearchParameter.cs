using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.DTOs;

public class TraineeSearchParameters
{
    public string? Search { get; set; }

    public string? Status {get; set;}

    [Range(1, int.MaxValue, ErrorMessage = "Page Number should be greater than 0")]
    public int PageNumber { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page Number should be greater than 0")]
    public int PageSize { get; set; } = 10;
}
