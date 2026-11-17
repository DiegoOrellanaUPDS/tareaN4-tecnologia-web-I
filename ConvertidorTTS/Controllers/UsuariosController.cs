using ConvertidorTTS.Data;
using ConvertidorTTS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConvertidorTTS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DBContext _context;
    

        public UsuariosController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        // Devuelve únicamente los usuarios con estado Activo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuariosActivos = await _context.Usuarios
                .Where(u => u.Estado == EstadoUsuario.Activo)
                .ToListAsync();

            return Ok(usuariosActivos);
        }

        // GET: api/Usuarios/5
        // Busca un usuario por ID, pero solo lo devuelve si está Activo
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id && u.Estado == EstadoUsuario.Activo);

            if (usuario == null)
            {
                return NotFound(new { mensaje = $"No se encontró un usuario activo con el ID {id}." });
            }

            return Ok(usuario);
        }

        // POST: api/Usuarios
        // Crea un nuevo usuario (por defecto nace como Activo)
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            // Aseguramos que al crearse siempre inicie como Activo
            usuario.Estado = EstadoUsuario.Activo;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // PUT: api/Usuarios/5
        // Actualiza un usuario únicamente si su estado actual es Activo
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuarioActualizado)
        {
            if (id != usuarioActualizado.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del cuerpo de la petición." });
            }

            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id && u.Estado == EstadoUsuario.Activo);

            if (usuarioExistente == null)
            {
                return NotFound(new { mensaje = $"No se encontró un usuario activo con el ID {id} para actualizar." });
            }

            // Actualizamos los campos permitidos
            usuarioExistente.Nombre = usuarioActualizado.Nombre;
            usuarioExistente.Correo = usuarioActualizado.Correo;
            usuarioExistente.Estado = usuarioActualizado.Estado;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Usuarios/5
        // Borrado lógico: cambia el estado del usuario a Inactivo
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound(new { mensaje = $"No se encontró el usuario con el ID {id}." });
            }

            if (usuario.Estado == EstadoUsuario.Inactivo)
            {
                return BadRequest(new { mensaje = "El usuario ya se encuentra en estado Inactivo." });
            }

            usuario.Estado = EstadoUsuario.Inactivo;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"El usuario con ID {id} ha pasado a estado Inactivo." });
        
        }
    }
    
}
