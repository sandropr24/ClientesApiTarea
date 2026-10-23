using ClientesApi.Data;
using ClientesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ClientesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;


        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Producto>>> Listar()
        {
            var productos = await _context.Productos.AsNoTracking().ToListAsync();
            return Ok(productos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cliente>> ObtenerPorId(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {

                return NotFound(new { mensaje = $"No existe el producto con ID: {id}" });
            }

            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> Registrar([FromBody] Producto producto)
        {


            producto.Id = 0;



            if (await _context.Productos.AnyAsync(c => c.NumSerie == producto.NumSerie))
            {

                return Conflict(new { mensaje = "Ya existe un producto con ese numero de serie" });
            }



            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id }, producto);
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Actualizar(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest(new { mensaje = "El ID del URL no coincide con el ID del cuerpo" });
            }

            var existente = await _context.Productos.FindAsync(id); 

            if (existente == null)
            {
                return NotFound(new { mensaje = $"No existe producto con id: {id}" });
            }

            if (await _context.Productos.AnyAsync(c => c.NumSerie == producto.NumSerie && c.Id != id))
            {
                return Conflict(new { mensaje = "Ya existe un producto con ese DNI" });
            }


            existente.Nombre = producto.Nombre;
            existente.Descripcion = producto.Descripcion;
            existente.Precio = producto.Precio;
            existente.Garantia = producto.Garantia;
            existente.NumSerie = producto.NumSerie;


            await _context.SaveChangesAsync(); 

            return NoContent();


        }



        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound(new { mensaje = $"No existe producto con ID: {id}" });
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
