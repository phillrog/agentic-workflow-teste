using Microsoft.AspNetCore.Mvc;
using Domain;
using Application;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _service = new();

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (!_service.ValidateProduct(product))
            return BadRequest("Invalid product data.");
            
        return Ok(product);
    }
}