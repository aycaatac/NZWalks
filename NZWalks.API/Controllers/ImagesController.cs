using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
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

        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage([FromForm] InputImageDto imageDto)
        {
            validateFileUpload(imageDto);
            if (ModelState.IsValid)
            {
                //use repo to upload image
                var imageDomainModel = new Image
                {
                    File = imageDto.File,
                    imageExtension = Path.GetExtension(imageDto.File.FileName),
                    imageSizeInBytes = imageDto.File.Length,
                    imageName = imageDto.imageName,
                    imageDescription = imageDto.imageDescription
                    //file path??
                };

                //send domain model to repo
                await imageRepository.UploadImage(imageDomainModel); //is file path nullable because it is null while passing this?????? does it only matter when we are posting it to the db??

                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);

        }

        private void validateFileUpload(InputImageDto image)
        {
            //validate request by extension and size
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(image.File.FileName);
            if (!allowedExtensions.Contains(Path.GetExtension(image.File.FileName)))
            {
                ModelState.AddModelError("file", "File extension is unsupported!");
            }
            else
            {
                if(image.File.Length > 10485760)
                {
                    ModelState.AddModelError("file", "File is too large! Please upload a smaller file!");
                }
            }
        }

    }
}
