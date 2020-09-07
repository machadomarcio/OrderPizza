using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderPizza.Data.Context;
using OrderPizza.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderPizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private DataContext context;

        public CustomerController(DataContext context)
        {
            this.context = context;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(context.Customers.AsNoTracking());
        }

        // GET api/<CustomerController>/5
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var customer = context.Customers.AsNoTracking().FirstOrDefault(a => a.Id == id);

            if (customer == null)
                return BadRequest("Cliente não encontrado!");
            return Ok(customer);
        }

        // GET api/<CustomerController>/5
        [HttpGet("byCpf/{cpf}")]
        public IActionResult GetByCpf(string cpf)
        {
            var customer = context.Customers.AsNoTracking().FirstOrDefault(a => a.Cpf == cpf);

            if (customer == null)
                return BadRequest("Cliente não encontrado!");
            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            context.Add(customer);
            context.SaveChanges();
            return Ok(customer);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Customer customer)
        {
            var customerRegistered = context.Customers.AsNoTracking().FirstOrDefault(a => a.Id == id);

            if (customerRegistered == null)
                return BadRequest("Cliente não encontrado!");

            customer.Id = id;
            context.Update(customer);
            context.SaveChanges();
            return Ok(customer);
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = context.Customers.FirstOrDefault(a => a.Id == id);

            if (customer == null)
                return BadRequest("Cliente não encontrado!");

            context.Remove(customer);
            context.SaveChanges();
            return Ok();
        }
    }
}
