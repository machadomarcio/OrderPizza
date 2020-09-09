using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;

namespace OrderPizza.Domain.Models
{
    public class Customer
    {
        public Customer()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }

        public string Cpf { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }

        public string Phone { get; set; }

        public virtual Address Address { get; set; }

        public virtual List<Order> Orders { get; set; }

        public ValidationResult Validate()
        {
            var validator = new CustomerValidator();
            return validator.Validate(this);
        }

        public class CustomerValidator : AbstractValidator<Customer>
        {
            public CustomerValidator()
            {

                RuleFor(x => x.Name).Must(x => !string.IsNullOrEmpty(x))
                    .WithMessage("Deve ser informado o nome do cliente.");

                RuleFor(x => x.Cpf).Must(x => !string.IsNullOrEmpty(x))
                    .WithMessage("Deve ser informado o CPF do cliente.");

                RuleFor(x => x.Phone).Must(x => !string.IsNullOrEmpty(x))
                    .WithMessage("Deve ser informado o telefone do cliente.");

                RuleFor(x => x.Address).NotNull().WithMessage("Deve ser informado o endereço de entrega.");

                When(x => x.Address != null , () =>
                {
                    RuleFor(x => x.Address).NotNull().SetValidator(new AddressValidator());
                });

            }
        }
    }
}
