using BackendApiLogistica.Data.Models;
using BackendApiLogistica.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// Define la ruta base para los endpoints de este controlador, apuntando a la entidad 'Clientes'.
[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IClienteRepository _clienteRepository;

    // Constructor: Inyecta el repositorio de clientes para su uso en los endpoints.
    public ClientesController(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    // Obtiene y devuelve todos los clientes registrados.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
    {
        return Ok(await _clienteRepository.GetAllAsync());
    }

    // Crea un nuevo cliente basado en el cuerpo de la petición y lo devuelve con su ID.
    [HttpPost]
    public async Task<ActionResult<Cliente>> PostCliente([FromBody] Cliente cliente)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Valida el modelo de entrada.
        }

        var nuevoCliente = await _clienteRepository.CreateAsync(cliente);
        return CreatedAtAction(nameof(GetClienteById), new { id = nuevoCliente.ClienteID }, nuevoCliente);
    }

    // Obtiene un cliente específico por su ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetClienteById(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);

        if (cliente == null)
        {
            return NotFound(); // No se encontró el cliente solicitado.
        }

        return cliente; // Devuelve el cliente encontrado.
    }

    // Actualiza un cliente existente, identificado por su ID.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCliente(int id, [FromBody] Cliente cliente)
    {
        if (id != cliente.ClienteID || !ModelState.IsValid)
        {
            return BadRequest(); // La ID no coincide o el modelo es inválido.
        }

        try
        {
            await _clienteRepository.UpdateAsync(cliente);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ClienteExists(id))
            {
                return NotFound(); // No se encontró el cliente para actualizar.
            }
            else
            {
                throw; // Lanza la excepción de concurrencia si existe otro problema.
            }
        }

        return NoContent(); // Actualización completada con éxito.
    }

    // Verifica si existe un cliente por su ID.
    private async Task<bool> ClienteExists(int id)
    {
        return await _clienteRepository.GetByIdAsync(id) != null;
    }

    // Elimina un cliente especificado por su ID.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        if (cliente == null)
        {
            return NotFound(); // No se encontró el cliente para eliminar.
        }

        await _clienteRepository.DeleteAsync(id);

        return NoContent(); // Eliminación completada con éxito.
    }
}
