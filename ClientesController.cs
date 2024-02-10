using BackendApiLogistica.Data.Models;
using BackendApiLogistica.Repositories.Interfaces;
using BackendApiLogistica.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


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

    // Implementa aquí los demás métodos (POST, GET por ID, PUT, DELETE)
}
