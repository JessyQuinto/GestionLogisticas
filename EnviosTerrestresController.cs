using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendApiLogistica.Data.Models;
using BackendApiLogistica.Repositories.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class EnviosTerrestresController : ControllerBase
{
    private readonly IEnvioTerrestreRepository _envioTerrestreRepository;

    public EnviosTerrestresController(IEnvioTerrestreRepository envioTerrestreRepository)
    {
        _envioTerrestreRepository = envioTerrestreRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EnvioTerrestre>>> GetEnviosTerrestres()
    {
        var envios = await _envioTerrestreRepository.GetAllAsync();
        return Ok(envios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EnvioTerrestre>> GetEnvioTerrestre(int id)
    {
        var envio = await _envioTerrestreRepository.GetByIdAsync(id);

        if (envio == null)
        {
            return NotFound();
        }

        return Ok(envio);
    }

    [HttpPost]
    public async Task<ActionResult<EnvioTerrestre>> PostEnvioTerrestre(EnvioTerrestre envioTerrestre)
    {
        var nuevoEnvio = await _envioTerrestreRepository.CreateAsync(envioTerrestre);
        return CreatedAtAction(nameof(GetEnvioTerrestre), new { id = nuevoEnvio.EnvioTerrestreID }, nuevoEnvio);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEnvioTerrestre(int id, EnvioTerrestre envioTerrestre)
    {
        if (id != envioTerrestre.EnvioTerrestreID)
        {
            return BadRequest();
        }

        await _envioTerrestreRepository.UpdateAsync(envioTerrestre);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEnvioTerrestre(int id)
    {
        var envio = await _envioTerrestreRepository.GetByIdAsync(id);
        if (envio == null)
        {
            return NotFound();
        }

        await _envioTerrestreRepository.DeleteAsync(id);

        return NoContent();
    }
}
