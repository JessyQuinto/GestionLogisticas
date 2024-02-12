using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendApiLogistica.Data.Models;
using BackendApiLogistica.Repositories.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class PuertosController : ControllerBase
{
    private readonly IPuertoRepository _puertoRepository;

    public PuertosController(IPuertoRepository puertoRepository)
    {
        _puertoRepository = puertoRepository;
    }

    // Obtiene lista de puertos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Puerto>>> GetPuertos()
    {
        return Ok(await _puertoRepository.GetAllAsync());
    }

    // Obtiene un puerto por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Puerto>> GetPuerto(int id)
    {
        var puerto = await _puertoRepository.GetByIdAsync(id);

        if (puerto == null)
        {
            return NotFound();
        }

        return puerto;
    }

    // Crea un puerto
    [HttpPost]
    public async Task<ActionResult<Puerto>> PostPuerto(Puerto puerto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _puertoRepository.CreateAsync(puerto);
        return CreatedAtAction(nameof(GetPuerto), new { id = puerto.PuertoID }, puerto);
    }

    // Actualiza un puerto
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPuerto(int id, Puerto puerto)
    {
        if (id != puerto.PuertoID || !ModelState.IsValid)
        {
            return BadRequest();
        }

        await _puertoRepository.UpdateAsync(puerto);

        return NoContent();
    }

    // Elimina un puerto
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePuerto(int id)
    {
        var existingPuerto = await _puertoRepository.GetByIdAsync(id);
        if (existingPuerto == null)
        {
            return NotFound();
        }

        await _puertoRepository.DeleteAsync(id);
        return NoContent();
    }
}
