using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // Post : /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        //public async Task<IActionResult> Upload([FromForm]ImageUploadRequestDto request)
        //{
        //    ValidateFileUpload(request);    

        //}
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
