using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendApiLogistica.Data.Models;
using BackendApiLogistica.Repositories.Interfaces;

// Define la ruta base para los endpoints de este controlador.
[Route("api/[controller]")]
[ApiController]
public class BodegasController : ControllerBase
{
    private readonly IBodegaRepository _bodegaRepository;

    // Constructor: Inyecta el repositorio de bodegas para su uso en los endpoints.
    public BodegasController(IBodegaRepository bodegaRepository)
    {
        _bodegaRepository = bodegaRepository;
    }

    // Obtiene una lista de todas las bodegas.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bodega>>> GetBodegas()
    {
        var bodegas = await _bodegaRepository.GetAllAsync();
        return Ok(bodegas);
    }

    // Obtiene una bodega específica por ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<Bodega>> GetBodega(int id)
    {
        var bodega = await _bodegaRepository.GetByIdAsync(id);
        if (bodega == null)
        {
            return NotFound();
        }
        return Ok(bodega);
    }

    // Crea una nueva bodega.
    [HttpPost]
    public async Task<ActionResult<Bodega>> PostBodega(Bodega bodega)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _bodegaRepository.CreateAsync(bodega);
        return CreatedAtAction(nameof(GetBodega), new { id = bodega.BodegaID }, bodega);
    }

    // Actualiza una bodega existente.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBodega(int id, Bodega bodega)
    {
        if (id != bodega.BodegaID || !ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            await _bodegaRepository.UpdateAsync(bodega);
        }
        catch (System.Exception)
        {
            if (!await BodegaExists(id))
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

    // Elimina una bodega por su ID.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBodega(int id)
    {
        var bodega = await _bodegaRepository.GetByIdAsync(id);
        if (bodega == null)
        {
            return NotFound();
        }

        await _bodegaRepository.DeleteAsync(id);
        return NoContent();
    }

    // Verifica si una bodega existe por ID.
    private async Task<bool> BodegaExists(int id)
    {
        var bodega = await _bodegaRepository.GetByIdAsync(id);
        return bodega != null;
    }
}
