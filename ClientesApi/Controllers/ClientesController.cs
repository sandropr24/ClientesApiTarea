using ClientesApi.Data;
using ClientesApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientesApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        //Todas las funcionalidades GET - POST - PUT - DELETE necesitan acceso al DB
        private readonly AppDbContext _context;

        //Contructor
        public ClientesController(AppDbContext context) 
        { 
            _context = context;
        }

        //GET: api/clientes => retornar la lista de clientes
        //async Task     :asincrono(tiene latencia)
        //ActionResult   : Valor a obtener (coleccion definida en el modelo)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Listar()
        {
            var clientes = await _context.Clientes.AsNoTracking().ToListAsync();
            return Ok(clientes);
        }

        //GET
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cliente>> ObtenerPorId(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if(cliente == null)
            {
                //Error 404
                return NotFound(new { mensaje = $"No existe el cliente con ID: {id}" });
            }

            //OK (metodo 200)
            return Ok(cliente);
        }

        //POST : api/cliente -> registrar (No olvidar que debemos enviar en el BODY JSON {})
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Cliente>> Registrar([FromBody] Cliente cliente)
        {
            //Evitar que el cliente envie u ID manualmente

            cliente.Id = 0;


            //Validar que el DNI no este repetido.....
            if(await _context.Clientes.AnyAsync(c => c.Dni == cliente.Dni))
            {   
                //Server code 409 (error)
                return Conflict(new {mensaje = "Ya existe un cliente con ese DNI"});
            }


            //procedemos a registro
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            //Server code (201) (Save ok) - Ejecutamos en segundo plano el buscador por ID , pasamos el ID entrada , modelo
            return CreatedAtAction(nameof(ObtenerPorId), new {id = cliente.Id}, cliente);
        }

        //PUT : api/clientes/1 -> actualizar (lleva un JSON en el BODY)
        [Authorize]
        [HttpPut("{id:int}")]

        public async Task<ActionResult> Actualizar(int id, [FromBody] Cliente cliente)
        {
            ///Validacion 1: Coincidencia de ID´s
            if(id != cliente.Id)
            {   
                //Server code 400 (error)
                return BadRequest(new {mensaje = "El ID del URL no coincide con el ID del cuerpo"});
            }

            //Validación 2 : ¿Existe el ID?
            var existente = await _context.Clientes.FindAsync(id); //FindAsync () buscador por ID

            if(existente == null)
            {
                return NotFound(new {mensaje = $"No existe cliente con id: {id}"});
            }

            //Validacion 3: Conflicto DNI repetido
            if (await _context.Clientes.AnyAsync(c => c.Dni == cliente.Dni && c.Id != id))
            {
                //Server code 409 (error)
                return Conflict(new { mensaje = "Ya existe un cliente con ese DNI" });
            }


            //Procedemos a realizar actualización......
            existente.Apellidos = cliente.Apellidos;
            existente.Nombres = cliente.Nombres;
            existente.Telefono = cliente.Telefono;
            existente.Correo = cliente.Correo;
            existente.Dni = cliente.Dni;
            existente.Direccion = cliente.Direccion;


            //Ahora confirmemos los cambiso utilizando el contenedor "existente"
            await _context.SaveChangesAsync(); //esta enlazado a <<<FinsAsync>>>

            //Server Code 204
            return NoContent();


        }


        //DELETE: api/clientes/1  -> eliminar
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {   
                //Server Code 404
                return NotFound(new { mensaje = $"No existe cliente con ID: {id}" });
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            //Server Code 204 (accion ejecutada correctamente)
            return NoContent();
        }

    }
}
