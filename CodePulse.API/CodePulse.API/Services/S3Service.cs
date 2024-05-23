using Amazon.S3;
using Amazon.S3.Model;

namespace CodePulse.API.Services;

public class S3Service : IS3Service
{
    private const string BucketName = "nmd-code-pulse";
    private readonly IAmazonS3 _s3Client;

    public S3Service(IAmazonS3 s3Client)
    {
        this._s3Client = s3Client;
    }

    public async Task<string> UploadImageToS3(IFormFile file, string path)
    {
        var key = $"{path}{file.FileName}";

        var putObjectRequest = new PutObjectRequest
        {
            BucketName = BucketName,
            Key = key,
            InputStream = file.OpenReadStream(),
            ContentType = file.ContentType
        };

        await _s3Client.PutObjectAsync(putObjectRequest);

        return key;
    }
}