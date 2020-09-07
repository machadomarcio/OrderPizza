using Microsoft.AspNetCore.Mvc;
using OrderPizza.Data.Repositories;
using OrderPizza.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderPizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository _repository;

        public CustomerController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllCustomers());
        }

        // GET api/<CustomerController>/5
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var customer = _repository.GetCustomerById(id);

            if (customer == null)
                return BadRequest("Cliente não encontrado!");
            return Ok(customer);
        }

        // GET api/<CustomerController>/5
        [HttpGet("byCpf/{cpf}")]
        public IActionResult GetByCpf(string cpf)
        {
            var customer = _repository.GetCustomerByCpf(cpf);

            if (customer == null)
                return BadRequest("Cliente não encontrado!");
            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            _repository.Add(customer);

            if (_repository.SaveChanges())
                return Ok(customer);

            return BadRequest("Não foi possível cadastrar o cliente");
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Customer customer)
        {
            var customerRegistered = _repository.GetCustomerById(id);

            if (customerRegistered == null)
                return BadRequest("Cliente não encontrado!");

            customer.Id = customerRegistered.Id;
            _repository.Update(customer);

            if (_repository.SaveChanges())
                return Ok(customer);

            return BadRequest("Não foi possível editar o cliente");
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _repository.GetCustomerById(id);

            if (customer == null)
                return BadRequest("Cliente não encontrado!");
            
            _repository.Delete(customer);

            if (_repository.SaveChanges())
                return Ok();

            return BadRequest("Não foi possível  excluir o cliente");
        }
    }
}
