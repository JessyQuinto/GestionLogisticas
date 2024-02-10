using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendApiLogistica.Data.Models;
using BackendApiLogistica.Repositories.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class BodegasController : ControllerBase
{
    private readonly IBodegaRepository _bodegaRepository;

    public BodegasController(IBodegaRepository bodegaRepository)
    {
        _bodegaRepository = bodegaRepository;
    }

    // GET: api/Bodegas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bodega>>> GetBodegas()
    {
        var bodegas = await _bodegaRepository.GetAllAsync();
        return Ok(bodegas);
    }

    // GET: api/Bodegas/5
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

    // POST: api/Bodegas
    [HttpPost]
    public async Task<ActionResult<Bodega>> PostBodega(Bodega bodega)
    {
        await _bodegaRepository.CreateAsync(bodega);
        return CreatedAtAction(nameof(GetBodega), new { id = bodega.BodegaID }, bodega);
    }

    // PUT: api/Bodegas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBodega(int id, Bodega bodega)
    {
        if (id != bodega.BodegaID)
        {
            return BadRequest();
        }

        await _bodegaRepository.UpdateAsync(bodega);

        return NoContent();
    }

    // DELETE: api/Bodegas/5
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
}
