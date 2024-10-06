﻿using System.ComponentModel.DataAnnotations;

namespace ProductService.API.Models.DTO
{
    public class ProductDto
    {
        
        public int ProductId { get; set; }
        
        public string Name { get; set; }
        
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
