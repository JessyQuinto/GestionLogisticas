using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendApiLogistica.Data.Models; // Asegúrate de que el namespace sea correcto
using BackendApiLogistica.Repositories;

[Route("api/[controller]")]
[ApiController]
public class PuertosController : ControllerBase
{
    private readonly IPuertoRepository _puertoRepository;

    public PuertosController(IPuertoRepository puertoRepository)
    {
        _puertoRepository = puertoRepository;
    }

    // GET: api/Puertos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Puerto>>> GetPuertos()
    {
        return Ok(await _puertoRepository.GetAllAsync());
    }

    // GET: api/Puertos/5
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

    // POST: api/Puertos
    [HttpPost]
    public async Task<ActionResult<Puerto>> PostPuerto(Puerto puerto)
    {
        await _puertoRepository.CreateAsync(puerto);
        return CreatedAtAction("GetPuerto", new { id = puerto.PuertoID }, puerto);
    }

    // PUT: api/Puertos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPuerto(int id, Puerto puerto)
    {
        if (id != puerto.PuertoID)
        {
            return BadRequest();
        }

        await _puertoRepository.UpdateAsync(puerto);

        return NoContent();
    }

    // DELETE: api/Puertos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePuerto(int id)
    {
        await _puertoRepository.DeleteAsync(id);
        return NoContent();
    }
}
