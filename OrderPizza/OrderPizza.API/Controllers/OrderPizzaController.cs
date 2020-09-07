using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderPizza.Data.Repositories;
using OrderPizza.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderPizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPizzaController : ControllerBase
    {
        private readonly IRepository _repository;

        public OrderPizzaController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<OrderPizzaController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllOrders());
        }

        // GET api/<OrderPizzaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = _repository.GetOrderById(id);

            if (order == null)
                return BadRequest("Pedido não encontrado!");
            return Ok(order);
        }

        // POST api/<OrderPizzaController>
        [HttpPost]
        public IActionResult Post(Order order)
        {
            _repository.Add(order);

            if (_repository.SaveChanges())
                return Ok(order);

            return BadRequest("Não foi possível cadastrar o pedido");
        }

        // PUT api/<OrderPizzaController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Order order)
        {
            var orderRegistered = _repository.GetOrderById(id);

            if (orderRegistered == null)
                return BadRequest("Pedido não encontrado!");

            order.Id = orderRegistered.Id;
            _repository.Update(order);

            if (_repository.SaveChanges())
                return Ok(order);

            return BadRequest("Não foi possível editar o pedido");
        }

        // DELETE api/<OrderPizzaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var orderRegistered = _repository.GetOrderById(id);

            if (orderRegistered == null)
                return BadRequest("Pedido não encontrado!");

            _repository.Delete(orderRegistered);

            if (_repository.SaveChanges())
                return Ok();

            return BadRequest("Não foi possível  excluir o pedido");
        }
    }
}
