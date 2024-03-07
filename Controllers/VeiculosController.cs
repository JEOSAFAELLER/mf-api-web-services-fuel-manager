using mf_api_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_api_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VeiculosController (AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult>  GetAll()
        {
            var model = await _context.Veiculos.ToListAsync();
            return Ok(model);
        }
        [HttpPost]
        public async Task<ActionResult> Create(Veiculo model)
        {
            //criar validação
            if(model.AnoFabricacao <=0 || model.AnoModelo <= 0)
            {
                return BadRequest(new {message = "Ano de Fabricação e Ano Modelo são obrigatórios e devem ser maiores que zero"});
            }

            _context.Veiculos.Add(model);
            await _context.SaveChangesAsync();
            
            //metodo 201 nome do metodo , rota e modelo

            return CreatedAtAction("GetById" , new { id  = model.Id }, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Veiculos
                .Include(t=>t.Consumos)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (model == null) return NotFound();

            GerarLinks(model);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Veiculo model)
        {
            //validação
            if (id != model.Id) return BadRequest();
            //colocar o as notraking para funcionar
            var modeloDB = await _context.Veiculos.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if(modeloDB == null) return NotFound();

            _context.Veiculos.Update(model);
            await _context.SaveChangesAsync();
            
            //ja passou a atualização e não espera receber nada 204
            return NoContent();
           
        }

        //HttpDelete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) 
        {
            var model = await _context.Veiculos.FindAsync(id);

            if (model == null) return NotFound();

            _context.Veiculos.Remove(model);
            await _context.SaveChangesAsync();

            return Ok(model);

        }

        private void GerarLinks(Veiculo model)
        {
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "self", metodo: "GET"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "update", metodo: "PUT"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "delete", metodo: "DELETE"));
         
        }





    }

}
