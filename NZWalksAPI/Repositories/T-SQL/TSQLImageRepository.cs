using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalksAPI.Repositories.T_SQL
{
    public class TSQLImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly NZWalksDBContext _dBContext;

        public TSQLImageRepository(IWebHostEnvironment environment, IHttpContextAccessor contextAccessor,
            NZWalksDBContext dBContext)
        {
            _environment = environment;
            _contextAccessor = contextAccessor;
            _dBContext = dBContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(_environment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            // Upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // https://localhost:1234/images/image.jpg
            var urlFilePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}" +
                $"{_contextAccessor.HttpContext.Request.PathBase}/images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            //Add image to images table
            await _dBContext.Images.AddAsync(image);
            await _dBContext.SaveChangesAsync();

            return image;
        }
    }
}
