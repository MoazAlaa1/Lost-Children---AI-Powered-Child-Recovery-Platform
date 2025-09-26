using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace LostChildrenGP.BL
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(string cloudName, string apiKey, string apiSecret)
        {
            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }
            // Use memory stream for better performance
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0; // Reset stream position

            // Create transformation with quality and format settings
            var transformation = new Transformation()
                .Quality("auto")
                .FetchFormat("auto");

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, memoryStream),
                Folder = folderName,
                PublicId = $"img_{Guid.NewGuid()}",
                Overwrite = true,
                Transformation = transformation, // Apply optimization transformations
                UseFilename = true,
                UniqueFilename = false
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString(); // Return the secure URL of the uploaded image
        }
    }

}
