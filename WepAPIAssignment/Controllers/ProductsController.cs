using AutoMapper;
using Core.entites;
using Core.Interfaces;
using Core.Specifiactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WepAPIAssignment.Dtos;
using WepAPIAssignment.Helpers;
using WepAPIAssignment.ResponseModule;

namespace WepAPIAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenricRepo<Product> productRepo;
        private readonly IGenricRepo<ProductBrand> brandRepo;
        private readonly IGenricRepo<ProductType> productType;
        private readonly IMapper mapper;

        public ProductsController(IGenricRepo<Product> _productRepo,IGenricRepo<ProductBrand> BrandRepo,
            IGenricRepo<ProductType> _ProductType, IMapper _mapper)
        {
            productRepo = _productRepo;
            brandRepo = BrandRepo;
            productType = _ProductType;
            mapper = _mapper;
        }

        [HttpGet("GetProducts")]

        public async Task<ActionResult<Pagination<ProductDTO>>> GetProductsAsync([FromQuery] SpecifiactionsParams specifiactionsParams)
        {
            var spec = new ProductWithBrandAndTypeSpecifiactions(specifiactionsParams);
            var countSpec = new ProductWithCountSpecifiactions(specifiactionsParams);
            var totalCount = await productRepo.CountAsync(countSpec);

            var products = await productRepo.ListUnderSpecifications(spec);
            var mappedProducts = mapper.Map<IReadOnlyList<ProductDTO>>(products);
            var paginaitionData = new Pagination<ProductDTO>(specifiactionsParams.pageIndex,
                specifiactionsParams.pageSize, totalCount,mappedProducts);
            return Ok(paginaitionData);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {

            var spec = new ProductWithBrandAndTypeSpecifiactions(id);

            var product = await productRepo.GetEntitytWithSpecifiaction(spec);

            if (product == null)
                return NotFound(new ApiResponse(404));
            var mappedProduct = mapper.Map<Product,ProductDTO>(product);
            return Ok(mappedProduct);
        }


        [HttpGet("Brands")]

        public async Task<ActionResult<ProductBrand>> GetProductBrands()
        {
            return Ok(await productRepo.GetAllAsync());
        }

        [HttpGet("Types")]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {
            var types = await productRepo.GetAllAsync();
            return Ok(types);
        }

    }
}
