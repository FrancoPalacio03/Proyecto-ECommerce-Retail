using Application.Interfaces.product;
using Application.Request;
using Application.Responce;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace _TP2_REST_Palacio_Franco.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductControler : Controller
{
    private readonly IProductService _productService;
    public ProductControler(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProductGetResponse), 201)]
    [ProducesResponseType(typeof(ApiError), 409)]
    public async Task<IActionResult> GetListProducts([FromQuery] int[]? categories, string? name, int limit = 0, int offset = 0)
    {
        try
        {
            var result = await _productService.GetAll(name, limit, offset, categories);
            return new JsonResult(result) { StatusCode = 200 };
        }
        catch (Exception ex)
        {
            return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 };
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductGetResponse), 201)]
    [ProducesResponseType(typeof(ApiError), 409)]
    public async Task<IActionResult> CreateProduct(ProductRequest request)
    {
        try
        {
            var result = await _productService.CreateProduct(request);
            return new JsonResult(result) { StatusCode = 200 };
        }
        catch (Conflict ex)
        {

            return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 409 };
        }
    }
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductGetResponse), 201)]
    [ProducesResponseType(typeof(ApiError), 409)]
    [ProducesResponseType(typeof(ApiError), 404)]
    public async Task<ActionResult> GetProductById(Guid id)
    {
        try
        {
            var result = await _productService.GetProductById(id);
            return new JsonResult(result) { StatusCode = 200 };
        }
        catch (ExceptionNotFound ex)
        {
            return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 };
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductGetResponse), 201)]
    [ProducesResponseType(typeof(ApiError), 409)]
    public async Task<ActionResult> UpdateProduct(Guid id, ProductRequest request)
    {
        try
        {
            var result = await _productService.UpdateProduct(id,request);
            return new JsonResult(result) { StatusCode = 200 };
        }
        catch (ExceptionNotFound ex)
        {
            return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 };
        }
    }

    [HttpDelete]
    [ProducesResponseType(typeof(ProductGetResponse), 201)]
    [ProducesResponseType(typeof(ApiError), 409)]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var result = await _productService.DeleteProduct(id);
            return new JsonResult(result) { StatusCode = 200 };
        }
        catch (ExceptionNotFound ex)
        {
            return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 };
        }
    }
   
}
