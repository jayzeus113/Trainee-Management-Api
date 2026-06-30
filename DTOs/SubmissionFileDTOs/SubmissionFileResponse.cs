namespace TraineeManagement.DTOs;
 
public class SubmissionFileResponse
{
    public int Id { get; set; }
    public int SubmissionId { get; set; }
    public string OriginalFileName { get; set; } = null!;
    public string GeneratedStorageName {get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public long Size { get; set; }
    public string CheckSum { get; set; } = null!;
    public string UploadedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
}
 