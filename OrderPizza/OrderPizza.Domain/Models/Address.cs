using System.Linq;
using FluentValidation;

namespace OrderPizza.Domain.Models
{
    public class Address
    {

        public int Id { get; set; }

        public string Street { get; set; }

        public int Number { get; set; }

        public string Complement { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }  
    }

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.City).Must(x => !string.IsNullOrEmpty(x)).WithMessage("Deve ser informado a cidade de entrega.");
            RuleFor(x => x.Number).NotNull().WithMessage("Deve ser informado o número de entrega.");
            RuleFor(x => x.Number).NotEmpty().WithMessage("Deve ser informado o número de entrega.");
            RuleFor(x => x.Street).Must(x => !string.IsNullOrEmpty(x)).WithMessage("Deve ser informado a rua de entrega.");
            RuleFor(x => x.Neighborhood).Must(x => !string.IsNullOrEmpty(x)).WithMessage("Deve ser informado o bairro de entrega.");
        }
    }
}
