using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Repository;
using ProAgil.Domain;

namespace ProAgil.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController: ControllerBase
    {
        private readonly IProAgilRepository _repo;
        public PalestranteController(IProAgilRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{PalestranteId}")]
        public async Task<IActionResult> Get(int PalestranteId)
        {
           try
           {
               var results = await _repo.GetPalestranteAsync(PalestranteId, true);

               return Ok(results);
           }
           catch (System.Exception)
           {               
               return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
           }           
        }

        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> Get(string nome)
        {
           try
           {
               var results = await _repo.GetAllPalestrantesAsyncByName(nome, true);

               return Ok(results);
           }
           catch (System.Exception)
           {               
               return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
           }           
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
           try
           {
               _repo.Add(model);

               if (await _repo.SaveChangesAsync())
               {
                   return Created($"/api/palestrante/{model.Id}", model);
               }               
           }
           catch (System.Exception)
           {               
               return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
           }          

           return BadRequest(""); 
        }

        [HttpPut]
        public async Task<IActionResult> Put(int Id, Palestrante model)
        {
           try
           {
                var palestrante = await _repo.GetPalestranteAsync(Id, false);

                if (palestrante == null) return NotFound();

               _repo.Update(model);

               if (await _repo.SaveChangesAsync())
               {
                   return Created($"/api/palestrante/{model.Id}", model);
               }               
           }
           catch (System.Exception)
           {               
               return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
           }          

           return BadRequest(""); 
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
           try
           {
                var palestrante = await _repo.GetPalestranteAsync(Id, false); 

                if (palestrante == null) return NotFound();

               _repo.Delete(palestrante);

               if (await _repo.SaveChangesAsync())
               {
                   return Ok();
               }               
           }
           catch (System.Exception)
           {               
               return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
           }          

           return BadRequest(""); 
        }
    }
}