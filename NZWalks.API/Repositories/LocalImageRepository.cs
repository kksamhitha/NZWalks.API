using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext nzWalksDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, 
                                    IHttpContextAccessor httpContextAccessor,
                                    NZWalksDbContext nzWalksDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.nzWalksDbContext = nzWalksDbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, 
                                             "Images", $"{image.fileName}{image.fileExtension}");
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.file.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.fileName}{image.fileExtension}";
            image.filePath = urlFilePath;

            //Save the image to images table
            await nzWalksDbContext.Images.AddAsync(image);  
            await nzWalksDbContext.SaveChangesAsync();

            return image;
        }
    }
}
