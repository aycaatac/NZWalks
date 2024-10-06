using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.API.Data;
using ProductService.API.Models.Domain;
using ProductService.API.Models.DTO;

namespace ProductService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppProductDbContext appProductDbContext;
        private readonly IMapper mapper;

        public ProductController(AppProductDbContext appProductDbContext, IMapper mapper)
        {
            this.appProductDbContext = appProductDbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<Product> products = appProductDbContext.Products.ToList<Product>(); //?
            return Ok(mapper.Map<List<ProductDto>>(products));
        }

        [HttpGet]
        //[Authorize]
        [Route("GetAllResponse")]
        public IActionResult GetAllResponse()
        {
            IEnumerable<Product> products = appProductDbContext.Products.ToList<Product>(); //?
            ResponseDto resp = new()
            {
                IsSuccess = true,
                Result = mapper.Map<List<ProductDto>>(products)
            };
            return Ok(resp);
        }

        [HttpGet]
        [Authorize]
        [Route("GetById/{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            Product product = appProductDbContext.Products.FirstOrDefault(x => x.ProductId == id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProductDto>(product));
        }


        [HttpGet]
        [Authorize]
        [Route("GetByName/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            Product product = appProductDbContext.Products.FirstOrDefault(x => x.Name == name);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("Create")]
        public IActionResult Create([FromBody] InputProductDto productDto)
        {
            var productDomainModel = mapper.Map<Product>(productDto);
            appProductDbContext.Products.Add(productDomainModel);
            appProductDbContext.SaveChanges();

            return Ok(mapper.Map<ProductDto>(productDomainModel));

        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        [Route("Update/{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] InputProductDto productDto)
        {
            var productDomainModel = appProductDbContext.Products.FirstOrDefault(x => x.ProductId == id);
            if(productDomainModel == null)
            {
                return NotFound();
            }
            productDomainModel.Name = productDto.Name;
            productDomainModel.Price = productDto.Price;
            productDomainModel.Description = productDto.Description;
            productDomainModel.CategoryName = productDto.CategoryName;
            productDomainModel.ImageUrl = productDto.ImageUrl;
            appProductDbContext.SaveChanges();
            return Ok(mapper.Map<ProductDto>(productDomainModel));
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("Delete/{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var productDomainModel = appProductDbContext.Products.FirstOrDefault(x => x.ProductId == id);
            if (productDomainModel == null)
            {
                return NotFound();
            }
            appProductDbContext.Products.Remove(productDomainModel);
            appProductDbContext.SaveChanges();
            return Ok(mapper.Map<ProductDto>(productDomainModel));
        }
    }
}
