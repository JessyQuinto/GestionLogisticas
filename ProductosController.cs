using BackendApiLogistica.Data.Models;
using BackendApiLogistica.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _productoRepository;

    public ProductosController(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    // Obtener todos los productos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        return Ok(await _productoRepository.GetAllAsync());
    }

    // Obtener un producto por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetProductoById(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        if (producto == null)
        {
            return NotFound();
        }
        return producto;
    }

    // Crear un nuevo producto
    [HttpPost]
    public async Task<ActionResult<Producto>> PostProducto([FromBody] Producto producto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var nuevoProducto = await _productoRepository.CreateAsync(producto);
        return CreatedAtAction(nameof(GetProductoById), new { id = nuevoProducto.ProductoID }, nuevoProducto);
    }

    // Actualizar un producto existente
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProducto(int id, [FromBody] Producto producto)
    {
        if (id != producto.ProductoID || !ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            await _productoRepository.UpdateAsync(producto);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ProductoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // Eliminar un producto
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        if (producto == null)
        {
            return NotFound();
        }

        await _productoRepository.DeleteAsync(id);

        return NoContent();
    }

    private async Task<bool> ProductoExists(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        return producto != null;
    }
}