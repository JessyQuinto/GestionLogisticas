using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendApiLogistica.Data.Models;
using BackendApiLogistica.Repositories.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class EnviosMaritimosController : ControllerBase
{
    private readonly IEnvioMaritimoRepository _envioMaritimoRepository;

    public EnviosMaritimosController(IEnvioMaritimoRepository envioMaritimoRepository)
    {
        _envioMaritimoRepository = envioMaritimoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EnvioMaritimo>>> GetEnviosMaritimos()
    {
        var envios = await _envioMaritimoRepository.GetAllAsync();
        return Ok(envios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EnvioMaritimo>> GetEnvioMaritimo(int id)
    {
        var envio = await _envioMaritimoRepository.GetByIdAsync(id);

        if (envio == null)
        {
            return NotFound();
        }

        return Ok(envio);
    }

    [HttpPost]
    public async Task<ActionResult<EnvioMaritimo>> PostEnvioMaritimo(EnvioMaritimo envioMaritimo)
    {
        var nuevoEnvio = await _envioMaritimoRepository.CreateAsync(envioMaritimo);
        return CreatedAtAction(nameof(GetEnvioMaritimo), new { id = nuevoEnvio.EnvioMaritimoID }, nuevoEnvio);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEnvioMaritimo(int id, EnvioMaritimo envioMaritimo)
    {
        if (id != envioMaritimo.EnvioMaritimoID)
        {
            return BadRequest();
        }

        await _envioMaritimoRepository.UpdateAsync(envioMaritimo);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEnvioMaritimo(int id)
    {
        var envio = await _envioMaritimoRepository.GetByIdAsync(id);
        if (envio == null)
        {
            return NotFound();
        }

        await _envioMaritimoRepository.DeleteAsync(id);

        return NoContent();
    }
}
