using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHost;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHost, IHttpContextAccessor httpContextAccessor,
            NZWalksDbContext dbContext)
        {
            this.webHost = webHost;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<Image> UploadImage(Image image)
        {
            var localFilePath = Path.Combine(webHost.ContentRootPath, 
                "Images",$"{ image.imageName}{ image.imageExtension}");

            //upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            //now we have the image in images folder


            //this path will look like this: https://localhost:portNo/images/image.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.imageName}{image.imageExtension}";
            //this url will be uploaded to the table and returned to the client

            image.imagePath = urlFilePath;

            //add image to images table, save to db
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
