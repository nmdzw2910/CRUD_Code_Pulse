using Amazon.S3;
using Amazon.S3.Model;
using CodePulse.API.Services;

public class S3Service : IS3Service
{
    private const string bucketName = "nmd-code-pulse";
    private readonly IAmazonS3 s3Client;

    public S3Service(IAmazonS3 s3Client)
    {
        this.s3Client = s3Client;
    }

    public async Task<string> UploadImageToS3(IFormFile file, string path)
    {
        var key = $"{path}{file.FileName}";

        var putObjectRequest = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = key,
            InputStream = file.OpenReadStream(),
            ContentType = file.ContentType
        };

        await s3Client.PutObjectAsync(putObjectRequest);

        return key;
    }
}
