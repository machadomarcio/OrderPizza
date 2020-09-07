using Microsoft.AspNetCore.Mvc;
using OrderPizza.Data.Repositories;
using OrderPizza.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderPizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlavorController : ControllerBase
    {
        private readonly IRepository _repository;

        public FlavorController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<FlavorController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllFlavors());
        }

        // POST api/<FlavorController>
        [HttpPost]
        public IActionResult Post(Flavor flavor)
        {
            _repository.Add(flavor);

            if (_repository.SaveChanges())
                return Ok(flavor);

            return BadRequest("Não foi possível cadastrar um novo sabor");
        }

        // PUT api/<FlavorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Flavor flavor)
        {
            var flavorRegistered = _repository.GetFlavorById(id);

            if (flavorRegistered == null)
                return BadRequest("Sabor não encontrado!");

            flavor.Id = flavorRegistered.Id;
            _repository.Update(flavor);

            if (_repository.SaveChanges())
                return Ok(flavor);

            return BadRequest("Não foi possível editar o sabor");
        }

        // DELETE api/<FlavorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var flavorRegistered = _repository.GetFlavorById(id);

            if (flavorRegistered == null)
                return BadRequest("Sabor não encontrado!");

            _repository.Delete(flavorRegistered);

            if (_repository.SaveChanges())
                return Ok();

            return BadRequest("Não foi possível excluir o sabor");
        }
    }
}
