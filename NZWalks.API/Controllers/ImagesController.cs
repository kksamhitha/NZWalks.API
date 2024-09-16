using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        // Post : /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image();
                imageDomainModel = new Image
                {
                    file = request.file,
                    fileExtension = Path.GetExtension(request.file.FileName),
                    fileSizeInBytes = request.file.Length,
                    fileName = request.file.FileName,
                    fileDescription = request.fileDescription,
                };
                //upload image using repository
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpeg", ".jpg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.file.FileName)))
            {
                ModelState.AddModelError("file", "unsupported file extension");
            }
            if(request.file.Length > 10485760) 
            {
                ModelState.AddModelError("file", "file size is more than 10MB. Supported size 10MB");
            }
        }

    }
}
