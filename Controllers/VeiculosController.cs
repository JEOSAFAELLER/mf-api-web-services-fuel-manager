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
                .FirstOrDefaultAsync(c => c.Id == id);
            if (model == null) NotFound();

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Veiculo model)
        {
            //validação
            if (id != model.Id) return BadRequest();

            var modeloDB = await _context.Veiculos
                .FirstOrDefaultAsync(c => c.Id == id);

            if(modeloDB == null) NotFound();

            _context.Veiculos.Update(model);
            await _context.SaveChangesAsync();
            
            //ja passou a atualização e não espera receber nada
            return NoContent();
           
        }





    }

}
