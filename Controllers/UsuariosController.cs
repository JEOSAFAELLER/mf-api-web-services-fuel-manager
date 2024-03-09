using mf_api_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_api_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Usuarios.ToListAsync();
            return Ok(model);
        }
        [HttpPost]
        public async Task<ActionResult> Create(UsuarioDto model)
        {

            Usuario novo = new Usuario()
            {
                Nome = model.Nome,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Perfil = model.Perfil
            };

            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            _context.Usuarios.Add(novo);
            await _context.SaveChangesAsync();

            //metodo 201 nome do metodo , rota e modelo

            return CreatedAtAction("GetById", new { id = novo.Id }, novo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Usuarios
                .FirstOrDefaultAsync(c => c.Id == id);
            if (model == null) return NotFound();

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Usuario model)
        {
            //validação
            if (id != model.Id) return BadRequest();
            //colocar o as notraking para funcionar
            var modeloDB = await _context.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (modeloDB == null) return NotFound();

            modeloDB.Nome = model.Nome;
            modeloDB.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            modeloDB.Perfil = model.Perfil;

            _context.Usuarios.Update(modeloDB);
            await _context.SaveChangesAsync();

            //ja passou a atualização e não espera receber nada 204
            return NoContent();

        }

        //HttpDelete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Usuarios.FindAsync(id);

            if (model == null) return NotFound();

            _context.Usuarios.Remove(model);
            await _context.SaveChangesAsync();

            return Ok(model);

        }

        




    }
}
