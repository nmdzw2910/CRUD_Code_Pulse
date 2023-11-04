namespace CodePulse.API.Services
{
    public interface IS3Service
    {
        Task<string> UploadImageToS3(IFormFile file, string path);
    }
}
