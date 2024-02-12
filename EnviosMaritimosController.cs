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

    // Inyección del repositorio de envíos marítimos
    public EnviosMaritimosController(IEnvioMaritimoRepository envioMaritimoRepository)
    {
        _envioMaritimoRepository = envioMaritimoRepository;
    }

    // Lista todos los envíos marítimos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EnvioMaritimo>>> GetEnviosMaritimos()
    {
        var envios = await _envioMaritimoRepository.GetAllAsync();
        return Ok(envios);
    }

    // Devuelve un envío marítimo específico
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

    // Crea un nuevo envío marítimo
    [HttpPost]
    public async Task<ActionResult<EnvioMaritimo>> PostEnvioMaritimo(EnvioMaritimo envioMaritimo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var nuevoEnvio = await _envioMaritimoRepository.CreateAsync(envioMaritimo);
        return CreatedAtAction(nameof(GetEnvioMaritimo), new { id = nuevoEnvio.EnvioMaritimoID }, nuevoEnvio);
    }

    // Actualiza un envío marítimo existente
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEnvioMaritimo(int id, EnvioMaritimo envioMaritimo)
    {
        if (id != envioMaritimo.EnvioMaritimoID)
        {
            return BadRequest();
        }

        try
        {
            await _envioMaritimoRepository.UpdateAsync(envioMaritimo);
        }
        catch (System.Exception)
        {
            if (await _envioMaritimoRepository.GetByIdAsync(id) == null)
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

    // Elimina un envío marítimo por su ID
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
