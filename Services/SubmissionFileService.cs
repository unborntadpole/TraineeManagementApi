namespace TraineeManagementApi.Services;

using TraineeManagementApi.db;
using TraineeManagementApi.DTO;
using System.Security.Cryptography;
using TraineeManagementApi.Models;

public class SubmissionFileService
{
    private readonly ILogger<SubmissionFileService> _logger;
    private readonly SubmissionFileRepository _repository;

    private readonly IFileStorageService _fileStorageService;

    public SubmissionFileService(SubmissionFileRepository repository, ILogger<SubmissionFileService> logger, IFileStorageService fileStorageService)
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
        // if (file == null || file.Length == 0)
        // {
        //     _logger.LogWarning("Save file failed: File is empty");
        //     return Result<PostFileResponse>.ServerError("File cannot be empty", 400);
        // }
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

    public async Task<Result<DownloadFileDTO>> GetFile(long submissionFileId)
    {
        SubmissionFile? fileMetadata = await _repository.GetByIdAsync(submissionFileId);
        if (fileMetadata == null)
        {
            return Result<DownloadFileDTO>.ServerError($"File with id {submissionFileId} not found", 404);
        }
        var res = await _fileStorageService.OpenReadAsync(fileMetadata.GeneratedStorageName);
        if (!res.IsSuccess)
        {
            return Result<DownloadFileDTO>.ServerError(res.Error,res.ErrorCode);
        }
        return Result<DownloadFileDTO>.Success(new DownloadFileDTO(res.Value, fileMetadata.ContentType, fileMetadata.OriginalFileName));
    }

    public async Task<Result<string>> DeleteFile(long id)
    {
        SubmissionFile? fileMetadata = await _repository.GetByIdAsync(id);
        if (fileMetadata == null)
        {
            return Result<string>.ServerError($"File with id {id} not found", 404);
        }
        try
        {
            Result<string> res = await _fileStorageService.DeleteAsync(fileMetadata.GeneratedStorageName);
            if (res.IsSuccess)
            {
                string name = fileMetadata.OriginalFileName;
                await _repository.DeleteAsync(fileMetadata);
                return Result<string>.Success($"Deleted file {name} Successfully.");
            }
            else
            {
                return res;
            }
        }
        catch( Exception e)
        {
            return Result<string>.ServerError(e.Message, 500);
        }
    }
}