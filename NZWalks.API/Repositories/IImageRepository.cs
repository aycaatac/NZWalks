﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IImageRepository
    {
        public Task<Image> UploadImage(Image image);
    }
}
