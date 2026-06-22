using System.ComponentModel.DataAnnotations;
using TraineeManagement.DTOs;

namespace TraineeManagement.Models;

public class SubmissionFile
{
    [Key]
    public int Id { get; set; }
    
    public int SubmissionId { get; set; }
    public string OriginalFileName { get; set; } = "";
    
    public string GeneratedStorageName { get; set; } = "";
    public string ContentType { get; set; } = "";
    
    public long Size { get; set; }
    
    public string CheckSum { get; set; } = "";
    
    public int UploadedBy { get; set; }
    
    public DateTime CreatedDate { get; set; }
}
