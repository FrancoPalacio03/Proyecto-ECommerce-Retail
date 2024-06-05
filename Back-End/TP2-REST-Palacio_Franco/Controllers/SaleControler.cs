using Application.Exceptions;
using Application.Interfaces.sale;
using Application.Request;
using Application.Responce;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace _TP2_REST_Palacio_Franco.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SaleControler : Controller
{
    private readonly ISaleService _saleService;
    public SaleControler(ISaleService saleService)
    {
        _saleService = saleService;
    }


    [HttpGet]
    [ProducesResponseType(typeof(ProductGetResponse), 201)]
    [ProducesResponseType(typeof(ApiError), 409)]
    [ProducesResponseType(typeof(ApiError), 404)]
    public async Task<ActionResult> GetListProducts(DateTime? from, DateTime? to)
    {
        try
        {
            var result = await _saleService.GetAll(from, to);
            return new JsonResult(result) { StatusCode = 200 };
        }
        catch (Exception ex)
        {
            return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 };
        }
    }

    [ProducesResponseType(typeof(ProductGetResponse), 201)]
    [ProducesResponseType(typeof(ApiError), 409)]
    [ProducesResponseType(typeof(ApiError), 404)]
    [HttpPost]
    public async Task<IActionResult> CreateSale(SaleRequest request)
    {
        try
        {
            var result = await _saleService.CreateSale(request);
            return new JsonResult(result) { StatusCode = 201 };
        }
        catch (Exception ex)
        {
            return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 };
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductGetResponse), 201)]
    [ProducesResponseType(typeof(ApiError), 409)]
    [ProducesResponseType(typeof(ApiError), 404)]
    public async Task<ActionResult> GetProductById(int id)
    {
        try
        {
            var result = await _saleService.GetSaleById(id);
            return new JsonResult(result) { StatusCode = 200 };
        }
        catch (ExceptionNotFound ex)
        {
            return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 };
        }
    }
}
