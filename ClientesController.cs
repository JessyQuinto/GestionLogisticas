using BackendApiLogistica.Data.Models;
using BackendApiLogistica.Repositories.Interfaces;
using BackendApiLogistica.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IClienteRepository _clienteRepository;

    public ClientesController(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
    {
        return Ok(await _clienteRepository.GetAllAsync());
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> PostCliente([FromBody] Cliente cliente)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var nuevoCliente = await _clienteRepository.CreateAsync(cliente);
        return CreatedAtAction(nameof(GetClienteById), new { id = nuevoCliente.ClienteID }, nuevoCliente);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetClienteById(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);

        if (cliente == null)
        {
            return NotFound();
        }

        return cliente;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCliente(int id, [FromBody] Cliente cliente)
    {
        if (id != cliente.ClienteID)
        {
            return BadRequest();
        }

        try
        {
            await _clienteRepository.UpdateAsync(cliente);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClienteExists(id))
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

    private bool ClienteExists(int id)
    {
        return _clienteRepository.GetByIdAsync(id) != null;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }

        await _clienteRepository.DeleteAsync(id);

        return NoContent();
    }



}
