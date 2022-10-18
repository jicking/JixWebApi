using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JixWebApi.Core.Services;

public interface IStorageService {
	Task<string> UploadFileAsync(IFormFile file, string containerName, string fileName);
	Task<string> UploadFileDemoAsync(IFormFile file, string metaid);
}

public class StorageServiceOptions {
	public const string SectionName = "AzureBlobStorage";
	public string ConnectionString { get; set; } = String.Empty;
	public string ContainerName { get; set; } = String.Empty;
}

public class StorageService : IStorageService {
	private readonly ILogger<StorageService> _logger;
	private readonly StorageServiceOptions _options;

	public StorageService(
		IOptions<StorageServiceOptions> options,
		ILogger<StorageService> logger
		) {
		_logger = logger;
		_options = options.Value;
	}

	public async Task<string> UploadFileAsync(IFormFile file, string containerName, string fileName) {
		BlobServiceClient blobServiceClient = new BlobServiceClient(_options.ConnectionString);
		BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

		// Create blob container
		if (!await containerClient.ExistsAsync()) {
			containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName, Azure.Storage.Blobs.Models.PublicAccessType.Blob);
			_logger.LogInformation($"created blob container : {containerName}");
		}
		BlobClient blobClient = containerClient.GetBlobClient(fileName);

		// upload file
		using Stream uploadFileStream = file.OpenReadStream();
		uploadFileStream.Position = 0;
		var res = await blobClient.UploadAsync(uploadFileStream, true);
		uploadFileStream.Close();

		var url = GetBlobUrl(blobServiceClient.AccountName, containerName, fileName);
		return url;
	}

	public static string GetBlobUrl(string account, string container, string fileName) {
		// https://<storage_account_name>.blob.core.windows.net/<container_name>/<blob_name>
		return $"https://{account}.blob.core.windows.net/{container}/{fileName}";
	}

	public static string GenerateImageFileName(string rawFileName, string metaid) {
		rawFileName = rawFileName.Trim();
		string fileExtension = Path.GetExtension(rawFileName);
		return $"{metaid}-{Guid.NewGuid()}{fileExtension}";
	}

	public async Task<string> UploadFileDemoAsync(IFormFile file, string metaid) {
		string containerName = _options.ContainerName;
		string fileName = GenerateImageFileName(file.FileName, metaid);
		return await UploadFileAsync(file, containerName, fileName);
	}
}