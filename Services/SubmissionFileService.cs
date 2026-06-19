namespace TraineeManagementApi.Services;

using TraineeManagementApi.db;
using TraineeManagementApi.DTO;
using System.Security.Cryptography;
using System.Data;
using TraineeManagementApi.Models;
using Org.BouncyCastle.Asn1;

public class SubmissionFileService
{
    private readonly ILogger<LocalFileStorageService> _logger;
    private readonly SubmissionFileRepository _repository;

    private readonly IFileStorageService _fileStorageService;

    public SubmissionFileService(SubmissionFileRepository repository, ILogger<LocalFileStorageService> logger, LocalFileStorageService fileStorageService)
    {
        _repository = repository;
        _logger = logger;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> GetChecksum(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        using var sha256 = SHA256.Create();
        byte[] hashBytes = await sha256.ComputeHashAsync(stream);
        string calculatedChecksum = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        return calculatedChecksum;        
    }

    public async Task<Result<PostFileResponse>> PostFile(IFormFile file, string user, long submissionId)
    {
        if (file == null || file.Length == 0)
        {
            _logger.LogWarning("Save file failed: File is empty");
            return Result<PostFileResponse>.ServerError("File cannot be empty", 400);
        }
        Result<string> result = await _fileStorageService.SaveAsync(file);
        if(result.IsSuccess)
        {
            string checksum = await GetChecksum(file);
            SubmissionFile saveFile = new SubmissionFile()
            {
                OriginalFileName = file.FileName,
                GeneratedStorageName = result.Value,
                Size = file.Length,
                ContentType = file.ContentType,
                Checksum = checksum,
                UploadedByUser = user,
                UploadedDate = DateTime.UtcNow,
                SubmissionId = submissionId
            };
            await _repository.AddAsync(saveFile);
            PostFileResponse response = new PostFileResponse(saveFile);
            return Result<PostFileResponse>.Success(response);
        }
        else return Result<PostFileResponse>.ServerError(result.Error, result.ErrorCode);
    }
}